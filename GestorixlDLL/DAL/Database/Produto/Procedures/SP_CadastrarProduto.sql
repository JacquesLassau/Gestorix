USE [DB_GESTORIX]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_CadastrarProduto]  
	@ReferenciaProduto varchar(50),
	@Tipo varchar(10),
	@Descricao varchar(255),
	@Fabricante varchar(255) = NULL,
	@CustoProduto decimal (9,2) = NULL,
	@PrecoProduto decimal (9,2) = NULL,
	@Situacao char(1)
AS
BEGIN
	IF EXISTS (Select IdREFPRODUTO From DB_GESTORIX..Produto With(nolock) Where IdREFPRODUTO = @ReferenciaProduto AND SITUACAO = 'A')
	BEGIN					
		SELECT 'Não foi possível incluir esse Produto porque a referência usada já existe em outro cadastro.'	
	END	
	ELSE
	BEGIN
		Insert Into DB_GESTORIX..Produto Values (@ReferenciaProduto, @Tipo, @Descricao, @Fabricante, @CustoProduto, @PrecoProduto, @Situacao);		
		Insert Into DB_GESTORIX..Estoque Values (@ReferenciaProduto, 0, NULL, NULL, 'SISTEMA', @Situacao);
	END
END
GO