CREATE OR REPLACE FUNCTION attendance.getattendancerequestbycompany(companyid uuid, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("Id" uuid, "EmployeeId" uuid, "RequestTypeId" uuid, "StartTime" timestamp without time zone, "EndTime" timestamp without time zone, "Reason" character varying, "AprovalStatus" character varying, "Note" character varying, "IsDeleted" boolean, "CompanyId" uuid)
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
			AttendanceRequest."AprovalStatus" , 
			AttendanceRequest."Note" , 
			AttendanceRequest."IsDeleted" , 
			AttendanceRequest."CompanyId" 
		 FROM attendance."AttendanceRequests" AS AttendanceRequest 
		 WHERE AttendanceRequest."IsDeleted" = False and AttendanceRequest."CompanyId" = companyId 
                and ((startdate is null or AttendanceRequest."StartDate" >= startdate)
				and (enddate is null or AttendanceRequest."StartDate" <= enddate) 
				or (startdate is null or AttendanceRequest."EndDate" >= startdate)
				and (enddate is null or AttendanceRequest."EndDate" <= enddate))
				order by AttendanceRequest."EmployeeId" , AttendanceRequest."ApprovalStatus", AttendanceRequest."StartTime";
	END;
$function$
;
