CREATE PROCEDURE [dbo].[spGetProjectsByUserId]
	@Id nvarchar(128)
AS
begin
	set nocount on;

	SELECT p.Id, p.Name, p.Description, ppI.UserId, p.CreateDate
	from [dbo].[Project] p
	join ProjectParticipants pp
	on pp.ProjectId = p.Id
	join ProjectParticipants ppI
	on ppi.ProjectId = p.Id
	where pp.UserId = @Id and ppI.RoleId = 1
end
