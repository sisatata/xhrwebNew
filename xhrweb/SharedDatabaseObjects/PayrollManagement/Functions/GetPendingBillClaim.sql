CREATE OR REPLACE FUNCTION payroll.getpendingbillclaim(managerid uuid, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("Id" uuid, "BenefitBillClaimId" uuid, "ManagerId" uuid, "ApprovedAmount" numeric, "ApprovalStatus" character varying, "ApprovedDate" timestamp without time zone, "Notes" character varying, "ApplicantName" character varying, "BenefitDeductionId" uuid, "BillDate" timestamp without time zone, "ClaimDate" timestamp without time zone, "AllocatedAmount" numeric, "ClaimAmount" numeric, "Remarks" character varying, "ManagerName" character varying, "BillAttachmentName" character varying, "BranchId" uuid, "BranchName" character varying, "PositionId" uuid, "DepartmentId" uuid, "CompanyEmployeeId" character varying, "DepartmentName" character varying, "DesignationName" character varying, "LoginId" character varying, "LocationFrom" character varying, "LocationTo" character varying, "NameOfAttendees" character varying, "NumberOfAttendees" integer, "VehicleTypeId" uuid,"VehicleTypeName" character varying, "PaidBy" uuid, "PaidDate" timestamp without time zone, "PaymentStatus" integer, "SettledBy" uuid, "SettledDate" timestamp without time zone)
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
			app."FullName" as "ApplicantName",
			bill."BenefitDeductionId" ,
			bill."BillDate" ,
			bill."ClaimDate" ,
			bill."AllocatedAmount" ,
			bill ."ClaimAmount",
			bill."Remarks" ,
			man."FullName" ManagerName,
			bill."BillAttachmentName" ,
			app."BranchId" ,
			b."BranchName" ,
			app."PositionId" ,
			app."DepartmentId" ,
			app."EmployeeId" as "CompanyEmployeeId" ,
			d."DepartmentName" ,
			d2."DesignationName" ,
			anu."Email"  as "LoginId",
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
		 inner join main."Branch" b on  app."BranchId"  = b."Id" 
		 inner join main."Departments" d  on app."DepartmentId"  = d."Id" 
		 inner join main."Designations" d2 on app."PositionId"  = d2."Id" 
		 inner join "access"."AspNetUsers" anu  on app."LoginId" :: text = anu."Id" 
		 left join main."CommonLookUpTypes" clut  on bill."VehicleTypeId" = clut."Id" 
		 WHERE BenefitBillClaimApproveQueue."ManagerId" = managerId
		 and b."IsDeleted"  = false and d."IsDeleted"  = false and d2."IsDeleted"  = false and app."IsDeleted" =false
		 and (((startdate is null or bill."BillDate" ::date >= startdate)
		 and (enddate is null or bill."BillDate" ::date <= enddate) )
		 OR ((startdate is null or bill."BillDate" ::date >= startdate)
		 and (enddate is null or bill."BillDate" ::date <= enddate) or bill."ApprovalStatus" = '1'))
		 order by bill."ApprovalStatus", bill."ClaimDate";
	END;
$function$
;


 --DROP FUNCTION payroll.getpendingbillclaim(uuid,timestamp without time zone,timestamp without time zone)