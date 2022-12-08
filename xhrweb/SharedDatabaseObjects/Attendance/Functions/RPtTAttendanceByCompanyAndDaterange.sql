CREATE OR REPLACE FUNCTION attendance.rptattendancebycompanyanddaterange(companyid uuid, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("EmployeeCode" character varying, "FullName" character varying, "DepartmentName" character varying, "DesignationName" character varying, "TotalPresentDays" integer, "TotalAbsentDays" integer, "TotalLeaveDays" integer, "Id" uuid, "EmployeeId" uuid, "PunchDate" timestamp without time zone, "PunchYear" character varying, "PunchMonth" integer, "TimeIn" timestamp without time zone, "TimeOut" timestamp without time zone, "ShiftIn" timestamp without time zone, "ShiftOut" timestamp without time zone, "BreakIn" timestamp without time zone, "BreakOut" timestamp without time zone, "BreakLate" timestamp without time zone, "Late" timestamp without time zone, "ShiftId" uuid, "ShiftCode" character varying, "RegularHour" timestamp without time zone, "OTHour" timestamp without time zone, "Status" character varying, "Status_V2" character varying, "BuyerShiftIn" timestamp without time zone, "BuyerShiftOut" timestamp without time zone, "BuyerOTTime" timestamp without time zone, "Remarks" character varying, "IsLunchOut" boolean, "FinancialYearId" uuid, "MonthCycleId" uuid, timeinlatitude numeric, timeinlongitude numeric, timeoutlatitude numeric, timeoutlongitude numeric, "EmployeeName" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
		    e."EmployeeId" as "EmployeeCode" ,
		    e."FullName" ,
		    d."DepartmentName" ,
		    d2."DesignationName" ,
		    espd."TotalPresentDays" ,
		    espd."TotalAbsentDays" ,
		    espd."TotalLeaveDays" ,
			AttendanceProcessedData."Id" , 
			AttendanceProcessedData."EmployeeId" , 
			AttendanceProcessedData."TimeIn" , 
			AttendanceProcessedData."TimeOut" , 
			AttendanceProcessedData."OTHour" , 
			AttendanceProcessedData."Status" , 
			AttendanceProcessedData."Status_V2"   
		 FROM attendance."AttendanceProcessedData" AS AttendanceProcessedData 
		 inner join employee."Employees" e on AttendanceProcessedData."EmployeeId"  = e."Id" 
		 inner join main."Departments" d on d."Id" = e."DepartmentId" 
		 inner  join main."Designations" d2 on e."PositionId" =d2."Id" 
		 inner join payroll."EmployeeSalaryProcessedDatas" espd on espd."EmployeeId" =AttendanceProcessedData."EmployeeId" 
		 WHERE e."CompanyId" = companyid
		 and (startdate is null or AttendanceProcessedData."PunchDate" :: date >= startdate)
		 and e."IsDeleted"=false  and d."IsDeleted" =false and d2."IsDeleted" =false  and espd."IsDeleted" =false 
		
	and (enddate is null or AttendanceProcessedData."PunchDate" :: date<= enddate) order by AttendanceProcessedData."PunchDate" desc
		 ;
	END;
$function$
;
