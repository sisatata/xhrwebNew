-- main."FinancialYearMonth" source

CREATE OR REPLACE VIEW leave."FinancialYearMonth"
AS SELECT mnth."MonthNumber",
    yr."YearNumber",
    mnth."Id",
    mnth."FinancialYearId",
    mnth."MonthCycleName",
    mnth."MonthCycleLocalizedName",
    mnth."MonthStartDate",
    mnth."MonthEndDate",
    mnth."TotalWorkingDays",
    mnth."RunningFlag",
    mnth."IsSelected",
    mnth."SortOrder" AS "MonthSortOrder",
    yr."CompanyId",
    yr."FinancialYearName",
    yr."FinancialYearLocalizedName",
    yr."FinancialYearStartDate",
    yr."FinancialYearEndDate",
    yr."IsRunningYear",
    yr."SortOrder" AS "YearSortOrder"
   FROM ( SELECT row_number() OVER (PARTITION BY mc."CompanyId" ORDER BY mc."MonthStartDate") AS "MonthNumber",
            mc."Id",
            mc."CompanyId",
            mc."FinancialYearId",
            mc."MonthCycleName",
            mc."MonthCycleLocalizedName",
            mc."MonthStartDate",
            mc."MonthEndDate",
            mc."TotalWorkingDays",
            mc."RunningFlag",
            mc."IsSelected",
            mc."SortOrder",
            mc."IsDeleted"
           FROM main."MonthCycles" mc
          WHERE mc."IsDeleted" = false) mnth
     JOIN ( SELECT row_number() OVER (PARTITION BY fy."CompanyId" ORDER BY fy."FinancialYearStartDate" desc) AS "YearNumber",
            fy."Id",
            fy."CompanyId",
            fy."FinancialYearName",
            fy."FinancialYearLocalizedName",
            fy."FinancialYearStartDate",
            fy."FinancialYearEndDate",
            fy."IsRunningYear",
            fy."SortOrder",
            fy."IsDeleted"
           FROM main."FinancialYears" fy
          WHERE fy."IsDeleted" = false) yr ON mnth."FinancialYearId" = yr."Id";