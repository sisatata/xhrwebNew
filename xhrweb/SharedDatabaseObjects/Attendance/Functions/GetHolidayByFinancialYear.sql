CREATE OR REPLACE FUNCTION attendance.getholidaybyfinancialyear(companyid uuid, financialyear character varying)
 RETURNS TABLE("Id" uuid, "HolidayDate" timestamp without time zone, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "Reason" character varying, "ReasonLocalized" character varying, "CompanyId" uuid)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			Holiday."Id" , 
			Holiday."HolidayDate" , 
			Holiday."StartDate", 
			Holiday."EndDate",
			Holiday."Reason" , 
			Holiday."ReasonLocalized" , 
			Holiday."CompanyId"
		 FROM attendance."Holidays" AS Holiday 
		 WHERE Holiday."IsDeleted" = False and Holiday."CompanyId" = companyId
		 and (extract ( year from Holiday."StartDate")  = cast(financialyear as double precision)   or extract ( year from Holiday."HolidayDate")  = cast(financialyear as double precision)) ;
	END;
$function$
;
