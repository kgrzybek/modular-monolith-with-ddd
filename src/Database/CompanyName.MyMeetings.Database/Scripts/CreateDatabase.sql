CREATE DATABASE [MyMeetings]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MyMeetings', FILENAME = N'/var/opt/mssql/data/MyMeetings.mdf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MyMeetings_log', FILENAME = N'/var/opt/mssql/data/MyMeetings_log.ldf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
GO

CREATE SCHEMA app AUTHORIZATION dbo
GO