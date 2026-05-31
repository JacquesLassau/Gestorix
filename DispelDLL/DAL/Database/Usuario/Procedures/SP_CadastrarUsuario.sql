USE [DB_DISPEL] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CadastrarUsuario]	
	@Tipo char(1),
	@Situacao char(1),
	@Nome varchar(255) = NULL,
	@Ddd varchar(3) = NULL,
	@Telefone varchar(10) = NULL,
	@Email varchar(255),
	@Senha varchar(255)		
AS
BEGIN	
	IF EXISTS (Select EMAIL From DB_DISPEL..Usuario With(nolock) Where EMAIL = @Email And SITUACAO = 'A')
	BEGIN			
		SELECT 'Já existe um usuário vinculado a este e-mail. Usuário não foi incluído.' AS MSgERRO	
	END
	ELSE
	BEGIN
		Insert Into DB_DISPEL..Usuario Values (@Tipo, @Situacao, @Nome, @Ddd, @Telefone, @Email, @Senha);							
	END
END
GO