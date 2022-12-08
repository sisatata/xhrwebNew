CREATE OR REPLACE FUNCTION task.getchildtaskdetail(parenttaskid uuid)
 RETURNS TABLE("Id" uuid, "TaskTypeId" uuid, "TaskName" character varying, "TaskDescription" character varying, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "StatusId" integer, "AssigneeId" uuid, "CreatedBy" character varying, "TaskCreator" uuid, "FullName" character varying, "CompanyId" uuid, "ParentTaskId" uuid, "TaskCategoryName" character varying)
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
			TaskDetail ."AssigneeId" ,
			TaskDetail."CreatedBy",
			TaskDetail."TaskCreator",
			e."FullName" ,
			TaskDetail ."CompanyId" ,
			TaskDetail ."ParentTaskId" ,
			tc."TaskCategoryName" 
		 FROM Task."TaskDetails"   AS TaskDetail 
		 left  join   employee."Employees" e on e."Id" =TaskDetail ."AssigneeId" 
		 inner join task."TaskCategories" tc on tc."Id" = TaskDetail."TaskTypeId"
		 WHERE TaskDetail."IsDeleted" = false 
		 and TaskDetail."ParentTaskId" = parenttaskid ; --and (employeeid is null or Taskdetail."CreatedBy"  =  userid  or employeeid = Taskdetail."ManagerId" )
		
		 
	END;
$function$
;
