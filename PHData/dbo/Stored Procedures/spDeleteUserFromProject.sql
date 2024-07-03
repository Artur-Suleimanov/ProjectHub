CREATE PROCEDURE [dbo].[spDeleteUserFromProject]
	@ProjectId int,
	@UserId nvarchar(128)
AS
begin
	set nocount on;

	delete from ProjectParticipants 
	where ProjectParticipants.ProjectId = @ProjectId and ProjectParticipants.UserId = @UserId
end
