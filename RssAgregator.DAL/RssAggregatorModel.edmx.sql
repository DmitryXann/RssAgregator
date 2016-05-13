
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/13/2016 02:39:52
-- Generated from EDMX file: C:\Users\Дмитрий\Documents\RssAgregator\RssAgregator\RssAgregator.DAL\RssAggregatorModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RssAggregator];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_NewsDataSources]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NewsSet] DROP CONSTRAINT [FK_NewsDataSources];
GO
IF OBJECT_ID(N'[dbo].[FK_NewsUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NewsSet] DROP CONSTRAINT [FK_NewsUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersTemplates]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TemplateSet] DROP CONSTRAINT [FK_UsersTemplates];
GO
IF OBJECT_ID(N'[dbo].[FK_NewsComments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentsSet] DROP CONSTRAINT [FK_NewsComments];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersComments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentsSet] DROP CONSTRAINT [FK_UsersComments];
GO
IF OBJECT_ID(N'[dbo].[FK_UserImage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsersImageSet] DROP CONSTRAINT [FK_UserImage];
GO
IF OBJECT_ID(N'[dbo].[FK_NewsUsersImage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsersImageSet] DROP CONSTRAINT [FK_NewsUsersImage];
GO
IF OBJECT_ID(N'[dbo].[FK_UserNewDataSourceRequest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NewDataSourceRequestSet] DROP CONSTRAINT [FK_UserNewDataSourceRequest];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserFeedback]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserFeedbackSet] DROP CONSTRAINT [FK_UserUserFeedback];
GO
IF OBJECT_ID(N'[dbo].[FK_UserMessagesUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMessagesSet] DROP CONSTRAINT [FK_UserMessagesUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UserMessagesUser1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMessagesSet] DROP CONSTRAINT [FK_UserMessagesUser1];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[DataSourcesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DataSourcesSet];
GO
IF OBJECT_ID(N'[dbo].[NewsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NewsSet];
GO
IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[TemplateSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TemplateSet];
GO
IF OBJECT_ID(N'[dbo].[CommentsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CommentsSet];
GO
IF OBJECT_ID(N'[dbo].[UsersImageSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsersImageSet];
GO
IF OBJECT_ID(N'[dbo].[IconsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IconsSet];
GO
IF OBJECT_ID(N'[dbo].[LogSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LogSet];
GO
IF OBJECT_ID(N'[dbo].[NewDataSourceRequestSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NewDataSourceRequestSet];
GO
IF OBJECT_ID(N'[dbo].[UserFeedbackSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserFeedbackSet];
GO
IF OBJECT_ID(N'[dbo].[UserMessagesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserMessagesSet];
GO
IF OBJECT_ID(N'[dbo].[SettingsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SettingsSet];
GO
IF OBJECT_ID(N'[dbo].[SongsBlackListSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SongsBlackListSet];
GO
IF OBJECT_ID(N'[dbo].[TransliterationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransliterationSet];
GO
IF OBJECT_ID(N'[dbo].[NavigationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NavigationSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'DataSourcesSet'
CREATE TABLE [dbo].[DataSourcesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Uri] nvarchar(max)  NOT NULL,
    [Type] int  NOT NULL,
    [IsActive] bit  NOT NULL,
    [XMLGuide] nvarchar(max)  NOT NULL,
    [BaseUri] nvarchar(max)  NOT NULL,
    [IsNewsSource] bit  NOT NULL
);
GO

-- Creating table 'NewsSet'
CREATE TABLE [dbo].[NewsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PostId] nvarchar(max)  NOT NULL,
    [AuthorName] nvarchar(50)  NOT NULL,
    [AuthorId] nvarchar(50)  NOT NULL,
    [AuthorLink] nvarchar(255)  NULL,
    [PostLikes] int  NOT NULL,
    [PostName] nvarchar(255)  NULL,
    [PostLink] nvarchar(max)  NULL,
    [PostContent] nvarchar(max)  NOT NULL,
    [PostTags] nvarchar(max)  NOT NULL,
    [External] bit  NOT NULL,
    [IsActive] bit  NOT NULL,
    [AdultContent] bit  NOT NULL,
    [CreationDateTime] datetime  NOT NULL,
    [ModificationDateTime] datetime  NOT NULL,
    [DataSource_Id] int  NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [Type] tinyint  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'TemplateSet'
CREATE TABLE [dbo].[TemplateSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Description] nvarchar(255)  NOT NULL,
    [View] nvarchar(max)  NOT NULL,
    [Version] int  NULL,
    [Type] tinyint  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'CommentsSet'
CREATE TABLE [dbo].[CommentsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL,
    [RespondTo] int  NULL,
    [News_Id] int  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'UsersImageSet'
CREATE TABLE [dbo].[UsersImageSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Uri] nvarchar(max)  NOT NULL,
    [User_Id] int  NOT NULL,
    [News_Id] int  NOT NULL
);
GO

-- Creating table 'IconsSet'
CREATE TABLE [dbo].[IconsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Uri] nvarchar(max)  NOT NULL,
    [Name] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'LogSet'
CREATE TABLE [dbo].[LogSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Message] nvarchar(max)  NOT NULL,
    [StackTrace] nvarchar(max)  NOT NULL,
    [Source] nvarchar(max)  NOT NULL,
    [DateTime] datetime  NOT NULL,
    [Type] tinyint  NOT NULL
);
GO

-- Creating table 'NewDataSourceRequestSet'
CREATE TABLE [dbo].[NewDataSourceRequestSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Uri] nvarchar(max)  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [Seen] bit  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'UserFeedbackSet'
CREATE TABLE [dbo].[UserFeedbackSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [Header] nvarchar(255)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [User_Id] int  NULL
);
GO

-- Creating table 'UserMessagesSet'
CREATE TABLE [dbo].[UserMessagesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Topic] nvarchar(255)  NOT NULL,
    [Message] nvarchar(max)  NOT NULL,
    [SendDateTime] datetime  NOT NULL,
    [Read] bit  NOT NULL,
    [FromUser_Id] int  NOT NULL,
    [ToUser_Id] int  NOT NULL
);
GO

-- Creating table 'SettingsSet'
CREATE TABLE [dbo].[SettingsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Key] nvarchar(255)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [ForUI] bit  NOT NULL
);
GO

-- Creating table 'SongsBlackListSet'
CREATE TABLE [dbo].[SongsBlackListSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SongURL] nvarchar(255)  NOT NULL,
    [Country] nvarchar(50)  NOT NULL,
    [City] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'TransliterationSet'
CREATE TABLE [dbo].[TransliterationSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FromLetter] nvarchar(10)  NOT NULL,
    [ToLetter] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'NavigationSet'
CREATE TABLE [dbo].[NavigationSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(25)  NOT NULL,
    [RedirectTo] nvarchar(25)  NOT NULL,
    [OrderNo] int  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'DataSourcesSet'
ALTER TABLE [dbo].[DataSourcesSet]
ADD CONSTRAINT [PK_DataSourcesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NewsSet'
ALTER TABLE [dbo].[NewsSet]
ADD CONSTRAINT [PK_NewsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TemplateSet'
ALTER TABLE [dbo].[TemplateSet]
ADD CONSTRAINT [PK_TemplateSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CommentsSet'
ALTER TABLE [dbo].[CommentsSet]
ADD CONSTRAINT [PK_CommentsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UsersImageSet'
ALTER TABLE [dbo].[UsersImageSet]
ADD CONSTRAINT [PK_UsersImageSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IconsSet'
ALTER TABLE [dbo].[IconsSet]
ADD CONSTRAINT [PK_IconsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LogSet'
ALTER TABLE [dbo].[LogSet]
ADD CONSTRAINT [PK_LogSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NewDataSourceRequestSet'
ALTER TABLE [dbo].[NewDataSourceRequestSet]
ADD CONSTRAINT [PK_NewDataSourceRequestSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserFeedbackSet'
ALTER TABLE [dbo].[UserFeedbackSet]
ADD CONSTRAINT [PK_UserFeedbackSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserMessagesSet'
ALTER TABLE [dbo].[UserMessagesSet]
ADD CONSTRAINT [PK_UserMessagesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SettingsSet'
ALTER TABLE [dbo].[SettingsSet]
ADD CONSTRAINT [PK_SettingsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SongsBlackListSet'
ALTER TABLE [dbo].[SongsBlackListSet]
ADD CONSTRAINT [PK_SongsBlackListSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TransliterationSet'
ALTER TABLE [dbo].[TransliterationSet]
ADD CONSTRAINT [PK_TransliterationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NavigationSet'
ALTER TABLE [dbo].[NavigationSet]
ADD CONSTRAINT [PK_NavigationSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DataSource_Id] in table 'NewsSet'
ALTER TABLE [dbo].[NewsSet]
ADD CONSTRAINT [FK_NewsDataSources]
    FOREIGN KEY ([DataSource_Id])
    REFERENCES [dbo].[DataSourcesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NewsDataSources'
CREATE INDEX [IX_FK_NewsDataSources]
ON [dbo].[NewsSet]
    ([DataSource_Id]);
GO

-- Creating foreign key on [User_Id] in table 'NewsSet'
ALTER TABLE [dbo].[NewsSet]
ADD CONSTRAINT [FK_NewsUsers]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NewsUsers'
CREATE INDEX [IX_FK_NewsUsers]
ON [dbo].[NewsSet]
    ([User_Id]);
GO

-- Creating foreign key on [User_Id] in table 'TemplateSet'
ALTER TABLE [dbo].[TemplateSet]
ADD CONSTRAINT [FK_UsersTemplates]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersTemplates'
CREATE INDEX [IX_FK_UsersTemplates]
ON [dbo].[TemplateSet]
    ([User_Id]);
GO

-- Creating foreign key on [News_Id] in table 'CommentsSet'
ALTER TABLE [dbo].[CommentsSet]
ADD CONSTRAINT [FK_NewsComments]
    FOREIGN KEY ([News_Id])
    REFERENCES [dbo].[NewsSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NewsComments'
CREATE INDEX [IX_FK_NewsComments]
ON [dbo].[CommentsSet]
    ([News_Id]);
GO

-- Creating foreign key on [User_Id] in table 'CommentsSet'
ALTER TABLE [dbo].[CommentsSet]
ADD CONSTRAINT [FK_UsersComments]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersComments'
CREATE INDEX [IX_FK_UsersComments]
ON [dbo].[CommentsSet]
    ([User_Id]);
GO

-- Creating foreign key on [User_Id] in table 'UsersImageSet'
ALTER TABLE [dbo].[UsersImageSet]
ADD CONSTRAINT [FK_UserImage]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserImage'
CREATE INDEX [IX_FK_UserImage]
ON [dbo].[UsersImageSet]
    ([User_Id]);
GO

-- Creating foreign key on [News_Id] in table 'UsersImageSet'
ALTER TABLE [dbo].[UsersImageSet]
ADD CONSTRAINT [FK_NewsUsersImage]
    FOREIGN KEY ([News_Id])
    REFERENCES [dbo].[NewsSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NewsUsersImage'
CREATE INDEX [IX_FK_NewsUsersImage]
ON [dbo].[UsersImageSet]
    ([News_Id]);
GO

-- Creating foreign key on [User_Id] in table 'NewDataSourceRequestSet'
ALTER TABLE [dbo].[NewDataSourceRequestSet]
ADD CONSTRAINT [FK_UserNewDataSourceRequest]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserNewDataSourceRequest'
CREATE INDEX [IX_FK_UserNewDataSourceRequest]
ON [dbo].[NewDataSourceRequestSet]
    ([User_Id]);
GO

-- Creating foreign key on [User_Id] in table 'UserFeedbackSet'
ALTER TABLE [dbo].[UserFeedbackSet]
ADD CONSTRAINT [FK_UserUserFeedback]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserFeedback'
CREATE INDEX [IX_FK_UserUserFeedback]
ON [dbo].[UserFeedbackSet]
    ([User_Id]);
GO

-- Creating foreign key on [FromUser_Id] in table 'UserMessagesSet'
ALTER TABLE [dbo].[UserMessagesSet]
ADD CONSTRAINT [FK_UserMessagesUser]
    FOREIGN KEY ([FromUser_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserMessagesUser'
CREATE INDEX [IX_FK_UserMessagesUser]
ON [dbo].[UserMessagesSet]
    ([FromUser_Id]);
GO

-- Creating foreign key on [ToUser_Id] in table 'UserMessagesSet'
ALTER TABLE [dbo].[UserMessagesSet]
ADD CONSTRAINT [FK_UserMessagesUser1]
    FOREIGN KEY ([ToUser_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserMessagesUser1'
CREATE INDEX [IX_FK_UserMessagesUser1]
ON [dbo].[UserMessagesSet]
    ([ToUser_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------