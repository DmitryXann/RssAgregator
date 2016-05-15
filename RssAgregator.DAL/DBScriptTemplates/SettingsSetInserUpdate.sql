--========================================================
DECLARE @Key nvarchar(255), @Value nvarchar(max), @ForUI bit

SELECT @Key		= N''
	   ,@Value	= N''
	   ,@ForUI  = 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key] = @Key, [Value] = @Value, [ForUI] = @ForUI
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key], [Value], [ForUI])
	VALUES
		(@Key, @Value, @ForUI)
END
GO
--========================================================