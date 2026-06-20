USE [DB_GESTORIX] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_VerificarEmailUsuario]		 	
	@Tipo char(1),
	@Email varchar(255)
AS
BEGIN 	
	IF EXISTS (Select EMAIL From DB_GESTORIX..Usuario With(nolock) Where EMAIL = @Email And SITUACAO = 'A' And TIPO = @Tipo)
	BEGIN			
		Select EMAIL From DB_GESTORIX..Usuario With(nolock) Where EMAIL = @Email And SITUACAO = 'A' And TIPO = @Tipo
	END	
END
GO