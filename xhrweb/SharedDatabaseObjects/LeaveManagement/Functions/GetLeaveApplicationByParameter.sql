CREATE OR REPLACE FUNCTION leave.getleaveapplicationbyparameter(companyid uuid, startdate timestamp without time zone, enddate timestamp without time zone, employeename character varying, leavetypename character varying, approvalstatustext character varying)
 RETURNS TABLE("Id" uuid, "CompanyId" uuid, "CompanyName" character varying, "LeaveTypeName" character varying, "Balance" integer, "EmployeeId" uuid, "EmployeeName" character varying, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "LeaveDays" double precision, "Reason" text, "ApplyDate" timestamp without time zone, "ApprovalStatus" text, "Notes" text, "EmployeeCode" character varying, "AddressOnLeave" text, "EmergencyContact" text, "IsHalfDayLeave" boolean, "HalfDayLeaveTypeId" integer)
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
	la."AddressOnLeave" , la."EmergencyContact" , la."IsHalfDayLeave" , la."HalfDayLeaveTypeId" 
from
	leave."LeaveApplications" as la
inner join leave."LeaveTypes" as lt on
	lt."Id" = la."LeaveTypeId"
inner join employee."Employees" as e on
	e."Id" = la."EmployeeId"
inner join main."Company" as c on
	la."CompanyId" = c."Id"
where
	la."CompanyId" = companyid
and (employeename is null or e."FullName" = employeename)
	and (leavetypename is null or lt."LeaveTypeName"= leavetypename)
	and (approvalstatustext is null or la."ApprovalStatus" = approvalstatustext)	
and ((startdate is null or la."StartDate" :: date >= startdate)
	and (enddate is null or la."StartDate" :: date <= enddate) 
	or (startdate is null or la."EndDate" :: date >= startdate)
	and (enddate is null or la."EndDate"  :: date<= enddate)) 
  and 
   e."IsDeleted"=false and
   c."IsDeleted"=false and
   lt."IsDeleted" =false 
   order by la."StartDate"
	;
end;
$function$
;
