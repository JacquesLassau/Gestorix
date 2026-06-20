USE [DB_GESTORIX]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AtualizarPedido]  
	@IdPedido int,
	@DescricaoItensPedido varchar(2000),
	@CustoPedido decimal (9,2),
	@PrecoPedido decimal (9,2),
	@LucroPedido decimal (9,2)
AS
BEGIN
	Update Pedido Set
		DESCRICAO = @DescricaoItensPedido,
		TOTAlCUSTO = @CustoPedido,
		TOTAlPEDIDO = @PrecoPedido,
		TOTAlLUCRO = @LucroPedido,
		DATaHORaFINALIZADO = GETUTCDATE()
	Where
		IdPEDIDO = @IdPedido AND SITUACAO = 'A'	
END
GO