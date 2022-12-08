CREATE OR REPLACE FUNCTION main.GetUpazilaByDistrict(districtId uuid)
RETURNS TABLE (
		"Id" uuid ,
		"DistrictId" uuid ,
		"UpazilaName" character varying(50) ,
		"UpazilaNameLocalized" character varying(50) 
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			Upazila."Id" , 
			Upazila."DistrictId" , 
			Upazila."UpazilaName" , 
			Upazila."UpazilaNameLocalized" 
		 FROM main."Upazilas" AS Upazila 
		 WHERE Upazila."IsDeleted" = False and Upazila."DistrictId" = districtId;
	END;
$BODY$
LANGUAGE plpgsql;

