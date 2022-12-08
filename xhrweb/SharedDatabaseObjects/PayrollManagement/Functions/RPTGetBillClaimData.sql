CREATE OR REPLACE FUNCTION payroll.rptgetbillclaimdata(companyid uuid, employeeid uuid, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("EmployeeId" character varying, "DesignationOrder" bigint, "ApplicantName" character varying, "BillType" character varying, "BillDate" text, "ClaimDate" text, "AllocatedAmount" numeric, "ClaimAmount" numeric, "Remarks" character varying, "ApprovedAmount" numeric, "ApprovalStatus" text, "ApprovedDate" text, "Note" character varying, "ManagerName" character varying, "Branch" character varying, "Department" character varying, "Designation" character varying, "JoiningDate" timestamp without time zone, "CompanyName" character varying, "EmployeeSignature" text, "ManagerSignature" text, "ReportTitle" text, "PaidBy" character varying, "PaidDate" text, "PaymentStatus" integer, "SettledBy" character varying, "SettledDate" text,
 "LocationFrom" character varying,
	"LocationTo"  character varying,
	"VehicleTypeId" character varying,
	"NumberOfAttendees" int4,
	"NameOfAttendees"  character varying)
 LANGUAGE plpgsql
AS $function$ begin return QUERY
select
	app."EmployeeId" as "EmployeeId",
	des."SortOrder"  as "DesignationOrder",
	app."FullName" as "ApplicantName",
	bdc."Name" as "BillType",
	to_char( bill."BillDate", 'dd-Mon-yyyy') "BillDate",
	to_char(case when bill."ClaimDate" is null then bill."CreatedDate" else bill."ClaimDate" end , 'dd-Mon-yyyy') "ClaimDate",
	coalesce (bill."AllocatedAmount",
	0) "AllocatedAmount" ,
	coalesce (bill ."ClaimAmount",
	0) "ClaimAmount",
	bill."Remarks" ,
	coalesce (bill."ApprovedAmount" ,
	0) "ApprovedAmount" ,
	case
		when BenefitBillClaimApproveQueue."ApprovalStatus" = '1' then 'Pending'
		else
		case
			when BenefitBillClaimApproveQueue."ApprovalStatus" = '2' then 'InProgress'
			else
			case
				when BenefitBillClaimApproveQueue."ApprovalStatus" = '3' then 'Approved'
				else 'Declined'
			end
		end
	end "ApprovalStatus",
	to_char(BenefitBillClaimApproveQueue."ApprovedDate", 'dd-Mon-yyyy') "ApprovedDate" ,
	BenefitBillClaimApproveQueue."Note",
	man."FullName" as "ManagerName"
	,b."BranchName" as "Branch",
	d."DepartmentName" as "Department", 
	des."DesignationName" as "Designation",
	ee."JoiningDate" ,
	c."CompanyName" ,
   ee."EmployeeSignature" ,
   ee2."EmployeeSignature"  as "ManagerSignature",
   	--case when ca."AddressLine1" is null then 'No Address' else ca."AddressLine1" end as "CompanyAddress",
	'Voucher' as "ReportTitle",
	acc."FullName" "PaidBy" ,
	case when bill."PaidDate" is not null then to_char(bill."PaidDate" , 'dd-Mon-yyyy') else '' end "PaidDate",
	bill."PaymentStatus" ,
	setl."FullName" "SettledBy" ,
	case when bill."SettledDate" is not null then to_char(bill."SettledDate" , 'dd-Mon-yyyy') else '' end "SettledDate",
	bill."LocationFrom" ,
	bill."LocationTo" ,
	vh."LookUpTypeName" "VehicleTypeId",
	bill."NumberOfAttendees" ,
	bill."NameOfAttendees" 
from
	payroll."BenefitBillClaimApproveQueues" as BenefitBillClaimApproveQueue
inner join payroll."BenefitBillClaims" bill on
	bill ."Id" = BenefitBillClaimApproveQueue."BenefitBillClaimId"
inner join payroll."BenefitDeductionConfigs" bdc on
	bill."BenefitDeductionId" = bdc ."Id"
inner join employee."Employees" app on
	bill."EmployeeId" = app."Id"
inner join employee."Employees" man on
	man."Id" = BenefitBillClaimApproveQueue."ManagerId"
	inner join employee."EmployeeEnrollments" ee on app."Id" = ee."EmployeeId" 
	inner join main."Company" as c on app."CompanyId" = c."Id"
	inner join main."Branch" as b on app."BranchId" = b."Id"
	inner join main."Departments" as d on app."DepartmentId" = d."Id"
	inner join main."Designations" as des on app."PositionId" = des."Id"
	inner join employee."EmployeeEnrollments" ee2  on ee2."EmployeeId"  = BenefitBillClaimApproveQueue."ManagerId" 
	left  join employee."Employees" acc on bill."PaidBy" = acc."Id" 
	left  join employee."Employees" setl  on bill."SettledBy" = setl."Id" 
	left join main."CommonLookUpTypes" vh on bill."VehicleTypeId" = vh."Id" 
where
	bill."IsDeleted" = false
	and bill."CompanyId" = companyid
	and (startdate is null
	or bill."BillDate" :: date >= startdate ::date)
	and (enddate is null
	or bill."BillDate" ::date <= enddate ::date)
	and (employeeid is null or bill."EmployeeId" = employeeid)
order by
	bill."EmployeeId",
	bill."ClaimDate";
end;

$function$
;


--DROP FUNCTION payroll.rptgetbillclaimdata(uuid,uuid,timestamp without time zone,timestamp without time zone)

--select * from payroll.rptgetbillclaimdata('723f3be1-cc7e-490d-a7a5-dcc263d248a7', 'dc47523a-ed65-4bd9-9c0d-f3602f1870b5','2021-09-22', '2021-09-22');
