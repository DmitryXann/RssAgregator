USE RssAggregator

IF EXISTS(SELECT * FROM [dbo].[DataSourcesSet])
BEGIN
	DELETE [dbo].[DataSourcesSet]
END
--========================================================
--========================================================
DECLARE @Name nvarchar(50), @Uri nvarchar(max), @Type int, @IsActive bit, @XMLGuide nvarchar(max), @BaseUri nvarchar(max), @IsNewsSource bit 

SELECT @Name		= N'MDK'
	   ,@Uri		= N'https://vk.com/wall-10639516'
	   ,@Type		= 2
	   ,@IsActive	= 1
	   ,@XMLGuide	= N'<?xml version="1.0" encoding="utf-8" ?>
<PostModelXMLGuide>
  <PostRootNode>
    <name>div</name>
    <id>post-</id>
    <class strictEqual="true">post all own</class>
    <onmouseover>wall.postOver</onmouseover>
    <onmouseout>wall.postOut</onmouseout>
    <onclick>wall.postClick</onclick>
  </PostRootNode>
  <AuthorId>
    <SearchCriterea>
      <name>div</name>
      <id>post-</id>
      <class strictEqual="true">post all own</class>
      <onmouseover>wall.postOver</onmouseover>
      <onmouseout>wall.postOut</onmouseout>
      <onclick>wall.postClick</onclick>
    </SearchCriterea>
    <GetCriterea>
      <id><![CDATA[-([0-9]*)_]]></id>
    </GetCriterea>
    <TrimCriterea>
      <![CDATA[-_]]>
    </TrimCriterea>
  </AuthorId>
  <PostId>
    <SearchCriterea>
      <name>div</name>
      <id>post-</id>
      <class strictEqual="true">post all own</class>
      <onmouseover>wall.postOver</onmouseover>
      <onmouseout>wall.postOut</onmouseout>
      <onclick>wall.postClick</onclick>
    </SearchCriterea>
    <GetCriterea>
      <id><![CDATA[_([0-9]*)]]></id>
    </GetCriterea>
    <TrimCriterea>
      <![CDATA[_]]>
    </TrimCriterea>
  </PostId>
  <PostLikes>
    <SearchCriterea>
      <name>span</name>
      <id>like_count-{AuthorId}_{PostId}</id>
    </SearchCriterea>
    <GetCriterea>
      <values></values>
    </GetCriterea>
  </PostLikes>
  <AuthorName>
    <SearchCriterea>
      <name>a</name>
      <data-post-id>-{AuthorId}_{PostId}</data-post-id>
    </SearchCriterea>
    <GetCriterea>
      <values></values>
    </GetCriterea>
  </AuthorName>
  <AuthorLink>
    <SearchCriterea>
      <name>a</name>
      <data-post-id>-{AuthorId}_{PostId}</data-post-id>
    </SearchCriterea>
    <GetCriterea>
      <href><![CDATA[/([\w]*)]]></href>
    </GetCriterea>
  </AuthorLink>
  <TextContent>
    <SearchCriterea>
      <name>div</name>
      <class>wall_post_text</class>
    </SearchCriterea>
    <GetCriterea>
      <childs></childs>
    </GetCriterea>
  </TextContent>
  <ImgContent>
    <SearchCriterea>
      <name>div</name>
      <class>page_post_sized_thumbs</class>
    </SearchCriterea>
    <SubSearchCriterea>
      <name>a</name>
      <onclick>return showPhoto(''-{AuthorId}</onclick>
    </SubSearchCriterea>
    <GetCriterea>
      <onclick><![CDATA[((http|https):([\w]*.*)\/&quot;)|(&quot;x_&quot;:\[&quot;([a-zA-Z0-9_-]*\/*[a-zA-Z0-9_-]*)&quot;)]]></onclick>
    </GetCriterea>
    <TrimCriterea stringTrim="true">
      <![CDATA[&quot;x_&quot;:[&quot; &quot;]]>
    </TrimCriterea>
    <ImgExt>jpg</ImgExt>
  </ImgContent>
  <AudioContent>
    <SearchCriterea>
      <name>div</name>
      <class>post_media clear_fix wall_audio</class>
    </SearchCriterea>
    <SubSearchCritereaSongLink>
      <name>input</name>
      <type>hidden</type>
      <id>-{AuthorId}_{PostId}</id>
    </SubSearchCritereaSongLink>
    <SubSearchCritereaSongAuthor>
      <name>a</name>
      <onclick>return nav.go(this, event);</onclick>
    </SubSearchCritereaSongAuthor>
    <SubSearchCritereaSongName>
      <name>span</name>
      <class>title</class>
      <id>-{AuthorId}_{PostId}</id>
    </SubSearchCritereaSongName>
    <GetCritereaSongLink>
      <value><![CDATA[^([\w]*.*)\?]]></value>
    </GetCritereaSongLink>
    <GetCritereaSongAuthor>
      <values></values>
    </GetCritereaSongAuthor>
    <GetCritereaSongName>
      <values></values>
    </GetCritereaSongName>
    <TrimCritereaSongLink>
      <![CDATA[\?]]>
    </TrimCritereaSongLink>
  </AudioContent>
</PostModelXMLGuide>'
	   ,@BaseUri	= N'https://vk.com/'
	   ,@IsNewsSource = 1

IF EXISTS(SELECT * FROM [dbo].[DataSourcesSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[DataSourcesSet]
	SET
		 [Name] = @Name, [Uri] = @Uri, [Type] = @Type, [IsActive] = @IsActive, [XMLGuide] = @XMLGuide, [BaseUri] = @BaseUri, [IsNewsSource] = @IsNewsSource
	WHERE [Name] = @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[DataSourcesSet]
		([Name], [Uri], [Type], [IsActive], [XMLGuide], [BaseUri], [IsNewsSource])
	VALUES
		(@Name, @Uri, @Type, @IsActive, @XMLGuide, @BaseUri, @IsNewsSource)
END
GO
--========================================================
--========================================================
DECLARE @Name nvarchar(50), @Uri nvarchar(max), @Type int, @IsActive bit, @XMLGuide nvarchar(max), @BaseUri nvarchar(max), @IsNewsSource bit

SELECT @Name		= N'pikabu'
	   ,@Uri		= N'http://pikabu.ru/'
	   ,@Type		= 3
	   ,@IsActive	= 0
	   ,@XMLGuide	= N'<?xml version="1.0" encoding="utf-8" ?>
<PostModelXMLGuide>
  <PostRootNode>
    <name>div</name>
    <id>stories_container</id>
  </PostRootNode>
  <!--<AuthorId>
    <SearchCriterea>
      <name>div</name>
      <id>post-</id>
      <class strictEqual="true">post all own</class>
      <onmouseover>wall.postOver</onmouseover>
      <onmouseout>wall.postOut</onmouseout>
      <onclick>wall.postClick</onclick>
    </SearchCriterea>
    <GetCriterea>
      <id><![CDATA[-([0-9]*)_]]></id>
    </GetCriterea>
    <TrimCriterea>
      <![CDATA[-_]]>
    </TrimCriterea>
  </AuthorId>-->
  <PostId>
    <SearchCriterea>
      <name>table</name>
      <id>inner_wrap_</id>
    </SearchCriterea>
    <GetCriterea>
      <data-story-id><![CDATA[([0-9]*)]]></data-story-id>
    </GetCriterea>
  </PostId>
  <PostLikes>
    <SearchCriterea>
      <name>li</name>
      <id>num_digs{PostId}</id>
    </SearchCriterea>
    <GetCriterea>
      <values></values>
    </GetCriterea>
  </PostLikes>
  <PostName>
    <SearchCriterea>
      <name>a</name>
      <id>num_dig3{PostId}</id>
    </SearchCriterea>
    <GetCriterea>
      <values></values>
    </GetCriterea>
  </PostName>
  <PostLink>
    <SearchCriterea>
      <name>a</name>
      <id>num_dig3{PostId}</id>
    </SearchCriterea>
    <GetCriterea>
      <href><![CDATA[[\w]*[\W]*]]></href>
    </GetCriterea>
  </PostLink>
  
  <!--<AuthorName>
    <SearchCriterea>
      <name>a</name>
      <data-post-id>-{AuthorId}_{PostId}</data-post-id>
    </SearchCriterea>
    <GetCriterea>
      <values></values>
    </GetCriterea>
  </AuthorName>
  <AuthorLink>
    <SearchCriterea>
      <name>a</name>
      <data-post-id>-{AuthorId}_{PostId}</data-post-id>
    </SearchCriterea>
    <GetCriterea>
      <href><![CDATA[/([\w]*)]]></href>
    </GetCriterea>
  </AuthorLink>
  <TextContent>
    <SearchCriterea>
      <name>div</name>
      <class>wall_post_text</class>
    </SearchCriterea>
    <GetCriterea>
      <childs></childs>
    </GetCriterea>
  </TextContent>
  <ImgContent>
    <SearchCriterea>
      <name>div</name>
      <class>page_post_sized_thumbs</class>
    </SearchCriterea>
    <SubSearchCriterea>
      <name>a</name>
      <onclick>return showPhoto(''-{AuthorId}</onclick>
    </SubSearchCriterea>
    <GetCriterea>
      <onclick><![CDATA[(http:([\w]*.*)\/&quot;)|(&quot;x_&quot;:\[&quot;([\w]*\/*[\w]*)&quot;)]]></onclick>
    </GetCriterea>
    <TrimCriterea stringTrim="true">
      <![CDATA[&quot;x_&quot;:[&quot; &quot;]]>
    </TrimCriterea>
    <ImgExt>jpg</ImgExt>
  </ImgContent>-->
</PostModelXMLGuide>'
	   ,@BaseUri	= N'http://pikabu.ru/'
	   ,@IsNewsSource = 1
IF EXISTS(SELECT * FROM [dbo].[DataSourcesSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[DataSourcesSet]
	SET
		 [Name] = @Name, [Uri] = @Uri, [Type] = @Type, [IsActive] = @IsActive, [XMLGuide] = @XMLGuide, [BaseUri] = @BaseUri, [IsNewsSource] = @IsNewsSource
	WHERE [Name] = @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[DataSourcesSet]
		([Name], [Uri], [Type], [IsActive], [XMLGuide], [BaseUri], [IsNewsSource])
	VALUES
		(@Name, @Uri, @Type, @IsActive, @XMLGuide, @BaseUri, @IsNewsSource)
END
GO
--========================================================
--========================================================
DECLARE @Name nvarchar(50), @Uri nvarchar(max), @Type int, @IsActive bit, @XMLGuide nvarchar(max), @BaseUri nvarchar(max), @IsNewsSource bit

SELECT @Name		= N'mainfun'
	   ,@Uri		= N'http://mainfun.ru/'
	   ,@Type		= 4
	   ,@IsActive	= 0
	   ,@XMLGuide	= N'<?xml version="1.0" encoding="utf-8" ?>
<PostModelXMLGuide>
</PostModelXMLGuide>'
	   ,@BaseUri	= N'http://mainfun.ru/'
	   ,@IsNewsSource = 1
IF EXISTS(SELECT * FROM [dbo].[DataSourcesSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[DataSourcesSet]
	SET
		 [Name] = @Name, [Uri] = @Uri, [Type] = @Type, [IsActive] = @IsActive, [XMLGuide] = @XMLGuide, [BaseUri] = @BaseUri, [IsNewsSource] = @IsNewsSource
	WHERE [Name] = @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[DataSourcesSet]
		([Name], [Uri], [Type], [IsActive], [XMLGuide], [BaseUri], [IsNewsSource])
	VALUES
		(@Name, @Uri, @Type, @IsActive, @XMLGuide, @BaseUri, @IsNewsSource)
END
GO
--========================================================
--========================================================
DECLARE @Name nvarchar(50), @Uri nvarchar(max), @Type int, @IsActive bit, @XMLGuide nvarchar(max), @BaseUri nvarchar(max), @IsNewsSource bit

SELECT @Name		= N'joyreactor'
	   ,@Uri		= N'http://joyreactor.cc/'
	   ,@Type		= 5
	   ,@IsActive	= 0
	   ,@XMLGuide	= N'<?xml version="1.0" encoding="utf-8" ?>
<PostModelXMLGuide>
</PostModelXMLGuide>'
	   ,@BaseUri	= N'http://joyreactor.cc/'
	   ,@IsNewsSource = 1

IF EXISTS(SELECT * FROM [dbo].[DataSourcesSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[DataSourcesSet]
	SET
		 [Name] = @Name, [Uri] = @Uri, [Type] = @Type, [IsActive] = @IsActive, [XMLGuide] = @XMLGuide, [BaseUri] = @BaseUri, [IsNewsSource] = @IsNewsSource
	WHERE [Name] = @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[DataSourcesSet]
		([Name], [Uri], [Type], [IsActive], [XMLGuide], [BaseUri], [IsNewsSource])
	VALUES
		(@Name, @Uri, @Type, @IsActive, @XMLGuide, @BaseUri, @IsNewsSource)
END
GO
--========================================================
--========================================================
DECLARE @Name nvarchar(50), @Uri nvarchar(max), @Type int, @IsActive bit, @XMLGuide nvarchar(max), @BaseUri nvarchar(max), @IsNewsSource bit

SELECT @Name		= N'ZaycevNet'
	   ,@Uri		= N'http://zaycev.net/search.html'
	   ,@Type		= 1
	   ,@IsActive	= 0
	   ,@XMLGuide	= N'<?xml version="1.0" encoding="utf-8" ?>
<PostModelXMLGuide>
  <PostRootNode>
    <name>div</name>
    <class strictEqual="true">musicset-track-list__items</class>
  </PostRootNode>
  <AudioContent>
    <SearchCriterea>
      <name>div</name>
      <class>musicset-track</class>
    </SearchCriterea>

    <SubSearchCritereaSongLink>
      <name>div</name>
      <class>musicset-track</class>
    </SubSearchCritereaSongLink>
    <SubSearchCritereaSongAuthor>
      <name>div</name>
      <class>musicset-track__artist</class>
    </SubSearchCritereaSongAuthor>
    <SubSearchCritereaSongName>
      <name>div</name>
      <class>musicset-track__track-name</class>
    </SubSearchCritereaSongName>

    <GetCritereaSongLink>
      <data-url><![CDATA[((\/([\w]+))+).json]]></data-url>
    </GetCritereaSongLink>
    <GetCritereaSongAuthor>
      <childs></childs>
    </GetCritereaSongAuthor>
    <GetCritereaSongName>
      <childs></childs>
    </GetCritereaSongName>

    <TrimCritereaSongAuthor regExpTrim="true">
      <![CDATA[>[\s]?(.)+[\s]?<]]>
    </TrimCritereaSongAuthor>
    <TrimCritereaSongName regExpTrim="true">
      <![CDATA[>[\s]?(.)+[\s]?<]]>
    </TrimCritereaSongName>

    <AdditionalTrimCritereaSongAuthor>
      <![CDATA[><]]>
    </AdditionalTrimCritereaSongAuthor>
    <AdditionalTrimCritereaSongName>
      <![CDATA[><]]>
    </AdditionalTrimCritereaSongName>
  </AudioContent>
</PostModelXMLGuide>'
	   ,@BaseUri	= N'http://zaycev.net/'
	   ,@IsNewsSource = 0

IF EXISTS(SELECT * FROM [dbo].[DataSourcesSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[DataSourcesSet]
	SET
		 [Name] = @Name, [Uri] = @Uri, [Type] = @Type, [IsActive] = @IsActive, [XMLGuide] = @XMLGuide, [BaseUri] = @BaseUri, [IsNewsSource] = @IsNewsSource
	WHERE [Name] = @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[DataSourcesSet]
		([Name], [Uri], [Type], [IsActive], [XMLGuide], [BaseUri], [IsNewsSource])
	VALUES
		(@Name, @Uri, @Type, @IsActive, @XMLGuide, @BaseUri, @IsNewsSource)
END
GO
--========================================================
--========================================================
DECLARE @Name nvarchar(50), @Uri nvarchar(max), @Type int, @IsActive bit, @XMLGuide nvarchar(max), @BaseUri nvarchar(max), @IsNewsSource bit

SELECT @Name		= N'System'
	   ,@Uri		= N'http://SOME.COM/'
	   ,@Type		= 0
	   ,@IsActive	= 0
	   ,@XMLGuide	= N''
	   ,@BaseUri	= N'http://SOME.COM/'
	   ,@IsNewsSource = 1

IF EXISTS(SELECT * FROM [dbo].[DataSourcesSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[DataSourcesSet]
	SET
		 [Name] = @Name, [Uri] = @Uri, [Type] = @Type, [IsActive] = @IsActive, [XMLGuide] = @XMLGuide, [BaseUri] = @BaseUri, [IsNewsSource] = @IsNewsSource
	WHERE [Name] = @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[DataSourcesSet]
		([Name], [Uri], [Type], [IsActive], [XMLGuide], [BaseUri], [IsNewsSource])
	VALUES
		(@Name, @Uri, @Type, @IsActive, @XMLGuide, @BaseUri, @IsNewsSource)
END
GO
--========================================================