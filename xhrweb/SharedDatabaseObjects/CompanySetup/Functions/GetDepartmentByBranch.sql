CREATE OR REPLACE FUNCTION main.getdepartmentbybranch(branchid uuid)
 RETURNS TABLE("Id" uuid, "CompanyId" uuid, "BranchId" uuid, "CompanyName" character varying, "BranchName" character varying, "CompanyLocalizedName" character varying, "BranchLocalizedName" character varying, "DepartmentName" character varying,"DepartmentLocalizedName" character varying, "ShortName" character varying,  "SortOrder" bigint)
 LANGUAGE plpgsql
AS $function$
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
    where d."IsDeleted" = False and d."BranchId" = branchId;
	
END;
$function$
;

--DROP FUNCTION getdepartmentbybranch(uuid) 
