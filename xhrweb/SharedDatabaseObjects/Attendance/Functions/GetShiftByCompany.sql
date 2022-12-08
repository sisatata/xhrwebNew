CREATE OR REPLACE FUNCTION "attendance".GetShiftByCompany(compnayId uuid)
RETURNS TABLE (
       "Id" uuid,
        "CompanyId" uuid,
        "CompanyName" character varying(250),
		"ShiftGroupId" uuid,
        "ShiftGroupName" character varying(250),
        "ShiftName" character varying(250),
	    "ShiftLocalizedName" character varying(250),
	    "ShiftCode"  Text,
	    "ShiftIn"  timestamp without time zone,
	    "ShiftOut"  timestamp without time zone,
		"ShiftLate"  timestamp without time zone,
		 "LunchBreakIn"  timestamp without time zone,
		 "LunchBreakOut"  timestamp without time zone,
		 "EarlyOut"  timestamp without time zone,
		 "RegHour"  timestamp without time zone,
		 "RamadanIn"  timestamp without time zone,
		 "RamadanOut"  timestamp without time zone,
		 "RamadanLate"  timestamp without time zone,
		 "RamadanEarlyOut"  timestamp without time zone,
		 "RamadanLunchBreakIn"  timestamp without time zone,
		 "RamadanLunchBreakOut"  timestamp without time zone,
		 "StartTime"   text,
	     "EndTime"   text,
	     "GraceIn" int ,
	     "GraceOut" int ,
	     "Range" int ,
	     "IsRollingShift" boolean,
	      "IsRelaborShift" boolean,
	      "IsActive" boolean
) 
AS $$
BEGIN
	RETURN QUERY
	select s."Id", c."Id" as "CompanyId", c."CompanyName"
            ,sg."Id" as "ShiftGroupId" ,sg."ShiftGroupName"
           ,s."ShiftName",s."ShiftLocalizedName",s."ShiftCode",s."ShiftIn",s."ShiftOut",s."ShiftLate",s."LunchBreakIn"
		   ,s."LunchBreakOut",s."EarlyOut",s."RegHour",s."RamadanIn",s."RamadanOut",s."RamadanLate",s."RamadanEarlyOut"
		   ,s."RamadanLunchBreakIn",s."RamadanLunchBreakOut",s."StartTime",s."EndTime",s."GraceIn",s."GraceOut",s."Range"
		   ,s."IsRollingShift",s."IsRelaborShift",s."IsActive"
	from  attendance."Shifts" as s
	INNER JOIN attendance."ShiftGroups" as sg ON sg."Id"=s."ShiftGroupID"
    INNER JOIN main."Company" as c on s."CompanyId" = c."Id"
    where s."IsDeleted" = False and s."CompanyId" = compnayId;
	
END;
$$
LANGUAGE plpgsql;