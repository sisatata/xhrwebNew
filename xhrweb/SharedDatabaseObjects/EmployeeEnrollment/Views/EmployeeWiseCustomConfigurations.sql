CREATE OR REPLACE VIEW employee."EmployeeWiseCustomConfigurations"
AS SELECT
        CASE
            WHEN ecc."Id" IS NULL THEN uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)
            ELSE ecc."Id"
        END AS "Id",
    dc."Code",
        CASE
            WHEN ecc."CustomValue" IS NULL THEN
            CASE
                WHEN cc."CustomValue" IS NULL THEN dc."DefaultValue"
                ELSE cc."CustomValue"
            END
            ELSE ecc."CustomValue"
        END AS "Value",
    e."Id" AS "EmployeeId",
    e."CompanyId"
   FROM main."DefaultConfigurations" dc
     CROSS JOIN main."Company" c
     JOIN employee."Employees" e ON c."Id" = e."CompanyId"
     LEFT JOIN main."CustomConfigurations" cc ON dc."Code"::text = cc."Code"::text AND c."Id" = cc."CompanyId" AND cc."StartDate" <= CURRENT_DATE AND cc."EndDate" >= CURRENT_DATE
     LEFT JOIN employee."EmployeeCustomConfigurations" ecc ON dc."Code"::text = ecc."Code"::text AND e."Id" = ecc."EmployeeId" AND ecc."StartDate" <= CURRENT_DATE AND ecc."EndDate" >= CURRENT_DATE
  ORDER BY e."EmployeeId";