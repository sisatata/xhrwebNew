CREATE OR REPLACE FUNCTION attendance.getattendanceprocesseddatalistbycompany(companyid uuid, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("Id" uuid, "EmployeeId" uuid, "PunchDate" timestamp without time zone, "PunchYear" character varying, "PunchMonth" integer, "TimeIn" timestamp without time zone, "TimeOut" timestamp without time zone, "ShiftIn" timestamp without time zone, "ShiftOut" timestamp without time zone, "BreakIn" timestamp without time zone, "BreakOut" timestamp without time zone, "BreakLate" timestamp without time zone, "Late" timestamp without time zone, "ShiftId" uuid, "ShiftCode" character varying, "RegularHour" timestamp without time zone, "OTHour" timestamp without time zone, "Status" character varying, "Status_V2" character varying, "BuyerShiftIn" timestamp without time zone, "BuyerShiftOut" timestamp without time zone, "BuyerOTTime" timestamp without time zone, "Remarks" character varying, "IsLunchOut" boolean, "FinancialYearId" uuid, "MonthCycleId" uuid, timeinlatitude numeric, timeinlongitude numeric, timeoutlatitude numeric, timeoutlongitude numeric, "EmployeeName" character varying, "EmployeeRemarks" character varying, "ClockInAddress" character varying, "ClockOutAddress" character varying, "BranchId" uuid, "BranchName" character varying, "PositionId" uuid, "DepartmentId" uuid, "EmployeeCompanyId" character varying, "DepartmentName" character varying, "DesignationName" character varying, "LoginId" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			AttendanceProcessedData."Id" , 
		    AttendanceProcessedData."EmployeeId" ,
			AttendanceProcessedData."PunchDate" , 
			AttendanceProcessedData."PunchYear" , 
			AttendanceProcessedData."PunchMonth" , 
			AttendanceProcessedData."TimeIn" , 
			AttendanceProcessedData."TimeOut" , 
			AttendanceProcessedData."ShiftIn" , 
			AttendanceProcessedData."ShiftOut" , 
			AttendanceProcessedData."BreakIn" , 
			AttendanceProcessedData."BreakOut" , 
			AttendanceProcessedData."BreakLate" , 
			AttendanceProcessedData."Late" , 
			AttendanceProcessedData."ShiftId" , 
			AttendanceProcessedData."ShiftCode" , 
			AttendanceProcessedData."RegularHour" , 
			AttendanceProcessedData."OTHour" , 
			AttendanceProcessedData."Status" , 
			AttendanceProcessedData."Status_V2" , 
			AttendanceProcessedData."BuyerShiftIn" , 
			AttendanceProcessedData."BuyerShiftOut" , 
			AttendanceProcessedData."BuyerOTTime" , 
			AttendanceProcessedData."Remarks" , 
			AttendanceProcessedData."IsLunchOut" , 
			AttendanceProcessedData."FinancialYearId" , 
			AttendanceProcessedData."MonthCycleId" ,
			AttendanceProcessedData."TimeInLatitude" ,
			AttendanceProcessedData."TimeInLongitude" ,
			AttendanceProcessedData."TimeOutLatitude" ,
			AttendanceProcessedData."TimeOutLongitude" ,
			e."FullName" AS "EmployeeName",
			AttendanceProcessedData."EmployeeRemarks" ,
			AttendanceProcessedData."ClockInAddress" ,
			AttendanceProcessedData."ClockOutAddress" ,
			e."BranchId" ,
			b."BranchName" ,
			e."PositionId" ,
			e."DepartmentId" ,
			e."EmployeeId" as "EmployeeCompanyId" ,
			d."DepartmentName" ,
			d2."DesignationName" ,
			anu."Email"  as "LoginId"
		 FROM attendance."AttendanceProcessedData" AS AttendanceProcessedData 
		 inner join employee."Employees" e on AttendanceProcessedData."EmployeeId"  = e."Id" 
		 inner join main."Branch" b on  e."BranchId"  = b."Id" 
		 inner join main."Departments" d  on e."DepartmentId"  = d."Id" 
		 inner join main."Designations" d2 on e."PositionId"  = d2."Id" 
		 inner join "access"."AspNetUsers" anu  on e."LoginId" :: text = anu."Id" 
		 WHERE e."CompanyId" = companyId 
		 and (startdate is null or AttendanceProcessedData."PunchDate" >= startdate)
		 and e."IsDeleted" =false and  b."IsDeleted"  = false and d."IsDeleted"  = false and d2."IsDeleted"  = false
	and (enddate is null or AttendanceProcessedData."PunchDate" <= enddate) order by AttendanceProcessedData."PunchDate" desc
		 ;
	END;
$function$
;
