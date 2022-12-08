CREATE OR REPLACE FUNCTION payroll.rptgetpayslipbycompany(companyid uuid, financialyearid uuid, monthcycleid uuid)
 RETURNS TABLE("EmployeeId" character varying, "SalaryProcessedId" uuid, "FullName" character varying, "SalaryId" uuid, "JoiningDate" text, "MonthCycleName" character varying, "FinancialYearName" character varying, "CompanyName" character varying, "DepartmentName" character varying, "DesignationName" character varying, "GradeName" character varying, "PaymentOption" character varying, "TotalDaysInMonth" smallint, "TotalWorkingDays" smallint, "TotalPresentDays" smallint, "TotalAbsentDays" smallint, "TotalLeaveDays" smallint, "WeeklyOffDays" smallint, "GovernmentOffDays" smallint, "TotalWorkingHoliday" smallint, "OTHour" text, "OTRate" numeric, "GrossSalary" numeric, "PayableSalary" numeric, "TotalDeductionAmount" numeric, "NetPayableAmount" numeric, "ProcessDate" text, "StampCost" smallint, "Remarks" character varying)
 LANGUAGE plpgsql
AS $function$
	begin   
	RETURN QUERY
		SELECT 
			
			distinct
			e."EmployeeId" , 
			EmployeeSalaryProcessedData."Id" as "SalaryProcessedId",
			Employee."FullName" ,
		EmployeeSalaryProcessedData."EmloyeeSalaryId"   as "SalaryId",
			to_char(EmployeeEnrollment."JoiningDate",'dd-Mon-yyyy') as "JoiningDate", 
			MonthCycle."MonthCycleName" ,
			FinancialYear."FinancialYearName" ,
		--	ca."AddressLine1" as "Address",
			Company."CompanyName"  as "CompanyName",
			Department."DepartmentName",
			Designation."DesignationName"  ,
			Grade."GradeName" ,
			PaymentOption."OptionName" ,
			
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
			EmployeeSalaryProcessedData."GrossSalary",
			TRUNC(EmployeeSalaryProcessedData."PayableSalary",2) as PayableSalary , 
			TRUNC(EmployeeSalaryProcessedData."TotalDeductionAmount" ,2) as  "TotalDeductionAmount",
			TRUNC(EmployeeSalaryProcessedData."NetPayableAmount"  ,2) as "NetPayableAmount" , 
			to_char(EmployeeSalaryProcessedData."ProcessDate" ,'dd-Mon-yyyy') as "ProcessDate", 
			EmployeeSalaryProcessedData."StampCost" ,  			
			EmployeeSalaryProcessedData."Remarks" 	
		 FROM payroll."EmployeeSalaryProcessedDatas" AS EmployeeSalaryProcessedData 
		 inner join employee."Employees" e on EmployeeSalaryProcessedData."EmployeeId" = e."Id" 
		 inner join main."Company" Company on  Company."Id" = EmployeeSalaryProcessedData."CompanyId" 
		 inner join main."Designations" Designation on Designation."Id" = EmployeeSalaryProcessedData."PositionId" 
		 inner join main."Grades" Grade on Grade."Id" = EmployeeSalaryProcessedData."GradeId" 
		 inner join employee."Employees" Employee on Employee."Id" = EmployeeSalaryProcessedData."EmployeeId" 
		 inner join payroll."EmployeeSalaries" es on es."EmployeeId" = e."Id" 
	     inner join payroll."EmployeeSalaryComponents" esc3 on esc3."EmployeeSalaryId" =es."Id" 
		 inner join payroll."PaymentOptions" PaymentOption on EmployeeSalaryProcessedData."PaymentOptionId" = PaymentOption."PaymentOptionId" 
		 inner join main."FinancialYears" FinancialYear on FinancialYear."Id"  = financialyearid
		 inner join main."MonthCycles" MonthCycle on MonthCycle."Id" = monthcycleid
		 inner join main."Departments" Department  on Department."Id" = e."DepartmentId" 
	    -- inner join main."CompanyAddresses" ca  on ca."CompanyId" = Company."Id" 
		 inner join employee."EmployeeEnrollments" EmployeeEnrollment on  EmployeeSalaryProcessedData."EmployeeId" = EmployeeEnrollment."EmployeeId" 
		 WHERE EmployeeSalaryProcessedData."IsDeleted" = False and EmployeeSalaryProcessedData."CompanyId"  = companyid and EmployeeSalaryProcessedData."FinancialYearId"  = financialyearid
		 and (es."StartDate"<= CURRENT_DATE and es."EndDate">= CURRENT_DATE)  
		and EmployeeSalaryProcessedData."MonthCycleId"  = monthcycleid and es."IsDeleted" = false and Company."CompanyName" is not null  ;
		
	  
	END;  
$function$
;
