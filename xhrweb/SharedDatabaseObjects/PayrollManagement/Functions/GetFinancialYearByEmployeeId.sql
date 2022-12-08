CREATE OR REPLACE FUNCTION payroll.GetFinancialYearByEmployeeId(employeeId uuid)
 RETURNS TABLE("Id" uuid, "CompanyId" uuid, "CompanyName" character varying, "FinancialYearName" character varying, "FinancialYearLocalizedName" character varying, "FinancialYearStartDate" timestamp without time zone, "FinancialYearEndDate" timestamp without time zone, "IsRunningYear" boolean, "SortOrder" smallint)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
		SELECT fy."Id"
			, c."Id" as "CompanyId"
			, c."CompanyName"
			, fy."FinancialYearName"
			, fy."FinancialYearLocalizedName"
			, fy."FinancialYearStartDate"
			, fy."FinancialYearEndDate"
			, fy."IsRunningYear"
			, fy."SortOrder"
		FROM main."FinancialYears" as fy
			INNER JOIN main."Company" as c on fy."CompanyId" = c."Id"
		WHERE fy."IsDeleted" = False and fy."Id" in (select espd."FinancialYearId"  
	from payroll."EmployeeSalaryProcessedDatas" espd where espd."EmployeeId"=employeeId);
	
END;
$function$
;


--select * from payroll.GetFinancialYearByEmployeeId('e6a94443-8f81-4e15-be9d-3fd178ed4212');