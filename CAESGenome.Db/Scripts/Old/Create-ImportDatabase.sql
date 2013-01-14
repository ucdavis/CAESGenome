USE [CgfOld]
GO
/****** Object:  Table [dbo].[barcode]    Script Date: 11/19/2012 3:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[barcode](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[plateID] [int] NOT NULL,
	[plateSubID] [int] NOT NULL,
	[primerID] [int] NOT NULL,
	[stage] [int] NOT NULL,
	[sourceBarcode] [int] NOT NULL,
	[date] [date] NULL,
	[done] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[departments]    Script Date: 11/19/2012 3:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[departments](
	[departmentid] [int] IDENTITY(1,1) NOT NULL,
	[department] [varchar](150) NULL,
PRIMARY KEY CLUSTERED 
(
	[departmentid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pi]    Script Date: 11/19/2012 3:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pi](
	[piid] [int] IDENTITY(1,1) NOT NULL,
	[pilastname] [varchar](100) NULL,
	[pifirstname] [varchar](100) NULL,
	[piemail] [varchar](100) NULL,
	[pifax] [varchar](50) NULL,
	[piphonenum] [varchar](50) NULL,
	[pititle] [varchar](50) NULL,
	[pidatejoined] [datetime] NULL,
	[piusername] [varchar](100) NULL,
	[pipassword] [varchar](20) NULL,
	[universityid] [int] NOT NULL,
	[departmentid] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[piid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[quality_phred]    Script Date: 11/19/2012 3:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[quality_phred](
	[Id] [nvarchar](255) NULL,
	[Barcode] [nvarchar](255) NULL,
	[CellChar] [nvarchar](255) NULL,
	[CellNum] [nvarchar](255) NULL,
	[Start] [nvarchar](255) NULL,
	[End] [nvarchar](255) NULL,
	[Datesubmitted] [nvarchar](255) NULL,
	[DateTimeSubmitted] [nvarchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[recharge]    Script Date: 11/19/2012 3:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[recharge](
	[rechargeid] [int] IDENTITY(1,1) NOT NULL,
	[accountnum] [varchar](50) NULL,
	[valid] [varchar](10) NULL,
	[description] [varchar](200) NULL,
	[datestart] [datetime] NULL,
	[dateend] [datetime] NULL,
	[piid] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[rechargeid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[staff]    Script Date: 11/19/2012 3:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[staff](
	[staffid] [decimal](22, 0) NOT NULL,
	[stafffirst] [varchar](30) NULL,
	[stafflast] [varchar](30) NULL,
	[staffemail] [varchar](50) NULL,
	[stafftitle] [varchar](50) NULL,
	[staffpassword] [varchar](10) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[submission_userjob_bacterialclone]    Script Date: 11/19/2012 3:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[submission_userjob_bacterialclone](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Seq_Direction] [varchar](10) NOT NULL,
	[Primer1] [int] NOT NULL,
	[Primer2] [int] NOT NULL,
	[StrainID] [int] NOT NULL,
	[VectorID] [int] NOT NULL,
	[antibioticID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[submission_userjob_dna]    Script Date: 11/19/2012 3:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[submission_userjob_dna](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Seq_Direction] [varchar](10) NOT NULL,
	[Primer1] [int] NOT NULL,
	[Primer2] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[submission_userjob_qbot_colonypicking]    Script Date: 11/19/2012 3:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[submission_userjob_qbot_colonypicking](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[vectorID] [int] NOT NULL,
	[hostID] [int] NOT NULL,
	[numOfQTrays] [varchar](50) NOT NULL,
	[numOfGlycerol] [varchar](50) NOT NULL,
	[concentration] [varchar](50) NOT NULL,
	[replication] [varchar](50) NOT NULL,
	[numOfColonies] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[submission_userjob_qbot_gridding]    Script Date: 11/19/2012 3:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[submission_userjob_qbot_gridding](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[vectorID] [int] NOT NULL,
	[hostID] [int] NOT NULL,
	[numOfSourcePlates] [varchar](50) NOT NULL,
	[copiesOfMembranes] [varchar](50) NOT NULL,
	[pattern] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[submission_userjob_qbot_replicating]    Script Date: 11/19/2012 3:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[submission_userjob_qbot_replicating](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[vectorID] [int] NOT NULL,
	[hostID] [int] NOT NULL,
	[numOfSourcePlates] [varchar](50) NOT NULL,
	[plateTypeDestination] [varchar](50) NOT NULL,
	[replication] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[submission_userjob_sublibrary]    Script Date: 11/19/2012 3:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[submission_userjob_sublibrary](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[typeOfSamples] [varchar](50) NOT NULL,
	[concentrationOfDNA] [float] NULL,
	[insertGenomeSize] [float] NULL,
	[coverage] [int] NULL,
	[vectorID] [int] NULL,
	[antibioticID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[submission_userjob_userrun]    Script Date: 11/19/2012 3:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[submission_userjob_userrun](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Seq_Direction] [varchar](10) NOT NULL,
	[DyeID] [smallint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[submission_userjob_userrun_genotyping]    Script Date: 11/19/2012 3:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[submission_userjob_userrun_genotyping](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dyeIDs] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[submission_userjobs]    Script Date: 11/19/2012 3:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[submission_userjobs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dateSubmitted] [date] NULL,
	[uid] [int] NOT NULL,
	[accountid] [varchar](50) NOT NULL,
	[JobName] [varchar](255) NOT NULL,
	[submissionType] [int] NOT NULL,
	[HowManyPlates] [int] NOT NULL,
	[plateType] [varchar](255) NOT NULL,
	[Comment] [varchar](max) NULL,
	[id2] [int] NOT NULL,
	[done] [int] NOT NULL,
	[statusDate] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[submission_userplates]    Script Date: 11/19/2012 3:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[submission_userplates](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[JobID] [int] NOT NULL,
	[plateName] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[user]    Script Date: 11/19/2012 3:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user](
	[userid] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](100) NULL,
	[lastname] [varchar](50) NULL,
	[firstname] [varchar](50) NULL,
	[title] [varchar](50) NULL,
	[email] [varchar](100) NULL,
	[passwords] [varchar](15) NULL,
	[phonenum] [varchar](20) NULL,
	[valid] [varchar](10) NULL,
	[datejoined] [datetime] NULL,
	[piid] [decimal](22, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[useracct]    Script Date: 11/19/2012 3:15:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[useracct](
	[useracctid] [int] IDENTITY(1,1) NOT NULL,
	[valid] [varchar](10) NULL,
	[rechargeid] [int] NOT NULL,
	[userid] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[useracctid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[barcode] ADD  DEFAULT ('0') FOR [plateID]
GO
ALTER TABLE [dbo].[barcode] ADD  DEFAULT ('0') FOR [plateSubID]
GO
ALTER TABLE [dbo].[barcode] ADD  DEFAULT ('0') FOR [primerID]
GO
ALTER TABLE [dbo].[barcode] ADD  DEFAULT ('0') FOR [stage]
GO
ALTER TABLE [dbo].[barcode] ADD  DEFAULT ('0') FOR [sourceBarcode]
GO
ALTER TABLE [dbo].[barcode] ADD  DEFAULT ('1900-01-01') FOR [date]
GO
ALTER TABLE [dbo].[barcode] ADD  DEFAULT ('0') FOR [done]
GO
ALTER TABLE [dbo].[departments] ADD  DEFAULT (NULL) FOR [department]
GO
ALTER TABLE [dbo].[pi] ADD  DEFAULT (NULL) FOR [pilastname]
GO
ALTER TABLE [dbo].[pi] ADD  DEFAULT (NULL) FOR [pifirstname]
GO
ALTER TABLE [dbo].[pi] ADD  DEFAULT (NULL) FOR [piemail]
GO
ALTER TABLE [dbo].[pi] ADD  DEFAULT (NULL) FOR [pifax]
GO
ALTER TABLE [dbo].[pi] ADD  DEFAULT (NULL) FOR [piphonenum]
GO
ALTER TABLE [dbo].[pi] ADD  DEFAULT (NULL) FOR [pititle]
GO
ALTER TABLE [dbo].[pi] ADD  DEFAULT (NULL) FOR [pidatejoined]
GO
ALTER TABLE [dbo].[pi] ADD  DEFAULT (NULL) FOR [piusername]
GO
ALTER TABLE [dbo].[pi] ADD  DEFAULT (NULL) FOR [pipassword]
GO
ALTER TABLE [dbo].[pi] ADD  DEFAULT ('0') FOR [universityid]
GO
ALTER TABLE [dbo].[pi] ADD  DEFAULT ('0') FOR [departmentid]
GO
ALTER TABLE [dbo].[recharge] ADD  DEFAULT (NULL) FOR [accountnum]
GO
ALTER TABLE [dbo].[recharge] ADD  DEFAULT (NULL) FOR [valid]
GO
ALTER TABLE [dbo].[recharge] ADD  DEFAULT (NULL) FOR [description]
GO
ALTER TABLE [dbo].[recharge] ADD  DEFAULT (NULL) FOR [datestart]
GO
ALTER TABLE [dbo].[recharge] ADD  DEFAULT (NULL) FOR [dateend]
GO
ALTER TABLE [dbo].[recharge] ADD  DEFAULT ('0') FOR [piid]
GO
ALTER TABLE [dbo].[staff] ADD  DEFAULT ('0') FOR [staffid]
GO
ALTER TABLE [dbo].[staff] ADD  DEFAULT (NULL) FOR [stafffirst]
GO
ALTER TABLE [dbo].[staff] ADD  DEFAULT (NULL) FOR [stafflast]
GO
ALTER TABLE [dbo].[staff] ADD  DEFAULT (NULL) FOR [staffemail]
GO
ALTER TABLE [dbo].[staff] ADD  DEFAULT (NULL) FOR [stafftitle]
GO
ALTER TABLE [dbo].[staff] ADD  DEFAULT (NULL) FOR [staffpassword]
GO
ALTER TABLE [dbo].[submission_userjob_bacterialclone] ADD  DEFAULT ('') FOR [Seq_Direction]
GO
ALTER TABLE [dbo].[submission_userjob_bacterialclone] ADD  DEFAULT ('0') FOR [Primer1]
GO
ALTER TABLE [dbo].[submission_userjob_bacterialclone] ADD  DEFAULT ('0') FOR [Primer2]
GO
ALTER TABLE [dbo].[submission_userjob_bacterialclone] ADD  DEFAULT ('0') FOR [StrainID]
GO
ALTER TABLE [dbo].[submission_userjob_bacterialclone] ADD  DEFAULT ('0') FOR [VectorID]
GO
ALTER TABLE [dbo].[submission_userjob_bacterialclone] ADD  DEFAULT ('5') FOR [antibioticID]
GO
ALTER TABLE [dbo].[submission_userjob_dna] ADD  DEFAULT ('') FOR [Seq_Direction]
GO
ALTER TABLE [dbo].[submission_userjob_dna] ADD  DEFAULT ('0') FOR [Primer1]
GO
ALTER TABLE [dbo].[submission_userjob_dna] ADD  DEFAULT ('0') FOR [Primer2]
GO
ALTER TABLE [dbo].[submission_userjob_qbot_colonypicking] ADD  DEFAULT ('0') FOR [vectorID]
GO
ALTER TABLE [dbo].[submission_userjob_qbot_colonypicking] ADD  DEFAULT ('0') FOR [hostID]
GO
ALTER TABLE [dbo].[submission_userjob_qbot_colonypicking] ADD  DEFAULT ('') FOR [numOfQTrays]
GO
ALTER TABLE [dbo].[submission_userjob_qbot_colonypicking] ADD  DEFAULT ('') FOR [numOfGlycerol]
GO
ALTER TABLE [dbo].[submission_userjob_qbot_colonypicking] ADD  DEFAULT ('') FOR [concentration]
GO
ALTER TABLE [dbo].[submission_userjob_qbot_colonypicking] ADD  DEFAULT ('') FOR [replication]
GO
ALTER TABLE [dbo].[submission_userjob_qbot_colonypicking] ADD  DEFAULT ('') FOR [numOfColonies]
GO
ALTER TABLE [dbo].[submission_userjob_qbot_gridding] ADD  DEFAULT ('0') FOR [vectorID]
GO
ALTER TABLE [dbo].[submission_userjob_qbot_gridding] ADD  DEFAULT ('0') FOR [hostID]
GO
ALTER TABLE [dbo].[submission_userjob_qbot_gridding] ADD  DEFAULT ('') FOR [numOfSourcePlates]
GO
ALTER TABLE [dbo].[submission_userjob_qbot_gridding] ADD  DEFAULT ('') FOR [copiesOfMembranes]
GO
ALTER TABLE [dbo].[submission_userjob_qbot_gridding] ADD  DEFAULT ('') FOR [pattern]
GO
ALTER TABLE [dbo].[submission_userjob_qbot_replicating] ADD  DEFAULT ('0') FOR [vectorID]
GO
ALTER TABLE [dbo].[submission_userjob_qbot_replicating] ADD  DEFAULT ('0') FOR [hostID]
GO
ALTER TABLE [dbo].[submission_userjob_qbot_replicating] ADD  DEFAULT ('') FOR [numOfSourcePlates]
GO
ALTER TABLE [dbo].[submission_userjob_qbot_replicating] ADD  DEFAULT ('') FOR [plateTypeDestination]
GO
ALTER TABLE [dbo].[submission_userjob_qbot_replicating] ADD  DEFAULT ('') FOR [replication]
GO
ALTER TABLE [dbo].[submission_userjob_sublibrary] ADD  DEFAULT ('') FOR [typeOfSamples]
GO
ALTER TABLE [dbo].[submission_userjob_sublibrary] ADD  DEFAULT (NULL) FOR [concentrationOfDNA]
GO
ALTER TABLE [dbo].[submission_userjob_sublibrary] ADD  DEFAULT (NULL) FOR [insertGenomeSize]
GO
ALTER TABLE [dbo].[submission_userjob_sublibrary] ADD  DEFAULT (NULL) FOR [coverage]
GO
ALTER TABLE [dbo].[submission_userjob_sublibrary] ADD  DEFAULT (NULL) FOR [vectorID]
GO
ALTER TABLE [dbo].[submission_userjob_sublibrary] ADD  DEFAULT ('5') FOR [antibioticID]
GO
ALTER TABLE [dbo].[submission_userjob_userrun] ADD  DEFAULT ('') FOR [Seq_Direction]
GO
ALTER TABLE [dbo].[submission_userjob_userrun] ADD  DEFAULT ('0') FOR [DyeID]
GO
ALTER TABLE [dbo].[submission_userjob_userrun_genotyping] ADD  DEFAULT ('') FOR [dyeIDs]
GO
ALTER TABLE [dbo].[submission_userjobs] ADD  DEFAULT ('0') FOR [uid]
GO
ALTER TABLE [dbo].[submission_userjobs] ADD  DEFAULT ('') FOR [accountid]
GO
ALTER TABLE [dbo].[submission_userjobs] ADD  DEFAULT ('') FOR [JobName]
GO
ALTER TABLE [dbo].[submission_userjobs] ADD  DEFAULT ('0') FOR [submissionType]
GO
ALTER TABLE [dbo].[submission_userjobs] ADD  DEFAULT ('0') FOR [HowManyPlates]
GO
ALTER TABLE [dbo].[submission_userjobs] ADD  DEFAULT ('') FOR [plateType]
GO
ALTER TABLE [dbo].[submission_userjobs] ADD  DEFAULT ('0') FOR [id2]
GO
ALTER TABLE [dbo].[submission_userjobs] ADD  DEFAULT ('0') FOR [done]
GO
ALTER TABLE [dbo].[submission_userplates] ADD  DEFAULT ('0') FOR [JobID]
GO
ALTER TABLE [dbo].[submission_userplates] ADD  DEFAULT ('') FOR [plateName]
GO
ALTER TABLE [dbo].[user] ADD  DEFAULT (NULL) FOR [username]
GO
ALTER TABLE [dbo].[user] ADD  DEFAULT (NULL) FOR [lastname]
GO
ALTER TABLE [dbo].[user] ADD  DEFAULT (NULL) FOR [firstname]
GO
ALTER TABLE [dbo].[user] ADD  DEFAULT (NULL) FOR [title]
GO
ALTER TABLE [dbo].[user] ADD  DEFAULT (NULL) FOR [email]
GO
ALTER TABLE [dbo].[user] ADD  DEFAULT (NULL) FOR [passwords]
GO
ALTER TABLE [dbo].[user] ADD  DEFAULT (NULL) FOR [phonenum]
GO
ALTER TABLE [dbo].[user] ADD  DEFAULT (NULL) FOR [valid]
GO
ALTER TABLE [dbo].[user] ADD  DEFAULT (NULL) FOR [datejoined]
GO
ALTER TABLE [dbo].[user] ADD  DEFAULT ('0') FOR [piid]
GO
ALTER TABLE [dbo].[useracct] ADD  DEFAULT (NULL) FOR [valid]
GO
ALTER TABLE [dbo].[useracct] ADD  DEFAULT ('0') FOR [rechargeid]
GO
ALTER TABLE [dbo].[useracct] ADD  DEFAULT ('0') FOR [userid]
GO
