CREATE OR REPLACE FUNCTION payroll.getemployeesalaryprocesseddatacomponentbyparent(employeesalaryprocesseddataid uuid)
 RETURNS TABLE("Id" uuid, "EmployeeSalaryProcessedDataId" uuid, "EmployeeSalaryComponentId" uuid, "BenefitDeductionId" uuid, "ComponentValue" numeric, "Type" character varying, "CompanyId" uuid, "DisplayName" character varying,
 "Description" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			espd."Id" , 
			espd."EmployeeSalaryProcessedDataId" , 
			espd."EmployeeSalaryComponentId" , 
			espd."BenefitDeductionId" , 
			espd."ComponentValue" , 
			espd."Type" , 
			espd."CompanyId" ,
		 case when ssc."SalaryComponentName" is null then	espd."DisplayName" else ssc."SalaryComponentName" end as "DisplayName",
		 espd ."Description" 
		 FROM payroll."EmployeeSalaryProcessedDataComponents" AS espd 
		 left join payroll."EmployeeSalaryComponents" esc  on espd."EmployeeSalaryComponentId"  = esc."Id" 
		 left join payroll."SalaryStructureComponents" ssc  on esc."SalaryStructureComponentId"  = ssc."Id" 
		 WHERE espd."IsDeleted" = False and espd."EmployeeSalaryProcessedDataId" = employeeSalaryProcessedDataId order by ssc."SortOrder" ;
	END;
$function$
;

--DROP FUNCTION getemployeesalaryprocesseddatacomponentbyparent(uuid) 