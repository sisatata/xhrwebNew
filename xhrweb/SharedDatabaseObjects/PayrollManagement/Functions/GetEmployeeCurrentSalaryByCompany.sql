CREATE OR REPLACE FUNCTION payroll.getemployeecurrentsalarybycompany(companyid uuid)
 RETURNS TABLE("Id" uuid, "EmployeeId" uuid, "BranchId" uuid, "DepartmentId" uuid, "PositionId" uuid, "GradeId" uuid, "SalaryStructureId" uuid, "PaymentOptionId" smallint, "GrossSalary" numeric, "CompanyId" uuid, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "IsDeleted" boolean, "BranchName" character varying, "DepartmentName" character varying, "DesignationName" character varying, "GradeName" character varying, "StructureName" character varying, "OptionName" character varying, "FullName" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			EmployeeSalary."Id" , 
			EmployeeSalary."EmployeeId" , 
			EmployeeSalary."BranchId" , 
			EmployeeSalary."DepartmentId" , 
			EmployeeSalary."PositionId" , 
			EmployeeSalary."GradeId" , 
			EmployeeSalary."SalaryStructureId" , 
			EmployeeSalary."PaymentOptionId" , 
			EmployeeSalary."GrossSalary" , 
			EmployeeSalary."CompanyId" , 
			EmployeeSalary."StartDate" , 
			EmployeeSalary."EndDate" , 
			EmployeeSalary."IsDeleted" ,
			b."BranchName" ,
			d."DepartmentName" ,
			d2."DesignationName" ,
			g."GradeName" ,
			ss."StructureName" ,
			po."OptionName" ,
			e."FullName" 
		 FROM payroll."EmployeeSalaries" AS EmployeeSalary 
		 inner join employee."Employees" e  on e."Id" = EmployeeSalary."EmployeeId"
		 inner join employee."EmployeeEnrollments" ee   on ee."EmployeeId" = EmployeeSalary."EmployeeId"
		 inner join main."Branch" b on b."Id" = EmployeeSalary."BranchId" 
		 inner join main."Departments" d on d."Id" = EmployeeSalary."DepartmentId"
		 inner join main."Designations" d2  on d2."Id"  = EmployeeSalary."PositionId"
		 inner join main."Grades" g on g."Id" = EmployeeSalary."GradeId"
		 inner join payroll."SalaryStructures" ss  on ss ."Id" = EmployeeSalary."SalaryStructureId" 
		 left join payroll."PaymentOptions" po  on po."PaymentOptionId"  = EmployeeSalary."PaymentOptionId" 
		 WHERE EmployeeSalary."IsDeleted" = False and EmployeeSalary."CompanyId"  = companyId
		and date(EmployeeSalary."StartDate") <= current_date and  date(EmployeeSalary."EndDate") >=current_date
	and e."IsDeleted" = false ;
	END;
$function$
;


--DROP FUNCTION payroll.getemployeecurrentsalarybycompany(uuid) 