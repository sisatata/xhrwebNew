CREATE OR REPLACE FUNCTION attendance.getattendancerequestbysearch(employeeid uuid, requesttypeid integer, starttime timestamp without time zone, endtime timestamp without time zone)
 RETURNS TABLE("Id" uuid, "EmployeeId" uuid, "RequestTypeId" integer, "StartTime" timestamp without time zone, "EndTime" timestamp without time zone, "Reason" character varying, "ApprovalStatus" character varying, "Note" character varying, "IsDeleted" boolean, "CompanyId" uuid)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			AttendanceRequest."Id" , 
			AttendanceRequest."EmployeeId" , 
			AttendanceRequest."RequestTypeId" , 
			AttendanceRequest."StartTime" , 
			AttendanceRequest."EndTime" , 
			AttendanceRequest."Reason" , 
			AttendanceRequest."ApprovalStatus" , 
			AttendanceRequest."Note" , 
			AttendanceRequest."IsDeleted" , 
			AttendanceRequest."CompanyId" 
		 FROM attendance."AttendanceRequests" AS AttendanceRequest 
		 WHERE AttendanceRequest."IsDeleted" = False 
				and (employeeId is null or AttendanceRequest."EmployeeId" = employeeId)
			and (requestTypeId is null or requestTypeId = 0 or AttendanceRequest."RequestTypeId" = requestTypeId) and
		(((starttime is null or AttendanceRequest."StartTime" :: date >= starttime)
				and (endtime is null or AttendanceRequest."StartTime" :: date <= endtime) )
				or ((starttime is null or AttendanceRequest."EndTime" :: date >= starttime)
				and (endtime is null or AttendanceRequest."EndTime":: date <= endtime)));
	END;
$function$
;
