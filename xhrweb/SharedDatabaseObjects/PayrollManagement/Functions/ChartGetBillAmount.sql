CREATE OR REPLACE FUNCTION payroll.chartgetbillamount(companyid uuid, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("ApprovedAmount" numeric)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			  
			COALESCE( SUM(bbc."ApprovedAmount"),0) as "ApprovedAmount" from
			payroll."BenefitBillClaims" bbc where bbc."CompanyId" = '7b9a4259-97ef-4419-b1cd-7573cac7327f' and--bbc."BillDate" >= startdate and bbc."BillDate" <=enddate
			bbc."IsDeleted" = false;
	END;
$function$
;
