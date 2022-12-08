CREATE OR REPLACE FUNCTION leave.getleaveapplication(companyid uuid, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("Id" uuid, "CompanyId" uuid, "CompanyName" character varying, "LeaveTypeName" character varying, "Balance" integer, "EmployeeId" uuid, "EmployeeName" character varying, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "LeaveDays" double precision, "Reason" text, "ApplyDate" timestamp without time zone, "ApprovalStatus" text, "Notes" text, "EmployeeCode" character varying, "AddressOnLeave" text, "EmergencyContact" text, "IsHalfDayLeave" boolean, "HalfDayLeaveTypeId" integer, "BranchId" uuid, "DepartmentId" uuid, "PositionId" uuid, "BranchName" character varying, "DepartmentName" character varying, "DesignationName" character varying, "LoginId" character varying, "CompanyEmployeeId" character varying)
 LANGUAGE plpgsql
AS $function$ begin return QUERY
select
	la."Id",
	c."Id" as "CompanyId",
	c."CompanyName",
	lt."LeaveTypeName",
	lt."Balance",
	e."Id" EmployeeId,
	e."FullName" as EmployeeName,
	la."StartDate",
	la."EndDate",
	la."NoOfDays" as LeaveDays,
	la."Reason",
	la."ApplyDate" ,
	la."ApprovalStatus" ,
	la."Notes" ,
	e."EmployeeId" EmployeeCode,
	la."AddressOnLeave" , la."EmergencyContact" , la."IsHalfDayLeave" , la."HalfDayLeaveTypeId" ,
	e."BranchId" ,
    e."DepartmentId" ,
    e."PositionId" ,
    b."BranchName" ,
    d."DepartmentName" ,
    d2."DesignationName" ,
    anu."Email" as "LoginId",
     e."EmployeeId" as "CompanyEmployeeId"
from
	leave."LeaveApplications" as la
inner join leave."LeaveTypes" as lt on
	lt."Id" = la."LeaveTypeId"
inner join employee."Employees" as e on
	e."Id" = la."EmployeeId"
inner join main."Company" as c on
	la."CompanyId" = c."Id"
inner join main."Branch" b on
    e."BranchId"  = b."Id" 
inner join main."Departments" d  on 
    e."DepartmentId"  = d."Id" 
inner join main."Designations" d2 on
    e."PositionId"  = d2."Id" 
inner join "access"."AspNetUsers" anu  on 
    e."LoginId" :: text = anu."Id" 
where
	la."CompanyId" = companyid
and ((startdate is null or la."StartDate" :: date >= startdate)
	and (enddate is null or la."StartDate" :: date <= enddate) 
	or (startdate is null or la."EndDate" :: date >= startdate)
	and (enddate is null or la."EndDate"  :: date<= enddate)) 
  and 
   e."IsDeleted"=false and
   c."IsDeleted"=false and
   lt."IsDeleted" =false and 
   b."IsDeleted" = false and 
   d."IsDeleted" = false and
   d2."IsDeleted"  = false  
   order by la."StartDate"
	;
end;
$function$
;
