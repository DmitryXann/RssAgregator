--========================================================
DECLARE @Key nvarchar(255)
	   ,@Value nvarchar(max)

SELECT @Key		= N''
	   ,@Value	= N''

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key]		= @Key
		,[Value]	= @Value
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key]
		,[Value])
	VALUES
		(@Key
		,@Value)
END
GO
--========================================================