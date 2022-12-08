CREATE OR REPLACE FUNCTION attendance.getattendancerequestapprovependingqueue(managerid uuid, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("Id" uuid, "AttendanceApplicationId" uuid, "ManagerId" uuid, "ApprovalStatus" character varying, "ApprovedDate" timestamp without time zone, "Notes" character varying, "ApplicantName" character varying, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "RequestTypeId" integer, "ManagerName" character varying, "Reason" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			AttendanceRequestApproveQueue."Id" , 
			AttendanceRequestApproveQueue."AttendanceApplicationId" , 
			AttendanceRequestApproveQueue."ManagerId" , 
			AttendanceRequestApproveQueue."ApprovalStatus" , 
			AttendanceRequestApproveQueue."ApprovedDate" , 
			AttendanceRequestApproveQueue."Note" as "Notes"  ,
			app."FullName" ApplicantName,
			r."StartTime" StartDate,
			r."EndTime" EndDate,
			r."RequestTypeId" RequestTypeId,
			man."FullName"  ManagerName,
			r."Reason" 
		 FROM attendance."AttendanceRequestApproveQueues" AS AttendanceRequestApproveQueue 
		 inner join attendance."AttendanceRequests" r on AttendanceRequestApproveQueue."AttendanceApplicationId" = r."Id" 
		 inner  join employee."Employees"  app on r."EmployeeId" = app."Id" 
		 inner join employee."Employees" man on man."Id" = AttendanceRequestApproveQueue."ManagerId"
		WHERE  AttendanceRequestApproveQueue."ManagerId" = managerid  and (((startdate is null or r."StartTime" :: date >= startdate)
				and (enddate is null or r."StartTime" :: date <= enddate) )
				or ((startdate is null or r."EndTime" :: date >= startdate)
				and (enddate is null or r."EndTime":: date <= enddate)) or 
				( AttendanceRequestApproveQueue."ApprovalStatus" = '1' and r."StartTime" :: date >= startdate))
				order by r."ApprovalStatus" asc, r."StartTime" desc;
		WHERE  AttendanceRequestApproveQueue."ManagerId" = managerid  and (((startdate is null or r."StartTime" :: date >= startdate)
				and (enddate is null or r."StartTime" :: date <= enddate) )
				or ((startdate is null or r."EndTime" :: date >= startdate)
				and (enddate is null or r."EndTime":: date <= enddate)) or AttendanceRequestApproveQueue."ApprovalStatus" = '1' )
				order by r."ApprovalStatus", r."StartTime";
				END;
$function$
;
