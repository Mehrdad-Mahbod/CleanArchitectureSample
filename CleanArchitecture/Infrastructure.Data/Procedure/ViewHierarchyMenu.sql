USE [CleanArchitecture]
GO

/****** Object:  View [dbo].[ViewHierarchyMenu]    Script Date: 2/24/2022 10:09:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ViewHierarchyMenu] AS WITH RecursiveMember ( ID, ParentID, Name, /*Controller, Action*/ Component, Icon, IsSelect, AddedDate, IsDeleted, [Level], MyGroup ) AS (
	SELECT
		Menus.* ,
		0,
		Menus.ID 
	FROM
		dbo.Menus 
	WHERE
		ParentID IS NULL UNION ALL
	SELECT
		Menus.* ,
		[Level] + 1,
		RecursiveMember.ID 
	FROM
		dbo.Menus
		INNER JOIN RecursiveMember ON RecursiveMember.ID = Menus.ParentID 
	) SELECT
	RecursiveMember.* 
FROM
	RecursiveMember
GO


