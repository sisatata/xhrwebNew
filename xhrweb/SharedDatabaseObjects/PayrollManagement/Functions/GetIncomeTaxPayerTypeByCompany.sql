CREATE OR REPLACE FUNCTION payroll.GetIncomeTaxPayerTypeByCompany(companyId uuid)
RETURNS TABLE (
		"Id" uuid ,
		"PayerTypeCode" character varying(20) ,
		"PayerTypeName" character varying(50) ,
		"Remarks" character varying(250) ,
		"IsActive" boolean ,
		"IsDeleted" boolean ,
		"CompanyId" uuid
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			IncomeTaxPayerType."Id" , 
			IncomeTaxPayerType."PayerTypeCode" , 
			IncomeTaxPayerType."PayerTypeName" , 
			IncomeTaxPayerType."Remarks" , 
			IncomeTaxPayerType."IsActive" , 
			IncomeTaxPayerType."IsDeleted" , 
			IncomeTaxPayerType."CompanyId" 
		 FROM payroll."IncomeTaxPayerTypes" AS IncomeTaxPayerType 
		 WHERE IncomeTaxPayerType."IsDeleted" = False and IncomeTaxPayerType."CompanyId" = companyId;
	END;
$BODY$
LANGUAGE plpgsql;

