USE [Northwind]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 04/15/2006 18:29:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[PER_ID] [int] IDENTITY(1,1) NOT NULL,
	[PER_FIRST_NAME] [nvarchar](40) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[PER_Last_NAME] [nvarchar](40) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[PER_BIRTH_DATE] [datetime] NULL,
	[PER_WEIGHT_KG] [float] NULL,
	[PER_HEIGHT_M] [float] NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[PER_ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
--51aspx.com