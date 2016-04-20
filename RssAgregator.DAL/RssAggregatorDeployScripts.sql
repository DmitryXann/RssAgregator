USE RssAggregator
--========================================================
--========================================================
DECLARE @Name nvarchar(max)
	   ,@Uri nvarchar(max)
	   ,@Type int
	   ,@IsActive bit
	   ,@XMLGuide nvarchar(max)
	   ,@BaseUri nvarchar(max)
	   ,@IsNewsSource bit

SELECT @Name		= N'MDK'
	   ,@Uri		= N'https://vk.com/wall-10639516'
	   ,@Type		= 1
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
      <onclick><![CDATA[((http|https):([\w]*.*)\/&quot;)|(&quot;x_&quot;:\[&quot;([\w]*\/*[\w]*)&quot;)]]></onclick>
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
		 [Name]		    = @Name
		,[Uri]		    = @Uri
		,[Type]		    = @Type
		,[IsActive]	    = @IsActive
		,[XMLGuide]	    = @XMLGuide
		,[BaseUri]	    = @BaseUri
		,[IsNewsSource] = @IsNewsSource
	WHERE [Name]	= @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[DataSourcesSet]
		([Name]
		,[Uri]
		,[Type]
		,[IsActive]
		,[XMLGuide]
		,[BaseUri]
		,[IsNewsSource])
	VALUES
		(@Name
		,@Uri
		,@Type
		,@IsActive
		,@XMLGuide
		,@BaseUri
		,@IsNewsSource)
END
GO
--========================================================
--========================================================
DECLARE @Name nvarchar(max)
	   ,@Uri nvarchar(max)
	   ,@Type int
	   ,@IsActive bit
	   ,@XMLGuide nvarchar(max)
	   ,@BaseUri nvarchar(max)
	   ,@IsNewsSource bit

SELECT @Name		= N'pikabu'
	   ,@Uri		= N'http://pikabu.ru/'
	   ,@Type		= 2
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
		 [Name]		    = @Name
		,[Uri]		    = @Uri
		,[Type]		    = @Type
		,[IsActive]	    = @IsActive
		,[XMLGuide]	    = @XMLGuide
		,[BaseUri]	    = @BaseUri
		,[IsNewsSource] = @IsNewsSource
	WHERE [Name]	= @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[DataSourcesSet]
		([Name]
		,[Uri]
		,[Type]
		,[IsActive]
		,[XMLGuide]
		,[BaseUri]
		,[IsNewsSource])
	VALUES
		(@Name
		,@Uri
		,@Type
		,@IsActive
		,@XMLGuide
		,@BaseUri
		,@IsNewsSource)
END
GO
--========================================================
--========================================================
DECLARE @Name nvarchar(max)
	   ,@Uri nvarchar(max)
	   ,@Type int
	   ,@IsActive bit
	   ,@XMLGuide nvarchar(max)
	   ,@BaseUri nvarchar(max)
	   ,@IsNewsSource bit

SELECT @Name		= N'mainfun'
	   ,@Uri		= N'http://mainfun.ru/'
	   ,@Type		= 3
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
		 [Name]		    = @Name
		,[Uri]		    = @Uri
		,[Type]		    = @Type
		,[IsActive]	    = @IsActive
		,[XMLGuide]	    = @XMLGuide
		,[BaseUri]	    = @BaseUri
		,[IsNewsSource] = @IsNewsSource
	WHERE [Name]	= @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[DataSourcesSet]
		([Name]
		,[Uri]
		,[Type]
		,[IsActive]
		,[XMLGuide]
		,[BaseUri]
		,[IsNewsSource])
	VALUES
		(@Name
		,@Uri
		,@Type
		,@IsActive
		,@XMLGuide
		,@BaseUri
		,@IsNewsSource)
END
GO
--========================================================
--========================================================
DECLARE @Name nvarchar(max)
	   ,@Uri nvarchar(max)
	   ,@Type int
	   ,@IsActive bit
	   ,@XMLGuide nvarchar(max)
	   ,@BaseUri nvarchar(max)
	   ,@IsNewsSource bit

SELECT @Name		= N'joyreactor'
	   ,@Uri		= N'http://joyreactor.cc/'
	   ,@Type		= 4
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
		 [Name]		    = @Name
		,[Uri]		    = @Uri
		,[Type]		    = @Type
		,[IsActive]	    = @IsActive
		,[XMLGuide]	    = @XMLGuide
		,[BaseUri]	    = @BaseUri
		,[IsNewsSource] = @IsNewsSource
	WHERE [Name]	= @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[DataSourcesSet]
		([Name]
		,[Uri]
		,[Type]
		,[IsActive]
		,[XMLGuide]
		,[BaseUri]
		,[IsNewsSource])
	VALUES
		(@Name
		,@Uri
		,@Type
		,@IsActive
		,@XMLGuide
		,@BaseUri
		,@IsNewsSource)
END
GO
--========================================================
--========================================================
DECLARE @Name nvarchar(max)
	   ,@Email nvarchar(max)
	   ,@Password nvarchar(max)
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
--========================================================
DECLARE @Name nvarchar(max)
	   ,@Description nvarchar(max)
	   ,@View nvarchar(max)
	   ,@Version int
	   ,@Type tinyint
	   ,@User_Id int

SELECT @Name			= N'ImgContainer'
	   ,@Description	= N'Image Container'
	   ,@View			= N'<img-container link="{ContainerValue}" active-image="activeImage" images="images"></img-container>'
	   ,@Version		= NULL
	   ,@Type			= 0
	   ,@User_Id		= 1

IF EXISTS(SELECT * FROM [dbo].[TemplateSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[TemplateSet]
	SET
		 [Name]			= @Name
		,[Description]	= @Description
		,[View]			= @View
		,[Version]		= @Version
		,[Type]			= @Type
		,[User_Id]		= @User_Id
	WHERE [Name]		= @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[TemplateSet]
		([Name]
		,[Description]
		,[View]
		,[Version]
		,[Type]
		,[User_Id])
	VALUES
		(@Name
		,@Description
		,@View
		,@Version
		,@Type
		,@User_Id)
END
GO
--========================================================
--========================================================
DECLARE @Name nvarchar(max)
	   ,@Description nvarchar(max)
	   ,@View nvarchar(max)
	   ,@Version int
	   ,@Type tinyint
	   ,@User_Id int

SELECT @Name			= N'ImageGalleryContainer'
	   ,@Description	= N'Image Gallery Container'
	   ,@View			= N'<image-gallery-container>{ContainerValue}</image-gallery-container>'
	   ,@Version		= NULL
	   ,@Type			= 0
	   ,@User_Id		= 1

IF EXISTS(SELECT * FROM [dbo].[TemplateSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[TemplateSet]
	SET
		 [Name]			= @Name
		,[Description]	= @Description
		,[View]			= @View
		,[Version]		= @Version
		,[Type]			= @Type
		,[User_Id]		= @User_Id
	WHERE [Name]		= @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[TemplateSet]
		([Name]
		,[Description]
		,[View]
		,[Version]
		,[Type]
		,[User_Id])
	VALUES
		(@Name
		,@Description
		,@View
		,@Version
		,@Type
		,@User_Id)
END
GO
--========================================================
--========================================================
DECLARE @Name nvarchar(max)
	   ,@Description nvarchar(max)
	   ,@View nvarchar(max)
	   ,@Version int
	   ,@Type tinyint
	   ,@User_Id int

SELECT @Name			= N'AudioContainer'
	   ,@Description	= N'Audio Container'
	   ,@View			= N'<audio-container link="{ContainerValue}" artist="{MediaAuthor}" name="{MediaName}"></audio-container>'
	   ,@Version		= NULL
	   ,@Type			= 0
	   ,@User_Id		= 1

IF EXISTS(SELECT * FROM [dbo].[TemplateSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[TemplateSet]
	SET
		 [Name]			= @Name
		,[Description]	= @Description
		,[View]			= @View
		,[Version]		= @Version
		,[Type]			= @Type
		,[User_Id]		= @User_Id
	WHERE [Name]		= @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[TemplateSet]
		([Name]
		,[Description]
		,[View]
		,[Version]
		,[Type]
		,[User_Id])
	VALUES
		(@Name
		,@Description
		,@View
		,@Version
		,@Type
		,@User_Id)
END
GO
--========================================================
--========================================================
DECLARE @Name nvarchar(max)
	   ,@Description nvarchar(max)
	   ,@View nvarchar(max)
	   ,@Version int
	   ,@Type tinyint
	   ,@User_Id int

SELECT @Name			= N'VideoContainer'
	   ,@Description	= N'Video Container'
	   ,@View			= N'<video-container link="{ContainerValue}" preview="{MediaPreview}" name="{MediaName}"></video-container>'
	   ,@Version		= NULl
	   ,@Type			= 0
	   ,@User_Id		= 1

IF EXISTS(SELECT * FROM [dbo].[TemplateSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[TemplateSet]
	SET
		 [Name]			= @Name
		,[Description]	= @Description
		,[View]			= @View
		,[Version]		= @Version
		,[Type]			= @Type
		,[User_Id]		= @User_Id
	WHERE [Name]		= @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[TemplateSet]
		([Name]
		,[Description]
		,[View]
		,[Version]
		,[Type]
		,[User_Id])
	VALUES
		(@Name
		,@Description
		,@View
		,@Version
		,@Type
		,@User_Id)
END
GO
--========================================================
--========================================================
DECLARE @Name nvarchar(max)
	   ,@Description nvarchar(max)
	   ,@View nvarchar(max)
	   ,@Version int
	   ,@Type tinyint
	   ,@User_Id int

SELECT @Name			= N'TextContainer'
	   ,@Description	= N'Text Container'
	   ,@View			= N'<div>{ContainerValue}</div>'
	   ,@Version		= NULl
	   ,@Type			= 0
	   ,@User_Id		= 1

IF EXISTS(SELECT * FROM [dbo].[TemplateSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[TemplateSet]
	SET
		 [Name]			= @Name
		,[Description]	= @Description
		,[View]			= @View
		,[Version]		= @Version
		,[Type]			= @Type
		,[User_Id]		= @User_Id
	WHERE [Name]		= @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[TemplateSet]
		([Name]
		,[Description]
		,[View]
		,[Version]
		,[Type]
		,[User_Id])
	VALUES
		(@Name
		,@Description
		,@View
		,@Version
		,@Type
		,@User_Id)
END
GO
--========================================================
--========================================================
DECLARE @Name nvarchar(max)
	   ,@Description nvarchar(max)
	   ,@View nvarchar(max)
	   ,@Version int
	   ,@Type tinyint
	   ,@User_Id int

SELECT @Name			= N'PostContainer'
	   ,@Description	= N'Post Container'
	   ,@View			= N'<post-container>{ContainerValue}</post-container>'
	   ,@Version		= NULL
	   ,@Type			= 0
	   ,@User_Id		= 1

IF EXISTS(SELECT * FROM [dbo].[TemplateSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[TemplateSet]
	SET
		 [Name]			= @Name
		,[Description]	= @Description
		,[View]			= @View
		,[Version]		= @Version
		,[Type]			= @Type
		,[User_Id]		= @User_Id
	WHERE [Name]		= @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[TemplateSet]
		([Name]
		,[Description]
		,[View]
		,[Version]
		,[Type]
		,[User_Id])
	VALUES
		(@Name
		,@Description
		,@View
		,@Version
		,@Type
		,@User_Id)
END
GO
--========================================================
--========================================================
DECLARE @Name nvarchar(max)
	   ,@Description nvarchar(max)
	   ,@View nvarchar(max)
	   ,@Version int
	   ,@Type tinyint
	   ,@User_Id int

SELECT @Name			= N'TempHeader'
	   ,@Description	= N'Temp header'
	   ,@View			= N'<post-container ng-repeat="post in postList" post-id="{{post.Id}}" post-object="post"></post-container>'
	   ,@Version		= NULL
	   ,@Type			= 2
	   ,@User_Id		= 1

IF EXISTS(SELECT * FROM [dbo].[TemplateSet] WHERE [Name] like @Name)
BEGIN
	UPDATE [dbo].[TemplateSet]
	SET
		 [Name]			= @Name
		,[Description]	= @Description
		,[View]			= @View
		,[Version]		= @Version
		,[Type]			= @Type
		,[User_Id]		= @User_Id
	WHERE [Name]		= @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[TemplateSet]
		([Name]
		,[Description]
		,[View]
		,[Version]
		,[Type]
		,[User_Id])
	VALUES
		(@Name
		,@Description
		,@View
		,@Version
		,@Type
		,@User_Id)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255)
	   ,@Value nvarchar(max)
	   ,@ForUI bit

SELECT @Key		= N'ExceptionOccured'
	   ,@Value	= N'An error has occurred in the system, and the administrator of the application has been notified.'
	   ,@ForUI  = 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key]		= @Key
		,[Value]	= @Value
		,[ForUI]    = @ForUI
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key]
		,[Value]
		,[ForUI])
	VALUES
		(@Key
		,@Value
		,@ForUI)
END
GO
--========================================================
--========================================================
DECLARE @Name nvarchar(max)
	   ,@Uri nvarchar(max)
	   ,@Type int
	   ,@IsActive bit
	   ,@XMLGuide nvarchar(max)
	   ,@BaseUri nvarchar(max)
	   ,@IsNewsSource bit

SELECT @Name		= N'ZaycevNet'
	   ,@Uri		= N'http://zaycev.net/search.html'
	   ,@Type		= 0
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
		 [Name]		    = @Name
		,[Uri]		    = @Uri
		,[Type]		    = @Type
		,[IsActive]	    = @IsActive
		,[XMLGuide]	    = @XMLGuide
		,[BaseUri]	    = @BaseUri
		,[IsNewsSource] = @IsNewsSource
	WHERE [Name]	= @Name
END
ELSE
BEGIN
	INSERT INTO [dbo].[DataSourcesSet]
		([Name]
		,[Uri]
		,[Type]
		,[IsActive]
		,[XMLGuide]
		,[BaseUri]
		,[IsNewsSource])
	VALUES
		(@Name
		,@Uri
		,@Type
		,@IsActive
		,@XMLGuide
		,@BaseUri
		,@IsNewsSource)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255)
	   ,@Value nvarchar(max)
	   ,@ForUI bit

SELECT @Key		= N'CantPlaySong'
	   ,@Value	= N'Can`t play this song, there are an network problem or this song is not available in you`r location'
	   ,@ForUI  = 1

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key]		= @Key
		,[Value]	= @Value
		,[ForUI]    = @ForUI
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key]
		,[Value]
		,[ForUI])
	VALUES
		(@Key
		,@Value
		,@ForUI)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255)
	   ,@Value nvarchar(max)
	   ,@ForUI bit

SELECT @Key		= N'OnlineRadioTypeAheadPostfix'
	   ,@Value	= N'suggest4'
	   ,@ForUI  = 0

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key]		= @Key
		,[Value]	= @Value
		,[ForUI]    = @ForUI
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key]
		,[Value]
		,[ForUI])
	VALUES
		(@Key
		,@Value
		,@ForUI)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255)
	   ,@Value nvarchar(max)
	   ,@ForUI bit

SELECT @Key		= N'SongInBlackList'
	   ,@Value	= N'Song has been added to a black list'
	   ,@ForUI  = 1

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key]		= @Key
		,[Value]	= @Value
		,[ForUI]    = @ForUI
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key]
		,[Value]
		,[ForUI])
	VALUES
		(@Key
		,@Value
		,@ForUI)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255)
	   ,@Value nvarchar(max)
	   ,@ForUI bit

SELECT @Key		= N'IpInfoLink'
	   ,@Value	= N'http://ipinfo.io'
	   ,@ForUI  = 1

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key]		= @Key
		,[Value]	= @Value
		,[ForUI]    = @ForUI
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key]
		,[Value]
		,[ForUI])
	VALUES
		(@Key
		,@Value
		,@ForUI)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255)
	   ,@Value nvarchar(max)
	   ,@ForUI bit

SELECT @Key		= N'VideoJSCantPlay'
	   ,@Value	= N'To view this video please enable JavaScript, and consider upgrading to a web browser that <a href="http://videojs.com/html5-video-support/" target="_blank">supports HTML5 video</a>'
	   ,@ForUI  = 1

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key]		= @Key
		,[Value]	= @Value
		,[ForUI]    = @ForUI
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key]
		,[Value]
		,[ForUI])
	VALUES
		(@Key
		,@Value
		,@ForUI)
END
GO
--========================================================
--========================================================
DECLARE @Key nvarchar(255)
	   ,@Value nvarchar(max)
	   ,@ForUI bit

SELECT @Key		= N'VideoJSNotSupportedFormat'
	   ,@Value	= N'This video fromat is not supported, please check http://videojs.com/html5-video-support/ for supported HTML5 video formats</a>'
	   ,@ForUI  = 1

IF EXISTS(SELECT * FROM [dbo].[SettingsSet] WHERE [Key] like @Key)
BEGIN
	UPDATE [dbo].[SettingsSet]
	SET
		 [Key]		= @Key
		,[Value]	= @Value
		,[ForUI]    = @ForUI
	WHERE [Key]	= @Key
END
ELSE
BEGIN
	INSERT INTO [dbo].[SettingsSet]
		([Key]
		,[Value]
		,[ForUI])
	VALUES
		(@Key
		,@Value
		,@ForUI)
END
GO
--========================================================