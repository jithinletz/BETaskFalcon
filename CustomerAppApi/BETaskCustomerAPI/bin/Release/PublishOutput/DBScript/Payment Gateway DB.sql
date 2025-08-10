ALTER TABLE [dbo].[customer]
ADD app_customer_name nvarchar(250),app_phone nvarchar(50),
app_email nvarchar(50),app_address1 nvarchar(250),
app_address2 nvarchar(250)


----------------------------------------------------------------


USE [betaskdb_falcon_test]
GO

/****** Object:  StoredProcedure [dbo].[APP_SaveComplaint]    Script Date: 24-05-2023 11.27.22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sanoop S
-- Create date: 24-05-2023
-- Description:	Update Customer APP profile
-- =============================================
CREATE PROCEDURE [dbo].[APP_UpdateCustomerProfile]	   
		@CustomerId int ,
	    @APP_CustomerName nvarchar(250),
		@APP_Phone nvarchar(50), 
		@APP_Email nvarchar(50),
		@APP_Address1 nvarchar(250),
	    @APP_Address2 nvarchar(250) 
AS
BEGIN  

UPDATE  [dbo].[customer]  SET
[app_customer_name]=@APP_CustomerName,
[app_phone]=@APP_Phone,
[app_email]=@APP_Email,
[app_address1]=@APP_Address1,
[app_address2]=@APP_Address2  
WHERE [customer_id] =@CustomerId

	   
   
END

GO



