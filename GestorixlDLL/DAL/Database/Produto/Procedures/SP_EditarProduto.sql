USE [DB_GESTORIX]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_EditarProduto]  
	@ReferenciaProduto varchar(50),
	@Tipo varchar(10),
	@Descricao varchar(255),
	@Fabricante varchar(255),
	@CustoProduto decimal (9,2),
	@PrecoProduto decimal (9,2)	
AS
BEGIN
	UPDATE DB_GESTORIX..Produto SET
		TIPO = @Tipo,
		DESCRICAO = @Descricao,
		FABRICANTE = @Fabricante,
		CUSTO = (CASE WHEN @CustoProduto IS NOT NULL then @CustoProduto else CUSTO end),
		PRECO = (CASE WHEN @PrecoProduto IS NOT NULL then @PrecoProduto else PRECO end)	
	WHERE 
		IdREFPRODUTO = @ReferenciaProduto
END
GO