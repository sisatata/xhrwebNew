CREATE OR REPLACE VIEW leave."EmployeeManagers"
AS SELECT e."Id",
    e."EmployeeId",
    e."ManagerId",
    e."IsPrimaryManager",
    e."CompanyId",
    e."ManagerTypeId",
    clut."LookUpTypeName" AS "ManagerTypeName",
    clut."ShortCode" AS "ManagerTypeCode"
   FROM employee."EmployeeManagers" e
     JOIN main."CommonLookUpTypes" clut ON e."ManagerTypeId" = clut."Id"
  WHERE e."IsDeleted" = false;