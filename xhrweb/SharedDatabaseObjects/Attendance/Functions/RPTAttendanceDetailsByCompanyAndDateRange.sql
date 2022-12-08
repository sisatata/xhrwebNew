CREATE OR REPLACE FUNCTION attendance.rptattendancedetailsbycompanyanddaterange(companyid uuid, startdate timestamp without time zone, enddate timestamp without time zone, status integer)
 RETURNS TABLE("Id" uuid, "EmployeeId" character varying, "BranchId" uuid, "DepartmentId" uuid, "DesignationId" uuid, "FullName" character varying, "Date" text, "InTime" text, "OutTime" text, "OfficeInTime" text, "OfficeOutTime" text, "Late" text, "ShiftCode" character varying, "Status" character varying, "Remarks" character varying, "Branch" character varying, "Department" character varying, "Designation" character varying, "JoiningDate" timestamp without time zone, "CompanyName" character varying, "ReportTitle" text)
 LANGUAGE plpgsql
AS $function$ begin 
	if status = 1 then
	return QUERY
select
    e."Id" ,
	e."EmployeeId" as "EmployeeId",
	b."Id" as "BranchId",
	d."Id" as "DepartmentId",
	des."Id" as "DesignationId",
	e."FullName",
	to_char(AttendanceProcessedData."PunchDate" ,'DD Mon YYYY') "Date",
	to_char(AttendanceProcessedData."TimeIn" , 'HH24:MI:SS') as "InTime",
	to_char(AttendanceProcessedData."TimeOut" , 'HH24:MI:SS') as "OutTime",
	to_char(AttendanceProcessedData."ShiftIn" , 'HH24:MI:SS') as "OfficeInTime",
	to_char(AttendanceProcessedData."ShiftOut" , 'HH24:MI:SS') as "OfficeOutTime",
	to_char(AttendanceProcessedData."Late" , 'HH24:MI:SS') as "Late",
	AttendanceProcessedData."ShiftCode" ,
	AttendanceProcessedData."Status" ,
	AttendanceProcessedData."Remarks"
	,b."BranchName" as "Branch",
	d."DepartmentName" as "Department", 
	des."DesignationName" as "Designation",
	ee."JoiningDate" ,
	c."CompanyName" ,
--	case when ca."AddressLine1" is null then 'No Address' else ca."AddressLine1" end as "CompanyAddress",
	concat ('Job Card Report, From ',to_char(startdate,'dd/MM/yyyy'),  ' To ',to_char(enddate,'dd/MM/yyyy')) as "ReportTitle"
from
	attendance."AttendanceProcessedData" as AttendanceProcessedData
inner join employee."Employees" e on
	AttendanceProcessedData."EmployeeId" = e."Id"
	inner join employee."EmployeeEnrollments" ee on e."Id" = ee."EmployeeId" 
	inner join main."Company" as c on e."CompanyId" = c."Id"
	inner join main."Branch" as b on e."BranchId" = b."Id"
	inner join main."Departments" as d on e."DepartmentId" = d."Id"
	inner join main."Designations" as des on e."PositionId" = des."Id"
	--left join main."CompanyAddresses" ca on c."Id" = ca."CompanyId" 
where
	e."CompanyId" = companyid
	and (startdate is null
	or AttendanceProcessedData."PunchDate" :: date >= startdate)
	and (enddate is null
	or AttendanceProcessedData."PunchDate" :: date <= enddate)
	
	 and
	d."IsDeleted" = false and 
	e."IsDeleted" = false and 
	des."IsDeleted" = false and 
	c."IsDeleted" = false and
	b."IsDeleted" = false and
	ee."IsDeleted" = false 
order by
	e."FullName",AttendanceProcessedData."PunchDate" ;
elsif status = 2 then 

return QUERY
select
     e."Id" ,
	e."EmployeeId" as "EmployeeId",
	b."Id" as "BranchId",
	d."Id" as "DepartmentId",
	des."Id" as "DesignationId",
	e."FullName",
	to_char(AttendanceProcessedData."PunchDate" ,'DD Mon YYYY') "Date",
	
	to_char(AttendanceProcessedData."TimeIn" , 'HH24:MI:SS') as "InTime",
	to_char(AttendanceProcessedData."TimeOut" , 'HH24:MI:SS') as "OutTime",
	to_char(AttendanceProcessedData."ShiftIn" , 'HH24:MI:SS') as "OfficeInTime",
	to_char(AttendanceProcessedData."ShiftOut" , 'HH24:MI:SS') as "OfficeOutTime",
	to_char(AttendanceProcessedData."Late" , 'HH24:MI:SS') as "Late",
	AttendanceProcessedData."ShiftCode" ,
	
	AttendanceProcessedData."Status" ,
	AttendanceProcessedData."Remarks"
	,b."BranchName" as "Branch",
	d."DepartmentName" as "Department", 
	des."DesignationName" as "Designation",
	ee."JoiningDate" ,
	c."CompanyName" ,
	--case when ca."AddressLine1" is null then 'No Address' else ca."AddressLine1" end as "CompanyAddress",
	concat ('Job Card Report, From ',to_char(startdate,'dd/MM/yyyy'),  ' To ',to_char(enddate,'dd/MM/yyyy')) as "ReportTitle"
from
	attendance."AttendanceProcessedData" as AttendanceProcessedData
inner join employee."Employees" e on
	AttendanceProcessedData."EmployeeId" = e."Id"
	inner join employee."EmployeeEnrollments" ee on e."Id" = ee."EmployeeId" 
	inner join main."Company" as c on e."CompanyId" = c."Id"
	inner join main."Branch" as b on e."BranchId" = b."Id"
	inner join main."Departments" as d on e."DepartmentId" = d."Id"
	inner join main."Designations" as des on e."PositionId" = des."Id"
	--left join main."CompanyAddresses" ca on c."Id" = ca."CompanyId" 
where
	e."CompanyId" = companyid
	and AttendanceProcessedData."Status" ='L'
	and (startdate is null
	or AttendanceProcessedData."PunchDate" :: date >= startdate)
	and (enddate is null
	or AttendanceProcessedData."PunchDate" :: date <= enddate)

	 and
	d."IsDeleted" = false and 
	e."IsDeleted" = false and 
	des."IsDeleted" = false and 
	c."IsDeleted" = false and
	b."IsDeleted" = false and
	ee."IsDeleted" = false 
order by
	e."FullName",AttendanceProcessedData."PunchDate" ;

elsif status = 3 then 

return QUERY
select
     e."Id" ,
	e."EmployeeId" as "EmployeeId",
	b."Id" as "BranchId",
	d."Id" as "DepartmentId",
	des."Id" as "DesignationId",
	e."FullName",
	to_char(AttendanceProcessedData."PunchDate" ,'DD Mon YYYY') "Date",
	
	to_char(AttendanceProcessedData."TimeIn" , 'HH24:MI:SS') as "InTime",
	to_char(AttendanceProcessedData."TimeOut" , 'HH24:MI:SS') as "OutTime",
	to_char(AttendanceProcessedData."ShiftIn" , 'HH24:MI:SS') as "OfficeInTime",
	to_char(AttendanceProcessedData."ShiftOut" , 'HH24:MI:SS') as "OfficeOutTime",
	to_char(AttendanceProcessedData."Late" , 'HH24:MI:SS') as "Late",
	AttendanceProcessedData."ShiftCode" ,
	
	AttendanceProcessedData."Status" ,
	AttendanceProcessedData."Remarks"
	,b."BranchName" as "Branch",
	d."DepartmentName" as "Department", 
	des."DesignationName" as "Designation",
	ee."JoiningDate" ,
	c."CompanyName" ,
	--case when ca."AddressLine1" is null then 'No Address' else ca."AddressLine1" end as "CompanyAddress",
	concat ('Job Card Report, From ',to_char(startdate,'dd/MM/yyyy'),  ' To ',to_char(enddate,'dd/MM/yyyy')) as "ReportTitle"
from
	attendance."AttendanceProcessedData" as AttendanceProcessedData
inner join employee."Employees" e on
	AttendanceProcessedData."EmployeeId" = e."Id"
	inner join employee."EmployeeEnrollments" ee on e."Id" = ee."EmployeeId" 
	inner join main."Company" as c on e."CompanyId" = c."Id"
	inner join main."Branch" as b on e."BranchId" = b."Id"
	inner join main."Departments" as d on e."DepartmentId" = d."Id"
	inner join main."Designations" as des on e."PositionId" = des."Id"
	--left join main."CompanyAddresses" ca on c."Id" = ca."CompanyId" 
where
	e."CompanyId" = companyid
	and AttendanceProcessedData."Status" ='A'
	and (startdate is null
	or AttendanceProcessedData."PunchDate" :: date >= startdate)
	and (enddate is null
	or AttendanceProcessedData."PunchDate" :: date <= enddate)

	 and 
	d."IsDeleted" = false and 
	e."IsDeleted" = false and 
	des."IsDeleted" = false and 
	c."IsDeleted" = false and
	b."IsDeleted" = false and
	ee."IsDeleted" = false 
order by
	e."FullName",AttendanceProcessedData."PunchDate" ;

elsif status = 4 then 

return QUERY
select
     e."Id" ,
	e."EmployeeId" as "EmployeeId",
	b."Id" as "BranchId",
	d."Id" as "DepartmentId",
	des."Id" as "DesignationId",
	e."FullName",
	to_char(AttendanceProcessedData."PunchDate" ,'DD Mon YYYY') "Date",
	
	to_char(AttendanceProcessedData."TimeIn" , 'HH24:MI:SS') as "InTime",
	to_char(AttendanceProcessedData."TimeOut" , 'HH24:MI:SS') as "OutTime",
	to_char(AttendanceProcessedData."ShiftIn" , 'HH24:MI:SS') as "OfficeInTime",
	to_char(AttendanceProcessedData."ShiftOut" , 'HH24:MI:SS') as "OfficeOutTime",
	to_char(AttendanceProcessedData."Late" , 'HH24:MI:SS') as "Late",
	AttendanceProcessedData."ShiftCode" ,
	
	AttendanceProcessedData."Status" ,
	AttendanceProcessedData."Remarks"
	,b."BranchName" as "Branch",
	d."DepartmentName" as "Department", 
	des."DesignationName" as "Designation",
	ee."JoiningDate" ,
	c."CompanyName" ,
	--case when ca."AddressLine1" is null then 'No Address' else ca."AddressLine1" end as "CompanyAddress",
	concat ('Job Card Report, From ',to_char(startdate,'dd/MM/yyyy'),  ' To ',to_char(enddate,'dd/MM/yyyy')) as "ReportTitle"
from
	attendance."AttendanceProcessedData" as AttendanceProcessedData
inner join employee."Employees" e on
	AttendanceProcessedData."EmployeeId" = e."Id"
	inner join employee."EmployeeEnrollments" ee on e."Id" = ee."EmployeeId" 
	inner join main."Company" as c on e."CompanyId" = c."Id"
	inner join main."Branch" as b on e."BranchId" = b."Id"
	inner join main."Departments" as d on e."DepartmentId" = d."Id"
	inner join main."Designations" as des on e."PositionId" = des."Id"
	left join main."CompanyAddresses" ca on c."Id" = ca."CompanyId" 
where
	e."CompanyId" = companyid
	and AttendanceProcessedData."TimeOut" < AttendanceProcessedData."ShiftOut" 
	and (startdate is null
	or AttendanceProcessedData."PunchDate"  :: date >= startdate)
	and (enddate is null
	or AttendanceProcessedData."PunchDate" :: date  <= enddate)

	 and 
	d."IsDeleted" = false and 
	e."IsDeleted" = false and 
	des."IsDeleted" = false and 
	c."IsDeleted" = false and
	b."IsDeleted" = false and
	ee."IsDeleted" = false 
order by
	e."FullName",AttendanceProcessedData."PunchDate" ;

elsif status = 5 then 

return QUERY
select
     e."Id" ,
	e."EmployeeId" as "EmployeeId",
	b."Id" as "BranchId",
	d."Id" as "DepartmentId",
	des."Id" as "DesignationId",
	e."FullName",
	to_char(AttendanceProcessedData."PunchDate" ,'DD Mon YYYY') "Date",
	
	to_char(AttendanceProcessedData."TimeIn" , 'HH24:MI:SS') as "InTime",
	to_char(AttendanceProcessedData."TimeOut" , 'HH24:MI:SS') as "OutTime",
	to_char(AttendanceProcessedData."ShiftIn" , 'HH24:MI:SS') as "OfficeInTime",
	to_char(AttendanceProcessedData."ShiftOut" , 'HH24:MI:SS') as "OfficeOutTime",
	to_char(AttendanceProcessedData."Late" , 'HH24:MI:SS') as "Late",
	AttendanceProcessedData."ShiftCode" ,
	
	AttendanceProcessedData."Status" ,
	AttendanceProcessedData."Remarks"
	,b."BranchName" as "Branch",
	d."DepartmentName" as "Department", 
	des."DesignationName" as "Designation",
	ee."JoiningDate" ,
	c."CompanyName" ,
	--case when ca."AddressLine1" is null then 'No Address' else ca."AddressLine1" end as "CompanyAddress",
	concat ('Job Card Report, From ',to_char(startdate,'dd/MM/yyyy'),  ' To ',to_char(enddate,'dd/MM/yyyy')) as "ReportTitle"
from
	attendance."AttendanceProcessedData" as AttendanceProcessedData
inner join employee."Employees" e on
	AttendanceProcessedData."EmployeeId" = e."Id"
	inner join employee."EmployeeEnrollments" ee on e."Id" = ee."EmployeeId" 
	inner join main."Company" as c on e."CompanyId" = c."Id"
	inner join main."Branch" as b on e."BranchId" = b."Id"
	inner join main."Departments" as d on e."DepartmentId" = d."Id"
	inner join main."Designations" as des on e."PositionId" = des."Id"
	--left join main."CompanyAddresses" ca on c."Id" = ca."CompanyId" 
where
	e."CompanyId" = companyid
	and AttendanceProcessedData."TimeIn" is null
	and (startdate is null
	or AttendanceProcessedData."PunchDate"  :: date >= startdate)
	and (enddate is null
	or AttendanceProcessedData."PunchDate" :: date  <= enddate)

	 and 
	d."IsDeleted" = false and 
	e."IsDeleted" = false and 
	des."IsDeleted" = false and 
	c."IsDeleted" = false and
	b."IsDeleted" = false and
	ee."IsDeleted" = false 
order by
	e."FullName",AttendanceProcessedData."PunchDate" ;
elsif status = 6 then 

return QUERY
select
     e."Id" ,
	e."EmployeeId" as "EmployeeId",
	b."Id" as "BranchId",
	d."Id" as "DepartmentId",
	des."Id" as "DesignationId",
	e."FullName",
	to_char(AttendanceProcessedData."PunchDate" ,'DD Mon YYYY') "Date",

	to_char(AttendanceProcessedData."TimeIn" , 'HH24:MI:SS') as "InTime",
	to_char(AttendanceProcessedData."TimeOut" , 'HH24:MI:SS') as "OutTime",
	to_char(AttendanceProcessedData."ShiftIn" , 'HH24:MI:SS') as "OfficeInTime",
	to_char(AttendanceProcessedData."ShiftOut" , 'HH24:MI:SS') as "OfficeOutTime",
	to_char(AttendanceProcessedData."Late" , 'HH24:MI:SS') as "Late",
	AttendanceProcessedData."ShiftCode" ,
	
	AttendanceProcessedData."Status" ,
	AttendanceProcessedData."Remarks"
	,b."BranchName" as "Branch",
	d."DepartmentName" as "Department", 
	des."DesignationName" as "Designation",
	ee."JoiningDate" ,
	c."CompanyName" ,
	--case when ca."AddressLine1" is null then 'No Address' else ca."AddressLine1" end as "CompanyAddress",
	concat ('Job Card Report, From ',to_char(startdate,'dd/MM/yyyy'),  ' To ',to_char(enddate,'dd/MM/yyyy')) as "ReportTitle"
from
	attendance."AttendanceProcessedData" as AttendanceProcessedData
inner join employee."Employees" e on
	AttendanceProcessedData."EmployeeId" = e."Id"
	inner join employee."EmployeeEnrollments" ee on e."Id" = ee."EmployeeId" 
	inner join main."Company" as c on e."CompanyId" = c."Id"
	inner join main."Branch" as b on e."BranchId" = b."Id"
	inner join main."Departments" as d on e."DepartmentId" = d."Id"
	inner join main."Designations" as des on e."PositionId" = des."Id"
	--left join main."CompanyAddresses" ca on c."Id" = ca."CompanyId" 
where
	e."CompanyId" = companyid
	and AttendanceProcessedData."TimeOut" is null
	and (startdate is null
	or AttendanceProcessedData."PunchDate"  :: date >= startdate)
	and (enddate is null
	or AttendanceProcessedData."PunchDate" :: date  <= enddate)

	 and 
	d."IsDeleted" = false and 
	e."IsDeleted" = false and 
	des."IsDeleted" = false and 
	c."IsDeleted" = false and
	b."IsDeleted" = false and
	ee."IsDeleted" = false 
order by
	e."FullName",AttendanceProcessedData."PunchDate" ;


elsif status = 7 then 

return QUERY
select
     e."Id" ,
	e."EmployeeId" as "EmployeeId",
	b."Id" as "BranchId",
	d."Id" as "DepartmentId",
	des."Id" as "DesignationId",
	e."FullName",
	to_char(AttendanceProcessedData."PunchDate" ,'DD Mon YYYY') "Date",

	to_char(AttendanceProcessedData."TimeIn" , 'HH24:MI:SS') as "InTime",
	to_char(AttendanceProcessedData."TimeOut" , 'HH24:MI:SS') as "OutTime",
	to_char(AttendanceProcessedData."ShiftIn" , 'HH24:MI:SS') as "OfficeInTime",
	to_char(AttendanceProcessedData."ShiftOut" , 'HH24:MI:SS') as "OfficeOutTime",
	to_char(AttendanceProcessedData."Late" , 'HH24:MI:SS') as "Late",
	AttendanceProcessedData."ShiftCode" ,
	
	AttendanceProcessedData."Status" ,
	AttendanceProcessedData."Remarks"
	,b."BranchName" as "Branch",
	d."DepartmentName" as "Department", 
	des."DesignationName" as "Designation",
	ee."JoiningDate" ,
	c."CompanyName" ,
	--case when ca."AddressLine1" is null then 'No Address' else ca."AddressLine1" end as "CompanyAddress",
	concat ('Job Card Report, From ',to_char(startdate,'dd/MM/yyyy'),  ' To ',to_char(enddate,'dd/MM/yyyy')) as "ReportTitle"
from
	attendance."AttendanceProcessedData" as AttendanceProcessedData
inner join employee."Employees" e on
	AttendanceProcessedData."EmployeeId" = e."Id"
	inner join employee."EmployeeEnrollments" ee on e."Id" = ee."EmployeeId" 
	inner join main."Company" as c on e."CompanyId" = c."Id"
	inner join main."Branch" as b on e."BranchId" = b."Id"
	inner join main."Departments" as d on e."DepartmentId" = d."Id"
	inner join main."Designations" as des on e."PositionId" = des."Id"
	--left join main."CompanyAddresses" ca on c."Id" = ca."CompanyId" 
where
	e."CompanyId" = companyid
	--and AttendanceProcessedData."TimeOut" is null
	and (startdate is null
	or AttendanceProcessedData."PunchDate"  :: date >= startdate)
	and (enddate is null
	or AttendanceProcessedData."PunchDate" :: date  <= enddate)

	and 
	d."IsDeleted" = false and 
	e."IsDeleted" = false and 
	des."IsDeleted" = false and 
	c."IsDeleted" = false and
	b."IsDeleted" = false and
	ee."IsDeleted" = false 
	and (AttendanceProcessedData."Status" = 'P' or AttendanceProcessedData."Status"= 'L')
order by
	e."FullName",AttendanceProcessedData."PunchDate" ;
end if;
end;

$function$
;
