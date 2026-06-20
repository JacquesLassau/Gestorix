USE [DB_GESTORIX] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_BuscarValidadeToken]		
	@StrToken varchar(255)
AS
BEGIN	
	IF NOT EXISTS (Select DATaACESSO From DB_GESTORIX..Token With(nolock) Where STrTOKEN = @StrToken And DATaACESSO IS NOT NULL)
	BEGIN			
		Select STrTOKEN From DB_GESTORIX..Token With(nolock) Where STrTOKEN = @StrToken
	END		
END
GO
