CREATE PROCEDURE [dbo].[spGetProjectById]
	@Id int
AS
begin
	set nocount on;

	SELECT p.Id, p.Name, p.Description, ppI.UserId, p.CreateDate
	from [dbo].[Project] p
	join ProjectParticipants ppI
	on ppi.ProjectId = p.Id
	where p.Id = @Id and ppI.RoleId = 1
end
