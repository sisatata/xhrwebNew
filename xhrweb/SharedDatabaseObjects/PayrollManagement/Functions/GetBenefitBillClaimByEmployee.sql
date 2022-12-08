CREATE OR REPLACE FUNCTION payroll.getbenefitbillclaimbyemployee(employeeid uuid, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("Id" uuid, "BenefitDeductionId" uuid, "EmployeeId" uuid, "BillDate" timestamp without time zone, "ClaimDate" timestamp without time zone, "AllocatedAmount" numeric, "ClaimAmount" numeric, "Remarks" character varying, "Status" character varying, "ApprovedAmount" numeric, "ApprovedBy" uuid, "ApprovedDate" timestamp without time zone, "ApprovedNotes" character varying, "IsDeleted" boolean, "CompanyId" uuid, "ApprovalStatus" character varying, "BillAttachmentName" character varying, "LocationFrom" character varying, "LocationTo" character varying, "NameOfAttendees" character varying, "NumberOfAttendees" integer, "VehicleTypeId" uuid,"VehicleTypeName" character varying, "PaidBy" uuid, "PaidDate" timestamp without time zone, "PaymentStatus" integer, "SettledBy" uuid, "SettledDate" timestamp without time zone)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			BenefitBillClaim."Id" , 
			BenefitBillClaim."BenefitDeductionId" , 
			BenefitBillClaim."EmployeeId" , 
			BenefitBillClaim."BillDate" , 
			BenefitBillClaim."ClaimDate" , 
			BenefitBillClaim."AllocatedAmount" , 
			BenefitBillClaim."ClaimAmount" , 
			BenefitBillClaim."Remarks" , 
			BenefitBillClaim."Status" , 
			BenefitBillClaim."ApprovedAmount" , 
			BenefitBillClaim."ApprovedBy" , 
			BenefitBillClaim."ApprovedDate" , 
			BenefitBillClaim."ApprovedNotes" , 
			BenefitBillClaim."IsDeleted" , 
			BenefitBillClaim."CompanyId" ,
			BenefitBillClaim."ApprovalStatus",
     		BenefitBillClaim."BillAttachmentName",
     		BenefitBillClaim."LocationFrom" ,
			BenefitBillClaim."LocationTo" ,
			BenefitBillClaim."NameOfAttendees" ,
			BenefitBillClaim."NumberOfAttendees" ,
			BenefitBillClaim."VehicleTypeId" ,
			clut."LookUpTypeName"  "VehicleTypeName" ,
			BenefitBillClaim."PaidBy" ,
			BenefitBillClaim."PaidDate" ,
			BenefitBillClaim."PaymentStatus" ,
			BenefitBillClaim."SettledBy" ,
			BenefitBillClaim."SettledDate" 
		 FROM payroll."BenefitBillClaims" AS BenefitBillClaim 
		 left join main."CommonLookUpTypes" clut  on BenefitBillClaim."VehicleTypeId" = clut."Id" 
		 WHERE BenefitBillClaim."IsDeleted" = false and BenefitBillClaim."EmployeeId"  = employeeId 
		and ((startdate is null or BenefitBillClaim."BillDate" :: date >= startdate)
	and (enddate is null or BenefitBillClaim."BillDate" :: date <= enddate) or BenefitBillClaim."ApprovalStatus" = '1');
	END;
$function$
;

DROP FUNCTION payroll.getbenefitbillclaimbyemployee(uuid,timestamp without time zone,timestamp without time zone) ;

select * from main."CommonLookUpTypes" clut ;
select * from payroll.getbenefitbillclaimbyemployee('dc47523a-ed65-4bd9-9c0d-f3602f1870b5','2021-08-15','2021-09-15');