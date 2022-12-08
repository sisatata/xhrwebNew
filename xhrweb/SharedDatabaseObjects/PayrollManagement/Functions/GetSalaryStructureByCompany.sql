CREATE OR REPLACE FUNCTION payroll.GetSalaryStructureByCompany(companyId uuid)
RETURNS TABLE (
		"Id" uuid ,
		"StructureName" character varying(50) ,
		"Description" character varying(250) ,
		"StartDate" timestamp ,
		"EndDate" timestamp,
		"CompanyId" uuid
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			SalaryStructure."Id" , 
			SalaryStructure."StructureName" , 
			SalaryStructure."Description" , 
			SalaryStructure."StartDate" , 
			SalaryStructure."EndDate" ,  
			SalaryStructure."CompanyId" 
		 FROM payroll."SalaryStructures" AS SalaryStructure 
		 WHERE SalaryStructure."IsDeleted" = False and SalaryStructure."CompanyId" = companyId;
	END;
$BODY$
LANGUAGE plpgsql;

--select * from payroll.GetSalaryStructureByCompany('ab5aeca2-7a4a-4a20-bb96-383e72e839dc');