CREATE OR REPLACE FUNCTION payroll.rptgetbonussheetbyemployee(companyid uuid, financialyearid uuid, bonustypeid uuid, employeeid uuid)
 RETURNS TABLE("EmployeeId" character varying, "FullName"  character varying, "CompanyName" character varying, "JoiningDate" text, "FinancialYearName" character varying, "DepartmentName" character varying, "DesignationName" character varying, "GradeName" character varying, "Basic" numeric, "HouseRent" numeric, "Medical" numeric, "Conveyance" numeric, "Food" numeric, "PayableBonus" numeric, "GrossSalary" numeric, "LookUpTypeName" character varying, "Remarks" character varying)
 LANGUAGE plpgsql
AS $function$
	begin   
	RETURN QUERY
		
			select 
			    e."EmployeeId",
			    e."FullName",
			    c."CompanyName" ,
			    to_char(ee."JoiningDate",'dd-Mon-yyyy') as "JoiningDate",
			    fy."FinancialYearName",
			    d."DepartmentName",
			    d2."DesignationName",
			    g."GradeName",
			    ebpd."Basic",
			    ebpd."HouseRent",
			    ebpd."Medical",
			    ebpd."Conveyance",
			    ebpd."Food",
			   ebpd."NetPayableBonus" as "PayableBonus",
			    ebpd."GrossSalary" ,
			    clut."LookUpTypeName", 
			    ebpd ."Remarks" 
			from 
			payroll."EmployeeBonusProcessedDatas" ebpd 
			inner join employee."Employees" e on ebpd."EmployeeId"  = e."Id" 
			inner join employee."EmployeeEnrollments" ee on ebpd."EmployeeId"  = ee."EmployeeId" 
			inner join main."Company" c  on ebpd."CompanyId"  = c."Id" 
			inner join main."Departments" d on ebpd."DepartmentId" = d."Id" 
			inner join main."FinancialYears" fy on ebpd."FinancialYearId" = fy."Id" 
			inner join main."Designations" d2 on ebpd."PositionId" = d2."Id" 
			inner join main."Grades" g on ebpd."GradeId"  = g."Id" 
			inner join main."CommonLookUpTypes" clut on ebpd."BonusTypeId" = clut."Id" 
			where ebpd."CompanyId"  = companyid 
			and ebpd."FinancialYearId"  = financialyearid
			and ebpd."EmployeeId"  = employeeid
			and ebpd."BonusTypeId"  = bonustypeid
			and ebpd."IsDeleted"  = false 
		    and    e."IsDeleted" = false
		    and ee."IsDeleted"  = false
		    and d."IsDeleted"  = false 
		    and d2."IsDeleted"  = false
		    and fy."IsDeleted"  = false 
		    and c."IsDeleted" = false
		    and g."IsDeleted"  = false;
			
	  
	END;  
$function$
;