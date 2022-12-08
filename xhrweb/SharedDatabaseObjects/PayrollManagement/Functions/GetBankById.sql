CREATE OR REPLACE FUNCTION payroll.GetBankById(id uuid)
RETURNS TABLE (
		"Id" uuid ,
		"BankName" character varying(50) ,
		"BankNameLC" character varying(50) ,
		"SortOrder" Int ,
		"CompanyId" uuid ,
		"IsDeleted" boolean ,
		"PaymentOptionId" int4 
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
		 WHERE Bank."IsDeleted" = False and Bank."Id" = id ;
	END;
$BODY$
LANGUAGE plpgsql;