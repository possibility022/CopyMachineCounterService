use MetCounterService

SELECT count(*)
  FROM [Machine].[Record]

SELECT
    [CounterBlackAndWhite], [CounterColor], [CounterScanner], [SerialNumber], [ReadDatetime], COUNT(*)
FROM
    [Machine].[Record]
GROUP BY
    [CounterBlackAndWhite], [CounterColor], [CounterScanner], [SerialNumber], [ReadDatetime]
HAVING 
    COUNT(*) > 1

DELETE FROM
	[Machine].[Record]
where
	Id NOT IN (
	SELECT MIN(Id) 
	FROM [Machine].[Record] 
	Group BY [CounterBlackAndWhite], [CounterColor], [CounterScanner], [SerialNumber], [ReadDatetime]
	)

DELETE FROM
	[Machine].[EmailSource]
	where NOT EXISTS (
		Select top(1) Id 
		FROM [Machine].Record rec 
		where rec.EmailSource = [Machine].[EmailSource].Id)

DELETE FROM
	[Machine].[ServiceSourceCounters]
	where NOT EXISTS (
		Select top(1) Id 
		FROM [Machine].Record rec 
		where rec.ServiceSourceCounters = [Machine].[ServiceSourceCounters].Id)

DELETE FROM
	[Machine].[ServiceSourceSerialNumber]
	where NOT EXISTS (
		Select top(1) Id 
		FROM [Machine].Record rec 
		where rec.ServiceSourceSerialNumber = [Machine].[ServiceSourceSerialNumber].Id)