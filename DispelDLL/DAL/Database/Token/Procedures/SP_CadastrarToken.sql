USE [DB_DISPEL] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CadastrarToken]		
	@Email varchar(255),
	@UrlBase varchar(255),
	@StrToken varchar(255)
AS
BEGIN	
	IF NOT EXISTS (Select EMAIL From DB_DISPEL..Usuario With(nolock) Where EMAIL = @Email And SITUACAO = 'A')
	BEGIN			
		SELECT 'Não existe um token vinculado a este e-mail. Token não foi incluído.'		
	END
	ELSE
	BEGIN
		Declare @Usuario Int = (Select IdUSUARIO From DB_DISPEL..Usuario With(nolock) Where EMAIL = @Email And SITUACAO = 'A')
		Insert Into DB_DISPEL..Token Values (@Usuario, @Email, @UrlBase, @StrToken, NULL);				
	END
END
GO