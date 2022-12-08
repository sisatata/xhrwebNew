
CREATE OR REPLACE FUNCTION "main".GetDesignationByEntity(entityId uuid)
RETURNS TABLE (
       "Id" uuid,
	    "CompanyId" uuid,
	    "CompanyName" character varying(250),
	    "BranchId" uuid,
	    "BranchName" character varying(250),
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
		    , c."Id" as CompanyId
			, c."CompanyName" as CompanyName
			, b."Id" as BranchId
			, b."BranchName" as BranchName
            , d."Id" as "DepartmentId"
            , d."DepartmentName"
            , ds."DesignationName"
            , d."DepartmentLocalizedName"
            , ds."DesignationLocalizedName"
            , ds."ShortName"
            , ds."SortOrder"
        from main."Designations" as ds
            inner join main."Departments" as d on ds."LinkedEntityId" = d."Id"
			INNER JOIN main."Branch" as b ON b."Id"=d."BranchId"
			INNER JOIN main."Company" as c ON c."Id"=b."CompanyId"
        where ds."IsDeleted" = False and ds."LinkedEntityId" = entityId;
	
END;
$$
LANGUAGE plpgsql;