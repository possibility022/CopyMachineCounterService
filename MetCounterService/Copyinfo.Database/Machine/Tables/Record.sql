CREATE TABLE [Machine].[Record]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[CounterBlackAndWhite] INT NULL, 
    [CounterColor] INT NULL, 
    [CounterScanner] INT NULL,
    [Description] NVARCHAR(300) NULL, 
    [AddressIp] NVARCHAR(32) NULL, 
    [ReadDatetime] DATETIME NOT NULL, 
    [SerialNumber] NVARCHAR(20) NOT NULL, 
    [TonerLevelBlack] NVARCHAR(20) NULL, 
    [TonerLevelCyan] NVARCHAR(20) NULL, 
    [TonerLevelYellow] NVARCHAR(20) NULL, 
    [TonerLevelMagenta] NVARCHAR(20) NULL, 
    [AddressMac] NVARCHAR(20) NULL, 
    [EmailSource] INT NULL, 
    [ServiceSourceCounters] INT NULL,
	[ServiceSourceSerialNumber] INT NULL, 
    CONSTRAINT [FK_MachineRecord_EmailSource] FOREIGN KEY ([EmailSource]) REFERENCES [Machine].[EmailSource]([Id]), 
    CONSTRAINT [FK_MachineRecord_ServiceSourceCounters] FOREIGN KEY ([ServiceSourceCounters]) REFERENCES [Machine].[ServiceSourceCounters]([Id]), 
    CONSTRAINT [FK_MachineRecord_ServiceSourceSerialNumber] FOREIGN KEY ([ServiceSourceSerialNumber]) REFERENCES [Machine].[ServiceSourceSerialNumber]([Id])
)
