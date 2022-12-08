CREATE OR REPLACE FUNCTION main.chartgetcurrentyearjoinedemployees(companyid uuid, currentyear numeric)
 RETURNS TABLE("FullName" character varying, "JoiningDate" timestamp without time zone, "DesignationName" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			e."FullName" , ee."JoiningDate" , d."DesignationName"  from employee."EmployeeEnrollments" ee 
			inner join employee."Employees" e on e."Id" = ee."EmployeeId" 
			inner join main."Designations" d on d."Id" = e."PositionId" 
      where extract (year from ee."JoiningDate") =  currentyear
      and e."CompanyId"  = companyid and e."IsDeleted" = false and d."IsDeleted" = false and ee."IsDeleted" =false
      and e."LoginId" is not null;
	END;
$function$
;
