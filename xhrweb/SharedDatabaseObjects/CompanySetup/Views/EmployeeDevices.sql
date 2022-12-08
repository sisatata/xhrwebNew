create or replace
view main."EmployeeDevices" as
select
	ed."Id",
	e."FullName" as "EmployeeName",
	ee."JoiningDate",
	e."CompanyId",
	ed."AccessToken" ,
	ed."UserId" ,
	e."Id" as "EmployeeId"
from
	employee."Employees" e
join employee."EmployeeEnrollments" ee on
	e."Id" = ee."EmployeeId"
inner join employee."EmployeeDevices" ed on
	e."Id" = ed."EmployeeId"
inner join main."Company" c on
	e."CompanyId" = c."Id"
where
	ee."StatusId" = 1
	and e."IsDeleted" = false
	and c."IsDeleted" = false
	and c."ApprovalStatus" = '3';
     
    
    
     