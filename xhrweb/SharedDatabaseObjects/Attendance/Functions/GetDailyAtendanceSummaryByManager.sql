CREATE OR REPLACE FUNCTION attendance.getdailyatendancesummarybymanager(companyid uuid, managerid uuid, punchdate timestamp without time zone)
 RETURNS TABLE("PunchDate" timestamp without time zone, "TotalPresent" bigint, "TotalLate" bigint, "TotalAbsent" bigint, "TotalLeave" double precision)
 LANGUAGE plpgsql
AS $function$ begin 
DROP TABLE IF EXISTS attnsum;
create temp table  attnsum(
"PunchDate" timestamp without time zone, "TotalPresent" bigint, "TotalLate" bigint, "TotalAbsent" bigint, "TotalLeave" double precision
);

insert into attnsum("PunchDate","TotalPresent" , "TotalLate", "TotalAbsent", "TotalLeave" )VALUES
(punchdate,0,0,0,0);

update attnsum set  "TotalPresent" = (select count(1) from employee."Employees" e inner join employee."EmployeeEnrollments" ee on e."Id" = ee."EmployeeId" 
left join employee."EmployeeManagers" em on e."Id" = em."EmployeeId" left join attendance."AttendanceProcessedData" apd on e."Id" = apd."EmployeeId" and apd."PunchDate" :: date = punchdate
where e."CompanyId" = companyid and em."ManagerId"  =  managerid and apd."Status" in('P','L') );

--Total Late
update attnsum set  "TotalLate" = (select count(1) from employee."Employees" e inner join employee."EmployeeEnrollments" ee on e."Id" = ee."EmployeeId" 
left join employee."EmployeeManagers" em on e."Id" = em."EmployeeId" left join attendance."AttendanceProcessedData" apd on e."Id" = apd."EmployeeId" and apd."PunchDate" :: date = punchdate
where e."CompanyId" = companyid and em."ManagerId"  =  managerid and apd."Status" ='L');


-- Total Leave
update attnsum set  "TotalLeave" = (select count(1) from employee."Employees" e inner join employee."EmployeeEnrollments" ee on e."Id" = ee."EmployeeId" 
left join employee."EmployeeManagers" em on e."Id" = em."EmployeeId" left join attendance."AttendanceProcessedData" apd on e."Id" = apd."EmployeeId" and apd."PunchDate" :: date = punchdate
where e."CompanyId" = companyid and em."ManagerId"  =  managerid and trim(apd."Status") in ('CL',
		'SL',
		'EL',
		'ML') );

	--Total Absent 
update attnsum set  "TotalAbsent" = (select count(1) from employee."Employees" e inner join employee."EmployeeEnrollments" ee on e."Id" = ee."EmployeeId" 
left join employee."EmployeeManagers" em on e."Id" = em."EmployeeId" left join attendance."AttendanceProcessedData" apd on e."Id" = apd."EmployeeId" and apd."PunchDate" :: date = punchdate
where e."CompanyId" = companyid and em."ManagerId"  =  managerid and apd."Status" ='A' );

update attnsum set "TotalAbsent" = ((select count(1) from employee."Employees" e inner join employee."EmployeeEnrollments" ee on e."Id" = ee."EmployeeId" 
left join employee."EmployeeManagers" em on e."Id" = em."EmployeeId" where e."CompanyId" = companyid and em."ManagerId"  =  managerid )
- (attnsum."TotalPresent" + attnsum."TotalLate" + attnsum."TotalLeave" )) where attnsum."TotalAbsent" = 0;

RETURN QUERY 
SELECT attnsum."PunchDate" , attnsum."TotalPresent" , attnsum."TotalLate", attnsum."TotalAbsent", attnsum."TotalLeave" 
from attnsum;
	
end;

$function$
;

--select * from attendance.getdailyatendancesummarybymanager('a2ab6a51-7a0b-42c4-a681-695a28bb6f80','7cd6fc78-ed5b-4003-a912-41fead84d132', '2021-01-28');
