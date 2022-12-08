CREATE OR REPLACE FUNCTION payroll.rptotsummarybycompanyanddaterange(id uuid, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("Hours" numeric, "TotalOTRate" numeric, "EmployeeId" uuid, "DepartmentId" uuid, "DepartmentName" character varying, "FullName" character varying, "CompanyName" character varying, "CompanyLogoUri" text )
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
	 select (cast( sum   ( extract ( hour from  apd."OTHour" )) as numeric ) + cast (sum   ( extract ( minute from  apd."OTHour" )) as numeric)/60 )  as Hours, 
   (cast( sum   ( extract ( hour from  apd."OTHour" )) as numeric ) + cast (sum   ( extract ( minute from  apd."OTHour" )) as numeric)/60 ) * 2 as "TotalOTRate",
  apd."EmployeeId",
  e."DepartmentId",
  d."DepartmentName",
  e."FullName",
  c."CompanyName" ,
  c."CompanyLogoUri"
  from attendance."AttendanceProcessedData" apd  
  inner  join employee."Employees" e on e."Id" = apd."EmployeeId" 
  inner join main."Departments" d on d."Id" = e."DepartmentId" 
  inner join main."Company" c on c."Id" = d."CompanyId" 
  where  c."Id"  = id  and c."IsDeleted" = false and e."IsDeleted" =false and d."IsDeleted" = false and
   apd."PunchDate" :: date >= startDate and apd."PunchDate" :: date <= endDate 
  group by apd."EmployeeId", e."DepartmentId" , d."DepartmentName" , e."FullName" , c."CompanyName" , c."CompanyLogoUri" 
  order by d."DepartmentName"
  ;
	END;
$function$
;