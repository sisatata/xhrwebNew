CREATE OR REPLACE FUNCTION main.GetGradeById(id uuid)
RETURNS TABLE (
		"Id" uuid ,
		"GradeName" character varying(50) ,
		"GradeNameLocalized" character varying(50) ,
		"Rank" int4 ,
		"IsDeleted" boolean ,
		"CompanyId" uuid 
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			Grade."Id" , 
			Grade."GradeName" , 
			Grade."GradeNameLocalized" , 
			Grade."Rank" , 
			Grade."IsDeleted" , 
			Grade."CompanyId" 
		 FROM main."Grade" AS Grade 
		 WHERE Grade."IsDeleted" = False and Grade."Id" = id;
	END;
$BODY$
LANGUAGE plpgsql;

