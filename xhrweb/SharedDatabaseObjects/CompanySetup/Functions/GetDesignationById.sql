CREATE OR REPLACE FUNCTION "main".GetDesignationById(id uuid)
RETURNS TABLE (
       "Id" uuid,
        "DepartmentId" uuid,
        "DepartmentName" character varying(250),
        "DesignationName" character varying(250),
        "DepartmentLocalizedName" character varying(250),
        "DesignationLocalizedName" character varying(250),
        "ShortName" character varying(20),
        "SortOrder" bigint	
) 
AS $$
BEGIN
    RETURN QUERY
        select ds."Id"
                , d."Id" as "DepartmentId"
                , d."DepartmentName"
                , ds."DesignationName"
                , d."DepartmentLocalizedName"
                , ds."DesignationLocalizedName"
                , ds."ShortName"
                , ds."SortOrder"
                from main."Designations" as ds
                inner join main."Departments" as d on ds."LinkedEntityId" = d."Id"
        where ds."IsDeleted" = False and ds."Id" = id;
	
END;
$$
LANGUAGE plpgsql;