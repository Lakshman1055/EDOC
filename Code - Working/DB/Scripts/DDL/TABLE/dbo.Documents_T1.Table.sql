
CREATE TABLE [dbo].[Documents_T1](
	[Document_ID] [int] NOT NULL,
	[Document_NAME] [varchar](50) NOT NULL,
	[Company_ID] [int] NULL,
	[Category_ID] [int] NULL,
	[Project_ID] [int] NULL,
	[Employee_ID] [char](9) NULL,
	[Document_DATE] [datetime2](7) NULL,
	[Uploaded_DATE] [datetime2](7) NULL,
	[Updated_DATE] [datetime2](7) NULL,
	[Updated_By_ID] [char](9) NULL,
 CONSTRAINT [PK_DocumentInfo] PRIMARY KEY CLUSTERED 
(
	[Document_ID] ASC
))

GO

ALTER TABLE [dbo].[Documents_T1]  WITH CHECK ADD  CONSTRAINT [FK_Documents_T1_Categories_T1] FOREIGN KEY([Category_ID])
REFERENCES [dbo].[Categories_T1] ([Category_ID])
GO
ALTER TABLE [dbo].[Documents_T1] CHECK CONSTRAINT [FK_Documents_T1_Categories_T1]
GO
ALTER TABLE [dbo].[Documents_T1]  WITH CHECK ADD  CONSTRAINT [FK_Documents_T1_Companies_T1] FOREIGN KEY([Company_ID])
REFERENCES [dbo].[Companies_T1] ([Company_ID])
GO
ALTER TABLE [dbo].[Documents_T1] CHECK CONSTRAINT [FK_Documents_T1_Companies_T1]
GO
ALTER TABLE [dbo].[Documents_T1]  WITH CHECK ADD  CONSTRAINT [FK_Documents_T1_Projects_T1] FOREIGN KEY([Project_ID])
REFERENCES [dbo].[Projects_T1] ([Project_ID])
GO
ALTER TABLE [dbo].[Documents_T1] CHECK CONSTRAINT [FK_Documents_T1_Projects_T1]
GO
ALTER TABLE [dbo].[Documents_T1]  WITH CHECK ADD  CONSTRAINT [FK_Documents_T1_Users_T1] FOREIGN KEY([Employee_ID])
REFERENCES [dbo].[Users_T1] ([User_ID])
GO
ALTER TABLE [dbo].[Documents_T1] CHECK CONSTRAINT [FK_Documents_T1_Users_T1]
GO
ALTER TABLE [dbo].[Documents_T1]  WITH CHECK ADD  CONSTRAINT [FK_Documents_T1_Users_T11] FOREIGN KEY([Updated_By_ID])
REFERENCES [dbo].[Users_T1] ([User_ID])
GO
ALTER TABLE [dbo].[Documents_T1] CHECK CONSTRAINT [FK_Documents_T1_Users_T11]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique ID generated for the document.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Documents_T1', @level2type=N'COLUMN',@level2name=N'Document_ID'
GO
