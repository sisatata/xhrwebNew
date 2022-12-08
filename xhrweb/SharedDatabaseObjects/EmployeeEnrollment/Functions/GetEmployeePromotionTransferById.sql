CREATE OR REPLACE FUNCTION employee.getemployeepromotiontransferbyid(id uuid)
 RETURNS TABLE("Id" uuid, "EmployeeId" uuid, "PreviousCompanyId" uuid, "PreviousBranchId" uuid, "PreviousDepartmentId" uuid, "PreviousPositionId" uuid, "PreviousGradeId" uuid, "PreviousSalaryStructureId" uuid, "PreviousPaymentOptionId" smallint, "NewCompanyId" uuid, "NewBranchId" uuid, "NewDepartmentId" uuid, "NewPositionId" uuid, "NewGradeId" uuid, "NewSalaryStructureId" uuid, "NewPaymentOptionId" smallint, "ProposedDate" timestamp without time zone, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "PreviousGross" numeric, "NewGross" numeric, "PreviousBasic" numeric, "NewBasic" numeric, "PreviousHouserent" numeric, "NewHouserent" numeric, "IncrementTypeId" integer, "IncrementValueTypeId" integer, "IncrementValue" numeric, "IncrementAmount" numeric, "IncrementOn" integer, "Reason" character varying, "Reference" character varying, "ApproveDate" timestamp without time zone, "ApproveBy" uuid, "ApproveNote" character varying, "ApprovalStatus" character varying)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
		SELECT 
			EmployeePromotionTransfer."Id" , 
			EmployeePromotionTransfer."EmployeeId" , 
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
		
		 WHERE EmployeePromotionTransfer."Id" = id  and  
		 
		EmployeePromotionTransfer."IsDeleted" = false ;
	END;
$function$
;
