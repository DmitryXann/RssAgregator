--========================================================
DECLARE @Name nvarchar(50), @Uri nvarchar(max), @Type int, @IsActive bit, @XMLGuide nvarchar(max), @BaseUri nvarchar(max), @IsNewsSource bit, @PostAmountPerIteration int, @Location int

SELECT @Name					= N''
	   ,@Uri					= N''
	   ,@Type					= 0
	   ,@IsActive				= 0
	   ,@XMLGuide				= N''
	   ,@BaseUri				= N''
	   ,@IsNewsSource			= 1
	   ,@PostAmountPerIteration = NULL
	   ,@Location				= NULL

IF EXISTS(SELECT * FROM [dbo].[DataSourcesSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[DataSourcesSet]
	SET
		 [Name] = @Name, [Uri] = @Uri, [Type] = @Type, [IsActive] = @IsActive, [XMLGuide] = @XMLGuide, [BaseUri] = @BaseUri, [IsNewsSource] = @IsNewsSource, [PostAmountPerIteration] = @PostAmountPerIteration, [Location] = @Location
	WHERE [Name] = @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[DataSourcesSet]
		([Name], [Uri], [Type], [IsActive], [XMLGuide], [BaseUri], [IsNewsSource], [PostAmountPerIteration], [Location])
	VALUES
		(@Name, @Uri, @Type, @IsActive, @XMLGuide, @BaseUri, @IsNewsSource, @PostAmountPerIteration, @Location)
END
GO
--========================================================