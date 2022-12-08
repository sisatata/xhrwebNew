CREATE OR REPLACE FUNCTION "main".GetDepartmentById(departmentId uuid)
RETURNS TABLE (
       "Id" uuid,
        "CompanyId" uuid,
        "BranchId" uuid,
        "CompanyName" character varying(250),
        "BranchName" character varying(250),
        "CompanyLocalizedName" character varying(250),
        "BranchLocalizedName" character varying(250),
        "DepartmentName" character varying(250),
        "ShortName" character varying(20),
        "DepartmentLocalizedName" character varying(250),
        "SortOrder" bigint	
) 
AS $$
BEGIN
	RETURN QUERY
	select d."Id"
        , d."CompanyId"
        , d."BranchId"
        , c."CompanyName"
        , c."CompanyLocalizedName"
        , b."BranchName"
        , b."BranchLocalizedName"
        , d."DepartmentName"
        , d."DepartmentLocalizedName"
        , d."ShortName"
        , d."SortOrder"
	from main."Departments" as d
        inner join main."Branch" as b on d."BranchId" = b."Id"
        inner join main."Company" as c on d."CompanyId" = c."Id"
    where d."IsDeleted" = False and d."Id" = departmentId;
	
END;
$$
LANGUAGE plpgsql;