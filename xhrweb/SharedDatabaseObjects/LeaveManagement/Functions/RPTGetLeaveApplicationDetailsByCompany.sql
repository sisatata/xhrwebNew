CREATE OR REPLACE FUNCTION leave.rptgetleaveapplicationdetailsbycompany(companyid uuid, startdate timestamp without time zone, enddate timestamp without time zone, approvalstatustext character varying)
 RETURNS TABLE("EID" uuid, "BranchName" character varying, "BranchId" uuid, "LeaveCalendar" character varying, "DesignationOrder" bigint, "Id" uuid, "DepartmentId" uuid, "DesignationId" uuid, "CompanyId" uuid, "CompanyName" character varying, "LeaveTypeName" character varying, "Balance" integer, "EmployeeId" character varying, "FullName" character varying, "StartDate" text, "EndDate" text, "JoiningDate" timestamp without time zone, "DepartmentName" character varying, "DesignationName" character varying, "LeaveDays" double precision, "Reason" text, "ApplyDate" timestamp without time zone, "ApprovalStatus" text, "Notes" text, "EmployeeCode" character varying, "AddressOnLeave" text, "EmergencyContact" text, "IsHalfDayLeave" boolean, "HalfDayLeaveTypeId" integer)
 LANGUAGE plpgsql
AS $function$ begin return QUERY
select
   e."Id" as "EID",
   b."BranchName" ,
   b."Id"  as "BranchId",
   la."LeaveCalendar" ,
   d."SortOrder"  as "DesignationOrder",
	la."Id",
	d2."Id" as "DepartmentId",
	d."Id"  as "DesignationId",
	c."Id" as "CompanyId",
	c."CompanyName",
	lt."LeaveTypeName",
	lt."Balance",
	e."EmployeeId" as "EmployeeId",
	e."FullName",
	to_char(la."StartDate",'dd/mm/yyyy') as "StartDate",
	to_char(la."EndDate",'dd/mm/yyyy') as "EndDate",
	ee."JoiningDate" ,
	d2."DepartmentName" ,
	d."DesignationName" ,
	la."NoOfDays" as LeaveDays,
	la."Reason",
	la."ApplyDate" ,
	la."ApprovalStatus" ,
	la."Notes" ,
	e."EmployeeId" EmployeeCode,
	la."AddressOnLeave" , la."EmergencyContact" , la."IsHalfDayLeave" , la."HalfDayLeaveTypeId" 
from
	leave."LeaveApplications" as la
inner join leave."LeaveTypes" as lt on
	lt."Id" = la."LeaveTypeId"
inner join employee."Employees" as e on
	e."Id" = la."EmployeeId"
inner join main."Company" as c on
	la."CompanyId" = c."Id"
inner join main."Designations" d on d."Id" = e."PositionId" 
inner join main."Departments" d2 on d2."Id" = e."DepartmentId" 
inner join main."Branch" b  on e."BranchId"  = b."Id"
inner join employee."EmployeeEnrollments" ee on e."Id" = ee."EmployeeId" 
where
	la."CompanyId" = companyid
	and (approvalstatustext is null or la."ApprovalStatus" = approvalstatustext)	
and ((startdate is null or la."StartDate" :: date >= startdate)
	and (enddate is null or la."StartDate" :: date <= enddate) 
	or (startdate is null or la."EndDate" :: date >= startdate)
	and (enddate is null or la."EndDate" :: date <= enddate)) 
  and 
   e."IsDeleted"=false and
   c."IsDeleted"=false and
   lt."IsDeleted" =false 
   and b."IsDeleted"  = false 
   order by la."StartDate"
	;
end;
$function$
;
