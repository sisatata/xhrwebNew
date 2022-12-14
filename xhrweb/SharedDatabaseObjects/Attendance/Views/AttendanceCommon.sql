CREATE OR REPLACE VIEW attendance."AttendanceCommon"
AS SELECT common."Id",
    common."CardId",
    common."EmployeeId",
    common."AttendanceYear",
    common."AttendanceDate",
    common."Punchtype",
    common."OverNightMark",
    common."Latitude",
    common."Longitude",
    common."IsDeleted",
            common."ClockTimeType" ,
            common."ClockTimeAddress" ,
            common."Remarks" 
   FROM ( SELECT a."Id",
            a."CardId",
            a."EmployeeId",
            a."AttendanceYear",
            a."AttendanceDate",
            a."Punchtype",
            a."OverNightMark",
            a."Latitude",
            a."Longitude",
            a."IsDeleted",
            a."ClockTimeType" ,
            a."ClockTimeAddress" ,
            a."Remarks" 
           FROM attendance."Attendance1" a
        UNION
         SELECT a."Id",
            a."CardId",
            a."EmployeeId",
            a."AttendanceYear",
            a."AttendanceDate",
            a."Punchtype",
            a."OverNightMark",
            a."Latitude",
            a."Longitude",
            a."IsDeleted",
            a."ClockTimeType" ,
            a."ClockTimeAddress" ,
            a."Remarks" 
           FROM attendance."Attendance2" a
        UNION
         SELECT a."Id",
            a."CardId",
            a."EmployeeId",
            a."AttendanceYear",
            a."AttendanceDate",
            a."Punchtype",
            a."OverNightMark",
            a."Latitude",
            a."Longitude",
            a."IsDeleted",
            a."ClockTimeType" ,
            a."ClockTimeAddress" ,
            a."Remarks" 
           FROM attendance."Attendance3" a
        UNION
         SELECT a."Id",
            a."CardId",
            a."EmployeeId",
            a."AttendanceYear",
            a."AttendanceDate",
            a."Punchtype",
            a."OverNightMark",
            a."Latitude",
            a."Longitude",
            a."IsDeleted",
            a."ClockTimeType" ,
            a."ClockTimeAddress" ,
            a."Remarks" 
           FROM attendance."Attendance4" a
        UNION
         SELECT a."Id",
            a."CardId",
            a."EmployeeId",
            a."AttendanceYear",
            a."AttendanceDate",
            a."Punchtype",
            a."OverNightMark",
            a."Latitude",
            a."Longitude",
            a."IsDeleted",
            a."ClockTimeType" ,
            a."ClockTimeAddress" ,
            a."Remarks" 
           FROM attendance."Attendance5" a
        UNION
         SELECT "Attendance6"."Id",
            "Attendance6"."CardId",
            "Attendance6"."EmployeeId",
            "Attendance6"."AttendanceYear",
            "Attendance6"."AttendanceDate",
            "Attendance6"."Punchtype",
            "Attendance6"."OverNightMark",
            "Attendance6"."Latitude",
            "Attendance6"."Longitude",
            "Attendance6"."IsDeleted",
            "Attendance6"."ClockTimeType" ,
            "Attendance6"."ClockTimeAddress" ,
            "Attendance6"."Remarks" 
           FROM attendance."Attendance6"
        UNION
         SELECT "Attendance7"."Id",
            "Attendance7"."CardId",
            "Attendance7"."EmployeeId",
            "Attendance7"."AttendanceYear",
            "Attendance7"."AttendanceDate",
            "Attendance7"."Punchtype",
            "Attendance7"."OverNightMark",
            "Attendance7"."Latitude",
            "Attendance7"."Longitude",
            "Attendance7"."IsDeleted",
            "Attendance7"."ClockTimeType" ,
            "Attendance7"."ClockTimeAddress" ,
            "Attendance7"."Remarks" 
           FROM attendance."Attendance7"
        UNION
         SELECT "Attendance8"."Id",
            "Attendance8"."CardId",
            "Attendance8"."EmployeeId",
            "Attendance8"."AttendanceYear",
            "Attendance8"."AttendanceDate",
            "Attendance8"."Punchtype",
            "Attendance8"."OverNightMark",
            "Attendance8"."Latitude",
            "Attendance8"."Longitude",
            "Attendance8"."IsDeleted",
            "Attendance8"."ClockTimeType" ,
            "Attendance8"."ClockTimeAddress" ,
            "Attendance8"."Remarks" 
           FROM attendance."Attendance8"
        UNION
         SELECT "Attendance9"."Id",
            "Attendance9"."CardId",
            "Attendance9"."EmployeeId",
            "Attendance9"."AttendanceYear",
            "Attendance9"."AttendanceDate",
            "Attendance9"."Punchtype",
            "Attendance9"."OverNightMark",
            "Attendance9"."Latitude",
            "Attendance9"."Longitude",
            "Attendance9"."IsDeleted",
            "Attendance9"."ClockTimeType" ,
            "Attendance9"."ClockTimeAddress" ,
            "Attendance9"."Remarks" 
           FROM attendance."Attendance9"
        UNION
         SELECT "Attendance10"."Id",
            "Attendance10"."CardId",
            "Attendance10"."EmployeeId",
            "Attendance10"."AttendanceYear",
            "Attendance10"."AttendanceDate",
            "Attendance10"."Punchtype",
            "Attendance10"."OverNightMark",
            "Attendance10"."Latitude",
            "Attendance10"."Longitude",
            "Attendance10"."IsDeleted",
            "Attendance10"."ClockTimeType" ,
            "Attendance10"."ClockTimeAddress" ,
            "Attendance10"."Remarks" 
           FROM attendance."Attendance10"
        UNION
         SELECT "Attendance11"."Id",
            "Attendance11"."CardId",
            "Attendance11"."EmployeeId",
            "Attendance11"."AttendanceYear",
            "Attendance11"."AttendanceDate",
            "Attendance11"."Punchtype",
            "Attendance11"."OverNightMark",
            "Attendance11"."Latitude",
            "Attendance11"."Longitude",
            "Attendance11"."IsDeleted",
            "Attendance11"."ClockTimeType" ,
            "Attendance11"."ClockTimeAddress" ,
            "Attendance11"."Remarks" 
           FROM attendance."Attendance11"
        UNION
         SELECT "Attendance12"."Id",
            "Attendance12"."CardId",
            "Attendance12"."EmployeeId",
            "Attendance12"."AttendanceYear",
            "Attendance12"."AttendanceDate",
            "Attendance12"."Punchtype",
            "Attendance12"."OverNightMark",
            "Attendance12"."Latitude",
            "Attendance12"."Longitude",
            "Attendance12"."IsDeleted",
            "Attendance12"."ClockTimeType" ,
            "Attendance12"."ClockTimeAddress" ,
            "Attendance12"."Remarks" 
           FROM attendance."Attendance12") common;