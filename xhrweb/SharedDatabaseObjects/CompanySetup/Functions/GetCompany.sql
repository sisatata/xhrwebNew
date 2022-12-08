CREATE OR REPLACE FUNCTION "main".GetCompany()
RETURNS TABLE (
       "Id" uuid,
        "CompanyName" character varying(250),
        "ShortName" character varying(20),
        "CompanyLocalizedName" character varying(250),
        "CompanySlogan" character varying(150),
        "LicenseKey" character varying(150),
        "SortOrder" bigint	
) 
AS $$
BEGIN
	RETURN QUERY
	select c."Id", c."CompanyName", c."ShortName", c."CompanyLocalizedName", c."CompanySlogan", c."LicenseKey", c."SortOrder"
	from main."Company" as c
    where c."IsDeleted" = False;
	
END;
$$
LANGUAGE plpgsql;