USE [DB_DISPEL] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AcessoUsuario]
	@Email varchar(255),
	@Senha varchar(255), 
	@Tipo char(1)
AS
BEGIN	
	IF EXISTS (Select EMAIL, SENHA From DB_DISPEL..Usuario With(nolock) Where EMAIL = @Email And SENHA = @Senha And SITUACAO = 'A' And TIPO = @Tipo)
	BEGIN			
		Select IdUSUARIO, EMAIL, TIPO From DB_DISPEL..Usuario With(nolock) Where EMAIL = @Email And SENHA = @Senha And SITUACAO = 'A' And TIPO = @Tipo
	END			
END
GO