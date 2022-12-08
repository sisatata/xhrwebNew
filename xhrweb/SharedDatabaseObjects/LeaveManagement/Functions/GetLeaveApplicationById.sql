create or replace
function leave.getleaveapplicationbyid(id uuid) returns table("Id" uuid,
"CompanyId" uuid, 
"CompanyName" character varying,
"LeaveTypeId" uuid,
"LeaveTypeName" character varying,
"Balance" int4,
"EmployeeId" uuid,
"EmployeeName" character varying,
"StartDate" timestamp without time zone,
"EndDate" timestamp without time zone,
"LeaveDays" double precision,
"Reason" text,
"LeaveCalendar" character varying,
"ApplyDate" timestamp without time zone,
"ApprovalStatus" text, 
"Notes" text,
"EmployeeCode" character varying,
"AddressOnLeave" text,
"EmergencyContact" text,
"IsHalfDayLeave" boolean,
"HalfDayLeaveTypeId" int4) language plpgsql as $function$ begin return QUERY
select
	la."Id",
	c."Id" as "CompanyId",
	c."CompanyName",
	la."LeaveTypeId",
	lt."LeaveTypeName",
	lt."Balance",
	e."Id" EmployeeId,
	e."FullName" as EmployeeName,
	la."StartDate",
	la."EndDate",
	la."NoOfDays" as LeaveDays,
	la."Reason",
	la."LeaveCalendar",
	la."ApplyDate" ,
	la."ApprovalStatus" ,
	la."Notes" ,
	e."EmployeeId" EmployeeCode,
	la."AddressOnLeave" ,
	la."EmergencyContact" ,
	la."IsHalfDayLeave" ,
	la."HalfDayLeaveTypeId"
from
	leave."LeaveApplications" as la
inner join leave."LeaveTypes" as lt on
	lt."Id" = la."LeaveTypeId"
inner join employee."Employees" as e on
	e."Id" = la."EmployeeId"
inner join main."Company" as c on
	la."CompanyId" = c."Id"
where
	la."Id" = id;
end;

$function$ ;


--select * from leave.getleaveapplicationbyid('1f4e1f27-a272-43a6-a6cc-2786c77bdf5d');
--DROP FUNCTION leave.getleaveapplicationbyid(uuid) 