USE [DB_DISPEL]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CadastrarItemPedido]  
	@Pedido int,
	@Tipo varchar(10),
    @Referencia varchar(50),
    @Descricao  varchar(255),
	@Custo decimal (9,2),
	@Preco decimal (9,2),
	@Lucro decimal (9,2),
	@Quantidade int
AS
BEGIN
	Insert Into ItemPedido Values (@Pedido, @Referencia, @Tipo, @Descricao, @Custo, @Preco, @Lucro, @Quantidade, 'A')
END
GO