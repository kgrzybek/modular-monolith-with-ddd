CREATE VIEW [meetings].[v_MeetingDetails]
AS
SELECT
    [Meeting].[Id],
    [Meeting].[MeetingGroupId],
    [Meeting].[Title],
    [Meeting].[TermStartDate],
    [Meeting].[TermEndDate],
    [Meeting].[Description],
    [Meeting].[LocationName],
    [Meeting].[LocationAddress],
    [Meeting].[LocationPostalCode],
    [Meeting].[LocationCity],
    [Meeting].[AttendeesLimit],
    [Meeting].[GuestsLimit],
    [Meeting].[RSVPTermStartDate],
    [Meeting].[RSVPTermEndDate],
    [Meeting].[EventFeeValue],
    [Meeting].[EventFeeCurrency]
FROM [meetings].[Meetings] AS [Meeting]
GO
