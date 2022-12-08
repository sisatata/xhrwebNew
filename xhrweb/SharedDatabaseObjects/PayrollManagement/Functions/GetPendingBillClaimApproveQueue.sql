CREATE OR REPLACE FUNCTION payroll.getpendingbillclaimapprovequeue(managerid uuid, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("Id" uuid, "BenefitBillClaimId" uuid, "ManagerId" uuid, "ApprovedAmount" numeric, "ApprovalStatus" character varying, "ApprovedDate" timestamp without time zone, "Notes" character varying, "ApplicantName" character varying, "BenefitDeductionId" uuid, "BillDate" timestamp without time zone, "ClaimDate" timestamp without time zone, "AllocatedAmount" numeric, "ClaimAmount" numeric, "Remarks" character varying, "ManagerName" character varying, "BillAttachmentName" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			BenefitBillClaimApproveQueue."Id" , 
			BenefitBillClaimApproveQueue."BenefitBillClaimId" , 
			BenefitBillClaimApproveQueue."ManagerId" , 
			BenefitBillClaimApproveQueue."ApprovedAmount" , 
			BenefitBillClaimApproveQueue."ApprovalStatus" , 
			BenefitBillClaimApproveQueue."ApprovedDate" , 
			BenefitBillClaimApproveQueue."Note" as "Notes" ,
			app."FullName"  ApplicantName,
			bill."BenefitDeductionId" ,
			bill."BillDate" ,
			bill."ClaimDate" ,
			bill."AllocatedAmount" ,
			bill ."ClaimAmount",
			bill."Remarks" ,
			man."FullName" ManagerName,
			bill."BillAttachmentName" 
			FROM payroll."BenefitBillClaimApproveQueues" AS BenefitBillClaimApproveQueue 
		 inner join payroll."BenefitBillClaims" bill on bill ."Id" = BenefitBillClaimApproveQueue."BenefitBillClaimId"
		 inner  join employee."Employees"  app on bill."EmployeeId" = app."Id" 
		 inner join employee."Employees" man on man."Id" = BenefitBillClaimApproveQueue."ManagerId"
		 		 WHERE BenefitBillClaimApproveQueue."ManagerId" = managerId
		 		and (((startdate is null or bill."BillDate" ::date >= startdate)
				and (enddate is null or bill."BillDate" ::date <= enddate) )
				OR ((startdate is null or bill."BillDate" ::date >= startdate)
				and (enddate is null or bill."BillDate" ::date <= enddate) or bill."ApprovalStatus" = '1'))
				 order by bill."ApprovalStatus", bill."ClaimDate";
	END;
$function$
;


--DROP FUNCTION payroll.getpendingbillclaimapprovequeue(uuid,timestamp without time zone,timestamp without time zone)