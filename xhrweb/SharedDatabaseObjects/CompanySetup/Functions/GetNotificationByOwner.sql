CREATE OR REPLACE FUNCTION main.getnotificationbyowner(notificationownerid uuid)
 RETURNS TABLE("Id" uuid, "ApplicationId" uuid, "ApplicantId" uuid, "ManagerId" uuid, "NotificationTitle" character varying, "NotificationDetail" character varying, "IsViewed" boolean, "ViewedTime" timestamp without time zone, "NotificationTypeId" smallint, "NotificationOwnerId" uuid)
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
			Notification."NotificationOwnerId"
		 FROM main."Notifications" AS Notification
		 WHERE Notification."NotificationOwnerId" = notificationOwnerId and Notification."ApplicationId" is not null order by "CreatedDate" desc, "IsViewed" ;
	END;
$function$
;


select * from main.getnotificationbyowner('e6a94443-8f81-4e15-be9d-3fd178ed4212');

select * from main."Notifications" n where "NotificationOwnerId" = 'e6a94443-8f81-4e15-be9d-3fd178ed4212';
