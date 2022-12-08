CREATE OR REPLACE FUNCTION payroll.GetBankBranchById(id uuid)
RETURNS TABLE (
		"Id" uuid ,
		"BranchName" character varying(50) ,
		"BranchNameLC" character varying(50) ,
		"BranchAddress" character varying(250) ,
		"ContactPerson" character varying(50) ,
		"ContactNumber" character varying(20) ,
		"ContactEmailId" character varying(50) ,
		"SortOrder" Int4 ,
		"CompanyId" uuid ,
		"IsDeleted" boolean ,
		"BankId" uuid
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			BankBranch."Id" , 
			BankBranch."BranchName" , 
			BankBranch."BranchNameLC" , 
			BankBranch."BranchAddress" , 
			BankBranch."ContactPerson" , 
			BankBranch."ContactNumber" , 
			BankBranch."ContactEmailId" , 
			BankBranch."SortOrder" , 
			BankBranch."CompanyId" , 
			BankBranch."IsDeleted" ,
			BankBranch."BankId"
		 FROM payroll."BankBranches" AS BankBranch 
		 WHERE BankBranch."IsDeleted" = False and BankBranch."Id" = id;
	END;
$BODY$
LANGUAGE plpgsql;

--select * from payroll.GetBankBranchById('5ac263cb-467c-42b0-90fb-ae1ab7242f74');
--select * from payroll."Banks" b ;