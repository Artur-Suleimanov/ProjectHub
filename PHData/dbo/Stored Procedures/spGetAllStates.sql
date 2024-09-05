CREATE PROCEDURE [dbo].[spGetAllStates]

AS
begin
	set nocount on;

	select * 
	from TaskState
end
