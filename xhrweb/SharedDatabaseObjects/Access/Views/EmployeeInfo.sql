CREATE OR REPLACE VIEW "access"."EmployeeInfo"
AS SELECT e."Id",
    e."FullName" AS "DisplayName",
    e."CompanyId",
    c."CompanyName",
    e."LoginId",
    e."EmployeeId",
    ee."EmployeeImageUri",
    d."DesignationName"
   FROM employee."Employees" e
     JOIN main."Company" c ON e."CompanyId" = c."Id"
     JOIN main."Designations" d ON e."PositionId" = d."Id"
     LEFT JOIN employee."EmployeeEnrollments" ee ON e."Id" = ee."EmployeeId";