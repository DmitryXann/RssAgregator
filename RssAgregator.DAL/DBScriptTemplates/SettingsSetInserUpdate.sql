--========================================================
DECLARE @Key nvarchar(255), @Value nvarchar(max), @ForUI bit, @Location int

SELECT @Key			= N''
	   ,@Value		= N''
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