/****** Object:  Table [dbo].[StudentDetails]    Script Date: 2022-08-03 6:07:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentDetails](
	[Id] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[Password] [nvarchar](max) NULL,
	[DateofBirth] [date] NULL,
	[DateofJoining] [date] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[StudentDetails] ([Id], [Name], [Email], [Password], [DateofBirth], [DateofJoining]) VALUES (1, N'Amit Mohanty', N'amit-mohanty@test.com', N'amit123', CAST(N'1986-07-08' AS Date), CAST(N'2022-08-01' AS Date))
GO
INSERT [dbo].[StudentDetails] ([Id], [Name], [Email], [Password], [DateofBirth], [DateofJoining]) VALUES (2, N'Abinash Hota', N'abhinash.hota@test.com', N'test123', CAST(N'1972-05-13' AS Date), CAST(N'2010-05-10' AS Date))
GO
/****** Object:  StoredProcedure [dbo].[GetStudentDetails]    Script Date: 2022-08-03 6:07:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetStudentDetails]
(
	@Id INT = NULL, @Name NVARCHAR(100) = NULL, @Email NVARCHAR(100) = NULL, 
	@Password NVARCHAR(MAX) = NULL, @DateofBirth DATE = NULL, @DateofJoining DATE = NULL,
	@Action NVARCHAR(20)
)
AS
BEGIN
	IF @Action = 'StudentDetails'
	BEGIN
		SELECT * FROM StudentDetails
	END

	IF @Action = 'StudentData'
	BEGIN
		SELECT * FROM StudentDetails WHERE id = @Id
	END

	IF @Action = 'AddStudent'
	BEGIN
		INSERT INTO StudentDetails VALUES (@Id, @Name, @Email, @Password, @DateofBirth, @DateofJoining)
	END

	IF @Action = 'UpdateStudent'
	BEGIN
		UPDATE StudentDetails SET Name = @Name, Email = @Email, Password = @Password, DateofBirth = @DateofBirth, 
		DateofJoining = @DateofJoining WHERE Id = @Id
	END

	IF @Action = 'DeleteStudent'
	BEGIN
		DELETE FROM StudentDetails WHERE id = @Id
	END
END
GO
