CREATE PROCEDURE [dbo].[spDeleteTask]
	@TaskId int
AS
begin
	set nocount on

	delete from dbo.Task
	where Task.id = @TaskId
end
