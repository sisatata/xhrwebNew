create or replace
function leave.GetLeaveDataListByCompanyAndDate(companyid uuid, leaveDate timestamp without time zone) 
returns table("EmployeeId" character varying, "FullName" character varying, "EmployeeEmail" character varying, "Department" character varying, "Designation" character varying, "ManagerEmail" character varying, "Reason" text, "ApprovalStatus" text, "HalfDayLeaveTypeId" int4, "ApplyDate" text, "Notes" text, "StartDate" text, "TodayDate" text, "EndDate" text, "LeaveTypeShortName" character varying ) language plpgsql as $function$ begin return QUERY
select
	e."EmployeeId" as "EmployeeId",
	e."FullName",
	anu."Email" as "EmployeeEmail",
	d."DepartmentName" as "Department",
	d2."DesignationName" as "Designation",
	case
		when manem ."Email" = 'mamun@gmail.com' then 'mamun@aplectrum.com'
		else manem ."Email"
	end as "ManagerEmail",
	la."Reason",
	la."ApprovalStatus" ,
	la."HalfDayLeaveTypeId" ,
	to_char( la."ApplyDate", 'DD Mon YYYY') as "ApplyDate" ,
	la ."Notes" ,
	to_char( la."StartDate" , 'DD Mon YYYY') as "StartDate" ,
	to_char(current_date , 'DD Mon YYYY') as "TodayDate",
	to_char( la."EndDate" , 'DD Mon YYYY') as "EndDate",
	lt."LeaveTypeShortName"
from
	leave."LeaveApplications" la
inner join employee."Employees" e on
	la."EmployeeId" = e."Id"
inner join main."Departments" d on
	e."DepartmentId" = d."Id"
inner join main."Designations" d2 on
	e."PositionId" = d2."Id"
inner join "access"."AspNetUsers" anu on
	e."LoginId"::text = anu."Id"
inner join employee."EmployeeManagers" em on
	e."Id" = em."EmployeeId"
inner join employee."Employees" mana on
	em."ManagerId" = mana."Id"
inner join "access"."AspNetUsers" manem on
	mana."LoginId"::text = manem."Id"
inner join leave."LeaveTypes" lt on
	la."LeaveTypeId" = lt."Id"
where
	to_char(leaveDate, 'yyyy-MM-dd') between to_char(la."StartDate", 'yyyy-MM-dd') and to_char(la."EndDate", 'yyyy-MM-dd')
	and e."CompanyId" = companyid
	and e."IsDeleted" = false
	and e."Id" not in ('1b7b9d6d-853d-4ac3-acf6-542c721af70f')
order by
	e."FullName" ;
end;

$function$ ;
