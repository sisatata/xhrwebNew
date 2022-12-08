CREATE OR REPLACE VIEW "leave"."EmployeeInfo"
AS
SELECT e."Id"
	, e."FullName"
	, ee."JoiningDate"
FROM employee."Employees" as e
	inner join employee."EmployeeEnrollments" as ee on e."Id" = ee."EmployeeId"