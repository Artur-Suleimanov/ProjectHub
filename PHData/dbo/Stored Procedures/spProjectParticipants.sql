CREATE PROCEDURE [dbo].[spProjectParticipants]
	@Id int
AS
begin
	set nocount on;

	select u.Id, u.FirstName, u.LastName, u.EmailAddress, u.CreateDate
	from dbo.ProjectParticipants pp
	join dbo.[User] u
	on u.id = pp.UserId
	where pp.ProjectId = @Id
end
