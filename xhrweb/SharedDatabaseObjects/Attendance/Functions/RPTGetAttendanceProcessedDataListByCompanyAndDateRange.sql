CREATE OR REPLACE FUNCTION attendance.rptgetattendanceprocesseddatalistbycompanyanddaterange(companyid uuid, employeeid uuid, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("EmployeeId" character varying, "EID" uuid, "BranchId" uuid, "DepartmentId" uuid, "PositionId" uuid, "DesignationOrder" bigint, "FullName" character varying, "Date" text, "InTime" text, "OutTime" text, "OfficeInTime" text, "OfficeOutTime" text, "Late" text, "ShiftCode" character varying, "Status" character varying, "OTHour" timestamp without time zone, "Remarks" character varying, "Branch" character varying, "Department" character varying, "Designation" character varying, "JoiningDate" timestamp without time zone, "CompanyName" character varying, "ReportTitle" text)
 LANGUAGE plpgsql
AS $function$ begin return QUERY
select
	e."EmployeeId" as "EmployeeId",
	e."Id" as "EID", 
	b."Id"  as "BranchId",
	d."Id" as "DepartmentId",
	des."Id"  as "PositionId",
	des."SortOrder" as "DesignationOrder",
	e."FullName",
	to_char(AttendanceProcessedData."PunchDate" ,'DD Mon YYYY') "Date",
	to_char(AttendanceProcessedData."TimeIn" , 'HH24:MI:SS') as "InTime",
	to_char(AttendanceProcessedData."TimeOut" , 'HH24:MI:SS') as "OutTime",
	to_char(AttendanceProcessedData."ShiftIn" , 'HH24:MI:SS') as "OfficeInTime",
	to_char(AttendanceProcessedData."ShiftOut" , 'HH24:MI:SS') as "OfficeOutTime",
	to_char(AttendanceProcessedData."Late" , 'HH24:MI:SS') as "Late",
	AttendanceProcessedData."ShiftCode" ,
	AttendanceProcessedData."Status" ,
	AttendanceProcessedData."OTHour" ,
	AttendanceProcessedData."Remarks"
	,b."BranchName" as "Branch",
	d."DepartmentName" as "Department", 
	des."DesignationName" as "Designation",
	ee."JoiningDate" ,
	c."CompanyName" ,
	concat ('Job Card Report From ',to_char(startdate,'dd/MM/yyyy'),  ' To ',to_char(enddate,'dd/MM/yyyy')) as "ReportTitle"
from
	attendance."AttendanceProcessedData" as AttendanceProcessedData
inner join employee."Employees" e on
	AttendanceProcessedData."EmployeeId" = e."Id"
	inner join employee."EmployeeEnrollments" ee on e."Id" = ee."EmployeeId" 
	inner join main."Company" as c on e."CompanyId" = c."Id"
	inner join main."Branch" as b on e."BranchId" = b."Id"
	inner join main."Departments" as d on e."DepartmentId" = d."Id"
	inner join main."Designations" as des on e."PositionId" = des."Id"
where
	e."CompanyId" = companyId
	and (startdate is null
	or AttendanceProcessedData."PunchDate" >= startdate)
	and (enddate is null
	or AttendanceProcessedData."PunchDate" <= enddate)
	and (employeeid is null or AttendanceProcessedData."EmployeeId" = employeeid)
	and e."IsDeleted"  = false 
	
order by
	e."FullName",AttendanceProcessedData."PunchDate" ;

end;

$function$
;

