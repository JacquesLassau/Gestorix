USE [DB_DISPEL] 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_BuscarEmailToken]		
	@StrToken varchar(255)
AS
BEGIN	
	IF EXISTS (Select EMAIL From DB_DISPEL..Token With(nolock) Where STrTOKEN = @StrToken)
	BEGIN			
		Select EMAIL From DB_DISPEL..Token With(nolock) Where STrTOKEN = @StrToken
	END		
END
GO
