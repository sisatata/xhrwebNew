CREATE OR REPLACE FUNCTION payroll.GetBankByCompany(companyId uuid)
RETURNS TABLE (
		"Id" uuid ,
		"BankName" character varying(50) ,
		"BankNameLC" character varying(50) ,
		"SortOrder" Int4 ,
		"CompanyId" uuid ,
		"IsDeleted" boolean ,
		"PaymentOptionId" int2 
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			Bank."Id" , 
			Bank."BankName" , 
			Bank."BankNameLC" , 
			Bank."SortOrder" , 
			Bank."CompanyId" , 
			Bank."IsDeleted" , 
			Bank."PaymentOptionId"  
		 FROM payroll."Banks" AS Bank 
		 WHERE Bank."IsDeleted" = False and Bank."CompanyId" = companyId ;
	END;
$BODY$
LANGUAGE plpgsql;


DROP FUNCTION payroll.getbankbycompany(uuid);