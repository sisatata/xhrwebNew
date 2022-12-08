CREATE OR REPLACE FUNCTION payroll.GetSalaryStructureComponentByStructureId(structureId uuid)
RETURNS TABLE (
		"Id" uuid ,
		"SalaryStrutureId" uuid ,
		"SalaryComponentName" character varying(50) ,
		"Value" numeric ,
		"ValueType" character varying(5) ,
		"PercentOn" character varying(50) ,
		"CompanyId" uuid ,
		"SortOrder" int2
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			ssc."Id" , 
			ssc."SalaryStrutureId" , 
			ssc."SalaryComponentName" , 
			ssc."Value" , 
			ssc."ValueType" , 
			ssc."PercentOn" , 
			ssc."CompanyId" ,
			ssc."SortOrder" 
		 FROM payroll."SalaryStructureComponents" AS ssc 
		 WHERE ssc."IsDeleted" = false and ssc."SalaryStrutureId" = structureId order by ssc."ValueType" desc , ssc."SortOrder" ;
	END;
$BODY$
LANGUAGE plpgsql;


--select  * from payroll.GetSalaryStructureComponentByStructureId('5f55ef94-a8d7-49b0-a70d-d2fc264a53d2');

--DROP FUNCTION payroll.getsalarystructurecomponentbystructureid(uuid) 
