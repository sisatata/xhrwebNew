CREATE OR REPLACE FUNCTION attendance.getattendancemisspunchdatalistbycompanyanddate(companyid uuid, punchdate timestamp without time zone)
 RETURNS TABLE("EmployeeId" character varying, "FullName" character varying, "Date" text, "PunchYear" character varying, "PunchMonth" integer, "InTime" text, "OutTime" text, "ShiftIn" timestamp without time zone, "ShiftOut" timestamp without time zone, "Late" timestamp without time zone, "ShiftCode" character varying, "RegularHour" timestamp without time zone, "Status" character varying, "Remarks" character varying, "EmployeeEmail" character varying, "Department" character varying, "Designation" character varying, "ManagerEmail" character varying)
 LANGUAGE plpgsql
AS $function$ begin return QUERY
select
	e."EmployeeId" as "EmployeeId",
	e."FullName",
	to_char( AttendanceProcessedData."PunchDate",'DD Mon YYYY')  as "Date",
	AttendanceProcessedData."PunchYear" ,
	AttendanceProcessedData."PunchMonth" ,
	case when AttendanceProcessedData."TimeIn" is null then 'Missed' else to_char( AttendanceProcessedData."TimeIn", 'HH24:MI:SS') end as "InTime",
	case when AttendanceProcessedData."TimeOut" is null then 'Missed' else to_char( AttendanceProcessedData."TimeOut", 'HH24:MI:SS') end as "OutTime",
	AttendanceProcessedData."ShiftIn" ,
	AttendanceProcessedData."ShiftOut" ,
	AttendanceProcessedData."Late" ,
	AttendanceProcessedData."ShiftCode" ,
	AttendanceProcessedData."RegularHour" ,
	AttendanceProcessedData."Status" ,
	AttendanceProcessedData."Remarks",
	anu."Email" as "EmployeeEmail",
	d."DepartmentName" as "Department",
	d2."DesignationName" as "Designation",
	case when manem ."Email" = 'mamun@gmail.com' then 'mamun@aplectrum.com' else manem ."Email" end  as "ManagerEmail"
from
	attendance."AttendanceProcessedData" as AttendanceProcessedData
inner join employee."Employees" e on
	AttendanceProcessedData."EmployeeId" = e."Id"
	inner join main."Departments" d  on e."DepartmentId" = d."Id" 
	inner join main."Designations" d2 on e."PositionId" = d2."Id" 
	inner join "access"."AspNetUsers" anu on e."LoginId"::text = anu."Id" 
	inner join employee."EmployeeManagers" em  on e."Id"  = em."EmployeeId" 
	inner join employee."Employees" mana on em."ManagerId" = mana."Id" 
	inner join "access"."AspNetUsers" manem on mana."LoginId"::text = manem."Id"
	inner join employee."EmployeeEnrollments" ee on e."Id" =ee."EmployeeId" 
	where e."CompanyId" = companyid and (AttendanceProcessedData."TimeIn" is null or AttendanceProcessedData."TimeOut" is null)
	and AttendanceProcessedData."PunchDate" >=ee."JoiningDate" and (ee."QuitDate"  is null or AttendanceProcessedData."PunchDate" < ee."QuitDate" )
and  AttendanceProcessedData."PunchDate" = punchDate and AttendanceProcessedData."Status" in('P','L','A') 
and e."IsDeleted" = false and e."Id" not in ('1b7b9d6d-853d-4ac3-acf6-542c721af70f') order by e."FullName" 
;
	
end;

$function$
;

--select * from attendance.getattendancemisspunchdatalistbycompanyanddate('ab5aeca2-7a4a-4a20-bb96-383e72e839dc', '2020-10-22');
