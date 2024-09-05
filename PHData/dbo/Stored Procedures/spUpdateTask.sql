CREATE PROCEDURE [dbo].[spUpdateTask]
	@Id int,
	@Name nvarchar(256),
	@Description nvarchar(MAX),
	@ExecutorId nvarchar(128),
	@StateId int,
	@SolutionText nvarchar(MAX)
AS
begin
	set nocount on;

	update Task
	set Task.Name = @Name, Task.Description = @Description, Task.ExecutorId = @ExecutorId,
	Task.StateId = @StateId, Task.SolutionText = @SolutionText
	where Task.Id = @Id
end
