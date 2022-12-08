CREATE OR REPLACE FUNCTION task.gettaskdetailbyid(companyid uuid)
 RETURNS TABLE("Id" uuid, "TaskTypeId" uuid, "TaskName" character varying, "TaskDescription" character varying, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "StatusId" integer, "ManagerId" uuid, "FullName" character varying, "CompanyId" uuid, "ParentTaskId" uuid, "TaskCreator" uuid, "TaskCategoryName" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
		    TaskDetail."Id", 
		    TaskDetail."TaskTypeId" ,
			TaskDetail."TaskName",
			TaskDetail."TaskDescription",
			TaskDetail."StartDate",
			TaskDetail."EndDate",
			TaskDetail."StatusId",
			TaskDetail ."ManagerId" ,
			e."FullName" ,
			TaskDetail ."CompanyId" ,
			TaskDetail ."ParentTaskId" ,
			TaskDetail."TaskCreator",
			tc."TaskCategoryName" 
			
		 FROM Task."TaskDetails"   AS TaskDetail 
		 left  join   employee."Employees" e on e."Id" =TaskDetail ."ManagerId" 
		 inner join task."TaskCategories" tc on tc."Id" = TaskDetail."TaskTypeId"
		 WHERE TaskDetail."IsDeleted" = false and TaskDetail."CompanyId" = companyid
		 and TaskDetail."ParentTaskId" is null
		
		 ;
	END;
$function$
;
