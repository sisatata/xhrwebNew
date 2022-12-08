CREATE OR REPLACE FUNCTION attendance.getattendancerequestbyid(id uuid)
 RETURNS TABLE("Id" uuid,
 "EmployeeId" uuid,
 "ApplicantName" character varying,
 "RequestTypeId" int4, 
 "StartTime" timestamp without time zone,
 "EndTime" timestamp without time zone,
 "Reason" character varying,
 "ApprovalStatus" character varying,
 "Note" character varying, 
 "IsDeleted" boolean, "CompanyId" uuid)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			AttendanceRequest."Id" , 
			AttendanceRequest."EmployeeId" , 
			e."FullName" "ApplicantName",
			AttendanceRequest."RequestTypeId" , 
			AttendanceRequest."StartTime" , 
			AttendanceRequest."EndTime" , 
			AttendanceRequest."Reason" , 
			AttendanceRequest."ApprovalStatus" , 
			AttendanceRequest."Note" , 
			AttendanceRequest."IsDeleted" , 
			AttendanceRequest."CompanyId" 
		 FROM attendance."AttendanceRequests" AS AttendanceRequest 
		 inner join employee."Employees" e on AttendanceRequest."EmployeeId" = e."Id" 
		 WHERE AttendanceRequest."IsDeleted" = False and AttendanceRequest."Id" = id ;
	END;
$function$
;


--select * from attendance.getattendancerequestbyid('1f4e1f27-a272-43a6-a6cc-2786c77bdf5d');
--DROP FUNCTION attendance.getattendancerequestbyid(uuid) 