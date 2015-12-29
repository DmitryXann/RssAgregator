--========================================================
DECLARE @Name nvarchar(max)
	   ,@Uri nvarchar(max)
	   ,@Type int
	   ,@IsActive bit
	   ,@XMLGuide nvarchar(max)
	   ,@BaseUri nvarchar(max)

SELECT @Name		= N''
	   ,@Uri		= N''
	   ,@Type		= 0
	   ,@IsActive	= 0
	   ,@XMLGuide	= N''
	   ,@BaseUri	= N''

IF EXISTS(SELECT * FROM [dbo].[DataSourcesSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[DataSourcesSet]
	SET
		 [Name]		= @Name
		,[Uri]		= @Uri
		,[Type]		= @Type
		,[IsActive]	= @IsActive
		,[XMLGuide]	= @XMLGuide
		,[BaseUri]	= @BaseUri
	WHERE [Name]	= @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[DataSourcesSet]
		([Name]
		,[Uri]
		,[Type]
		,[IsActive]
		,[XMLGuide]
		,[BaseUri])
	VALUES
		(@Name
		,@Uri
		,@Type
		,@IsActive
		,@XMLGuide
		,@BaseUri)
END
GO
--========================================================