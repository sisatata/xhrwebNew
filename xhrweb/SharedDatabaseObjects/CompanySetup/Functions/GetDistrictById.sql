CREATE OR REPLACE FUNCTION main.GetDistrictById(id uuid)
RETURNS TABLE (
		"Id" uuid ,
		"DivisionId" uuid ,
		"Name" character varying(50) ,
		"NameLocalized" character varying(50) ,
		--"Latitude" Float ,
		--"Longitude" Float ,
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
		 WHERE District."IsDeleted" = False and District."Id" = id;
	END;
$BODY$
LANGUAGE plpgsql;

