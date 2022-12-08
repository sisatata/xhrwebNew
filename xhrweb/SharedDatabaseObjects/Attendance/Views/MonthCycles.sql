CREATE OR REPLACE VIEW attendance."MonthCycles"
AS SELECT mc."Id",
    mc."CompanyId",
    mc."FinancialYearId",
    mc."MonthCycleName" AS "MonthName",
    mc."MonthStartDate",
    mc."MonthEndDate"
   FROM main."MonthCycles" mc
  WHERE mc."IsDeleted" = false;