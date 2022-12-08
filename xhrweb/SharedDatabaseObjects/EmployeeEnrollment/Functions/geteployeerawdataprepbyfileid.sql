CREATE OR REPLACE FUNCTION employee.geteployeerawdataprepbyfileid(fileid uuid)
 RETURNS TABLE("Status" character varying, "ErrorDescription" character varying, "PlanetEmployeeID" uuid, "EmployeeCode" character varying, "FullName" character varying, "Company" character varying, "Branch" character varying, "Department" character varying, "Position" character varying, "JoiningDate" character varying, "ConfirmationRuleName" character varying, "PermanentDate" character varying, "Grade" character varying, "LeaveTypeGroupName" character varying, "Gross" character varying, "SalaryStructureName" character varying, "PaymentOptionName" character varying, "BankName" character varying, "BankAccount" character varying, "NameofFather" character varying, "NameofSpouce" character varying, "NameofMother" character varying, "DOB" character varying, "NID" character varying, "BID" character varying, "Emailaddress" character varying, "MobileNo" character varying, "EmergencyContact" character varying, "BloodGroup" character varying, "Gender" character varying, "MaritalStatus" character varying, "Religion" character varying, "Nationality" character varying, "PermanentCountry" character varying, "PermanentDistrict" character varying, "PermanentThana" character varying, "PermanentPostOffice" character varying, "PermanentVillage" character varying, "PermanentAddressLine1" character varying, "PermanentAddressLine2" character varying, "PresentCountry" character varying, "PresentDistrict" character varying, "PresentThana" character varying, "PresentPostOffice" character varying, "PresentVillage" character varying, "PresentAddressLine1" character varying, "PresentAddressLine2" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
EployeeRawDataPrep."Status" ,
EployeeRawDataPrep."ErrorDescription" ,
		    EployeeRawDataPrep."PlanetEmployeeID" ,
			EployeeRawDataPrep."EmployeeCode" ,
			EployeeRawDataPrep."FullName" ,
			EployeeRawDataPrep."Company" ,
			EployeeRawDataPrep."Branch" ,
			EployeeRawDataPrep."Department" ,
			EployeeRawDataPrep."Position" ,
			EployeeRawDataPrep."JoiningDate" ,
			EployeeRawDataPrep."ConfirmationRuleName" ,
			EployeeRawDataPrep."PermanentDate" ,
			EployeeRawDataPrep."Grade",
			EployeeRawDataPrep."LeaveTypeGroupName",
			EployeeRawDataPrep."Gross" ,
			EployeeRawDataPrep."SalaryStructureName" ,
			EployeeRawDataPrep."PaymentOptionName" ,
			EployeeRawDataPrep."BankName" ,
			EployeeRawDataPrep."BankAccount" ,
			EployeeRawDataPrep."NameofFather" ,
			EployeeRawDataPrep."NameofSpouce" ,
			EployeeRawDataPrep."NameofMother" ,
			EployeeRawDataPrep."DOB" ,
			EployeeRawDataPrep."NID" ,
			EployeeRawDataPrep."BID" ,
			EployeeRawDataPrep."Emailaddress" ,
			EployeeRawDataPrep."MobileNo" ,
			EployeeRawDataPrep."EmergencyContact" ,
			EployeeRawDataPrep."BloodGroup" ,
			EployeeRawDataPrep."Gender" ,
			EployeeRawDataPrep."MaritalStatus" ,
			EployeeRawDataPrep."Religion" ,
			EployeeRawDataPrep."Nationality" ,
			EployeeRawDataPrep."PermanentCountry" ,
			EployeeRawDataPrep."PermanentDistrict" ,
			EployeeRawDataPrep."PermanentThana" ,
			EployeeRawDataPrep."PermanentPostOffice" ,
			EployeeRawDataPrep."PermanentVillage" ,
			EployeeRawDataPrep."PermanentAddressLine1" ,
			EployeeRawDataPrep."PermanentAddressLine2" ,
			EployeeRawDataPrep."PresentCountry" ,
			EployeeRawDataPrep."PresentDistrict" ,
			EployeeRawDataPrep."PresentThana" ,
			EployeeRawDataPrep."PresentPostOffice" ,
			EployeeRawDataPrep."PresentVillage" ,
			EployeeRawDataPrep."PresentAddressLine1" ,
			EployeeRawDataPrep."PresentAddressLine2" 
		 FROM employee."EmployeeRawDataPreps"  AS EployeeRawDataPrep 
		  where EployeeRawDataPrep."RawFileDetailId"= fileId;
END;
$function$
;


--select * from employee.GetEployeeRawDataPrepByFileId('e102f055-aba6-499d-a513-c1a4212548d4');