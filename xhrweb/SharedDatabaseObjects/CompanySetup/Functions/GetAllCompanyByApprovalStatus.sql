CREATE OR REPLACE FUNCTION main.GetAllCompanyByApprovalStatus(approvalStatus character varying)
 RETURNS TABLE("Id" uuid, "CompanyName" character varying, "ShortName" character varying, "CompanyLocalizedName" character varying, "CompanySlogan" character varying,
 "LicenseKey" character varying, "SortOrder" bigint,"ApprovalStatus" character varying, "Notes" character varying)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
	select c."Id", c."CompanyName", c."ShortName", c."CompanyLocalizedName", c."CompanySlogan", c."LicenseKey", c."SortOrder", c."ApprovalStatus",c."Notes" 
	from main."Company" as c
    where c."IsDeleted" = false and( c."ApprovalStatus" = approvalStatus or approvalStatus is null or approvalStatus ='' );
	END;
$function$
;
