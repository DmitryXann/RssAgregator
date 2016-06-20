--========================================================
DECLARE @Name nvarchar(50), @Email nvarchar(50), @Password nvarchar(50), @Type tinyint, @IsActive bit, @UserKey nvarchar(max), @Location int
	   
SELECT @Name		= N''
	   ,@Email		= N''
	   ,@Password	= N''
	   ,@Type		= 0
	   ,@IsActive	= 0
	   ,@UserKey	= N''
	   ,@Location	= 0

IF EXISTS(SELECT * FROM [dbo].[UserSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[UserSet]
	SET
		 [Name] = @Name, [Email] = @Email, [Password] = @Password, [Type] = @Type, [IsActive] = @IsActive, [UserKey] = @UserKey, [Location] = @Location
	WHERE [Name] = @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[UserSet]
		([Name], [Email], [Password], [Type], [IsActive], [UserKey], [Location])
	VALUES
		(@Name, @Email, @Password, @Type, @IsActive, @UserKey, @Location)
END
GO
--========================================================