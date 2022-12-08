CREATE OR REPLACE FUNCTION task.getparenttaskdetail(employeeid uuid, userid character varying, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("Id" uuid, "TaskTypeId" uuid, "TaskName" character varying, "TaskDescription" character varying, "Progress" integer, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "StatusId" integer, "AssigneeId" uuid, "CreatedBy" character varying, "TaskCreator" uuid, "FullName" character varying, "CompanyId" uuid, "ParentTaskId" uuid, "TaskCategoryName" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
		    TaskDetail."Id", 
		    TaskDetail."TaskTypeId" ,
			TaskDetail."TaskName",
			TaskDetail."TaskDescription",
			TaskDetail."Progress",
			TaskDetail."StartDate",
			TaskDetail."EndDate",
			TaskDetail."StatusId",
			TaskDetail."AssigneeId" ,
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
		 and TaskDetail."ParentTaskId" is null 
		 and (employeeid is null or Taskdetail."CreatedBy" = userid or Taskdetail."TaskCreator" = cast(userid as uuid) or employeeid = Taskdetail."AssigneeId" )
		 and
		 (((startdate is null or TaskDetail."StartDate" :: date >= startdate)
				and (enddate is null or TaskDetail."StartDate" :: date <= enddate) )
				or ((startdate is null or TaskDetail."EndDate" :: date >= startdate)
				and (enddate is null or TaskDetail."EndDate":: date <= enddate)))
				
		 
		
		 ;
	END;
$function$
;
