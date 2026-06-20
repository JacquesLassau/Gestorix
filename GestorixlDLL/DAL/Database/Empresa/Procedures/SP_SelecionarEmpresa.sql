USE [DB_GESTORIX]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[SP_SelecionarEmpresa]
 @CnpjEmpresa varchar(20)
AS  
BEGIN  
  Select * From DB_GESTORIX..Empresa With(nolock) Where CNPJ = @CnpjEmpresa
END