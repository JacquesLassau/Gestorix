USE [DB_GESTORIX] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GravarAcessoToken]		
	@StrToken varchar(255)
AS
BEGIN	
	IF NOT EXISTS (Select EMAIL From DbSosPernambucanas..Token With(nolock) Where STrTOKEN = @StrToken)
	BEGIN			
		SELECT 'Não existe usuário para este token. Não será possível alterar a senha.'		
	END
	IF EXISTS (Select DATaACESSO From DbSosPernambucanas..Token With(nolock) Where STrTOKEN = @StrToken And DATaACESSO IS NOT NULL)
	BEGIN			
		SELECT 'Token inválido. Usuário já utilizou o link de acesso. Deve solicitar um novo link.'		
	END
	ELSE
	BEGIN
		Declare @Usuario Int = (Select USUARIO From DB_GESTORIX..Token With(nolock) Where STrTOKEN = @StrToken)		
		Update DB_GESTORIX..Token Set DATaACESSO = GETUTCDATE() Where STrTOKEN =  @StrToken And USUARIO = @Usuario		
	END
END
GO