CREATE OR REPLACE VIEW attendance."Company"
AS SELECT "Company"."Id",
    "Company"."CompanyName",
    "Company"."CompanyLocalizedName",
    "Company"."LicenseKey",
    "Company"."SortOrder",
    "Company"."ApprovalStatus",
    "Company"."Notes",
    "Company"."ShortName",
   "Company"."CompanyMaskingId" 
   FROM main."Company"
  WHERE "Company"."IsDeleted" = false AND "Company"."ApprovalStatus"::text = '3'::text;