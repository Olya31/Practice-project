SELECT TOP (1000) [Id]
       [Name]
      ,[Year]
      ,[Professional]
      ,[Number]
  FROM [Candidate].[dbo].[Candidate]

  TRUNCATE TABLE [Candidate].[dbo].[Candidate];

  SELECT * FROM [Candidate].[dbo].[Candidate];

  insert into [Candidate].[dbo].[Candidate]( [Id]
      ,[Name]
      ,[Year]
      ,[Professional]
      ,[Number]
	  )
	  values(7,'Митронов К.Н.', 22, 'Программист', 7)