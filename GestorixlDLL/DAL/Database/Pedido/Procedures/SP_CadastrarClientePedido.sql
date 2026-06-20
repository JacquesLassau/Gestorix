USE [DB_GESTORIX]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CadastrarClientePedido]  
	@Pedido int,
	@Nome varchar(255),
    @Documento varchar(20),
    @Endereco  varchar(255),
	@Bairro varchar(255),
	@Cidade varchar(255),
	@Uf varchar(255),
	@Cep varchar(20)	
AS
BEGIN
	Insert Into ClientePedido Values (@Pedido, @Nome, @Documento, @Endereco, @Bairro, @Cidade, @Uf, @Cep)
END
GO