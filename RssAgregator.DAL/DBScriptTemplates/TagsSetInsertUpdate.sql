--========================================================
DECLARE @Name nvarchar(25), @Type int, @Location int

SELECT @Name		= N''
	   ,@Type		= 0
	   ,@Location	= 0

IF EXISTS(SELECT * FROM [dbo].[TagsSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[TagsSet]
	SET
		 [Name] = @Name, [Type] = @Type, [Location] = @Location
	WHERE [Name]	= @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[TagsSet]
		([Name], [Type], [Location])
	VALUES
		(@Name, @Type, @Location)
END
GO
--========================================================