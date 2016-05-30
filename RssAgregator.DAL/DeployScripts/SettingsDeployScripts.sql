USE RssAggregator

IF EXISTS(SELECT * FROM [dbo].[SettingsSet])
BEGIN
	DELETE [dbo].[SettingsSet]
END
--========================================================
--========================================================
DECLARE @Key nvarchar(255), @Value nvarchar(max), @ForUI bit, @Location int

SELECT @Key			= N'ExceptionOccured'
	   ,@Value		= N'An error has occurred in the system, and the administrator of the application has been notified.'
	   ,@ForUI		= 1
	   ,@Location	= 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key] = @Key, [Value] = @Value, [ForUI] = @ForUI, [Location] = @Location
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key], [Value], [ForUI], [Location])
	VALUES
		(@Key, @Value, @ForUI, @Location)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255), @Value nvarchar(max), @ForUI bit, @Location int

SELECT @Key			= N'PLAYER_CantPlaySong'
	   ,@Value		= N'Can`t play this song, there are an network problem or this song is not available in you`r location'
	   ,@ForUI		= 1
	   ,@Location	= 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key] = @Key, [Value] = @Value, [ForUI] = @ForUI, [Location] = @Location
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key], [Value], [ForUI], [Location])
	VALUES
		(@Key, @Value, @ForUI, @Location)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255), @Value nvarchar(max), @ForUI bit, @Location int

SELECT @Key			= N'PLAYER_OnlineRadioTypeAheadPostfix'
	   ,@Value		= N'suggest4'
	   ,@ForUI		= 0
	   ,@Location	= 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key] = @Key, [Value] = @Value, [ForUI] = @ForUI, [Location] = @Location
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key], [Value], [ForUI], [Location])
	VALUES
		(@Key, @Value, @ForUI, @Location)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255), @Value nvarchar(max), @ForUI bit, @Location int

SELECT @Key			= N'PLAYER_SongInBlackList'
	   ,@Value		= N'Song has been added to a black list'
	   ,@ForUI		= 1
	   ,@Location	= 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key] = @Key, [Value] = @Value, [ForUI] = @ForUI, [Location] = @Location
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key], [Value], [ForUI], [Location])
	VALUES
		(@Key, @Value, @ForUI, @Location)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255), @Value nvarchar(max), @ForUI bit, @Location int

SELECT @Key			= N'IpInfoLink'
	   ,@Value		= N'http://ipinfo.io'
	   ,@ForUI		= 1
	   ,@Location	= 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key] = @Key, [Value] = @Value, [ForUI] = @ForUI, [Location] = @Location
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key], [Value], [ForUI], [Location])
	VALUES
		(@Key, @Value, @ForUI, @Location)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255), @Value nvarchar(max), @ForUI bit, @Location int

SELECT @Key			= N'VPLAYER_VideoJSCantPlay'
	   ,@Value		= N'To view this video please enable JavaScript, and consider upgrading to a web browser that <a href="http://videojs.com/html5-video-support/" target="_blank">supports HTML5 video</a>'
	   ,@ForUI		= 1
	   ,@Location	= 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key] = @Key, [Value] = @Value, [ForUI] = @ForUI, [Location] = @Location
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key], [Value], [ForUI], [Location])
	VALUES
		(@Key, @Value, @ForUI, @Location)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255), @Value nvarchar(max), @ForUI bit, @Location int

SELECT @Key			= N'VPLAYER_VideoJSNotSupportedFormat'
	   ,@Value		= N'This video fromat is not supported, please check http://videojs.com/html5-video-support/ for supported HTML5 video formats</a>'
	   ,@ForUI		= 1
	   ,@Location	= 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key] = @Key, [Value] = @Value, [ForUI] = @ForUI, [Location] = @Location
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key], [Value], [ForUI], [Location])
	VALUES
		(@Key, @Value, @ForUI, @Location)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255), @Value nvarchar(max), @ForUI bit, @Location int

SELECT @Key			= N'POST_InvalidPostNameData'
	   ,@Value		= N'Post Name must have more than 2 symbols'
	   ,@ForUI		= 1
	   ,@Location	= 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key] = @Key, [Value] = @Value, [ForUI] = @ForUI, [Location] = @Location
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key], [Value], [ForUI], [Location])
	VALUES
		(@Key, @Value, @ForUI, @Location)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255), @Value nvarchar(max), @ForUI bit, @Location int

SELECT @Key			= N'POST_InvalidPostTagsData'
	   ,@Value		= N'Post Tags must have more than 2 symbols'
	   ,@ForUI		= 1
	   ,@Location	= 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key] = @Key, [Value] = @Value, [ForUI] = @ForUI, [Location] = @Location
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key], [Value], [ForUI], [Location])
	VALUES
		(@Key, @Value, @ForUI, @Location)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255), @Value nvarchar(max), @ForUI bit, @Location int

SELECT @Key			= N'LOGIN_IncorrectUserNameOrPassword'
	   ,@Value		= N'Incorrect user name or password, try again'
	   ,@ForUI		= 1
	   ,@Location	= 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key] = @Key, [Value] = @Value, [ForUI] = @ForUI, [Location] = @Location
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key], [Value], [ForUI], [Location])
	VALUES
		(@Key, @Value, @ForUI, @Location)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255), @Value nvarchar(max), @ForUI bit, @Location int

SELECT @Key			= N'LOGIN_UserNameAlreadyExists'
	   ,@Value		= N'This user name already exists, please try to use another name or restore password'
	   ,@ForUI		= 1
	   ,@Location	= 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key] = @Key, [Value] = @Value, [ForUI] = @ForUI, [Location] = @Location
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key], [Value], [ForUI], [Location])
	VALUES
		(@Key, @Value, @ForUI, @Location)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255), @Value nvarchar(max), @ForUI bit, @Location int

SELECT @Key			= N'LOGIN_TooShortUserName'
	   ,@Value		= N'User name must have more that 3 letters'
	   ,@ForUI		= 1
	   ,@Location	= 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key] = @Key, [Value] = @Value, [ForUI] = @ForUI, [Location] = @Location
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key], [Value], [ForUI], [Location])
	VALUES
		(@Key, @Value, @ForUI, @Location)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255), @Value nvarchar(max), @ForUI bit, @Location int

SELECT @Key			= N'LOGIN_EmailAlreadyExists'
	   ,@Value		= N'This E-Mail already exists, please try to use another E-Mail or restore password'
	   ,@ForUI		= 1
	   ,@Location	= 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key] = @Key, [Value] = @Value, [ForUI] = @ForUI, [Location] = @Location
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key], [Value], [ForUI], [Location])
	VALUES
		(@Key, @Value, @ForUI, @Location)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255), @Value nvarchar(max), @ForUI bit, @Location int

SELECT @Key			= N'LOGIN_IncorrectEmailAddress'
	   ,@Value		= N'Incorrect E-Mail address'
	   ,@ForUI		= 1
	   ,@Location	= 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key] = @Key, [Value] = @Value, [ForUI] = @ForUI, [Location] = @Location
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key], [Value], [ForUI], [Location])
	VALUES
		(@Key, @Value, @ForUI, @Location)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255), @Value nvarchar(max), @ForUI bit, @Location int

SELECT @Key			= N'LOGIN_IncorrectUserPassword'
	   ,@Value		= N'Passwords should be equal and have more that 5 symbols'
	   ,@ForUI		= 1
	   ,@Location	= 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key] = @Key, [Value] = @Value, [ForUI] = @ForUI, [Location] = @Location
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key], [Value], [ForUI], [Location])
	VALUES
		(@Key, @Value, @ForUI, @Location)
END
GO
--========================================================
