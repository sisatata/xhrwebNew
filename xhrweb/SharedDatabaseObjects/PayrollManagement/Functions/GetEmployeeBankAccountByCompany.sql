CREATE OR REPLACE FUNCTION payroll.GetEmployeeBankAccountByCompany(companyId uuid)
RETURNS TABLE (
		"Id" uuid ,
		"BankId" uuid ,
		"BankBranchId" uuid ,
		"AccountNo" character varying(20) ,
		"AccountTitle" character varying(100) ,
		"IsPrimary" boolean ,
		"CompanyId" uuid ,
		"EmployeeId" uuid ,
		"StartDate" timestamp ,
		"EndDate" timestamp ,
		"Remarks" character varying(250) ,
		"BankName" character varying(50),
		"BranchName"  character varying(50) ,
		"FullName"  character varying(50)
)
AS $BODY$
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
		 WHERE EmployeeBankAccount."IsDeleted" = false and EmployeeBankAccount."CompanyId" =  companyId;
	END;
$BODY$
LANGUAGE plpgsql;

--select * from payroll.GetEmployeeBankAccountByCompany('ab5aeca2-7a4a-4a20-bb96-383e72e839dc');
--DROP FUNCTION payroll.getemployeebankaccountbycompany(uuid) 
