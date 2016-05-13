﻿USE RssAggregator
--========================================================
--========================================================
DECLARE @Name nvarchar(50)
	   ,@Email nvarchar(50)
	   ,@Password nvarchar(50)
	   ,@Type tinyint
	   ,@IsActive bit

SELECT @Name		= N'SYSTEM'
	   ,@Email		= N''
	   ,@Password	= N''
	   ,@Type		= 0
	   ,@IsActive	= 1

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