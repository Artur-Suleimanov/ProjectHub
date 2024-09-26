CREATE PROCEDURE [dbo].[spDeleteProject]
	@ProjectId int
AS
begin
	set nocount on

	delete from dbo.Project
	where Project.id = @ProjectId
end
