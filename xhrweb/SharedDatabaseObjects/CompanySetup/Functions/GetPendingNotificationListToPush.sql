CREATE OR REPLACE FUNCTION main.getpendingnotificationlisttopush()
 RETURNS TABLE("Id" uuid, "ApplicationId" uuid, "ApplicantId" uuid, "ManagerId" uuid, "NotificationTitle" character varying, "NotificationDetail" character varying, "IsViewed" boolean, "ViewedTime" timestamp without time zone, "NotificationTypeId" smallint, "NotificationOwnerId" uuid, "IsPushed" boolean, "PushedTime" timestamp without time zone, "NotificationOwnerAccessToken" text)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			Notification."Id" , 
			Notification."ApplicationId" , 
			Notification."ApplicantId" , 
			Notification."ManagerId" , 
			Notification."NotificationTitle" , 
			Notification."NotificationDetail" , 
			Notification."IsViewed" , 
			Notification."ViewedTime" , 
			Notification."NotificationTypeId" , 
			Notification."NotificationOwnerId",
			Notification."IsPushed" ,
			Notification."PushedTime" ,
			Notification."NotificationOwnerAccessToken" 
		 FROM main."Notifications" AS Notification
		 WHERE Notification."IsPushed" = false ;
	END;
$function$
;

--DROP FUNCTION getpendingnotificationlisttopush() ;