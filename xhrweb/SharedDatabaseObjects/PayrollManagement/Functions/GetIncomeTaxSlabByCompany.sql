CREATE OR REPLACE FUNCTION payroll.getincometaxslabbycompany(companyid uuid)
 RETURNS TABLE("Id" uuid, "SlabName" character varying, "Description" character varying, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "IsRunningFlag" boolean, "IsDeleted" boolean, "CompanyId" uuid, "SlabAmount" numeric, "TaxablePercent" numeric, "TaxPayerTypeCode" text, "SlabOrder" integer, "PayerTypeName" character varying)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
		SELECT 
			IncomeTaxSlab."Id" , 
			IncomeTaxSlab."SlabName" , 
			IncomeTaxSlab."Description" , 
			IncomeTaxSlab."StartDate" , 
			IncomeTaxSlab."EndDate" , 
			IncomeTaxSlab."IsRunningFlag" , 
			IncomeTaxSlab."IsDeleted" , 
			IncomeTaxSlab."CompanyId" , 
			IncomeTaxSlab."SlabAmount" , 
			IncomeTaxSlab."TaxablePercent" , 
			IncomeTaxSlab."TaxPayerTypeCode" , 
			IncomeTaxSlab."SlabOrder" ,
			IPT."PayerTypeName"
		 FROM payroll."IncomeTaxSlabs" AS IncomeTaxSlab 
		 left join payroll."IncomeTaxPayerTypes" IPT on IPT."PayerTypeCode" = IncomeTaxSlab."TaxPayerTypeCode"
		 WHERE IncomeTaxSlab."IsDeleted" = False and IncomeTaxSlab."CompanyId" = companyId;
	END;
$function$
;
