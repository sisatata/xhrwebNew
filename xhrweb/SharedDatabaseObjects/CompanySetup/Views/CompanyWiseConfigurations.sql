CREATE OR REPLACE VIEW main."CompanyWiseConfigurations"
AS 
select
	case
		when cc."Id" is null then uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)
		else cc."Id"
	end as "Id",
	dc."Code",
	case
		when cc."CustomValue" is null then dc."DefaultValue"
		else cc."CustomValue"
	end "Value" ,
	c."Id" "CompanyId"
from
	main."DefaultConfigurations" dc
cross join main."Company" c
left join main."CustomConfigurations" cc on
	dc."Code"::text = cc."Code"::text
	and c."Id" = cc."CompanyId"
	and cc."StartDate" <= CURRENT_DATE
	and cc."EndDate" >= CURRENT_DATE 
	where c."IsDeleted" = false 
	and c."IsActive" = true 
	and c."ApprovalStatus" = '3';