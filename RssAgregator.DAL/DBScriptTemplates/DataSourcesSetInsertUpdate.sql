--========================================================
DECLARE @Name nvarchar(max)
	   ,@Uri nvarchar(max)
	   ,@Type int
	   ,@IsActive bit
	   ,@XMLGuide nvarchar(max)
	   ,@BaseUri nvarchar(max)
	   ,@IsNewsSource bit

SELECT @Name		  = N''
	   ,@Uri		  = N''
	   ,@Type		  = 0
	   ,@IsActive	  = 0
	   ,@XMLGuide	  = N''
	   ,@BaseUri	  = N''
	   ,@IsNewsSource = 1

IF EXISTS(SELECT * FROM [dbo].[DataSourcesSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[DataSourcesSet]
	SET
		 [Name]		    = @Name
		,[Uri]		    = @Uri
		,[Type]		    = @Type
		,[IsActive]	    = @IsActive
		,[XMLGuide]	    = @XMLGuide
		,[BaseUri]	    = @BaseUri
		,[IsNewsSource] = @IsNewsSource
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
		,[BaseUri]
		,[IsNewsSource])
	VALUES
		(@Name
		,@Uri
		,@Type
		,@IsActive
		,@XMLGuide
		,@BaseUri
		,@IsNewsSource)
END
GO
--========================================================