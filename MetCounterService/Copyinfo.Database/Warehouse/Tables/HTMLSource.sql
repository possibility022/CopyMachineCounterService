CREATE TABLE [Warehouse].[HTMLSource]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[CounterContent] NVARCHAR(MAX) NOT NULL,
	[SerialNumberContent] NVARCHAR(MAX) NOT NULL,
	[ReadDatetime] DATETIME NULL,
	[AddressMac] NVARCHAR(20) NULL, 
	[Description] VARCHAR(300) NULL,
	[AddressIp] NVARCHAR(32) NULL
)
