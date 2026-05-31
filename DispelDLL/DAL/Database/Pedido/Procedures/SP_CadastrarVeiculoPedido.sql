USE [DB_DISPEL]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CadastrarVeiculoPedido]  
	@Pedido int,
	@Descricao varchar(255),
    @Placa varchar(20),
    @QuilometrosRodados  varchar(20)	
AS
BEGIN
	Insert Into VeiculoPedido Values (@Pedido, @Descricao, @Placa, @QuilometrosRodados)
END
GO