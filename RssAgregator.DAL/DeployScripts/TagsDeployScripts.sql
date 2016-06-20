USE RssAggregator

IF EXISTS(SELECT * FROM [dbo].[TagsSet])
BEGIN
	DELETE [dbo].[TagsSet]
END
--========================================================
DECLARE @Name nvarchar(25), @Type int, @Location int

SELECT @Name		= N'Text'
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
--========================================================
DECLARE @Name nvarchar(25), @Type int, @Location int

SELECT @Name		= N'Image'
	   ,@Type		= 1
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
--========================================================
DECLARE @Name nvarchar(25), @Type int, @Location int

SELECT @Name		= N'Link'
	   ,@Type		= 2
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
--========================================================
DECLARE @Name nvarchar(25), @Type int, @Location int

SELECT @Name		= N'Audio'
	   ,@Type		= 3
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
--========================================================
DECLARE @Name nvarchar(25), @Type int, @Location int

SELECT @Name		= N'Video'
	   ,@Type		= 4
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
--========================================================
DECLARE @Name nvarchar(25), @Type int, @Location int

SELECT @Name		= N'Текст'
	   ,@Type		= 0
	   ,@Location	= 1

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
--========================================================
DECLARE @Name nvarchar(25), @Type int, @Location int

SELECT @Name		= N'Изображение'
	   ,@Type		= 1
	   ,@Location	= 1

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
--========================================================
DECLARE @Name nvarchar(25), @Type int, @Location int

SELECT @Name		= N'Ссылка'
	   ,@Type		= 2
	   ,@Location	= 1

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
--========================================================
DECLARE @Name nvarchar(25), @Type int, @Location int

SELECT @Name		= N'Аудио'
	   ,@Type		= 3
	   ,@Location	= 1

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
--========================================================
DECLARE @Name nvarchar(25), @Type int, @Location int

SELECT @Name		= N'Видео'
	   ,@Type		= 4
	   ,@Location	= 1

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