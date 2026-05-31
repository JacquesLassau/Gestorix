USE [DB_DISPEL]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[SP_ExcluirProduto]
 @ReferenciaProduto varchar(50),
 @Situacao char(1)
AS  
BEGIN  
 UPDATE DB_DISPEL..Produto SET SITUACAO = @Situacao WHERE IdREFPRODUTO = @ReferenciaProduto
 UPDATE DB_DISPEL..Estoque SET SITUACAO = @Situacao WHERE IdREFESTOQUE = @ReferenciaProduto
END