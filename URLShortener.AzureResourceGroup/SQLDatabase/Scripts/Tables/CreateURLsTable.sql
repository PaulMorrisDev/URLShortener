﻿USE [URLShortener]
GO

/****** Object: Table [dbo].[URLs] Script Date: 13/08/2021 12:30:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[URLs]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [ShortURLID] NVARCHAR(30) UNIQUE default LEFT(NEWID(),7) NOT NULL, 
    [LongURL] NVARCHAR(MAX) NOT NULL, 
    [MonthlyRequests] INT NOT NULL
)
GO