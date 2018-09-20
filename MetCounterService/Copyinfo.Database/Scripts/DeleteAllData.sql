/****** Script for SelectTopNRows command from SSMS  ******/
DELETE
  FROM [MetCounterService].[Machine].[Record]

DELETE
	FROM [MetCounterService].Machine.EmailSource

DELETE
	FROM [MetCounterService].Machine.ServiceSourceCounters

DELETE
	FROM [MetCounterService].Machine.ServiceSourceSerialNumber

DELETE
	FROM [MetCounterService].Warehouse.EmailSource