CREATE OR REPLACE FUNCTION leave.getleavetypebyemployee(employeeid uuid)
 RETURNS TABLE("Id" uuid, "CompanyId" uuid,  "LeaveTypeName" character varying, "LeaveTypeShortName" character varying, "LeaveTypeLocalizedName" character varying, "Balance" integer, "IsCarryForward" boolean, "IsFemaleSpecific" boolean, "IsPaid" boolean, "IsEncashable" boolean, "EncashReserveBalance" integer, "IsDependOnDateOfConfirmation" boolean, "IsLeaveDaysFixed" boolean, "IsBasedWorkingDays" boolean, "ConsecutiveLimit" integer, "IsAllowAdvanceLeaveApply" boolean, "IsAllowWithPrecedingHoliday" boolean, "IsAllowOpeningBalance" boolean, "IsPreventPostLeaveApply" boolean)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
	select lt."Id", lt."CompanyId" as "CompanyId",  lt."LeaveTypeName",
         lt."LeaveTypeShortName", lt."LeaveTypeLocalizedName", lt."Balance",
		 lt."IsCarryForward",lt."IsFemaleSpecific",lt."IsPaid",lt."IsEncashable",lt."EncashReserveBalance",
		 lt."IsDependOnDateOfConfirmation",lt."IsLeaveDaysFixed",lt."IsBasedWorkingDays",lt."ConsecutiveLimit",
		 lt."IsAllowAdvanceLeaveApply",lt."IsAllowWithPrecedingHoliday",lt."IsAllowOpeningBalance",lt."IsPreventPostLeaveApply"
	from leave."LeaveTypes" as lt
	inner join leave."EmployeeWiseCustomConfigurations" ewcc  on lt."CompanyId" = ewcc."CompanyId" 
	and ewcc ."Code" ='LeaveGroup'
	inner join leave."LeaveTypeGroups" ltg  on lt."CompanyId" = ltg."CompanyId" and ltg."LeaveTypeGroupName" = ewcc."Value" 
	and ltg."Id" = lt."LeaveTypeGroupId"
    where ewcc ."EmployeeId" = employeeid and lt."IsDeleted" = false;
	
END;
$function$
;


--DROP FUNCTION leave.getleavetypebyemployee(uuid)