CREATE OR REPLACE FUNCTION "attendance".GetShiftGroupByCompany(compnayId uuid)
RETURNS TABLE (
       "Id" uuid,
        "CompanyId" uuid,
        "CompanyName" character varying(250),
        "ShiftGroupName" character varying(250),
        "ShiftGroupNameLC" character varying(250)
) 
AS $$
BEGIN
	RETURN QUERY
	select b."Id", c."Id" as "CompanyId", c."CompanyName", b."ShiftGroupName",
         b."ShiftGroupNameLC"
	from attendance."ShiftGroups" as b
        inner join main."Company" as c on b."CompanyId" = c."Id"
    where b."IsDeleted" = False and b."CompanyId" = compnayId;
	
END;
$$
LANGUAGE plpgsql;