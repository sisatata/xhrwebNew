create or replace
function attendance.GetAllClockInOutByEmployeeAndDate(employeeId uuid, attendanceDate timestamp) 
returns table("EmployeeId" uuid, "FullName" character varying , "PositionId" uuid, "DepartmentId" uuid, "AttendanceDate" timestamp, "Punchtype" int2, "Latitude" numeric, "Longitude" numeric, "ClockTimeAddress" character varying, "ClockTimeType" int4, "Remarks" character varying, "TimeIn" timestamp, "TimeOut" timestamp, "Status" character varying, "ShiftCode" character varying, "ShiftIn" timestamp, "ShiftOut" timestamp) language plpgsql as $function$ begin return QUERY
select
	ac."EmployeeId" ,
	e."FullName" ,
	e."PositionId" ,
	e."DepartmentId" ,
	ac."AttendanceDate" ,
	ac."Punchtype" ,
	ac."Latitude" ,
	ac."Longitude" ,
	ac."ClockTimeAddress" ,
	ac."ClockTimeType" ,
	ac."Remarks" ,
	apd."TimeIn" ,
	apd."TimeOut" ,
	apd."Status" ,
	apd."ShiftCode" ,
	apd."ShiftIn" ,
	apd."ShiftOut"
from
	attendance."AttendanceCommon" ac
inner join employee."Employees" e on
	ac."EmployeeId" = e."Id"
inner join attendance."AttendanceProcessedData" apd on
	e."Id" = apd ."EmployeeId"
inner join attendance."Shifts" s on
	s."ShiftCode" = apd."ShiftCode"
	and s."CompanyId" = e."CompanyId"
	and s."IsDeleted" = false
where
	apd."PunchDate" = attendanceDate
	and apd."EmployeeId" = employeeId
	and ac."AttendanceDate" between (cast( apd."PunchDate" as date) + cast ( s."ShiftIn"- interval '119 minute' as time)) and (cast( apd."PunchDate" + interval '1D' as date) + cast ( s."ShiftIn" - interval '120m' as time)) ;
end;

$function$ ;

