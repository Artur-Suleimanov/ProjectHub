CREATE PROCEDURE [dbo].[spCreateTask]
	@Name  nvarchar(256),
	@Description nvarchar(MAX),
	@InitiatorId nvarchar(128),
	@ExecutorId nvarchar(128),
	@ProjectId int,
	@StateId int
AS
begin
	set nocount on;

	insert into dbo.Task([Name], [Description], InitiatorId, ExecutorId, ProjectId, StateId)
	values (@Name, @Description, @InitiatorId, @ExecutorId, @ProjectId, @StateId)

end
