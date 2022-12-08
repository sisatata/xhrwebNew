CREATE OR REPLACE FUNCTION payroll.getemployeebankaccountbyemployee(companyid uuid,employeeid uuid)
 RETURNS TABLE("Id" uuid, "BankId" uuid, "BankBranchId" uuid, "AccountNo" character varying, "AccountTitle" character varying, "IsPrimary" boolean, "CompanyId" uuid, "EmployeeId" uuid, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "Remarks" character varying, "BankName" character varying, "BranchName" character varying, "FullName" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			EmployeeBankAccount."Id" , 
			EmployeeBankAccount."BankId" , 
			EmployeeBankAccount."BankBranchId" , 
			EmployeeBankAccount."AccountNo" , 
			EmployeeBankAccount."AccountTitle" , 
			EmployeeBankAccount."IsPrimary" , 
			EmployeeBankAccount."CompanyId" , 
			EmployeeBankAccount."EmployeeId" , 
			EmployeeBankAccount."StartDate" , 
			EmployeeBankAccount."EndDate" , 
			EmployeeBankAccount."Remarks" ,
			b."BankName" ,
			bb."BranchName" ,
			e."FullName" 
		 FROM payroll."EmployeeBankAccounts" AS EmployeeBankAccount 
		 inner join payroll."Banks" b on EmployeeBankAccount."BankId" = b."Id" 
		 inner join payroll."BankBranches" bb on EmployeeBankAccount."BankBranchId" = bb."Id" 
		 inner join employee."Employees" e on EmployeeBankAccount."EmployeeId" = e."Id" 
		 WHERE EmployeeBankAccount."IsDeleted" = false and EmployeeBankAccount."CompanyId" =  companyId
		 and EmployeeBankAccount."EmployeeId"  = employeeid
		 ;
	END;
$function$
;