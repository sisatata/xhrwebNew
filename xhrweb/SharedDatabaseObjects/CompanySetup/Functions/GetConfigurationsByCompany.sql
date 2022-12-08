CREATE OR REPLACE FUNCTION main.getconfigurationsbycompany(companyid uuid)
 RETURNS TABLE("Id" uuid, "CustomConfigurationId" uuid,"Code" character varying, "DefaultValue" character varying, "DefaultDescription" character varying,
  "CustomValue" character varying, "CustomDescription" character varying, startdate timestamp without time zone, enddate timestamp without time zone, "CompanyId" uuid)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
	select
	dc."Id" ,
	cc."Id" as "CustomConfigurationId",
	dc."Code",
	dc."DefaultValue" ,
	dc."Description" "DefaultDescription",
	cc."CustomValue" ,
	cc."Description" as "CustomDescription",
	cc."StartDate" ,
	cc."EndDate",
	cc."CompanyId" 
from
	main."DefaultConfigurations" dc
left join main."CustomConfigurations" cc on
	dc."Code"::text = cc."Code"::text
	and cc."StartDate" <= CURRENT_DATE
	and cc."EndDate" >= CURRENT_DATE
	and cc."CompanyId" = companyid
order by
	dc."Code";
	END;
$function$
;

DROP FUNCTION main.getconfigurationsbycompany(uuid);

select * from main.getconfigurationsbycompany('') 
