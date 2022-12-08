CREATE OR REPLACE VIEW payroll."Employees"
AS SELECT e."Id",
    e."FullName",
    ee."JoiningDate",
    e."BranchId",
    e."DepartmentId",
    e."PositionId",
    e."DateOfBirth",
    e."GenderId",
    ee."StatusId",
    ee."QuitDate",
    ee."PermanentDate",
    e."CompanyId"
   FROM employee."Employees" e
     JOIN employee."EmployeeEnrollments" ee ON e."Id" = ee."EmployeeId"
     JOIN payroll."EmployeeSalaries" es ON e."Id" = es."EmployeeId"
  WHERE e."IsDeleted" = false and es."IsDeleted" = false ;