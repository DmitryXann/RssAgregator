--========================================================
DECLARE @FromLetter nvarchar(10), @ToLetter nvarchar(10)

SELECT @FromLetter	= N''
	   ,@ToLetter	= N''

IF EXISTS(SELECT * FROM [dbo].[TransliterationSet] WHERE [FromLetter] like @FromLetter)
BEGIN
	UPDATE [dbo].[TransliterationSet]
	SET
		 [FromLetter] = @FromLetter, [ToLetter] = @ToLetter
	WHERE [FromLetter]	= @FromLetter
END
ELSE
BEGIN
	INSERT INTO [dbo].[TransliterationSet]
		([FromLetter], [ToLetter])
	VALUES
		(@FromLetter, @ToLetter)
END
GO
--========================================================