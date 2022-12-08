CREATE OR REPLACE FUNCTION main.GetDistrict()
RETURNS TABLE (
		"Id" uuid ,
		"DivisionId" uuid ,
		"Name" character varying(50) ,
		"NameLocalized" character varying(50) ,
		--"Latitude" decimal ,
		--"Longitude" decimal ,
		"Website" character varying(150) 
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			District."Id" , 
			District."DivisionId" , 
			District."Name" , 
			District."NameLocalized" , 
			--District."Latitude" , 
			--District."Longitude" , 
			District."Website" 
		 FROM main."Districts" AS District 
		 WHERE District."IsDeleted" = False ;
	END;
$BODY$
LANGUAGE plpgsql;

