CREATE PROCEDURE [dbo].[spGetProjectsWhereUserIsInitiator]
	@Id nvarchar(128)
AS
begin
	set nocount on;

	SELECT p.Id, p.Name, p.Description, pp.UserId, p.CreateDate
	from [dbo].[Project] p
	join ProjectParticipants pp
	on pp.ProjectId = p.Id
	where pp.UserId = @Id and pp.RoleId = 1
end