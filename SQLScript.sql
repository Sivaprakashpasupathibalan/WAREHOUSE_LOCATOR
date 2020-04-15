-- =============================================                    
-- AUTHOR:  shivaprakash pasupathibalan                   
-- CREATE DATE: 15-04-2020                  
-- DESCRIPTION: TEST Table for WareHouse Locator                   
-- ============================================= 
CREATE TABLE [dbo].[WAREHOUSELOCATOR](
	[BinLocationName] [varchar](50) NOT NULL,
	[InvoiceType] [int] NOT NULL,
	[OutwardStatus] [int] NOT NULL,
	[CheckStatus] [int] NOT NULL,
	[StockId][int] PRIMARY KEY
) ON [PRIMARY]
SET ANSI_PADDING ON
GO
SET ANSI_PADDING OFF
GO
-- =============================================                    
-- AUTHOR:  shivaprakash pasupathibalan                   
-- CREATE DATE: 15-04-2020                  
-- DESCRIPTION:                    
-- =============================================
CREATE PROCEDURE [dbo].[GET_LOCATIONS]                                       
                                           
AS                        
BEGIN                        
                                
 select BinLocationName,InvoiceType,OutwardStatus,CheckStatus,StockId from WAREHOUSELOCATOR 
 where BinLocationName like 'A%-%-%' AND InvoiceType = 2 AND OutwardStatus=1  
 order by BinLocationName,StockId desc                        
END
GO