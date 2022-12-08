CREATE OR REPLACE VIEW attendance."EmployeeInfo"
AS SELECT e."Id",
    e."FullName",
    ee."JoiningDate",
    e."CompanyId",
    ee."StatusId",
    ee."QuitDate" 
   FROM employee."Employees" e
     JOIN employee."EmployeeEnrollments" ee ON e."Id" = ee."EmployeeId" inner join main."Company" c on e."CompanyId" = c."Id" 
    where c."IsDeleted" = false and c."ApprovalStatus" = '3';