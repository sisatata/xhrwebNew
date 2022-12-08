CREATE OR REPLACE FUNCTION employee.getemployeecardbyemployee(employeeid uuid)
 RETURNS TABLE("Id" uuid, "EmployeeId" uuid, "CardMaskingValue" character varying, "IssueDate" timestamp without time zone, "ChargeAmount" numeric, "IsPaid" bool, "PaymentDate" timestamp without time zone, "CardRevoked" bool, "CardRevokedDate" timestamp without time zone )
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
		   EmployeeCard."Id", 
		   EmployeeCard."EmployeeId" ,
		   EmployeeCard."CardMaskingValue" ,
		   EmployeeCard."IssueDate" ,
		   EmployeeCard."ChargeAmount" ,
		   EmployeeCard."IsPaid" ,
		   EmployeeCard."PaymentDate" ,
		   EmployeeCard."CardRevoked" ,
		   EmployeeCard."CardRevokedDate" 
		 FROM employee."EmployeeCards" EmployeeCard
		 WHERE EmployeeCard."IsDeleted"  = false and EmployeeCard."EmployeeId"  = employeeid;
	END;
$function$
;