USE RssAggregator

IF EXISTS(SELECT * FROM [dbo].[NavigationSet])
BEGIN
	DELETE [dbo].[NavigationSet]
END
--========================================================
--========================================================
DECLARE @Title nvarchar(25), @RedirectTo nvarchar(25), @OrderNo int, @IsActive bit

SELECT @Title		= N'Dashboard'
	  ,@RedirectTo	= N'/'
	  ,@OrderNo		= 0
	  ,@IsActive	= 1

IF EXISTS(SELECT * FROM [dbo].[NavigationSet] WHERE [Title] like @Title)
BEGIN
	UPDATE [dbo].[NavigationSet]
	SET
		 [Title] = @Title, [RedirectTo] = @RedirectTo, [OrderNo] = @OrderNo, [IsActive] = @IsActive
	WHERE [Title] LIKE @Title
END
ELSE
BEGIN
	INSERT [dbo].[NavigationSet]
	 ([Title], [RedirectTo], [OrderNo], [IsActive])
	VALUES
	(@Title, @RedirectTo, @OrderNo, @IsActive)
END
GO
--========================================================
--========================================================
DECLARE @Title nvarchar(25), @RedirectTo nvarchar(25), @OrderNo int, @IsActive bit

SELECT @Title		= N'About'
	  ,@RedirectTo	= N'/about'
	  ,@OrderNo		= 1
	  ,@IsActive	= 1

IF EXISTS(SELECT * FROM [dbo].[NavigationSet] WHERE [Title] like @Title)
BEGIN
	UPDATE [dbo].[NavigationSet]
	SET
		 [Title] = @Title, [RedirectTo] = @RedirectTo, [OrderNo] = @OrderNo, [IsActive] = @IsActive
	WHERE [Title] LIKE @Title
END
ELSE
BEGIN
	INSERT [dbo].[NavigationSet]
	 ([Title], [RedirectTo], [OrderNo], [IsActive])
	VALUES
	(@Title, @RedirectTo, @OrderNo, @IsActive)
END
GO
--========================================================