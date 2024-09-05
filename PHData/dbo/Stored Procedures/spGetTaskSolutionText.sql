CREATE PROCEDURE [dbo].[spGetTaskSolutionText]
	@Id int
AS
begin
	set nocount on;

	select Task.SolutionText
	from Task
	where Task.id = @id
end
