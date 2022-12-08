CREATE OR REPLACE FUNCTION task.getroottaskid(taskid uuid)
 RETURNS TABLE("Id" uuid, "TaskTypeId" uuid, "TaskName" character varying, "TaskDescription" character varying, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "StatusId" integer, "AssigneeId" uuid, "CreatedBy" character varying, "TaskCreator" uuid, "FullName" character varying, "CompanyId" uuid, "ParentTaskId" uuid, "TaskCategoryName" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		WITH RECURSIVE parents AS (
  SELECT td."Id" , td."TaskName", td."ParentTaskId" , 0 AS relative_depth, td."AssigneeId" , td."TaskTypeId" ,
  td."TaskDescription" , td."StartDate" ,td."EndDate" , td."StatusId" , td."CreatedBy", td."TaskCreator" ,td."CompanyId" 
  FROM task."TaskDetails" td  
  WHERE td."Id" = taskid

  UNION ALL

  SELECT cat."Id" , cat."TaskName" ,cat."ParentTaskId", p.relative_depth - 1,cat."AssigneeId" ,cat."TaskTypeId" ,
  cat."TaskDescription" , cat."StartDate" , cat."EndDate" , cat."StatusId" , cat."CreatedBy" , 
  cat."TaskCreator" ,
  cat."CompanyId" 
  FROM task."TaskDetails"  cat, parents p
  WHERE cat."Id" = p."ParentTaskId" 
)
select  
pp."Id", 
		    pp."TaskTypeId" ,
			pp."TaskName",
			pp."TaskDescription",
			pp."StartDate",
			pp."EndDate",
			pp."StatusId",
			pp ."AssigneeId" ,
			pp."CreatedBy",
			pp."TaskCreator" ,
			e."FullName",
			pp ."CompanyId" ,
			pp ."ParentTaskId" ,
			tc."TaskCategoryName" 

FROM parents pp 
left  join   employee."Employees" e on e."Id" =pp ."AssigneeId"  
inner join task."TaskCategories" tc on tc."Id" = pp."TaskTypeId"
 	 
order by pp.relative_depth limit 1;
	 
	END;
$function$
;
