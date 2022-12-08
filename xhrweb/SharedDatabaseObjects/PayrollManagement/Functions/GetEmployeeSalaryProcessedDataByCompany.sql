CREATE OR REPLACE FUNCTION payroll.getemployeesalaryprocesseddatabycompany(companyid uuid, financialyearid uuid, monthcycleid uuid)
 RETURNS TABLE("Id" uuid, "EmployeeId" uuid, "FinancialYearId" uuid, "MonthCycleId" uuid, "PaymentOptionId" smallint, "CompanyId" uuid, "BranchId" uuid, "DepartmentId" uuid, "PositionId" uuid, "GradeId" uuid, "TotalDaysInMonth" smallint, "TotalWorkingDays" smallint, "TotalPresentDays" smallint, "TotalAbsentDays" smallint, "TotalLeaveDays" smallint, "WeeklyOffDays" smallint, "GovernmentOffDays" smallint, "TotalWorkingHoliday" smallint, "OTHour" text, "OTRate" numeric, "GrossSalary" numeric, "PayableSalary" numeric, "TotalDeductionAmount" numeric, "NetPayableAmount" numeric, "ProcessDate" timestamp without time zone, "StampCost" smallint, "EmloyeeSalaryId" uuid, "IsApproved" boolean, "ApprovedBy" uuid, "ApprovedTime" timestamp without time zone, "Remarks" character varying, "FullName" character varying)
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
			e."FullName" 
		 FROM payroll."EmployeeSalaryProcessedDatas" AS EmployeeSalaryProcessedData inner join employee."Employees" e on EmployeeSalaryProcessedData."EmployeeId" = e."Id" 
		 WHERE EmployeeSalaryProcessedData."IsDeleted" = False and EmployeeSalaryProcessedData."CompanyId"  = companyId and EmployeeSalaryProcessedData."FinancialYearId"  =  financialYearId
		and EmployeeSalaryProcessedData."MonthCycleId"  = monthCycleId;
	END;
$function$
;
