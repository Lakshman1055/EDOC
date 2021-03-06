
CREATE TABLE [dbo].[Users_T1](
	[User_ID] [char](9) NOT NULL,
	[First_NAME] [varchar](30) NULL,
	[Last_NAME] [varchar](30) NULL,
	[Ssn_NUMB] [int] NULL,
	[Admin_FLAG] [char](1) NULL,
	[Start_DATE] [datetime2](7) NULL,
	[End_DATE] [datetime2](7) NULL,
 CONSTRAINT [PK_Users_T1] PRIMARY KEY CLUSTERED 
(
	[User_ID] ASC
))


GO
ALTER TABLE [dbo].[Users_T1]  WITH CHECK ADD  CONSTRAINT [CK_Ssn_NUMB] CHECK  (([Ssn_NUMB]>=(100000000) AND [Ssn_NUMB]<=(999999999)))
GO
ALTER TABLE [dbo].[Users_T1] CHECK CONSTRAINT [CK_Ssn_NUMB]
GO
