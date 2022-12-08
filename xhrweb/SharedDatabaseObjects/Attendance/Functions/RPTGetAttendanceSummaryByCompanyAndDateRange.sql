CREATE OR REPLACE FUNCTION attendance.rptattendancesummarybycompanyanddaterange(companyid uuid, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("EmployeeCode" character varying, "EID" uuid, "DepartmentId" uuid, "PositionId" uuid, "BranchName" character varying, "BranchId" uuid, "DesignationOrder" bigint, "FullName" character varying, "DepartmentName" character varying, "DesignationName" character varying, "CompanyName" character varying, "JoiningDateString" text, "Id" uuid, "EmployeeId" uuid, "PunchDate" timestamp without time zone, "PunchYear" character varying, "PunchMonth" integer, "TimeIn" timestamp without time zone, "TimeOut" timestamp without time zone, "OTHour" timestamp without time zone, "Status" character varying, "Status_V2" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
		
		    e."EmployeeId" as "EmployeeCode" ,
		    e."Id"  as "EID",
		    d."Id" as "DepartmentId",
		    d2."Id"  as "PositionId",
		    b."BranchName" ,
		    b."Id"  as "BranchId",
		    d2."SortOrder" as "DesignationOrder",
		    e."FullName" ,
		    d."DepartmentName" ,
		    d2."DesignationName" ,
		    c."CompanyName" ,
		    to_char(ee."JoiningDate",'dd/MM/yyyy') as "JoiningDateString",
			AttendanceProcessedData."Id" , 
			AttendanceProcessedData."EmployeeId" , 
			AttendanceProcessedData."PunchDate" , 
			AttendanceProcessedData."PunchYear" , 
			AttendanceProcessedData."PunchMonth" , 
		    AttendanceProcessedData."TimeIn", 
			AttendanceProcessedData."TimeOut" , 
			AttendanceProcessedData."OTHour" , 
			AttendanceProcessedData."Status" , 
			AttendanceProcessedData."Status_V2"   
		 FROM attendance."AttendanceProcessedData" AS AttendanceProcessedData 
		 inner join employee."Employees" e on AttendanceProcessedData."EmployeeId"  = e."Id" 
		 inner join main."Departments" d on d."Id" = e."DepartmentId" 
		 inner  join main."Designations" d2 on e."PositionId" =d2."Id" 
		 inner join main."Branch" b on e."BranchId" = b."Id" 
		 inner join employee."EmployeeEnrollments" ee on ee."EmployeeId" =e."Id" 
		 inner join main."Company" c on  e."CompanyId" = c."Id" 
		 WHERE e."CompanyId" = companyid
		 and (startdate is null or AttendanceProcessedData."PunchDate" >= startdate)
		 and e."IsDeleted"=false  and d."IsDeleted" =false and d2."IsDeleted" =false  
		 and ee."IsDeleted" =false and b."IsDeleted"  = false
	and (enddate is null or AttendanceProcessedData."PunchDate" <= enddate) order by AttendanceProcessedData."PunchDate" asc;
	END;
$function$
;
