CREATE OR REPLACE FUNCTION payroll.GetPaymentOptionByCompany(companyId uuid)
RETURNS TABLE (
		"Id" uuid ,
		"PaymentOptionId" int2 ,
		"OptionName" character varying(50) ,
		"OptionCode" character varying(10) ,
		"SortOrder" int2 
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			PaymentOption."Id" , 
			PaymentOption."PaymentOptionId" , 
			PaymentOption."OptionName" , 
			PaymentOption."OptionCode" , 
			PaymentOption."SortOrder" 
		 FROM payroll."PaymentOptions" AS PaymentOption 
		 WHERE PaymentOption."IsDeleted" = False order by "SortOrder" ;
	END;
$BODY$
LANGUAGE plpgsql;


--select * from payroll.GetPaymentOptionByCompany('22ec0727-7888-409b-bc9f-4bd25248caa2');

--INSERT INTO payroll."PaymentOptions"
--("Id", "PaymentOptionId", "OptionName", "OptionCode", "SortOrder", "IsDeleted")
--VALUES('22ec0727-7888-409b-bc9f-4bd25248caa2', 3, 'Mobile Bank', 'MobileBank', 3, false);