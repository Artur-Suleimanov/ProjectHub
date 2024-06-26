CREATE PROCEDURE [dbo].[spAddUserInProject]
	@ProjectId int,
	@UserId nvarchar(128),
	@RoleId nvarchar(256)
AS
begin
	set nocount on;

	insert into dbo.ProjectParticipants(ProjectId, UserId, RoleId)
	values (@ProjectId, @UserId, @RoleId)
end
