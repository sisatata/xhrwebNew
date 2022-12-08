CREATE OR REPLACE FUNCTION main.getofficenoticebyid(id uuid)
 RETURNS TABLE("Id" uuid, "CompanyId" uuid, "Subject" character varying, "Details" character varying, "IsGeneral" boolean, "IsSectionSpecific" boolean, "ApplicableSections" text, "PublishDate" timestamp, "StartDate" timestamp, "EndDate" timestamp, "IsDeleted" boolean, "IsPublished" boolean)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			OfficeNotice."Id" , 
			OfficeNotice."CompanyId" , 
			OfficeNotice."Subject" , 
			OfficeNotice."Details" , 
			OfficeNotice."IsGeneral" , 
			OfficeNotice."IsSectionSpecific" , 
			OfficeNotice."ApplicableSections" , 
			OfficeNotice."PublishDate" , 
			OfficeNotice."StartDate" , 
			OfficeNotice."EndDate" , 
			OfficeNotice."IsDeleted" , 
			OfficeNotice."IsPublished" 
		 FROM main."OfficeNotices" AS OfficeNotice 
		 WHERE OfficeNotice."IsDeleted" = False and OfficeNotice."Id"  = id ;
	END;
$function$
;


--DROP FUNCTION main.getofficenoticebyid(uuid) ;

--select * from main.getofficenoticebyid('ab5aeca2-7a4a-4a20-bb96-383e72e839dc')

