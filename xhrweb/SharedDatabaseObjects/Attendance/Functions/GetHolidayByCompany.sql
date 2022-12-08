CREATE OR REPLACE FUNCTION attendance.GetHolidayByCompany(companyId uuid )
RETURNS TABLE (
		"Id" uuid ,
		"HolidayDate" timestamp(6) ,
		"Reason" character varying(100) ,
		"ReasonLocalized" character varying(100) ,
		"CompanyId" uuid
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			Holiday."Id" , 
			Holiday."HolidayDate" , 
			Holiday."Reason" , 
			Holiday."ReasonLocalized" , 
			Holiday."CompanyId"
		 FROM attendance."Holidays" AS Holiday 
		 WHERE Holiday."IsDeleted" = False and Holiday."CompanyId" = companyId;
	END;
$BODY$
LANGUAGE plpgsql;