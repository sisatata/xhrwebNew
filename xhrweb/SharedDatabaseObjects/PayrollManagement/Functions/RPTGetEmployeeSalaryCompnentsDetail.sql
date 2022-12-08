CREATE OR REPLACE FUNCTION payroll.rptgetemployeesalarycompnentsdetail(companyid uuid)
 RETURNS TABLE("Id" uuid, "ComponentAmount" numeric, "EmployeeSalaryId" uuid, "SalaryComponentName" character varying)
 LANGUAGE plpgsql
AS $function$ begin return QUERY

select ssc."Id" ,esc."ComponentAmount"  ,esc."EmployeeSalaryId" , ssc."SalaryComponentName" from payroll."SalaryStructureComponents" ssc 
inner join payroll."EmployeeSalaryComponents" esc on esc."SalaryStructureComponentId" = ssc."Id" 
where 
ssc."CompanyId" = companyid and 
esc."IsDeleted" = false and( ssc."SalaryComponentName" Ilike 'Basic' or 

ssc."SalaryComponentName" Ilike 'Medical' or ssc."SalaryComponentName" Ilike 'HouseRent' or  ssc."SalaryComponentName" Ilike 'House Rent' or ssc."SalaryComponentName" Ilike 'Conveyance'
or ssc."SalaryComponentName" Ilike 'Food'
);

end;	

$function$
;

