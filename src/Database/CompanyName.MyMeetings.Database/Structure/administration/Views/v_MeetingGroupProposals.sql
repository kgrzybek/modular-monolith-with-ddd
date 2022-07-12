CREATE VIEW [administration].[v_MeetingGroupProposals]
AS
SELECT
    [MeetingGroupProposal].[Id],
    [MeetingGroupProposal].[Name],
    [MeetingGroupProposal].[Description],
    [MeetingGroupProposal].[LocationCity],
    [MeetingGroupProposal].[LocationCountryCode],
    [MeetingGroupProposal].[ProposalUserId],
    [MeetingGroupProposal].[ProposalDate],
    [MeetingGroupProposal].[StatusCode],
    [MeetingGroupProposal].[DecisionDate],
    [MeetingGroupProposal].[DecisionUserId],
    [MeetingGroupProposal].[DecisionCode],
    [MeetingGroupProposal].[DecisionRejectReason]
FROM [administration].[MeetingGroupProposals] AS [MeetingGroupProposal]
GO