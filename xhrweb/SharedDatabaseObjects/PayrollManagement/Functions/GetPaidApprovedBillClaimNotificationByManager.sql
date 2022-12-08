CREATE OR REPLACE FUNCTION payroll.getpaidapprovedbillclaimnotificationbymanager(managerid uuid, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("Id" uuid, "ApprovedManagerId" uuid, "ApprovedAmount" numeric, "ApprovalStatus" character varying, "ApprovedDate" timestamp without time zone, "Notes" character varying, "ApplicantName" character varying, "BenefitDeductionId" uuid, "BillDate" timestamp without time zone, "ClaimDate" timestamp without time zone, "AllocatedAmount" numeric, "ClaimAmount" numeric, "Remarks" character varying, "ApprovedManagerName" character varying, "BillAttachmentName" character varying, "AccountManageId" uuid,  "AccountManageName" character varying, paymentstatus integer, "LocationFrom" character varying, "LocationTo" character varying, "NameOfAttendees" character varying, "NumberOfAttendees" integer, "VehicleTypeId" uuid, "VehicleTypeName" character varying, "PaidBy" uuid, "PaidDate" timestamp without time zone, "PaymentStatus" integer, "SettledBy" uuid, "SettledDate" timestamp without time zone)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
		    bill."Id" ,
			BenefitBillClaimApproveQueue."ManagerId" "ApprovedManagerId", 
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
			man."FullName" "ApprovedManagerName",
			bill."BillAttachmentName" ,
			bill."PaidBy" "AccountManageId",
			acc."FullName" "AccountManageName",
			bill."PaymentStatus" ,
			bill."LocationFrom" ,
			bill."LocationTo" ,
			bill."NameOfAttendees" ,
			bill."NumberOfAttendees" ,
			bill."VehicleTypeId" ,
			clut."LookUpTypeName"  "VehicleTypeName" ,
			bill."PaidBy" ,
			bill."PaidDate" ,
			bill."PaymentStatus" ,
			bill."SettledBy" ,
			bill."SettledDate" 
			FROM payroll."BenefitBillClaimApproveQueues" AS BenefitBillClaimApproveQueue 
		 inner join payroll."BenefitBillClaims" bill on bill ."Id" = BenefitBillClaimApproveQueue."BenefitBillClaimId"
		 inner  join employee."Employees"  app on bill."EmployeeId" = app."Id" 
		 inner join employee."Employees" man on man."Id" = BenefitBillClaimApproveQueue."ManagerId"
		 inner join employee."Employees" acc on acc."Id" = bill."PaidBy"
		 left join main."CommonLookUpTypes" clut  on bill."VehicleTypeId" = clut."Id" 
	WHERE bill."PaidBy" = managerid and bill."ApprovalStatus" = '3' and bill."PaymentStatus" >= 3
		 		and (((startdate is null or bill."BillDate" ::date >= startdate)
				and (enddate is null or bill."BillDate" ::date <= enddate) )
				OR ((startdate is null or bill."BillDate" ::date >= startdate)
				and (enddate is null or bill."BillDate" ::date <= enddate)))
				order by bill."PaymentStatus", bill."ClaimDate";
	END;
$function$
;

DROP FUNCTION getpaidapprovedbillclaimnotificationbymanager(uuid,timestamp without time zone,timestamp without time zone) ;
