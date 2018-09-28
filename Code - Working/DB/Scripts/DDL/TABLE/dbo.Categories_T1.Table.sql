
CREATE TABLE [dbo].[Categories_T1](
	[Category_ID] [int] NOT NULL,
	[Category_NAME] [varchar](50) NOT NULL,
	[Parent_ID] [int] NULL,
	[Has_Children_BIT] [bit] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Category_ID] ASC
))
