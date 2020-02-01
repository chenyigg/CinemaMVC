USE [master]
GO
/****** Object:  Database [Cinema]    Script Date: 2020/2/1 14:11:12 ******/
CREATE DATABASE [Cinema]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Cinema', FILENAME = N'D:\C#\灵犀影院数据库\Cinema.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Cinema_log', FILENAME = N'D:\C#\灵犀影院数据库\Cinema_log.ldf' , SIZE = 139264KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Cinema] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Cinema].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Cinema] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Cinema] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Cinema] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Cinema] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Cinema] SET ARITHABORT OFF 
GO
ALTER DATABASE [Cinema] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Cinema] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Cinema] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Cinema] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Cinema] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Cinema] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Cinema] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Cinema] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Cinema] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Cinema] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Cinema] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Cinema] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Cinema] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Cinema] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Cinema] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Cinema] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Cinema] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Cinema] SET RECOVERY FULL 
GO
ALTER DATABASE [Cinema] SET  MULTI_USER 
GO
ALTER DATABASE [Cinema] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Cinema] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Cinema] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Cinema] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Cinema] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Cinema', N'ON'
GO
ALTER DATABASE [Cinema] SET QUERY_STORE = OFF
GO
USE [Cinema]
GO
/****** Object:  User [NT AUTHORITY\SYSTEM]    Script Date: 2020/2/1 14:11:12 ******/
CREATE USER [NT AUTHORITY\SYSTEM] FOR LOGIN [NT AUTHORITY\SYSTEM] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [NT AUTHORITY\NETWORK SERVICE]    Script Date: 2020/2/1 14:11:12 ******/
CREATE USER [NT AUTHORITY\NETWORK SERVICE] FOR LOGIN [NT AUTHORITY\NETWORK SERVICE] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
ALTER ROLE [db_owner] ADD MEMBER [NT AUTHORITY\NETWORK SERVICE]
GO
/****** Object:  Table [dbo].[ChipInfo]    Script Date: 2020/2/1 14:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChipInfo](
	[ChipInfoID] [int] IDENTITY(1,1) NOT NULL,
	[CinemaID] [int] NOT NULL,
	[OfficeID] [int] NOT NULL,
	[MovieName] [nvarchar](50) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[StopTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ChipInfo] PRIMARY KEY CLUSTERED 
(
	[ChipInfoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CinemaInfo]    Script Date: 2020/2/1 14:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CinemaInfo](
	[CinemaID] [int] IDENTITY(1,1) NOT NULL,
	[CinemaName] [nvarchar](50) NOT NULL,
	[CinemaAddress] [nvarchar](50) NOT NULL,
	[CinemaArea] [nvarchar](50) NULL,
	[CinemaImg] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[CinemaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[CinemaAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommentInfo]    Script Date: 2020/2/1 14:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommentInfo](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[MovieID] [int] NOT NULL,
	[UsersID] [int] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[UserFace] [nvarchar](50) NULL,
	[CommentInfo] [nvarchar](max) NOT NULL,
	[CommentTime] [date] NOT NULL,
 CONSTRAINT [PK_CommentInfo] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieInfo]    Script Date: 2020/2/1 14:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieInfo](
	[MovieID] [int] IDENTITY(1,1) NOT NULL,
	[MovieName] [nvarchar](50) NOT NULL,
	[MovieCover] [nvarchar](50) NOT NULL,
	[MovieBrief] [nvarchar](max) NOT NULL,
	[MovieMoney] [decimal](18, 2) NOT NULL,
	[MovieReleaseDate] [date] NOT NULL,
	[MovieDuration] [int] NOT NULL,
	[MovieDirect] [nvarchar](50) NOT NULL,
	[MovieType] [nvarchar](50) NULL,
	[MovieArea] [nvarchar](50) NULL,
	[MovieYears] [int] NULL,
	[MovieScore] [decimal](18, 1) NULL,
	[MoviePeople] [nvarchar](50) NULL,
 CONSTRAINT [PK__MovieInf__4BD2943AA6AD36E5] PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__MovieInf__F5C0AEA5692B90E1] UNIQUE NONCLUSTERED 
(
	[MovieName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MusicInfo]    Script Date: 2020/2/1 14:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MusicInfo](
	[MusicID] [int] IDENTITY(1,1) NOT NULL,
	[MusicNmae] [nvarchar](20) NOT NULL,
	[MusicLenth] [nvarchar](20) NOT NULL,
	[MusicSinger] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MusicID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsInfo]    Script Date: 2020/2/1 14:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsInfo](
	[NewsID] [int] IDENTITY(1,1) NOT NULL,
	[NewsPoster] [nvarchar](50) NOT NULL,
	[NewsSynopsis] [nvarchar](50) NOT NULL,
	[NewsContent] [nvarchar](500) NOT NULL,
	[NewsSource] [nvarchar](50) NOT NULL,
	[IssueTime] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NewsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OfficeInfo]    Script Date: 2020/2/1 14:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OfficeInfo](
	[OfficeID] [int] IDENTITY(1,1) NOT NULL,
	[OfficeName] [nvarchar](50) NOT NULL,
	[CinemaID] [int] NOT NULL,
	[NullColOne] [int] NULL,
	[NullColTwo] [int] NULL,
	[OfficeCount] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[OfficeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 2020/2/1 14:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderID] [int] NOT NULL,
	[SeatID] [int] NOT NULL,
	[OrderDetailsPrice] [decimal](18, 2) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[StopTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderInfo]    Script Date: 2020/2/1 14:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderInfo](
	[OrderID] [int] IDENTITY(10000,1) NOT NULL,
	[OrderSumMoney] [decimal](18, 2) NOT NULL,
	[UsersID] [int] NOT NULL,
	[OrderTime] [datetime] NOT NULL,
	[MovieName] [nvarchar](50) NOT NULL,
	[CinemaAddress] [nvarchar](50) NOT NULL,
	[OfficeID] [int] NOT NULL,
	[ChipInfoID] [int] NOT NULL,
	[IsPay] [int] NOT NULL,
	[PayTime] [int] NULL,
 CONSTRAINT [PK__OrderInf__C3905BAF9A9C3BD4] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrdersGoodsInfo]    Script Date: 2020/2/1 14:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdersGoodsInfo](
	[CinemaID] [int] NOT NULL,
	[OfficeID] [int] NOT NULL,
	[MovieName] [nvarchar](50) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[StopTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SeatInfo]    Script Date: 2020/2/1 14:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeatInfo](
	[SeatID] [int] IDENTITY(1,1) NOT NULL,
	[OfficeID] [int] NOT NULL,
	[IsSeat] [int] NOT NULL,
	[ChipInfoID] [int] NOT NULL,
 CONSTRAINT [PK__SeatInfo__311713D3FB382117] PRIMARY KEY CLUSTERED 
(
	[SeatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UrlCountInnfo]    Script Date: 2020/2/1 14:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UrlCountInnfo](
	[Msg] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersInfo]    Script Date: 2020/2/1 14:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersInfo](
	[UsersID] [int] IDENTITY(1,1) NOT NULL,
	[UserAccount] [nvarchar](50) NOT NULL,
	[UsersPwd] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[UserSex] [nvarchar](50) NULL,
	[UserPhone] [nvarchar](50) NULL,
	[UserIDCard] [nvarchar](50) NULL,
	[UserAddress] [nvarchar](50) NULL,
	[UserLevelNum] [int] NULL,
	[UserMvp] [int] NULL,
	[UserFace] [nvarchar](50) NULL,
	[UserEmail] [nvarchar](50) NULL,
 CONSTRAINT [PK__UsersInf__A349B042C126172C] PRIMARY KEY CLUSTERED 
(
	[UsersID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_UsersInfo] UNIQUE NONCLUSTERED 
(
	[UserAccount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UsersInfo] ADD  CONSTRAINT [DF_UsersInfo_UserLevelNum]  DEFAULT ((0)) FOR [UserLevelNum]
GO
ALTER TABLE [dbo].[UsersInfo] ADD  CONSTRAINT [DF__UsersInfo__UserM__6B24EA82]  DEFAULT ((0)) FOR [UserMvp]
GO
ALTER TABLE [dbo].[ChipInfo]  WITH CHECK ADD  CONSTRAINT [FK__Chip__CinemaID__3B40CD36] FOREIGN KEY([CinemaID])
REFERENCES [dbo].[CinemaInfo] ([CinemaID])
GO
ALTER TABLE [dbo].[ChipInfo] CHECK CONSTRAINT [FK__Chip__CinemaID__3B40CD36]
GO
ALTER TABLE [dbo].[ChipInfo]  WITH CHECK ADD  CONSTRAINT [FK__Chip__OfficeID__3C34F16F] FOREIGN KEY([OfficeID])
REFERENCES [dbo].[OfficeInfo] ([OfficeID])
GO
ALTER TABLE [dbo].[ChipInfo] CHECK CONSTRAINT [FK__Chip__OfficeID__3C34F16F]
GO
ALTER TABLE [dbo].[CommentInfo]  WITH CHECK ADD  CONSTRAINT [FK_Comment_UsersInfo] FOREIGN KEY([UsersID])
REFERENCES [dbo].[UsersInfo] ([UsersID])
GO
ALTER TABLE [dbo].[CommentInfo] CHECK CONSTRAINT [FK_Comment_UsersInfo]
GO
ALTER TABLE [dbo].[CommentInfo]  WITH CHECK ADD  CONSTRAINT [FK_CommentInfo_MovieInfo] FOREIGN KEY([MovieID])
REFERENCES [dbo].[MovieInfo] ([MovieID])
GO
ALTER TABLE [dbo].[CommentInfo] CHECK CONSTRAINT [FK_CommentInfo_MovieInfo]
GO
ALTER TABLE [dbo].[OfficeInfo]  WITH CHECK ADD FOREIGN KEY([CinemaID])
REFERENCES [dbo].[CinemaInfo] ([CinemaID])
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__Order__5535A963] FOREIGN KEY([OrderID])
REFERENCES [dbo].[OrderInfo] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK__OrderDeta__Order__5535A963]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__SeatI__5629CD9C] FOREIGN KEY([SeatID])
REFERENCES [dbo].[SeatInfo] ([SeatID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK__OrderDeta__SeatI__5629CD9C]
GO
ALTER TABLE [dbo].[OrderInfo]  WITH CHECK ADD  CONSTRAINT [FK__OrderInfo__Cinem__571DF1D5] FOREIGN KEY([CinemaAddress])
REFERENCES [dbo].[CinemaInfo] ([CinemaAddress])
GO
ALTER TABLE [dbo].[OrderInfo] CHECK CONSTRAINT [FK__OrderInfo__Cinem__571DF1D5]
GO
ALTER TABLE [dbo].[OrderInfo]  WITH CHECK ADD  CONSTRAINT [FK__OrderInfo__Movie__00200768] FOREIGN KEY([MovieName])
REFERENCES [dbo].[MovieInfo] ([MovieName])
GO
ALTER TABLE [dbo].[OrderInfo] CHECK CONSTRAINT [FK__OrderInfo__Movie__00200768]
GO
ALTER TABLE [dbo].[OrderInfo]  WITH CHECK ADD  CONSTRAINT [FK__OrderInfo__Offic__59063A47] FOREIGN KEY([OfficeID])
REFERENCES [dbo].[OfficeInfo] ([OfficeID])
GO
ALTER TABLE [dbo].[OrderInfo] CHECK CONSTRAINT [FK__OrderInfo__Offic__59063A47]
GO
ALTER TABLE [dbo].[OrderInfo]  WITH CHECK ADD  CONSTRAINT [FK__OrderInfo__Users__7F2BE32F] FOREIGN KEY([UsersID])
REFERENCES [dbo].[UsersInfo] ([UsersID])
GO
ALTER TABLE [dbo].[OrderInfo] CHECK CONSTRAINT [FK__OrderInfo__Users__7F2BE32F]
GO
ALTER TABLE [dbo].[OrderInfo]  WITH CHECK ADD  CONSTRAINT [FK_OrderInfo_ChipInfo] FOREIGN KEY([ChipInfoID])
REFERENCES [dbo].[ChipInfo] ([ChipInfoID])
GO
ALTER TABLE [dbo].[OrderInfo] CHECK CONSTRAINT [FK_OrderInfo_ChipInfo]
GO
ALTER TABLE [dbo].[OrdersGoodsInfo]  WITH CHECK ADD FOREIGN KEY([CinemaID])
REFERENCES [dbo].[CinemaInfo] ([CinemaID])
GO
ALTER TABLE [dbo].[OrdersGoodsInfo]  WITH CHECK ADD  CONSTRAINT [FK__OrdersGoo__Movie__7C4F7684] FOREIGN KEY([MovieName])
REFERENCES [dbo].[MovieInfo] ([MovieName])
GO
ALTER TABLE [dbo].[OrdersGoodsInfo] CHECK CONSTRAINT [FK__OrdersGoo__Movie__7C4F7684]
GO
ALTER TABLE [dbo].[OrdersGoodsInfo]  WITH CHECK ADD FOREIGN KEY([OfficeID])
REFERENCES [dbo].[OfficeInfo] ([OfficeID])
GO
ALTER TABLE [dbo].[SeatInfo]  WITH CHECK ADD  CONSTRAINT [FK__SeatInfo__Office__5DCAEF64] FOREIGN KEY([OfficeID])
REFERENCES [dbo].[OfficeInfo] ([OfficeID])
GO
ALTER TABLE [dbo].[SeatInfo] CHECK CONSTRAINT [FK__SeatInfo__Office__5DCAEF64]
GO
ALTER TABLE [dbo].[SeatInfo]  WITH CHECK ADD  CONSTRAINT [FK_SeatInfo_ChipInfo] FOREIGN KEY([ChipInfoID])
REFERENCES [dbo].[ChipInfo] ([ChipInfoID])
GO
ALTER TABLE [dbo].[SeatInfo] CHECK CONSTRAINT [FK_SeatInfo_ChipInfo]
GO
ALTER TABLE [dbo].[UsersInfo]  WITH CHECK ADD  CONSTRAINT [FK_UsersInfo_UsersInfo] FOREIGN KEY([UsersID])
REFERENCES [dbo].[UsersInfo] ([UsersID])
GO
ALTER TABLE [dbo].[UsersInfo] CHECK CONSTRAINT [FK_UsersInfo_UsersInfo]
GO
ALTER TABLE [dbo].[UsersInfo]  WITH CHECK ADD  CONSTRAINT [CK__UsersInfo__UserA__6477ECF3] CHECK  ((len([UserAccount])>(0)))
GO
ALTER TABLE [dbo].[UsersInfo] CHECK CONSTRAINT [CK__UsersInfo__UserA__6477ECF3]
GO
ALTER TABLE [dbo].[UsersInfo]  WITH CHECK ADD  CONSTRAINT [CK__UsersInfo__UserI__68487DD7] CHECK  ((len([UserIDCard])=(18)))
GO
ALTER TABLE [dbo].[UsersInfo] CHECK CONSTRAINT [CK__UsersInfo__UserI__68487DD7]
GO
ALTER TABLE [dbo].[UsersInfo]  WITH CHECK ADD  CONSTRAINT [CK__UsersInfo__UserL__693CA210] CHECK  (([UserLevelNum]=(0) OR [UserLevelNum]=(1) OR [UserLevelNum]=(2)))
GO
ALTER TABLE [dbo].[UsersInfo] CHECK CONSTRAINT [CK__UsersInfo__UserL__693CA210]
GO
ALTER TABLE [dbo].[UsersInfo]  WITH CHECK ADD  CONSTRAINT [CK__UsersInfo__UserM__6A30C649] CHECK  (([UserMvp]=(0) OR [UserMvp]=(1)))
GO
ALTER TABLE [dbo].[UsersInfo] CHECK CONSTRAINT [CK__UsersInfo__UserM__6A30C649]
GO
ALTER TABLE [dbo].[UsersInfo]  WITH CHECK ADD  CONSTRAINT [CK__UsersInfo__UserP__6754599E] CHECK  ((len([UserPhone])=(11)))
GO
ALTER TABLE [dbo].[UsersInfo] CHECK CONSTRAINT [CK__UsersInfo__UserP__6754599E]
GO
ALTER TABLE [dbo].[UsersInfo]  WITH CHECK ADD  CONSTRAINT [CK__UsersInfo__Users__656C112C] CHECK  ((len([UsersPwd])>(0)))
GO
ALTER TABLE [dbo].[UsersInfo] CHECK CONSTRAINT [CK__UsersInfo__Users__656C112C]
GO
ALTER TABLE [dbo].[UsersInfo]  WITH CHECK ADD  CONSTRAINT [CK__UsersInfo__UserS__66603565] CHECK  (([UserSex]='男' OR [UserSex]='女'))
GO
ALTER TABLE [dbo].[UsersInfo] CHECK CONSTRAINT [CK__UsersInfo__UserS__66603565]
GO
/****** Object:  StoredProcedure [dbo].[proc_CreateSeat]    Script Date: 2020/2/1 14:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  CREATE proc [dbo].[proc_CreateSeat] (@OfficeID int,@ChipID int) 
  as
	declare  @konglie1 int
	set @konglie1=(select NullColOne from OfficeInfo where OfficeID=@OfficeID)

	declare  @konglie2 int
	set @konglie2=(select NullColTwo from OfficeInfo where OfficeID=1)
	-- 160个座位所有座位
  declare @j int;
  set @j=1
  while(@j<=160)
  begin
  insert SeatInfo values(@OfficeID,1,@ChipID)
  set @j=@j+1
  end
   --所有不是座位置为0
   --找到座位数量
   declare @OfficeCount int
   set @OfficeCount=(select OfficeCount from OfficeInfo where OfficeID=@OfficeID)

  declare @i int;
  set @i=1;
  while(@i<=160)
  begin 

  if(@i<=@OfficeCount)
  begin 
  update SeatInfo set IsSeat=0 where OfficeID=@OfficeID and (SeatID%160%16=@konglie1 or SeatID%160%16=@konglie2) and @ChipID=ChipInfoID
  end

  else
  begin
  update SeatInfo set IsSeat=0 where OfficeID=@OfficeID and @ChipID=ChipInfoID and (SeatID%160>@OfficeCount or SeatID%160=0)
  end
  set @i+=1
  end
GO
/****** Object:  StoredProcedure [dbo].[proc_SelectCommentCount]    Script Date: 2020/2/1 14:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[proc_SelectCommentCount]
(
@pageSize int ,--控制一页显示数量
@page int ,--查看下一页
@MovieID int --哪个电影的评论
)
as 
select Count(*) from [dbo].[CommentInfo] where [MovieID] =  @MovieID --返回查询总数
 
if object_id('tempdb..#temp') is not null
begin
--删除临时表
DROP Table #temp
end

select * into #temp from (select top(@pageSize) * from [dbo].[CommentInfo] where [CommentID] not in (select top(@page) [CommentID] from [dbo].[CommentInfo] where MovieID=@MovieID order by [CommentID] desc)  and  MovieID=@MovieID order by [CommentID] desc ) CommentInfo --返回查询详情
select * from #temp order by [CommentID] desc
GO
/****** Object:  StoredProcedure [dbo].[proc_SelectPageMovieInfo]    Script Date: 2020/2/1 14:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[proc_SelectPageMovieInfo]
(
@PageSize int,
@PageNum int
)
as
select Count(*) from MovieInfo
select top(@PageSize) * from MovieInfo where MovieID not in(
		select Top (@PageNum) MovieID from MovieInfo order by MovieID
	) 
GO
/****** Object:  StoredProcedure [dbo].[proc_SelectPageMovieType]    Script Date: 2020/2/1 14:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[proc_SelectPageMovieType]
(
@MovieType nvarchar(50),
@MovieArea nvarchar(50),
@MovieYears nvarchar(50),
@PageSize int,
@PageNum int
)
as
select Count(*) from MovieInfo where 
		(
		(MovieType like '%'+@MovieType+'%') and 
		(MovieArea like '%'+@MovieArea+'%') and
		(MovieYears like '%'+@MovieYears+'%')
		)
if  @PageNum!=0
	begin
		select Top (@PageSize) * from MovieInfo where MovieID not in
		(
			select top (@PageNum) MovieID from MovieInfo
			where 
			(
			  (MovieType like '%'+@MovieType+'%') and
			  (MovieArea like '%'+@MovieArea+'%') and
			  (MovieYears like '%'+@MovieYears+'%')
			)order by MovieID
		) 
		and (MovieType like '%'+@MovieType+'%') 
		and (MovieArea like '%'+@MovieArea+'%')
		and (MovieYears like '%'+@MovieYears+'%')
	end

else
	begin
		select top(@PageSize) * from MovieInfo where MovieID in(
				select MovieID from MovieInfo 
				where 
				(
					(MovieType like '%'+@MovieType+'%') and 
					(MovieArea like '%'+@MovieArea+'%') and
					(MovieYears like '%'+@MovieYears+'%')
				)
		) 
				order by MovieID
	end
GO
USE [master]
GO
ALTER DATABASE [Cinema] SET  READ_WRITE 
GO
