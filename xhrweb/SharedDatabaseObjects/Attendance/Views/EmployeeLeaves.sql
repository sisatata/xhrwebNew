create or replace
view attendance."EmployeeLeaves" as
select
	la."Id" ,
	la."EmployeeId" ,
	lt."LeaveTypeShortName" ,
	lt."LeaveTypeName",
	la."StartDate",
	la."EndDate",
	la."CompanyId"
from
	leave."LeaveApplications" la
inner join leave."LeaveTypes" lt on
	la."LeaveTypeId" = lt."Id"
where
	la."ApprovalStatus" = '3';
