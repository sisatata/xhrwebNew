CREATE OR REPLACE FUNCTION payroll.GetBonusConfigurationById(id uuid)
RETURNS TABLE (
		"Id" uuid ,
		"CompanyId" uuid ,
		"ReligionId" uuid ,
		"RangeFromInMonth" int4 ,
		"RangeToInMonth" int4 ,
		"BasicOrGrossId" int4 ,
		"PercentOfBasicOrGross" int4 ,
		"FixedAmount" int4 ,
		"IsPaidFull" boolean ,
		"PartialOnId" int4 ,
		"StartDate" timestamp ,
		"EndDate" timestamp ,
		"Remarks" character varying(250) ,
		"IsDeleted" boolean 
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			BonusConfiguration."Id" , 
			BonusConfiguration."CompanyId" , 
			BonusConfiguration."ReligionId" , 
			BonusConfiguration."RangeFromInMonth" , 
			BonusConfiguration."RangeToInMonth" , 
			BonusConfiguration."BasicOrGrossId" , 
			BonusConfiguration."PercentOfBasicOrGross" , 
			BonusConfiguration."FixedAmount" , 
			BonusConfiguration."IsPaidFull" , 
			BonusConfiguration."PartialOnId" , 
			BonusConfiguration."StartDate" , 
			BonusConfiguration."EndDate" , 
			BonusConfiguration."Remarks" , 
			BonusConfiguration."IsDeleted" 
		 FROM payroll."BonusConfigurations" AS BonusConfiguration 
		 WHERE BonusConfiguration."IsDeleted" = False and BonusConfiguration."Id" = id ;
	END;
$BODY$
LANGUAGE plpgsql;


select * from payroll.GetBonusConfigurationById('7962408f-0a50-42fa-b38f-97c9dc0e4d46');