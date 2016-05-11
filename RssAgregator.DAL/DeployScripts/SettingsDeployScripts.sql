USE RssAggregator

IF EXISTS(SELECT * FROM [dbo].[SettingsSet])
BEGIN
	DELETE [dbo].[SettingsSet]
END
--========================================================
--========================================================
DECLARE @Key nvarchar(255)
	   ,@Value nvarchar(max)
	   ,@ForUI bit

SELECT @Key		= N'ExceptionOccured'
	   ,@Value	= N'An error has occurred in the system, and the administrator of the application has been notified.'
	   ,@ForUI  = 1

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key]		= @Key
		,[Value]	= @Value
		,[ForUI]    = @ForUI
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key]
		,[Value]
		,[ForUI])
	VALUES
		(@Key
		,@Value
		,@ForUI)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255)
	   ,@Value nvarchar(max)
	   ,@ForUI bit

SELECT @Key		= N'CantPlaySong'
	   ,@Value	= N'Can`t play this song, there are an network problem or this song is not available in you`r location'
	   ,@ForUI  = 1

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key]		= @Key
		,[Value]	= @Value
		,[ForUI]    = @ForUI
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key]
		,[Value]
		,[ForUI])
	VALUES
		(@Key
		,@Value
		,@ForUI)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255)
	   ,@Value nvarchar(max)
	   ,@ForUI bit

SELECT @Key		= N'OnlineRadioTypeAheadPostfix'
	   ,@Value	= N'suggest4'
	   ,@ForUI  = 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key]		= @Key
		,[Value]	= @Value
		,[ForUI]    = @ForUI
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key]
		,[Value]
		,[ForUI])
	VALUES
		(@Key
		,@Value
		,@ForUI)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255)
	   ,@Value nvarchar(max)
	   ,@ForUI bit

SELECT @Key		= N'SongInBlackList'
	   ,@Value	= N'Song has been added to a black list'
	   ,@ForUI  = 1

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key]		= @Key
		,[Value]	= @Value
		,[ForUI]    = @ForUI
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key]
		,[Value]
		,[ForUI])
	VALUES
		(@Key
		,@Value
		,@ForUI)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255)
	   ,@Value nvarchar(max)
	   ,@ForUI bit

SELECT @Key		= N'IpInfoLink'
	   ,@Value	= N'http://ipinfo.io'
	   ,@ForUI  = 1

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key]		= @Key
		,[Value]	= @Value
		,[ForUI]    = @ForUI
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key]
		,[Value]
		,[ForUI])
	VALUES
		(@Key
		,@Value
		,@ForUI)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255)
	   ,@Value nvarchar(max)
	   ,@ForUI bit

SELECT @Key		= N'VideoJSCantPlay'
	   ,@Value	= N'To view this video please enable JavaScript, and consider upgrading to a web browser that <a href="http://videojs.com/html5-video-support/" target="_blank">supports HTML5 video</a>'
	   ,@ForUI  = 1

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key]		= @Key
		,[Value]	= @Value
		,[ForUI]    = @ForUI
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key]
		,[Value]
		,[ForUI])
	VALUES
		(@Key
		,@Value
		,@ForUI)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255)
	   ,@Value nvarchar(max)
	   ,@ForUI bit

SELECT @Key		= N'VideoJSNotSupportedFormat'
	   ,@Value	= N'This video fromat is not supported, please check http://videojs.com/html5-video-support/ for supported HTML5 video formats</a>'
	   ,@ForUI  = 1

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key]		= @Key
		,[Value]	= @Value
		,[ForUI]    = @ForUI
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key]
		,[Value]
		,[ForUI])
	VALUES
		(@Key
		,@Value
		,@ForUI)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255)
	   ,@Value nvarchar(max)
	   ,@ForUI bit

SELECT @Key		= N'InvalidPostNameData'
	   ,@Value	= N'Post Name must have more than 2 symbols'
	   ,@ForUI  = 1

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key]		= @Key
		,[Value]	= @Value
		,[ForUI]    = @ForUI
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key]
		,[Value]
		,[ForUI])
	VALUES
		(@Key
		,@Value
		,@ForUI)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255)
	   ,@Value nvarchar(max)
	   ,@ForUI bit

SELECT @Key		= N'InvalidPostTagsData'
	   ,@Value	= N'Post Tags must have more than 2 symbols'
	   ,@ForUI  = 1

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key]		= @Key
		,[Value]	= @Value
		,[ForUI]    = @ForUI
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key]
		,[Value]
		,[ForUI])
	VALUES
		(@Key
		,@Value
		,@ForUI)
END
GO
--========================================================