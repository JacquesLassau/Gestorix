CREATE PROCEDURE SP_RelatorioAnaliticoPedidos    
    @DataInicial DATETIME,    
    @DataFinal DATETIME    
AS    
BEGIN    
    SET NOCOUNT ON     
  
    -- 1. Totais por pedido (sintético)  
    SELECT     
        p.IdPEDIDO,         
        p.DATaHORaFINALIZADO,    
        p.TOTAlCUSTO,    
        p.TOTAlPEDIDO,    
        p.TOTAlLUCRO    
    FROM Pedido p    
    WHERE p.SITUACAO = 'A'   
      AND p.DATaHORaFINALIZADO BETWEEN @DataInicial AND @DataFinal  
 ORDER BY p.DATaHORaFINALIZADO DESC  
  
    -- 2. Totais gerais da tabela sintética  
    SELECT   
        SUM(p.TOTAlCUSTO) AS TotalSinteticoDeCusto,  
        SUM(p.TOTAlPEDIDO) AS TotalSinteticoDePreco,  
        SUM(p.TOTAlLUCRO) AS TotalSinteticoDeLucro  
    FROM Pedido p  
    WHERE p.SITUACAO = 'A'  
      AND p.DATaHORaFINALIZADO BETWEEN @DataInicial AND @DataFinal  
  
    -- 3. Itens dos pedidos (analítico)  
    SELECT     
        ip.IdPEDIDO as IdPEDIDOiTEM,     
  p.DATaHORaFINALIZADO,  
        ip.TIPoITEM,    
        ip.IdREFERENCIA as IdERFiTEM,    
        ip.DESCRICAO as DESCRICAOiTEM,   
        ip.CUSTO as CUSTOiTEM,    
        ip.PRECO as PRECOiTEM,    
        ip.LUCRO as LUCROiTEM,    
        ip.QUANTIDADE as QTDiTEM    
    FROM ItemPedido ip    
    INNER JOIN Pedido p ON p.IdPEDIDO = ip.IdPEDIDO    
    WHERE p.SITUACAO = 'A'  
      AND p.DATaHORaFINALIZADO BETWEEN @DataInicial AND @DataFinal   
 ORDER BY   
  ip.IdREFERENCIA, p.DATaHORaFINALIZADO DESC     
  
    -- 4. Totais gerais da tabela analítica  
    SELECT   
        SUM(ip.CUSTO) AS TotalAnaliticoDeCusto,  
        SUM(ip.PRECO) AS TotalAnaliticoDePreco,  
        SUM(ip.LUCRO) AS TotalAnaliticoDeLucro  
    FROM ItemPedido ip  
    INNER JOIN Pedido p ON p.IdPEDIDO = ip.IdPEDIDO  
    WHERE p.SITUACAO = 'A'  
      AND p.DATaHORaFINALIZADO BETWEEN @DataInicial AND @DataFinal  
  
    -- 5. Top 3 mais vendidos    
    SELECT TOP 3     
        ip.TIPoITEM as TpITEMtOP3,    
        ip.IdREFERENCIA as IdREfITEMtOP3,    
        ip.DESCRICAO as DESCRICAOtOP3,    
        SUM(ip.LUCRO) as LUCROP3,    
        SUM(ip.QUANTIDADE) AS QUANTIDADeP3          
    FROM ItemPedido ip    
    INNER JOIN Pedido p ON p.IdPEDIDO = ip.IdPEDIDO    
    WHERE p.SITUACAO = 'A'  
      AND p.DATaHORaFINALIZADO BETWEEN @DataInicial AND @DataFinal    
    GROUP BY ip.TIPoITEM, ip.IdREFERENCIA, ip.DESCRICAO    
    ORDER BY LUCROP3 DESC    
  
    -- 6. Top 3 com maior lucro    
    SELECT TOP 3     
        ip.TIPoITEM as TIPoITEMTOPlUCRO,    
        ip.IdREFERENCIA as IdREFTOPlUCRO,    
        ip.DESCRICAO as DESCRICAoTOPlUCRO,           
        SUM(ip.LUCRO) AS TotalLucro,  
        SUM(ip.QUANTIDADE) AS QUANTIDADeP4  
    FROM ItemPedido ip    
    INNER JOIN Pedido p ON p.IdPEDIDO = ip.IdPEDIDO    
    WHERE p.SITUACAO = 'A'  
      AND p.DATaHORaFINALIZADO BETWEEN @DataInicial AND @DataFinal    
    GROUP BY ip.TIPoITEM, ip.IdREFERENCIA, ip.DESCRICAO    
    ORDER BY TotalLucro DESC    
  
    -- 7. Top 3 piores itens (maior prejuízo unitário)    
    SELECT TOP 3     
        ip.TIPoITEM as TIPoITEMpREJUIZO,    
        ip.IdREFERENCIA as IdREFpREJUIZO,    
        ip.DESCRICAO as DESCRICAOpREJUIZO,    
        SUM(ip.LUCRO) AS TotalPrejuizo,  
        SUM(ip.QUANTIDADE) AS QUANTIDADeP5    
    FROM ItemPedido ip    
    INNER JOIN Pedido p ON p.IdPEDIDO = ip.IdPEDIDO    
    WHERE p.SITUACAO = 'A'  
      AND p.DATaHORaFINALIZADO BETWEEN @DataInicial AND @DataFinal    
 GROUP BY  
  ip.TIPoITEM, ip.IdREFERENCIA, ip.DESCRICAO  
    ORDER BY TotalPrejuizo ASC    
  
END    