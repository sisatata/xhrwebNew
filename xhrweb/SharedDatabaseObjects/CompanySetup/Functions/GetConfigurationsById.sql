CREATE OR REPLACE FUNCTION main.GetConfigurationsById(id uuid)
 RETURNS TABLE("Id" uuid, "Code" character varying, "DefaultValue" character varying, "Description" character varying,
 "CustomConfigurationId" uuid, "CustomValue" character varying,"CustomDescription" character varying,
 StartDate timestamp(6)  ,EndDate timestamp(6), "CompanyId" uuid)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
	select
	dc ."Id" ,
	dc ."Code",
	dc ."DefaultValue" ,
	dc ."Description",
	case
		when cc."Id" is null then '00000000-0000-0000-0000-000000000000'::uuid
		else cc."Id" end as "CustomConfigurationId",
		cc."CustomValue" ,
		cc."Description" as "CustomDescription" ,
		cc."StartDate",
		cc."EndDate",
		c."Id" as "CompanyId"
	from
		main."DefaultConfigurations" dc
	cross join main."Company" c
	left join main."CustomConfigurations" cc on
		dc."Code"::text = cc."Code"::text
		and c."Id" = cc."CompanyId"
		--and cc."StartDate" <= current_date
		--and cc."EndDate" >= current_date 
		and cc."IsDeleted" = false 
	where
		c."IsDeleted" = false and dc."IsDeleted" = false and dc ."Id" = id ;
	END;
$function$
;


--DROP FUNCTION main.getconfigurationsbyid(uuid) ;