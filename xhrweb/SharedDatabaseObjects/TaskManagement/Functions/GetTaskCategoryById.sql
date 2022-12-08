CREATE OR REPLACE FUNCTION task.gettaskcategorybyid(companyid uuid)
 RETURNS TABLE("Id" uuid, "TaskCategoryName" character varying, "Remarks" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
		   TaskCategory."Id", 
			TaskCategory."TaskCategoryName" ,
			TaskCategory ."Remarks" 
			
		 FROM Task ."TaskCategories"  AS TaskCategory 
		 WHERE TaskCategory."IsDeleted" = false and TaskCategory."CompanyId" = companyid;
	END;
$function$
;