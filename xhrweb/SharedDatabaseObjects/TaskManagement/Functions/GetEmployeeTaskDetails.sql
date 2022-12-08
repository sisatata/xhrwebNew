CREATE OR REPLACE FUNCTION task.getemployeetaskdetails(employeeid uuid)
 RETURNS TABLE("Id" uuid)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
		    TaskDetail."Id"
		   
		 FROM Task."TaskDetails"   AS TaskDetail 
		
		 WHERE 
		  TaskDetail."AssigneeId" = employeeid;
		
		 
	END;
$function$
;
