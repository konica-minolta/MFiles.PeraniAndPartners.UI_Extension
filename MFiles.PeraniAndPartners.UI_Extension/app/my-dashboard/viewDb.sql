USE [Intranet_Perani]
GO

/****** Object:  View [dbo].[vw_domainnames]    Script Date: 23/01/2024 17:32:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Script for SelectTopNRows command from SSMS  ******/
CREATE VIEW [dbo].[vw_domainnames]
AS
SELECT dbo.[001-Domain name].[001-nome dominio completo] AS NomeDominioCompleto, dbo.[001-Domain name].[001-nome dominio] AS NomeDominio, dbo.[001-Domain name].[001- data registrazione] AS DataRegistrazione, 
                  dbo.[001-Domain name].[001- data scadenza] AS DataScadenza, dbo.[001-providerLS].Name AS Provider, dbo.[001-registrar-LS].Name AS Registrar, dbo.[001-clienteDominiLS].Name AS Cliente, dbo.[001-owner Domini].Name AS Owner, 
                  dbo.[001-Domain name].[001-noteexregistrar] AS NoteExRegistrar, dbo.[001-Domain name].[001- note-generali] AS NoteGenerali, RIGHT(dbo.[001-Domain name].[001-nome dominio completo], CHARINDEX('.', 
                  REVERSE(dbo.[001-Domain name].[001-nome dominio completo])) - 1) AS Estensione, dbo.[001-PRT Domini (ex ls)].[001-Num.Prt] AS NumeroPratica, dbo.[001- statusLS].Name AS Stato
FROM     dbo.[001-Domain name] LEFT OUTER JOIN
                  dbo.[001-providerLS] ON dbo.[001-Domain name].[001-provider_001-providerLS_ID] = dbo.[001-providerLS].ID LEFT OUTER JOIN
                  dbo.[001-registrar-LS] ON dbo.[001-Domain name].[001-registrar_001-registrar-LS_ID] = dbo.[001-registrar-LS].ID LEFT OUTER JOIN
                  dbo.[001-clienteDominiLS] ON dbo.[001-Domain name].[001-cliente Domini_001-clienteDominiLS_ID] = dbo.[001-clienteDominiLS].ID LEFT OUTER JOIN
                  dbo.[001-owner Domini] ON dbo.[001-Domain name].[001-owner Domini_ID] = dbo.[001-owner Domini].ID LEFT OUTER JOIN
                  dbo.[001-Domain name_001-Rif__ Pratica_001-PRT Domini (ex ls)] ON dbo.[001-Domain name].ID = dbo.[001-Domain name_001-Rif__ Pratica_001-PRT Domini (ex ls)].[001-Domain name_ID] LEFT OUTER JOIN
                  dbo.[001-PRT Domini (ex ls)] ON dbo.[001-PRT Domini (ex ls)].ID = dbo.[001-Domain name_001-Rif__ Pratica_001-PRT Domini (ex ls)].[001-PRT Domini (ex ls)_ID] LEFT OUTER JOIN
                  dbo.[001-Domain name_001-status_001- statusLS] ON dbo.[001-Domain name].ID = dbo.[001-Domain name_001-status_001- statusLS].[001-Domain name_ID] LEFT OUTER JOIN
                  dbo.[001- statusLS] ON dbo.[001- statusLS].ID = dbo.[001-Domain name_001-status_001- statusLS].[001- statusLS_ID]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "001-Domain name"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 435
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "001-providerLS"
            Begin Extent = 
               Top = 7
               Left = 483
               Bottom = 170
               Right = 677
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "001-registrar-LS"
            Begin Extent = 
               Top = 7
               Left = 725
               Bottom = 170
               Right = 919
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "001-clienteDominiLS"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 338
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "001-owner Domini"
            Begin Extent = 
               Top = 175
               Left = 290
               Bottom = 338
               Right = 484
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "001-Domain name_001-Rif__ Pratica_001-PRT Domini (ex ls)"
            Begin Extent = 
               Top = 175
               Left = 532
               Bottom = 294
               Right = 799
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "001-PRT Domini (ex ls)"
            Begin Extent = 
               Top = 294
               Left = 53' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_domainnames'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'2
               Bottom = 457
               Right = 919
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "001-Domain name_001-status_001- statusLS"
            Begin Extent = 
               Top = 175
               Left = 847
               Bottom = 294
               Right = 1087
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "001- statusLS"
            Begin Extent = 
               Top = 343
               Left = 48
               Bottom = 506
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_domainnames'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vw_domainnames'
GO

