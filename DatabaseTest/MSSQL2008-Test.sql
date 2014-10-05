USE MASTER
GO

IF NOT EXISTS( SELECT 1 FROM [master].dbo.sysdatabases WHERE name = 'DBTESTE' )
BEGIN
    CREATE DATABASE DBTESTE
    COLLATE SQL_Latin1_General_CP1_CI_AS
END
GO


USE DBTESTE
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'SimpleTable')
BEGIN
    CREATE TABLE [dbo].SimpleTable(
        id    int identity,
        name varchar(50) not null,
		comment nvarchar(max) null
    )
END
GO

IF NOT EXISTS( SELECT 1 FROM SimpleTable WHERE name = 'RegistroTeste1'  )
BEGIN
    INSERT INTO
        SimpleTable( name, comment )
    VALUES
        ( 'RegistroTeste1', 'Registro de teste simples versão 1.' )
END