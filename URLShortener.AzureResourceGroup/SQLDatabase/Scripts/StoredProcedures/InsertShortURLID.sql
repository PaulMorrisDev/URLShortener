USE [URLShortener]
GO

/****** Object: Table [dbo].[URLs] Script Date: 13/08/2021 12:30:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertShortURLID]
	@longURL NVARCHAR(MAX)
AS

	DECLARE @ShortURLID NVARCHAR(8)
	SELECT @ShortURLID = ShortURLId from URLs where LongURL = LOWER(@LongURL)
	IF LEN(@ShortURLID) > 0
	BEGIN
		UPDATE URLs SET MonthlyRequests = MonthlyRequests + 1 WHERE LongURL = LOWER(@LongURL) 
	END
	ELSE
	BEGIN
		INSERT INTO URLs(LongURL, MonthlyRequests)
		VALUES (LOWER(@longURL), +1) 
		
	END
	
	SELECT ShortURLId from URLs where LongURL = LOWER(@LongURL) FOR JSON AUTO, WITHOUT_ARRAY_WRAPPER