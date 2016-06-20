--========================================================
DECLARE @Title nvarchar(25), @RedirectTo nvarchar(25), @OrderNo int, @IsActive bit, @Location int

SELECT @Title		= N''
	  ,@RedirectTo	= N''
	  ,@OrderNo		= 0
	  ,@IsActive	= 1
	  ,@Location	= 0

IF EXISTS(SELECT * FROM [dbo].[NavigationSet] WHERE [Title] like @Title)
BEGIN
	UPDATE [dbo].[NavigationSet]
	SET
		 [Title] = @Title, [RedirectTo] = @RedirectTo, [OrderNo] = @OrderNo, [IsActive] = @IsActive, [Location] = @Location
	WHERE [Title] LIKE @Title
END
ELSE
BEGIN
	INSERT [dbo].[NavigationSet]
	 ([Title], [RedirectTo], [OrderNo], [IsActive], [Location])
	VALUES
	(@Title, @RedirectTo, @OrderNo, @IsActive, @Location)
END
GO
--========================================================