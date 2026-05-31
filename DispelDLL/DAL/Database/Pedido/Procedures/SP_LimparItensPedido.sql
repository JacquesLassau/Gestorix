CREATE PROCEDURE [dbo].[SP_LimparItensPedido]    
 @Pedido int
AS  
BEGIN  
 Delete From ItemPedido Where IdPEDIDO =  @Pedido 
END  