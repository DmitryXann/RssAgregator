USE RssAggregator
--========================================================
--========================================================
DECLARE @Name nvarchar(50), @Email nvarchar(50), @Password nvarchar(50), @Type tinyint, @IsActive bit, @UserKey nvarchar(max)
	   
SELECT @Name		= N'SYSTEM'
	   ,@Email		= N''
	   ,@Password	= N''
	   ,@Type		= 0
	   ,@IsActive	= 0
	   ,@UserKey	= N''

IF EXISTS(SELECT * FROM [dbo].[UserSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[UserSet]
	SET
		 [Name] = @Name, [Email] = @Email, [Password] = @Password, [Type] = @Type, [IsActive] = @IsActive, [UserKey] = @UserKey
	WHERE [Name] = @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[UserSet]
		([Name], [Email], [Password], [Type], [IsActive], [UserKey])
	VALUES
		(@Name, @Email, @Password, @Type, @IsActive, @UserKey)
END
GO
--========================================================