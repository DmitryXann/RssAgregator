USE RssAggregator
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
	   ,@View			= N'<img-container link="{ContainerValue}"></img-container>'
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

SELECT @Name			= N'VideoGalleryContainer'
	   ,@Description	= N'Video Gallery Container'
	   ,@View			= N'<video-gallery-container>{ContainerValue}</video-gallery-container>'
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

SELECT @Name			= N'TempHeader'
	   ,@Description	= N'Temp header'
	   ,@View			= N'<post-container ng-repeat="post in postList" post-id="{{::post.Id}}" post-object="::post" class="{{::post.DataSource}}"></post-container>'
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
DECLARE @Name nvarchar(max)
	   ,@Description nvarchar(max)
	   ,@View nvarchar(max)
	   ,@Version int
	   ,@Type tinyint
	   ,@User_Id int

SELECT @Name			= N'dashboardController'
	   ,@Description	= N'dashboardController'
	   ,@View			= N'<bind-html-compile source-html="::headerView"></bind-html-compile>'
	   ,@Version		= 0
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

SELECT @Name			= N'audioPlayer'
	   ,@Description	= N'audioPlayer'
	   ,@View			= N'<div>
    <div>
        <div class="audio-player">
            <div ng-click="playPrevious()">Previous</div>
            <div ng-click="payPause()">Play\Pause</div>
            <div ng-click="playNext()">Next</div>
        </div>
        <audio-player-progress-bar current-playing-song="currentPlayingSong" seek="seek"></audio-player-progress-bar>
        <div>
            <div ng-click="repeatAll()">Repeat</div>
            <div ng-click="shuffle()">Shuffle</div>
        </div>
        <div>
            <input type="text" ng-model="searchQuestion" placeholder="Enter song or artist" uib-typeahead="songs for songs in typeAheadSearchResult($viewValue)" typeahead-loading="typeAheadLoading" typeahead-no-results="typeAheadNoResults" class="form-control" ng-enter="search()">
            <i ng-show="typeAheadLoading" class="glyphicon glyphicon-refresh"></i>
            <div ng-show="typeAheadNoResults">
                <i class="glyphicon glyphicon-remove"></i> No Results Found
            </div>
            <button type="button" class="btn btn-default" ng-click="search()" ng-disabled="!searchQuestion">Search</button>
        </div>
    </div>
    <div class="audio-pay-list">
        <div>
            <uib-tabset active="activceTabIndex">
                <uib-tab heading="Play List">
                    <table>
                        <tr>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                            <th></th>
                        </tr>
                        <tr ng-repeat="el in playList">
                            <td><button type="button" class="btn btn-default" ng-click="play(el)">Play</button></td>
                            <td ng-bind="::el.artist"></td>
                            <td ng-bind="::el.name"></td>
                            <td ng-bind="el.userFriendlyEstimateDuration"></td>
                            <td><button type="button" class="btn btn-default" ng-click="deleteFromPlayList(el)">Delete</button></td>
                        </tr>
                    </table>
                </uib-tab>
                <uib-tab heading="Search Result">
                    <table>
                        <tr>
                            <th></th>
                            <th></th>
                            <th></th>
                        </tr>
                        <tr ng-repeat="el in searchResult">
                            <td ng-bind="::el.artist"></td>
                            <td ng-bind="::el.name"></td>
                            <td><button type="button" class="btn btn-default" ng-click="addToPlayList(el)">Add to playlist</button></td>
                        </tr>
                    </table>
                </uib-tab>
            </uib-tabset>
        </div>
    </div>
</div>'
	   ,@Version		= 0
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

SELECT @Name			= N'audioPlayerProgressBar'
	   ,@Description	= N'audioPlayerProgressBar'
	   ,@View			= N'<uib-progressbar max="maxSongDuration" value="currentSongProgress()"
                 ng-mousemove="mouseMove($event)" 
                 ng-mousedown="mouseClickToggle($event, true)" ng-mouseup="mouseClickToggle($event, false)"
                 ng-mouseleave="mouseEnterToggle(false)" ng-mouseenter="mouseEnterToggle(true)"
                 uib-tooltip="{{approximateSongProgress()}}" tooltip-enable="currentPlayingSong">
    <span class="song-name" ng-bind="currentSongName"></span> <span class="song-duraion" ng-bind="currentSongUserFriendlyProgress()"></span>
</uib-progressbar>'
	   ,@Version		= 0
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

SELECT @Name			= N'videoPlayer'
	   ,@Description	= N'videoPlayer'
	   ,@View			= N'<video class="video-js">
    <p class="vjs-no-js" ng-bind-html="::cantPlayVideo"></p>
</video>'
	   ,@Version		= 0
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

SELECT @Name			= N'audioContainer'
	   ,@Description	= N'audioContainer'
	   ,@View			= N'<button type="button" class="btn btn-default" ng-click="play()" ng-disabled="!link">Play <span ng-bind="::artist"></span> - <span ng-bind="::name"></span></button>'
	   ,@Version		= 0
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

SELECT @Name			= N'imageGalleryContainer'
	   ,@Description	= N'imageGalleryContainer'
	   ,@View			= N'<img ng-src="{{activeImage.link}}">
<button role="button" class="btn btn-default" ng-click="previous()" ng-if="::canChangeImage">Previous</button>
<button role="button" class="btn btn-default" ng-click="next()" ng-if="::canChangeImage">Next</button>
<div transclude-img></div>'
	   ,@Version		= 0
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

SELECT @Name			= N'imgContainer'
	   ,@Description	= N'imgContainer'
	   ,@View			= N'<img ng-src="{{::link}}" ng-click="activate()" is-active="{{isActive}}">'
	   ,@Version		= 0
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

SELECT @Name			= N'postContainer'
	   ,@Description	= N'postContainer'
	   ,@View			= N'<div ng-bind="::authorName"></div>
<div ng-bind="::authorLink"></div>
<div ng-bind="::postName"></div>
<div ng-bind="::getAuthorLink()"></div>
<div ng-bind="::getPostLink()"></div>
<button role="button" class="btn btn-default" ng-click="likeToggle(true)" ng-class="{''active'': likeToggleActive(true)}">+</button>
<div ng-bind="postLikes"></div>
<button role="button" class="btn btn-default" ng-click="likeToggle(false)" ng-class="{''active'': likeToggleActive(false)}">-</button>

<bind-html-compile source-html="::postView"></bind-html-compile>'
	   ,@Version		= 0
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

SELECT @Name			= N'videoContainer'
	   ,@Description	= N'videoContainer'
	   ,@View			= N'<div transclude-video class="transclude-video-active" ng-class="{''transclude-video-inactive'': videoNotActive}" ng-click="videoClick($event)"></div>'
	   ,@Version		= 0
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

SELECT @Name			= N'videoGalleryContainer'
	   ,@Description	= N'videoGalleryContainer'
	   ,@View			= N'<div transclude-video-containers></div>
<button role="button" class="btn btn-default" ng-click="previous()" ng-if="::canChangeVideos">Previous</button>
<button role="button" class="btn btn-default" ng-click="next()" ng-if="::canChangeVideos">Next</button>'
	   ,@Version		= 0
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