CREATE OR REPLACE FUNCTION payroll.GetIncomeTaxInvestmentByCompany(companyId uuid)
RETURNS TABLE (
		"Id" uuid ,
		"InvestmentPercentage" numeric ,
		"WaiverPercentage" numeric ,
		"StartDate" timestamp ,
		"EndDate" timestamp ,
		"Remarks" character varying(500) ,
		"CompanyId" uuid
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			IncomeTaxInvestment."Id" , 
			IncomeTaxInvestment."InvestmentPercentage" , 
			IncomeTaxInvestment."WaiverPercentage" , 
			IncomeTaxInvestment."StartDate" , 
			IncomeTaxInvestment."EndDate" , 
			IncomeTaxInvestment."Remarks" , 
			IncomeTaxInvestment."CompanyId" 
		 FROM payroll."IncomeTaxInvestments" AS IncomeTaxInvestment 
		 WHERE IncomeTaxInvestment."IsDeleted" = False and IncomeTaxInvestment."CompanyId" = companyId;
	END;
$BODY$
LANGUAGE plpgsql;

