CREATE OR REPLACE FUNCTION payroll.getemployeesalaryprocesseddata(companyid uuid, financialyearid uuid, monthcycleid uuid)
 RETURNS TABLE("Id" uuid, "EmployeeId" uuid, "FinancialYearId" uuid, "MonthCycleId" uuid, "PaymentOptionId" smallint, "CompanyId" uuid, "BranchId" uuid, "DepartmentId" uuid, "PositionId" uuid, "GradeId" uuid, "TotalDaysInMonth" smallint, "TotalWorkingDays" smallint, "TotalPresentDays" smallint, "TotalAbsentDays" smallint, "TotalLeaveDays" smallint, "WeeklyOffDays" smallint, "GovernmentOffDays" smallint, "TotalWorkingHoliday" smallint, "OTHour" text, "OTRate" numeric, "GrossSalary" numeric, "PayableSalary" numeric, "TotalDeductionAmount" numeric, "NetPayableAmount" numeric, "ProcessDate" timestamp without time zone, "StampCost" smallint, "EmloyeeSalaryId" uuid, "IsApproved" boolean, "ApprovedBy" uuid, "ApprovedTime" timestamp without time zone, "Remarks" character varying, "FullName" character varying, "CompanyEmployeeId" character varying, "BranchName" character varying, "DepartmentName" character varying, "DesignationName" character varying, "LoginId" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			EmployeeSalaryProcessedData."Id" , 
			EmployeeSalaryProcessedData."EmployeeId" , 
			EmployeeSalaryProcessedData."FinancialYearId" , 
			EmployeeSalaryProcessedData."MonthCycleId" , 
			EmployeeSalaryProcessedData."PaymentOptionId" , 
			EmployeeSalaryProcessedData."CompanyId" , 
			EmployeeSalaryProcessedData."BranchId" , 
			EmployeeSalaryProcessedData."DepartmentId" , 
			EmployeeSalaryProcessedData."PositionId" , 
			EmployeeSalaryProcessedData."GradeId" , 
			EmployeeSalaryProcessedData."TotalDaysInMonth" , 
			EmployeeSalaryProcessedData."TotalWorkingDays" , 
			EmployeeSalaryProcessedData."TotalPresentDays" , 
			EmployeeSalaryProcessedData."TotalAbsentDays" , 
			EmployeeSalaryProcessedData."TotalLeaveDays" , 
			EmployeeSalaryProcessedData."WeeklyOffDays" , 
			EmployeeSalaryProcessedData."GovernmentOffDays" , 
			EmployeeSalaryProcessedData."TotalWorkingHoliday" , 
			EmployeeSalaryProcessedData."OTHour" , 
			EmployeeSalaryProcessedData."OTRate" , 
			EmployeeSalaryProcessedData."GrossSalary" , 
			EmployeeSalaryProcessedData."PayableSalary" , 
			EmployeeSalaryProcessedData."TotalDeductionAmount" , 
			EmployeeSalaryProcessedData."NetPayableAmount" , 
			EmployeeSalaryProcessedData."ProcessDate" , 
			EmployeeSalaryProcessedData."StampCost" ,  
			EmployeeSalaryProcessedData."EmloyeeSalaryId" , 
			EmployeeSalaryProcessedData."IsApproved" , 
			EmployeeSalaryProcessedData."ApprovedBy" , 
			EmployeeSalaryProcessedData."ApprovedTime" , 
			EmployeeSalaryProcessedData."Remarks" ,
			e."FullName" ,
			e."EmployeeId" as "CompanyEmployeeId",
			b."BranchName" ,
			d."DepartmentName" ,
			d2."DesignationName" ,
			anu."Email"  as "LoginId"
		 FROM payroll."EmployeeSalaryProcessedDatas" AS EmployeeSalaryProcessedData inner join employee."Employees" e on EmployeeSalaryProcessedData."EmployeeId" = e."Id" 
		 inner join main."Branch" b on  EmployeeSalaryProcessedData."BranchId"  = b."Id" 
		 inner join main."Departments" d  on EmployeeSalaryProcessedData."DepartmentId"  = d."Id" 
		 inner join main."Designations" d2 on EmployeeSalaryProcessedData."PositionId"  = d2."Id" 
		 inner join payroll."EmployeeSalaries" es  on es."Id"  = EmployeeSalaryProcessedData."EmloyeeSalaryId" 
		 inner join "access"."AspNetUsers" anu  on e."LoginId" :: text = anu."Id" 
		 WHERE EmployeeSalaryProcessedData."IsDeleted" = False and EmployeeSalaryProcessedData."CompanyId"  = companyId and EmployeeSalaryProcessedData."FinancialYearId"  =  financialYearId
		 and b."IsDeleted"  = false and d."IsDeleted"  = false and d2."IsDeleted" =false  
		 and es."IsDeleted"  = false 
		 and  (es."StartDate"<= CURRENT_DATE and es."EndDate">= CURRENT_DATE) 
		and EmployeeSalaryProcessedData."MonthCycleId"  = monthCycleId;
	END;
$function$
;
