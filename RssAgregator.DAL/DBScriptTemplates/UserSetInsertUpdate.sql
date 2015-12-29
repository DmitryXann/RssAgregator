--========================================================
DECLARE @Name nvarchar(max)
	   ,@Email nvarchar(max)
	   ,@Password nvarchar(max)
	   ,@Type tinyint
	   ,@IsActive bit

SELECT @Name		= N''
	   ,@Email		= N''
	   ,@Password	= N''
	   ,@Type		= 0
	   ,@IsActive	= 0

IF EXISTS(SELECT * FROM [dbo].[UserSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[UserSet]
	SET
		 [Name]		= @Name
		,[Email]	= @Email
		,[Password]	= @Password
		,[Type]		= @Type
		,[IsActive]	= @IsActive
	WHERE [Name]	= @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[UserSet]
		([Name]
		,[Email]
		,[Password]
		,[Type]
		,[IsActive])
	VALUES
		(@Name
		,@Email
		,@Password
		,@Type
		,@IsActive)
END
GO
--========================================================