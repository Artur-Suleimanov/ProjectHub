CREATE PROCEDURE [dbo].[spCheckUserProjectMembership]
	@UserId nvarchar(128),
	@ProjectId int
	
AS
begin
	set nocount on;

	select pp.RoleId
	from ProjectParticipants pp
	where pp.UserId = @UserId and pp.ProjectId = @ProjectId
end
