USE [master]
GO
/****** Object:  Database [FamilyTree]    Script Date: 02/12/2024 09:51:44 ******/
CREATE DATABASE [FamilyTree]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FamilyTree', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FamilyTree.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FamilyTree_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FamilyTree_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FamilyTree] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FamilyTree].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FamilyTree] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FamilyTree] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FamilyTree] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FamilyTree] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FamilyTree] SET ARITHABORT OFF 
GO
ALTER DATABASE [FamilyTree] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FamilyTree] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FamilyTree] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FamilyTree] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FamilyTree] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FamilyTree] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FamilyTree] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FamilyTree] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FamilyTree] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FamilyTree] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FamilyTree] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FamilyTree] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FamilyTree] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FamilyTree] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FamilyTree] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FamilyTree] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FamilyTree] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FamilyTree] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FamilyTree] SET  MULTI_USER 
GO
ALTER DATABASE [FamilyTree] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FamilyTree] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FamilyTree] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FamilyTree] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FamilyTree] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FamilyTree] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FamilyTree] SET QUERY_STORE = OFF
GO
USE [FamilyTree]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 02/12/2024 09:51:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [int] NOT NULL,
	[FatherId] [int] NULL,
	[MotherId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Surname] [nvarchar](50) NULL,
	[BirthDate] [date] NULL,
	[IdentityNumber] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Person] ([Id], [FatherId], [MotherId], [Name], [Surname], [BirthDate], [IdentityNumber]) VALUES (1, NULL, NULL, N'GF1', N'Surname-GF1', CAST(N'2000-01-02' AS Date), N'GF1')
GO
INSERT [dbo].[Person] ([Id], [FatherId], [MotherId], [Name], [Surname], [BirthDate], [IdentityNumber]) VALUES (2, NULL, NULL, N'GM1', N'Surname-GM1', CAST(N'2000-01-02' AS Date), N'GM1')
GO
INSERT [dbo].[Person] ([Id], [FatherId], [MotherId], [Name], [Surname], [BirthDate], [IdentityNumber]) VALUES (3, NULL, NULL, N'GF2', N'Surname-GF2', CAST(N'2000-01-02' AS Date), N'GF2')
GO
INSERT [dbo].[Person] ([Id], [FatherId], [MotherId], [Name], [Surname], [BirthDate], [IdentityNumber]) VALUES (4, NULL, NULL, N'GM2', N'Surname-GM2', CAST(N'2000-01-02' AS Date), N'GM2')
GO
INSERT [dbo].[Person] ([Id], [FatherId], [MotherId], [Name], [Surname], [BirthDate], [IdentityNumber]) VALUES (5, NULL, NULL, N'GF3', N'Surname-GF3', CAST(N'2000-01-02' AS Date), N'GF3')
GO
INSERT [dbo].[Person] ([Id], [FatherId], [MotherId], [Name], [Surname], [BirthDate], [IdentityNumber]) VALUES (6, NULL, NULL, N'GM3', N'Surname-GM3', CAST(N'2000-01-02' AS Date), N'GM3')
GO
INSERT [dbo].[Person] ([Id], [FatherId], [MotherId], [Name], [Surname], [BirthDate], [IdentityNumber]) VALUES (7, NULL, NULL, N'GF4', N'Surname-GF4', CAST(N'2000-01-02' AS Date), N'GF4')
GO
INSERT [dbo].[Person] ([Id], [FatherId], [MotherId], [Name], [Surname], [BirthDate], [IdentityNumber]) VALUES (8, NULL, NULL, N'GM4', N'Surname-GM4', CAST(N'2000-01-02' AS Date), N'GM4')
GO
INSERT [dbo].[Person] ([Id], [FatherId], [MotherId], [Name], [Surname], [BirthDate], [IdentityNumber]) VALUES (9, 1, 2, N'F1', N'Surname-F1', CAST(N'2000-01-02' AS Date), N'F1')
GO
INSERT [dbo].[Person] ([Id], [FatherId], [MotherId], [Name], [Surname], [BirthDate], [IdentityNumber]) VALUES (10, 3, 4, N'M1', N'Surname-M1', CAST(N'2000-01-02' AS Date), N'M1')
GO
INSERT [dbo].[Person] ([Id], [FatherId], [MotherId], [Name], [Surname], [BirthDate], [IdentityNumber]) VALUES (11, 5, 6, N'F2', N'Surname-F2', CAST(N'2000-01-02' AS Date), N'F2')
GO
INSERT [dbo].[Person] ([Id], [FatherId], [MotherId], [Name], [Surname], [BirthDate], [IdentityNumber]) VALUES (12, 7, 8, N'M2', N'Surname-M2', CAST(N'2000-01-02' AS Date), N'M2')
GO
INSERT [dbo].[Person] ([Id], [FatherId], [MotherId], [Name], [Surname], [BirthDate], [IdentityNumber]) VALUES (13, 9, 10, N'B1', N'Surname-B1', CAST(N'2000-01-02' AS Date), N'B1')
GO
INSERT [dbo].[Person] ([Id], [FatherId], [MotherId], [Name], [Surname], [BirthDate], [IdentityNumber]) VALUES (14, 9, 10, N'G1', N'Surname-G1', CAST(N'2000-01-02' AS Date), N'G1')
GO
INSERT [dbo].[Person] ([Id], [FatherId], [MotherId], [Name], [Surname], [BirthDate], [IdentityNumber]) VALUES (15, 11, 12, N'B2', N'Surname-B2', CAST(N'2000-01-02' AS Date), N'B2')
GO
INSERT [dbo].[Person] ([Id], [FatherId], [MotherId], [Name], [Surname], [BirthDate], [IdentityNumber]) VALUES (16, 11, 12, N'G2', N'Surname-G2', CAST(N'2000-01-02' AS Date), N'G2')
GO
/****** Object:  StoredProcedure [dbo].[GetDescendantsStoredProcedure]    Script Date: 02/12/2024 09:51:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- GetDescendantsStoredProcedure
CREATE PROCEDURE [dbo].[GetDescendantsStoredProcedure]
    @IdentityNumber NVARCHAR(20)
AS
BEGIN
    WITH DescendantCTE AS
    (
        SELECT 
            Id,
            FatherId,
            MotherId,
            Name,
            Surname,
            BirthDate,
            IdentityNumber,
            0 AS Level
        FROM 
            Person
        WHERE 
            IdentityNumber = @IdentityNumber

        UNION ALL

        SELECT 
            p.Id,
            p.FatherId,
            p.MotherId,
            p.Name,
            p.Surname,
            p.BirthDate,
            p.IdentityNumber,
            d.Level + 1 AS Level
        FROM 
            Person p
        INNER JOIN 
            DescendantCTE d ON (p.FatherId = d.Id OR p.MotherId = d.Id)
        WHERE 
            d.Level < 10
    )

    SELECT * FROM DescendantCTE
END
GO
/****** Object:  StoredProcedure [dbo].[GetPartnerStoredProcedure]    Script Date: 02/12/2024 09:51:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- GetPartnerStoredProcedure
CREATE   PROCEDURE [dbo].[GetPartnerStoredProcedure]
AS
BEGIN
    SELECT DISTINCT [FatherId], [MotherId]
	FROM [FamilyTree].[dbo].[Person]
END
GO
/****** Object:  StoredProcedure [dbo].[GetRootAscendantStoredProcedure]    Script Date: 02/12/2024 09:51:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- GetRootAscendantStoredProcedure
CREATE PROCEDURE [dbo].[GetRootAscendantStoredProcedure]
    @IdentityNumber NVARCHAR(20)
AS
BEGIN
    WITH AncestorCTE AS
    (
        SELECT 
            Id,
            FatherId,
            MotherId,
            Name,
            Surname,
            BirthDate,
            IdentityNumber,
            0 AS Level
        FROM 
            Person
        WHERE 
            IdentityNumber = @IdentityNumber

        UNION ALL

        SELECT 
            p.Id,
            p.FatherId,
            p.MotherId,
            p.Name,
            p.Surname,
            p.BirthDate,
            p.IdentityNumber,
            a.Level + 1 AS Level
        FROM 
            Person p
        INNER JOIN 
            AncestorCTE a ON (p.Id = a.FatherId OR p.Id = a.MotherId)
    )

    SELECT TOP 1 * FROM AncestorCTE ORDER BY Level DESC
END
GO
USE [master]
GO
ALTER DATABASE [FamilyTree] SET  READ_WRITE 
GO
