CREATE OR REPLACE FUNCTION employee.getemployeepromotiontransferbycompanyid(companyid uuid)
 RETURNS TABLE("Id" uuid, "EmployeeId" uuid, "FullName" character varying, "CompanyEmployeeId" character varying, "PreviousCompanyName" character varying, "PreviousBranchName" character varying, "PreviousDepartmentName" character varying, "PreviousPositionName" character varying, "PreviousGradeName" character varying, "PreviousStructureName" character varying, "PreviousOptionName" character varying, "NewCompanyName" character varying, "NewBranchName" character varying, "NewDepartmentName" character varying, "NewPositionName" character varying, "NewGradeName" character varying, "NewStructureName" character varying, "NewOptionName" character varying, "PreviousCompanyId" uuid, "PreviousBranchId" uuid, "PreviousDepartmentId" uuid, "PreviousPositionId" uuid, "PreviousGradeId" uuid, "PreviousSalaryStructureId" uuid, "PreviousPaymentOptionId" smallint, "NewCompanyId" uuid, "NewBranchId" uuid, "NewDepartmentId" uuid, "NewPositionId" uuid, "NewGradeId" uuid, "NewSalaryStructureId" uuid, "NewPaymentOptionId" smallint, "ProposedDate" timestamp without time zone, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "PreviousGross" numeric, "NewGross" numeric, "PreviousBasic" numeric, "NewBasic" numeric, "PreviousHouserent" numeric, "NewHouserent" numeric, "IncrementTypeId" integer, "IncrementValueTypeId" integer, "IncrementValue" numeric, "IncrementAmount" numeric, "IncrementOn" integer, "Reason" character varying, "Reference" character varying, "ApproveDate" timestamp without time zone, "ApproveBy" uuid, "ApproveNote" character varying, "ApprovalStatus" character varying)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
		SELECT 
			EmployeePromotionTransfer."Id" , 
			EmployeePromotionTransfer."EmployeeId" , 
			e."FullName" ,
			e."EmployeeId" as "CompanyEmployeeId",
			c."CompanyName" as "PreviousCompanyName" ,
			b."BranchName"  as "PreviousBranchName" ,
			d."DepartmentName" as "PreviousDepartmentName" ,
			d2."DesignationName" as "PreviousPositionName" ,
			g."GradeName" as "PreviousGradeName",
			ss."StructureName"   as "PreviousStructureName",
			po."OptionName"   as "PreviousOptionName",
			c2."CompanyName" as "NewCompanyName" ,
			b2."BranchName"  as "NewBranchName" ,
			d3."DepartmentName" as "NewDepartmentName" ,
			d4."DesignationName" as "NewPositionName" ,
			g2."GradeName" as "NewGradeName",
			ss2."StructureName" as "NewStructureName",
			po2."OptionName"  as "NewOptionName",
			EmployeePromotionTransfer."PreviousCompanyId" , 
			EmployeePromotionTransfer."PreviousBranchId" , 
			EmployeePromotionTransfer."PreviousDepartmentId" , 
			EmployeePromotionTransfer."PreviousPositionId" , 
			EmployeePromotionTransfer."PreviousGradeId" ,
			EmployeePromotionTransfer."PreviousSalaryStructureId",
			EmployeePromotionTransfer."PreviousPaymentOptionId" ,
			EmployeePromotionTransfer."NewCompanyId" , 
			EmployeePromotionTransfer."NewBranchId" , 
			EmployeePromotionTransfer."NewDepartmentId" , 
			EmployeePromotionTransfer."NewPositionId" , 
			EmployeePromotionTransfer."NewGradeId" ,
			EmployeePromotionTransfer."NewSalaryStructureId" ,
			EmployeePromotionTransfer."NewPaymentOptionId" ,
			EmployeePromotionTransfer."ProposedDate" , 
			EmployeePromotionTransfer."StartDate" , 
			EmployeePromotionTransfer."EndDate" , 
			EmployeePromotionTransfer."PreviousGross" , 
			EmployeePromotionTransfer."NewGross" , 
			EmployeePromotionTransfer."PreviousBasic" , 
			EmployeePromotionTransfer."NewBasic" , 
			EmployeePromotionTransfer."PreviousHouserent" , 
			EmployeePromotionTransfer."NewHouserent" , 
			EmployeePromotionTransfer."IncrementTypeId" , 
			EmployeePromotionTransfer."IncrementValueTypeId" ,
			EmployeePromotionTransfer."IncrementValue" , 
			EmployeePromotionTransfer."IncrementAmount" , 
			EmployeePromotionTransfer."IncrementOn" , 
			EmployeePromotionTransfer."Reason" , 
			EmployeePromotionTransfer."Reference" , 
			EmployeePromotionTransfer."ApproveDate" , 
			EmployeePromotionTransfer."ApproveBy" , 
			EmployeePromotionTransfer."ApproveNote" , 
			EmployeePromotionTransfer."ApprovalStatus" 
		 FROM employee."EmployeePromotionTransfers" AS EmployeePromotionTransfer 
		 inner join employee."Employees" e on EmployeePromotionTransfer."EmployeeId" = e."Id" 
		 inner join main."Company" c on EmployeePromotionTransfer."PreviousCompanyId" = c."Id" 
		 inner join main."Branch" b on EmployeePromotionTransfer."PreviousBranchId" = b."Id" 
		 inner join main."Departments" d on EmployeePromotionTransfer."PreviousDepartmentId" = d."Id" 
		 inner join main."Designations" d2 on EmployeePromotionTransfer."PreviousPositionId" = d2."Id" 
		 inner join main."Grades" g on EmployeePromotionTransfer."PreviousGradeId" = g."Id" 
		 inner join payroll."SalaryStructures" ss on EmployeePromotionTransfer."PreviousSalaryStructureId" = ss."Id" 
		inner join payroll."PaymentOptions" po on EmployeePromotionTransfer."PreviousPaymentOptionId" = po."PaymentOptionId" 
		 left  join main."Company" c2 on EmployeePromotionTransfer."NewCompanyId" = c2."Id" 
		 left join main."Branch" b2 on EmployeePromotionTransfer."NewBranchId" = b2."Id" 
		 left join main."Departments" d3 on EmployeePromotionTransfer."NewDepartmentId" = d3."Id" 
		 left join main."Designations" d4 on EmployeePromotionTransfer."NewPositionId" = d4."Id" 
		 left join main."Grades" g2 on EmployeePromotionTransfer."NewGradeId" = g2."Id" 
		 left join payroll."PaymentOptions" po2 on EmployeePromotionTransfer."NewPaymentOptionId" = po2."PaymentOptionId" 
		 left join payroll."SalaryStructures" ss2 on EmployeePromotionTransfer."NewSalaryStructureId" = ss2."Id" 
		 WHERE EmployeePromotionTransfer."PreviousCompanyId" = companyid  and  
		 c."IsDeleted" = false and  b."IsDeleted" = false and 
		 d."IsDeleted" = false and d2."IsDeleted" = false 
		 and g."IsDeleted"  = false and ss."IsDeleted"  = false
		 and  po."IsDeleted"  = false and
		
		EmployeePromotionTransfer."IsDeleted" = false and e."IsDeleted" = false;
	END;
$function$
;
