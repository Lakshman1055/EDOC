
CREATE TABLE [dbo].[Tags_T1]
(
	[Document_ID] [int] NOT NULL,
	[Tag_TEXT] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Tags_T1] PRIMARY KEY CLUSTERED 
 (
	[Document_ID] ASC,
	[Tag_TEXT] ASC
  )
)

GO

GO
ALTER TABLE [dbo].[Tags_T1]  WITH CHECK ADD  CONSTRAINT [FK_Tags_T1_Documents_T11] FOREIGN KEY([Document_ID])
REFERENCES [dbo].[Documents_T1] ([Document_ID])
GO
ALTER TABLE [dbo].[Tags_T1] CHECK CONSTRAINT [FK_Tags_T1_Documents_T11]
GO
