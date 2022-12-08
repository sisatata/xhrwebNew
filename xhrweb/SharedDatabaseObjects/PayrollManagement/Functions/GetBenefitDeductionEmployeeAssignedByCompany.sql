CREATE OR REPLACE FUNCTION payroll.getbenefitdeductionemployeeassignedbycompany(companyid uuid)
 RETURNS TABLE("Id" uuid, "BenefitDeductionId" uuid, "EmployeeId" uuid, "Remarks" character varying, "IsDeleted" boolean, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "Amount" numeric, "BenefitDeductionCode" character varying, "Name" character varying,
 "EmployeeName" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			bdea."Id" , 
			bdea."BenefitDeductionId" , 
			bdea."EmployeeId" , 
			bdea."Remarks" , 
			bdea."IsDeleted" , 
			bdea."StartDate" , 
			bdea."EndDate" , 
			bdea."Amount" ,
			bdc."BenefitDeductionCode" ,
			bdc."Name" ,
			e."FullName" as "EmployeeName"
		 FROM payroll."BenefitDeductionEmployeeAssigneds" AS bdea inner join payroll."BenefitDeductionConfigs" bdc on 
		 bdea."BenefitDeductionId" = bdc."Id" 
		 inner join employee."Employees" e on bdea."EmployeeId" = e."Id" 
		 WHERE bdea."IsDeleted" = False and bdc."CompanyId" = companyid;
	END;
$function$
;



