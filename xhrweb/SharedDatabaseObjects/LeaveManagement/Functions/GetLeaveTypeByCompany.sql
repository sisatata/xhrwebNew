CREATE OR REPLACE FUNCTION leave.getleavetypebycompany(compnayid uuid)
 RETURNS TABLE("Id" uuid, "CompanyId" uuid, "CompanyName" character varying, "LeaveTypeName" character varying, "LeaveTypeShortName" character varying, "LeaveTypeLocalizedName" character varying, "Balance" integer, "IsCarryForward" boolean, "IsFemaleSpecific" boolean, "IsPaid" boolean, "IsEncashable" boolean, "EncashReserveBalance" integer, "IsDependOnDateOfConfirmation" boolean, "IsLeaveDaysFixed" boolean, "IsBasedWorkingDays" boolean, "ConsecutiveLimit" integer, "IsAllowAdvanceLeaveApply" boolean, "IsAllowWithPrecedingHoliday" boolean, "IsAllowOpeningBalance" boolean, "IsPreventPostLeaveApply" boolean, "LeaveTypeGroupId" int4 ,"LeaveTypeGroupName" character varying)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
	select lt."Id", c."Id" as "CompanyId", c."CompanyName", lt."LeaveTypeName",
         lt."LeaveTypeShortName", lt."LeaveTypeLocalizedName", lt."Balance",
		 lt."IsCarryForward",lt."IsFemaleSpecific",lt."IsPaid",lt."IsEncashable",lt."EncashReserveBalance",
		 lt."IsDependOnDateOfConfirmation",lt."IsLeaveDaysFixed",lt."IsBasedWorkingDays",lt."ConsecutiveLimit",
		 lt."IsAllowAdvanceLeaveApply",lt."IsAllowWithPrecedingHoliday",lt."IsAllowOpeningBalance",lt."IsPreventPostLeaveApply"
		 ,lt."LeaveTypeGroupId" ,
		 ltg."LeaveTypeGroupName" 
	from leave."LeaveTypes" as lt
    inner join main."Company" as c on lt."CompanyId" = c."Id"
    inner join leave."LeaveTypeGroups" ltg on lt."LeaveTypeGroupId" = ltg."Id" 
    where lt."CompanyId" = compnayId and lt."IsDeleted"=false;
	
END;
$function$
;

--DROP FUNCTION leave.getleavetypebycompany(uuid) ;