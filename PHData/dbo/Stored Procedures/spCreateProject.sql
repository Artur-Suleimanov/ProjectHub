CREATE PROCEDURE [dbo].[spCreateProject]
	@UserId nvarchar(128),
	@ProjectName nvarchar(256),
	@ProjectDescription nvarchar(max)
AS
begin
	set nocount on;

	DECLARE @OutputId INT

	insert into dbo.Project([Name], [Description])
	values (@ProjectName, @ProjectDescription);

	set @OutputId = Scope_Identity()

	insert into dbo.ProjectParticipants(ProjectId, UserId, RoleId)
	values (@OutputId, @UserId, 1)

	SELECT p.Id, p.Name, p.Description, pp.UserId, p.CreateDate
	from Project p
	join ProjectParticipants pp
	on pp.ProjectId = p.Id and pp.RoleId = 1
	where p.Id =  @OutputId

end
