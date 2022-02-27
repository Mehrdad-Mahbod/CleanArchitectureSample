CREATE PROC [dbo].[SelectListMenuWithUserIdAndRoleId] @UserId int,@RoleId int
AS
    BEGIN
        SELECT  AllMenu.*
        FROM    (--UserMenus
                  SELECT    ViewHierarchyMenu.ID ,
                            ViewHierarchyMenu.ParentID ,
                            ViewHierarchyMenu.Name ,
                            ViewHierarchyMenu.Component ,
                            ViewHierarchyMenu.Icon ,
                            ViewHierarchyMenu.MyGroup
                  FROM      ViewHierarchyMenu
                            INNER JOIN UserMenus ON ViewHierarchyMenu.ID = UserMenus.MenuId
														AND UserMenus.IsDeleted = 0

                  WHERE     UserMenus.UserID = @UserId
                  UNION ALL  
									--RoleMenus
                  SELECT    ViewHierarchyMenu.ID ,
                            ViewHierarchyMenu.ParentID ,
                            ViewHierarchyMenu.Name ,
                            ViewHierarchyMenu.Component ,
                            ViewHierarchyMenu.Icon ,
                            ViewHierarchyMenu.MyGroup
                  FROM      ViewHierarchyMenu
                            INNER JOIN RoleMenus ON ViewHierarchyMenu.ID = RoleMenus.MenuId
														WHERE RoleMenus.RoleID = @RoleId
                            AND RoleMenus.IsDeleted = 0

                ) AS AllMenu
        ORDER BY AllMenu.MyGroup ASC ,
                AllMenu.ParentID
    END