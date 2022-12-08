CREATE OR REPLACE FUNCTION main.getconfigurationsbycompanyandcode(companyid uuid, code character varying)
 RETURNS TABLE("Id" uuid, "EmployeeName" character varying, "Description" character varying, "Code" character varying, "Value" character varying, startdate timestamp without time zone, enddate timestamp without time zone, "EmployeeId" uuid, "CompanyId" uuid)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
	select ecc."Id" ,
        e."FullName" as "EmployeeName",
        ecc."Description" ,
    dc."Code",
        CASE
            WHEN ecc."CustomValue" IS NULL THEN
            CASE
                WHEN cc."CustomValue" IS NULL THEN dc."DefaultValue"
                ELSE cc."CustomValue"
            END
            ELSE ecc."CustomValue"
        END AS "Value",
                 CASE
            WHEN ecc."StartDate" IS NULL THEN
            CASE
                WHEN cc."StartDate" IS NULL THEN dc."CreatedDate"
                ELSE cc."StartDate"
            END
            ELSE ecc."StartDate"
        END AS "StartDate",
                 CASE
            WHEN ecc."EndDate" IS NULL THEN
            CASE
                WHEN cc."EndDate" IS NULL THEN dc."CreatedDate" + interval '1y' * 30 
                ELSE cc."EndDate"
            END
            ELSE ecc."EndDate"
        END AS "EndDate",
    e."Id" AS "EmployeeId",
        e."CompanyId" from main."DefaultConfigurations" dc 
 left join employee."EmployeeCustomConfigurations" ecc on dc."Code" = ecc."Code" and ecc."StartDate" <= CURRENT_DATE AND ecc."EndDate" >= CURRENT_DATE and ecc."IsDeleted" =false 
 left join main."CustomConfigurations" cc on dc."Code" = cc."Code" AND cc."StartDate" <= CURRENT_DATE AND cc."EndDate" >= CURRENT_DATE  and cc."CompanyId" = companyid
 left join employee."Employees" e ON ecc."EmployeeId" = e."Id" 
     left join employee."EmployeeEnrollments" ee on e."Id" = ee."EmployeeId"  and ee."StatusId" = 1
 where dc."Code"  = code and ecc."Id" is not null
  ORDER BY e."FullName" ,dc."Code";
	END;
$function$
;
