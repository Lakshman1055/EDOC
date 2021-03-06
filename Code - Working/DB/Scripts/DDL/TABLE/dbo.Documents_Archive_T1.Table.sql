
CREATE TABLE [dbo].[Documents_Archive_T1](
	[Document_ID] [int] NOT NULL,
	[Document_NAME] [varchar](50) NOT NULL,
	[Company_ID] [int] NULL,
	[Category_ID] [int] NULL,
	[Project_ID] [int] NULL,
	[Employee_ID] [char](9) NULL,
	[Document_DATE] [datetime2](7) NOT NULL,
	[Uploaded_DATE] [datetime2](7) NULL,
	[Updated_DATE] [datetime2](7) NULL,
	[Updated_By_ID] [char](9) NULL
) 

GO
ALTER TABLE [dbo].[Documents_Archive_T1]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Archive_T1_Categories_T1] FOREIGN KEY([Category_ID])
REFERENCES [dbo].[Categories_T1] ([Category_ID])
GO
ALTER TABLE [dbo].[Documents_Archive_T1] CHECK CONSTRAINT [FK_Documents_Archive_T1_Categories_T1]
GO
ALTER TABLE [dbo].[Documents_Archive_T1]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Archive_T1_Companies_T1] FOREIGN KEY([Company_ID])
REFERENCES [dbo].[Companies_T1] ([Company_ID])
GO
ALTER TABLE [dbo].[Documents_Archive_T1] CHECK CONSTRAINT [FK_Documents_Archive_T1_Companies_T1]
GO
ALTER TABLE [dbo].[Documents_Archive_T1]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Archive_T1_Projects_T1] FOREIGN KEY([Project_ID])
REFERENCES [dbo].[Projects_T1] ([Project_ID])
GO
ALTER TABLE [dbo].[Documents_Archive_T1] CHECK CONSTRAINT [FK_Documents_Archive_T1_Projects_T1]
GO
ALTER TABLE [dbo].[Documents_Archive_T1]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Archive_T1_Users_T11] FOREIGN KEY([Updated_By_ID])
REFERENCES [dbo].[Users_T1] ([User_ID])
GO
ALTER TABLE [dbo].[Documents_Archive_T1] CHECK CONSTRAINT [FK_Documents_Archive_T1_Users_T11]
GO
ALTER TABLE [dbo].[Documents_Archive_T1]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Archive_T1_Users_T12] FOREIGN KEY([Employee_ID])
REFERENCES [dbo].[Users_T1] ([User_ID])
GO
ALTER TABLE [dbo].[Documents_Archive_T1] CHECK CONSTRAINT [FK_Documents_Archive_T1_Users_T12]
GO
