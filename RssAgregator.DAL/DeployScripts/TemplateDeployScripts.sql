USE RssAggregator

IF EXISTS(SELECT * FROM [dbo].[DataSourcesSet])
BEGIN
	DELETE [dbo].[TemplateSet]
END
--========================================================
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

SELECT @Name			= N'dashboardController'
	   ,@Description	= N'dashboardController'
	   ,@View			= N'<ng-bind-html-compile source-html="::headerView"></ng-bind-html-compile>'
	   ,@Version		= 0
	   ,@Type			= 0
	   ,@User_Id		= 1

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

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

<ng-bind-html-compile source-html="::postView"></ng-bind-html-compile>'
	   ,@Version		= 0
	   ,@Type			= 0
	   ,@User_Id		= 1

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

SELECT @Name			= N'aboutController'
	   ,@Description	= N'aboutController'
	   ,@View			= N'<span>
    This is an information about this really cool resoutce :)
</span>
<uib-accordion close-others="true">
    <uib-accordion-group heading="Site license" is-open="true">
        [THIS IS THE SITE LICENCE]
    </uib-accordion-group>
	<uib-accordion-group heading="jQuery: The MIT License">
		<pre>
The MIT License

Copyright (c) 2006-2016 jQuery, https://jquery.org/license/

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
        </pre>
    </uib-accordion-group>
    <uib-accordion-group heading="AngularJS: The MIT License">
        <pre>
The MIT License

Copyright (c) 2010-2016 Google, Inc. http://angularjs.org

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
       </pre>
    </uib-accordion-group>
    <uib-accordion-group heading="UI Bootstrap: The MIT License">
        <pre>
The MIT License

Copyright (c) 2012-2016 the AngularUI Team, https://github.com/organizations/angular-ui/teams/291112

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
        </pre>
    </uib-accordion-group>
    <uib-accordion-group heading="Toastr: The MIT License">
        <pre>
The MIT License

Copyright (c) 2012-2015 Toastr, https://github.com/CodeSeven/toastr

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
        </pre>
    </uib-accordion-group>
	<uib-accordion-group heading="WordCloud 2: The MIT License">
		<pre>
The MIT License

Copyright (c) 2011 - 2013 Tim Chien, http://timdream.org/wordcloud2.js/

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
        </pre>
    </uib-accordion-group>
	<uib-accordion-group heading="ContentTools: The MIT License">
		<pre>
The MIT License

Copyright (c) 2014 Getme Limited (http://getme.co.uk)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
        </pre>
    </uib-accordion-group>
    <uib-accordion-group heading="SoundManager 2: BSD License">
        <pre>
Software License Agreement (BSD License)

Copyright (c) 2007, Scott Schiller (schillmania.com)
All rights reserved.

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this 
  list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice, this
  list of conditions and the following disclaimer in the documentation and/or
  other materials provided with the distribution.

* Neither the name of schillmania.com nor the names of its contributors may be
  used to endorse or promote products derived from this software without
  specific prior written permission from schillmania.com.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
        </pre>
    </uib-accordion-group>
    <uib-accordion-group heading="Video.js: Apache License, Version 2.0">
        <pre>
Copyright Brightcove, Inc.

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
        </pre>
    </uib-accordion-group>
</uib-accordion>'
	   ,@Version		= 0
	   ,@Type			= 0
	   ,@User_Id		= 1

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

SELECT @Name			= N'wordCloud'
	   ,@Description	= N'wordCloud'
	   ,@View			= N'<canvas></canvas>'
	   ,@Version		= 0
	   ,@Type			= 0
	   ,@User_Id		= 1

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

SELECT @Name			= N'addEditPostController'
	   ,@Description	= N'addEditPostController'
	   ,@View			= N'<span>
    Start by pressing the "Pencil icon" and make changes in glowind area below:
</span>
<div class="post-add-edit">
    <div><span>Post name:</span><input type="text" ng-model="postName" ng-change="postDataOnChange(2, ''postName'', ''invalidPostName'')" /></div>
    <div ng-show="invalidPostName"><span ng-bind="invalidPostNameMessage"></span></div>

    <div><span>Post tags separated by coma:</span><input type="text" ng-model="postTags" ng-change="postDataOnChange(2, ''postTags'', ''invalidPostTag'')" /></div>
    <div ng-show="invalidPostTag"><span ng-bind="invalidPostTagMessage"></span></div>

    <div><span>Is Adult content:</span><input type="checkbox" ng-model="isAdult" /></div>
    <div class="post-editable-container" data-editable data-name="main-content" ng-bind-html="::postContent">
    </div>
</div>'
	   ,@Version		= 0
	   ,@Type			= 0
	   ,@User_Id		= 1

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

SELECT @Name			= N'navigation'
	   ,@Description	= N'navigation'
	   ,@View			= N'<div>
    <div ng-repeat="item in ::navigationData | orderBy : ''OrderNo''">
        <span ng-bind="::item.Title" ng-click="redirect(item.RedirectTo)"></span>
    </div>
</div>'
	   ,@Version		= 0
	   ,@Type			= 0
	   ,@User_Id		= 1

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
--========================================================
DECLARE @Name nvarchar(255), @Description nvarchar(255), @View nvarchar(max), @Version int, @Type tinyint, @User_Id int

SELECT @Name			= N'login'
	   ,@Description	= N'login'
	   ,@View			= N'<div ng-show="!userIsLoggedIn">
    <uib-tabset active="activceTabIndex">
        <uib-tab heading="LogIn">
            <span>UserName: </span><input type="text" ng-model="userData.userName">
            <span>Password: </span><input type="password" ng-model="userData.password" ng-enter="login()">
            <span>Stay in the system </span><input type="checkbox" ng-model="userData.stayInTheSystem">
            <button type="button" class="btn btn-default" ng-click="login()" ng-disabled="!userData.userDataIsValid(false)">Login</button>
        </uib-tab>
        <uib-tab heading="Create an account">
            <div ng-init="userData.minNameLength = 3"><span>UserName: </span><input type="text" ng-model="userData.userName" ng-change="validateName(userData.userName, userData.minNameLength)"></div>
            <span ng-bind="::userNameAlreadyExistsError" ng-show="userData.userName && !userData.userNameIsValid"></span>
            <span ng-bind="::tooShortUserName" ng-show="userData.userName && userData.userName.length < userData.minNameLength"></span>

            <div><span>Email: </span><input type="text" ng-model="userData.email" ng-change="validateEmail(userData.email, 5)"></div>
            <span ng-bind="::incorrectEmailAdress" ng-show="userData.email && !userData.emailIsValid"></span>
            <span ng-bind="::emailAlreadyExistsError" ng-show="userData.email && !userData.emailIsNotTaken"></span>

            <div ng-init="userData.minPasswordLength = 5"><span>Password: </span><input type="password" ng-model="userData.password"></div>
            <div><span>Password: </span><input type="password" ng-model="userData.secondTimePassword"></div>
            <span ng-bind="::incorrectPasswords" ng-show="userData.password && userData.secondTimePassword && (userData.password.length < userData.minPasswordLength || userData.secondTimePassword.length < userData.minPasswordLength || userData.password !== userData.secondTimePassword)"></span>

            <span>Stay in the system </span><input type="checkbox" ng-model="userData.stayInTheSystem">
            <button type="button" class="btn btn-default" ng-click="create()" ng-disabled="!userData.userDataIsValid(true)">Create</button>
        </uib-tab>
    </uib-tabset>
</div>
<div ng-show="userIsLoggedIn">
    <span>Hello, {{::userName}}</span>
    <button type="button" class="btn btn-default" ng-click="logOut()">LogOut</button>
</div>'
	   ,@Version		= 0
	   ,@Type			= 0
	   ,@User_Id		= 1

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