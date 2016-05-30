--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

SELECT @Name			= N''
	   ,@Description	= N''
	   ,@View			= N''
	   ,@Version		= 0
	   ,@Type			= 0
	   ,@User_Id		= 0

IF EXISTS(SELECT * FROM [dbo].[TemplateSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[TemplateSet]
	SET
		 [Name] = @Name, [Description] = @Description, [View] = @View, [Version] = @Version, [Type] = @Type, [User_Id] = @User_Id
	WHERE [Name] = @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[TemplateSet]
		([Name], [Description], [View], [Version], [Type], [User_Id])
	VALUES
		(@Name, @Description, @View, @Version, @Type, @User_Id)
END
GO
--========================================================