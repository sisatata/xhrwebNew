CREATE OR REPLACE FUNCTION payroll.getincometaxparameterbycompany(companyid uuid)
 RETURNS TABLE("Id" uuid, "ParameterName" text, "LimitAmount" numeric, "LimitPercentageOfBasic" numeric, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "Remarks" character varying, "IsActive" boolean, "IsDeleted" boolean, "CompanyId" uuid, "PayerTypeCode" character varying, "PayerTypeName" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			IncomeTaxParameter."Id" , 
			IncomeTaxParameter."ParameterName" , 
			IncomeTaxParameter."LimitAmount" , 
			IncomeTaxParameter."LimitPercentageOfBasic" , 
			IncomeTaxParameter."StartDate" , 
			IncomeTaxParameter."EndDate" , 
			IncomeTaxParameter."Remarks" , 
			IncomeTaxParameter."IsActive" , 
			IncomeTaxParameter."IsDeleted" , 
			IncomeTaxParameter."CompanyId" , 
			IncomeTaxParameter."PayerTypeCode" ,
			IPT."PayerTypeName"
		 FROM payroll."IncomeTaxParameters" AS IncomeTaxParameter 
		  left join payroll."IncomeTaxPayerTypes" IPT on IPT."PayerTypeCode" = IncomeTaxParameter."PayerTypeCode"
		 WHERE IncomeTaxParameter."IsDeleted" = False and IncomeTaxParameter."CompanyId" = companyId;
	END;
$function$
;

--select * from payroll.getincometaxparameterbycompany('ab5aeca2-7a4a-4a20-bb96-383e72e839dc')
