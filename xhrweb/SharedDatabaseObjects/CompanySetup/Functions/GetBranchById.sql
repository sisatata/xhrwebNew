CREATE OR REPLACE FUNCTION "main".GetBranchById(branchId uuid)
RETURNS TABLE (
       "Id" uuid,
        "CompanyId" uuid,
        "CompanyName" character varying(250),
        "BranchName" character varying(250),
        "ShortName" character varying(20),
        "BranchLocalizedName" character varying(250),
        "SortOrder" bigint	
) 
AS $$
BEGIN
	RETURN QUERY
	select b."Id", c."Id" as "CompanyId", c."CompanyName", b."BranchName",
         b."ShortName", b."BranchLocalizedName", b."SortOrder"
	from main."Branch" as b
        inner join main."Company" as c on b."CompanyId" = c."Id"
    where b."IsDeleted" = False and b."Id" = branchId;
	
END;
$$
LANGUAGE plpgsql;