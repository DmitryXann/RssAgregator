USE RssAggregator

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet])
BEGIN
	DELETE [dbo].[TransliterationSet]
END
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'а'
	   ,@ToLetter	= N'a'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'б'
	   ,@ToLetter	= N'b'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'в'
	   ,@ToLetter	= N'v'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'гґ'
	   ,@ToLetter	= N'g'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'д'
	   ,@ToLetter	= N'd'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'её'
	   ,@ToLetter	= N'e'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'ж'
	   ,@ToLetter	= N'zh'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'з'
	   ,@ToLetter	= N'z'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'ийїi'
	   ,@ToLetter	= N'i'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'к'
	   ,@ToLetter	= N'k'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'л'
	   ,@ToLetter	= N'l'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'м'
	   ,@ToLetter	= N'm'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'н'
	   ,@ToLetter	= N'n'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'о'
	   ,@ToLetter	= N'o'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'п'
	   ,@ToLetter	= N'p'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'р'
	   ,@ToLetter	= N'r'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'с'
	   ,@ToLetter	= N's'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'т'
	   ,@ToLetter	= N't'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'у'
	   ,@ToLetter	= N'u'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'ф'
	   ,@ToLetter	= N'f'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'х'
	   ,@ToLetter	= N'kh'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'ц'
	   ,@ToLetter	= N'ts'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'ч'
	   ,@ToLetter	= N'ch'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'ш'
	   ,@ToLetter	= N'sh'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'щ'
	   ,@ToLetter	= N'shch'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'ъ'
	   ,@ToLetter	= N'ie'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'ы'
	   ,@ToLetter	= N'y'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'ь'
	   ,@ToLetter	= N''

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'э'
	   ,@ToLetter	= N'e'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'ю'
	   ,@ToLetter	= N'iu'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N'я'
	   ,@ToLetter	= N'ia'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================
--========================================================
DECLARE @FromLetter nvarchar(10)
	   ,@ToLetter nvarchar(10)

SELECT @FromLetter	= N' ' + CHAR(9)
	   ,@ToLetter	= N'_'

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter]	= @FromLetter
		,[ToLetter]		= @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter]
		,[ToLetter])
	VALUES
		(@FromLetter
		,@ToLetter)
END
GO
--========================================================