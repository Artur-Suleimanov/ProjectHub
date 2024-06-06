CREATE PROCEDURE [dbo].[spGetProjectTasks]
	@Id int
AS
begin
	set nocount on;

	select t.Id, t.[Name], [Description], InitiatorId, ExecutorId, ProjectId,StateId, ts.[Name] as State, CreateDate
	from Task t
	join dbo.TaskState ts
	on ts.Id = t.StateId
	where t.ProjectId = @Id
end