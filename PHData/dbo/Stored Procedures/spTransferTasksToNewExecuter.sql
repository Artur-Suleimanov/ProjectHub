CREATE PROCEDURE [dbo].[spTransferTasksToNewExecuter]
	@ProjectId int,
	@OldExecuterId nvarchar(128),
	@NewExecuterId nvarchar(128)
AS
begin
	set nocount on;

	update Task
	set  task.ExecutorId = @NewExecuterId
	where Task.ProjectId = @ProjectId and Task.ExecutorId = @OldExecuterId
end
