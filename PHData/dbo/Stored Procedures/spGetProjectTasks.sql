CREATE PROCEDURE [dbo].[spGetProjectTasks]
	@Id int
AS
begin
	set nocount on;

	select Id, [Name], [Description], InitiatorId, ExecutorId, ProjectId,StateId, CreateDate
	from Task
	where Task.ProjectId = @Id
end