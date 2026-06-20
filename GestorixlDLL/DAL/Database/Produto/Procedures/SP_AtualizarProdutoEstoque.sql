USE [DB_GESTORIX]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AtualizarProdutoEstoque]  
	@ReferenciaProduto varchar(50),
	@Quantidade int,
	@Controle char(3)	
AS
BEGIN	
	IF EXISTS (Select IdREFPRODUTO From DB_GESTORIX..Produto Where IdREFPRODUTO = @ReferenciaProduto AND SITUACAO = 'A')
	BEGIN
		IF (@Controle = 'add')
		BEGIN
			UPDATE DB_GESTORIX..Estoque SET
				QUANTIDADE = QUANTIDADE + @Quantidade,
				DATaHORaENTRADA = GETUTCDATE(),
				DATaHORaSAIDA = NULL,
				USUARIoMANUTENCAO = 'SISTEMA'
			WHERE 
				IdREFESTOQUE = @ReferenciaProduto AND SITUACAO = 'A'
		END
		ELSE
		BEGIN
			UPDATE DB_GESTORIX..Estoque SET
				QUANTIDADE = (CASE WHEN (QUANTIDADE = 0 OR (QUANTIDADE - @Quantidade < 0)) then 0 else QUANTIDADE - @Quantidade end),
				DATaHORaENTRADA = NULL,
				DATaHORaSAIDA = GETUTCDATE(),
				USUARIoMANUTENCAO = 'SISTEMA'
			WHERE 
				IdREFESTOQUE = @ReferenciaProduto AND SITUACAO = 'A'
		END
	END
	ELSE
	BEGIN
		SELECT 'Esse Produto não existe mais no Estoque. Por favor, realize um novo cadastro ou contate o suporte especializado.'
	END
END
GO
