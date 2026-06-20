USE [DB_GESTORIX] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AlterarSenhaUsuario]		
	@Email varchar(255),
	@NovaSenha varchar(255),
	@Tipo char(1)
AS
BEGIN	
	IF EXISTS (Select EMAIL From DB_GESTORIX..Usuario With(nolock) Where EMAIL = @Email And SITUACAO = 'A' And TIPO = @Tipo)
	BEGIN			
		Update DB_GESTORIX..Usuario Set SENHA = @NovaSenha Where EMAIL = @Email And TIPO = @Tipo		
	END	
END
GO
