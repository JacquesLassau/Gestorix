USE [DB_GESTORIX]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CadastrarPedido]  
	@DescricaoItensPedido varchar(2000),
	@CustoPedido decimal (9,2),
	@PrecoPedido decimal (9,2),
	@LucroPedido decimal (9,2)
AS
BEGIN
	SET NOCOUNT ON
	Insert Into Pedido Values (@DescricaoItensPedido, GETUTCDATE(), null, @CustoPedido, @PrecoPedido, @LucroPedido, 'SISTEMA', 'A')
	Select SCOPE_IDENTITY() AS IdPedido
END
GO