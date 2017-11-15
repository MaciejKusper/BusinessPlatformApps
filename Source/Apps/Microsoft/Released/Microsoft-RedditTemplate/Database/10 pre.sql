SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ANSI_NULL_DFLT_ON ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET QUOTED_IDENTIFIER ON;
go

-- Drop views
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='AllPostsView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.AllPostsView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='AllCommentsView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.AllCommentsView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='AllDocumentsView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.AllDocumentsView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='PostAuthorCountsView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.PostAuthorCountsView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='CommentAuthorCountsView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.CommentAuthorCountsView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='PostCoauthorsView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.PostCoauthorsView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='CommentCoauthorsView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.CommentCoauthorsView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='PostAndCommentCoAuthorsView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.PostAndCommentCoAuthorsView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='SubRedditPostCountView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.SubRedditPostCountView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='CommentCountView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.CommentCountView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='AllEntitiesView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.AllEntitiesView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='UniqueSubredditsView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.UniqueSubredditsView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='KeyPhraseCountView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.KeyPhraseCountView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='CoAuthorView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.CoAuthorView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='CommentCountPerDocumentView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.CommentCountPerDocumentView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='SubredditDocumentCountView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.SubredditDocumentCountView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='SubredditMappingView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.SubredditMappingView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='EmbeddedUrlSubredditView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.EmbeddedUrlSubredditView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='EmbeddedUrlDomainCountView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.EmbeddedUrlDomainCountView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='TopDocumentsByDayView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.TopDocumentsByDayView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='WeekOverWeekView' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.WeekOverWeekView;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='ChordChartSubredditAuthorConnections' AND TABLE_TYPE='VIEW')
    DROP VIEW reddit.ChordChartSubredditAuthorConnections;


-- Drop tables
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='Posts' AND TABLE_TYPE='BASE TABLE')
	DROP TABLE reddit.Posts;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='Comments' AND TABLE_TYPE='BASE TABLE')
	DROP TABLE reddit.Comments;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='KeyPhrases' AND TABLE_TYPE='BASE TABLE')
	DROP TABLE reddit.KeyPhrases;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='Entities' AND TABLE_TYPE='BASE TABLE')
	DROP TABLE reddit.Entities;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='Sentiment' AND TABLE_TYPE='BASE TABLE')
	DROP TABLE reddit.Sentiment;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='EmbeddedUrls' AND TABLE_TYPE='BASE TABLE')
	DROP TABLE reddit.EmbeddedUrls;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='Documents' AND TABLE_TYPE='BASE TABLE')
	DROP TABLE reddit.Documents;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='UserDefinedEntities' AND TABLE_TYPE='BASE TABLE')
	DROP TABLE reddit.UserDefinedEntities;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='UserDefinedEntityDefinitions' AND TABLE_TYPE='BASE TABLE')
	DROP TABLE reddit.UserDefinedEntityDefinitions;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='Staging_Sentiment' AND TABLE_TYPE='BASE TABLE')
	DROP TABLE reddit.Staging_Sentiment;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='Staging_Entities' AND TABLE_TYPE='BASE TABLE')
	DROP TABLE reddit.Staging_Entities;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='reddit' AND TABLE_NAME='Staging_KeyPhrases' AND TABLE_TYPE='BASE TABLE')
	DROP TABLE reddit.Staging_KeyPhrases;


-- Drop Stored Procedures
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA='reddit' AND ROUTINE_NAME='InsertPost' AND ROUTINE_TYPE='PROCEDURE')
    DROP PROCEDURE reddit.InsertPost;
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA='reddit' AND ROUTINE_NAME='InsertComment' AND ROUTINE_TYPE='PROCEDURE')
    DROP PROCEDURE reddit.InsertComment;
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA='reddit' AND ROUTINE_NAME='DeleteDocument' AND ROUTINE_TYPE='PROCEDURE')
    DROP PROCEDURE reddit.DeleteDocument;
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA='reddit' AND ROUTINE_NAME='DeleteDocuments' AND ROUTINE_TYPE='PROCEDURE')
    DROP PROCEDURE reddit.DeleteDocuments;
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA='reddit' AND ROUTINE_NAME='PostAzureML' AND ROUTINE_TYPE='PROCEDURE')
    DROP PROCEDURE reddit.PostAzureML;

GO

-- Drop types
-- would prefer not to use sys tables, but INFORMATION_SCHEMA views don't seem to have what we need
IF EXISTS(SELECT * FROM SYS.TYPES WHERE NAME='DocumentIdTable' AND IS_USER_DEFINED=1)
	DROP TYPE reddit.DocumentIdTable;
IF EXISTS(SELECT * FROM SYS.TYPES WHERE NAME='DOC_ID' AND IS_USER_DEFINED=1)
	DROP TYPE reddit.DOC_ID;
