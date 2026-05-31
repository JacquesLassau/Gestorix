USE [DB_DISPEL]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_EditarEmpresa]
	@Cep varchar(10),
	@Logradouro varchar(255),
	@Numero varchar(10),
	@Bairro varchar(255),
	@Cidade varchar(255),
	@Estado varchar(25),
	@Ddd varchar(3),
	@Telefone varchar(10),
	@Email varchar(255),
	@Cnpj varchar(20),
	@UrlRedeSocial varchar(255),
	@DddChat varchar(3),
	@TelefoneChat varchar(10)
AS
BEGIN
	Update Empresa Set
		CEP = @Cep,
		LOGRADOURO = @Logradouro,
		NUMERO = @Numero,
		CIDADE = @Cidade,
		ESTADO = @Estado,
		DDD = @Ddd,
		TELEFONE = @Telefone,
		EMAIL = @Email,
		URlREDeSOCIAL = @UrlRedeSocial,
		DDdCHAT = @DddChat,
		TELEFONeCHAT = @TelefoneChat
	Where 
		CNPJ = @Cnpj	
END
GO