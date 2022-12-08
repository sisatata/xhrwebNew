--DROP FUNCTION getbenefitbillclaimbyid(uuid)
CREATE OR REPLACE FUNCTION payroll.getbenefitbillclaimbyid(id uuid)
 RETURNS TABLE("Id" uuid, "BenefitDeductionId" uuid, "EmployeeId" uuid, "ApplicantName" character varying, "BillDate" timestamp without time zone, "ClaimDate" timestamp without time zone, "AllocatedAmount" numeric, "ClaimAmount" numeric, "Remarks" character varying, "ApprovalStatus" character varying, "ApprovedAmount" numeric, "ApprovedBy" uuid, "ApprovedDate" timestamp without time zone, "ApprovedNotes" character varying, "IsDeleted" boolean, "CompanyId" uuid, "BillAttachmentName" character varying, "LocationFrom" character varying, "LocationTo" character varying, "NameOfAttendees" character varying, "NumberOfAttendees" integer, "VehicleTypeId" uuid,
  "PaidBy" uuid,"PaidDate"  timestamp without time zone, "PaymentStatus" int, "SettledBy" uuid ,"SettledDate" timestamp without time zone)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			BenefitBillClaim."Id" , 
			BenefitBillClaim."BenefitDeductionId" , 
			BenefitBillClaim."EmployeeId" , 
			e."FullName" "ApplicantName",
			BenefitBillClaim."BillDate" , 
			BenefitBillClaim."ClaimDate" , 
			BenefitBillClaim."AllocatedAmount" , 
			BenefitBillClaim."ClaimAmount" , 
			BenefitBillClaim."Remarks" , 
			BenefitBillClaim."ApprovalStatus" , 
			BenefitBillClaim."ApprovedAmount" , 
			bbcaq."ManagerId" as "ApprovedBy" , 
			BenefitBillClaim."ApprovedDate" , 
			BenefitBillClaim."ApprovedNotes" , 
			BenefitBillClaim."IsDeleted" , 
			BenefitBillClaim."CompanyId" ,
			BenefitBillClaim."BillAttachmentName" ,
			BenefitBillClaim."LocationFrom" ,
			BenefitBillClaim."LocationTo" ,
			BenefitBillClaim."NameOfAttendees" ,
			BenefitBillClaim."NumberOfAttendees" ,
			BenefitBillClaim."VehicleTypeId" ,
			BenefitBillClaim."PaidBy" ,
			BenefitBillClaim."PaidDate" ,
			BenefitBillClaim."PaymentStatus" ,
			BenefitBillClaim."SettledBy" ,
			BenefitBillClaim."SettledDate" 
		 FROM payroll."BenefitBillClaims" AS BenefitBillClaim 
		 inner join employee."Employees" e on BenefitBillClaim."EmployeeId" = e."Id" 
		 left join payroll."BenefitBillClaimApproveQueues" bbcaq on BenefitBillClaim."Id" = bbcaq ."BenefitBillClaimId" 
		 WHERE BenefitBillClaim."IsDeleted" = false and BenefitBillClaim."Id"  = id ;
	END;
$function$
;
