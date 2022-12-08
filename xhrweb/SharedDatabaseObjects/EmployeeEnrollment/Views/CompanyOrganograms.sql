create or replace
view employee."CompanyOrganograms"
as
select
	uuid_in(md5(random()::text || clock_timestamp()::text)::cstring) "Id",
	d."Id" "DesignationId" ,
	d."DesignationName" ,
	d2."Id" "DepartmentId",
	d2."DepartmentName" ,
	b."Id" "BranchId",
	b."BranchName" ,
	c."Id" "CompanyId",
	c."CompanyName",
	g."Id" "GradeId",
	g."GradeName"
from
	main."Designations" d
inner join main."Departments" d2 
on
	d."LinkedEntityId" = d2."Id"
	and "LinkedEntityType" = 'Department'
inner join main."Branch" b on
	d2."BranchId" = b."Id"
inner join main."Company" c on
	c."Id" = b."CompanyId"
inner join main."Grades" g on
	g."CompanyId" = c."Id"
where
	c."IsDeleted" = false
	and g."IsDeleted" = false
	and d."IsDeleted" = false
	and d2."IsDeleted" = false
	and b."IsDeleted" = false ;