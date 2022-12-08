CREATE OR REPLACE FUNCTION leave.getpendingleavenotificationbymanager(managerid uuid, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("LeaveApplicationId" uuid, "ApplicantName" character varying, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "ApplyDate" timestamp without time zone, "NoOfDays" numeric, "LeaveTypeName" character varying, "LeaveTypeShortName" character varying, "ManagerId" uuid, "Notes" character varying, "ManagerName" character varying, "MaximumBalance" numeric, "UsedBalance" numeric, "RemainingBalance" numeric, "ApprovalStatus" text, "Reason" text, "EmergencyContact" text, "AddressOnLeave" text)
 LANGUAGE plpgsql
AS $function$
begin
return QUERY
select
	la."Id" as LeaveApplicationId ,
	e."FullName" as ApplicantName,
	la."StartDate" ,
	la."EndDate" ,
	la."ApplyDate" ,
	cast(la."NoOfDays" as numeric )NoOfDays ,
	lt."LeaveTypeName" ,
	lt."LeaveTypeShortName",
	laaq."ManagerId",
	laaq."Note" as "Notes",
	m."FullName" as ManagerName,
	cast(lb."MaximumBalance" as numeric ) MaximumBalance,
	cast(lb."UsedBalance" as numeric ) UsedBalance,
	cast(lb."RemainingBalance" as numeric ) RemainingBalance,
	la."ApprovalStatus" ,
	la."Reason" ,
	la."EmergencyContact" ,
	la."AddressOnLeave" 
from
	leave."LeaveApplicationApproveQueues" laaq
inner join leave."LeaveApplications" la on
	laaq."LeaveApplicationId" = la."Id"
inner join employee."Employees" e on
	la."EmployeeId" = e."Id"
inner join leave."LeaveTypes" lt on
	la."LeaveTypeId" = lt."Id"
inner join employee."Employees" m on
	laaq."ManagerId" = m."Id"
	inner join leave."LeaveBalance" lb on lb ."EmployeeId" =la."EmployeeId" and la."LeaveCalendar" = lb ."LeaveCalendar" 
	and lb ."LeaveTypeId" = la."LeaveTypeId" 
where
	--laaq."ApprovalStatus" = '1'	and 
laaq."ManagerId" = managerId and (((startdate is null or la."StartDate" :: date >= startdate)
	and (enddate is null or la."StartDate" :: date <= enddate) )
	or ((startdate is null or la."EndDate" :: date >= startdate)
	and (enddate is null or la."EndDate" ::date <= enddate)) or la."ApprovalStatus" = '1')  order by laaq."ApprovalStatus",la."StartDate";
end;

$function$
;
 DROP FUNCTION leave.getpendingleavenotificationbymanager(uuid,timestamp without time zone,timestamp without time zone) ;