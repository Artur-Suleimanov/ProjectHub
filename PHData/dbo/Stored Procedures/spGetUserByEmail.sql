CREATE PROCEDURE [dbo].[spGetUserByEmail]
	@Email nvarchar(256)
AS
begin
	set nocount on;

	select Id, FirstName, LastName, EmailAddress, CreateDate
	from dbo.[User]
	where EmailAddress = @Email
end
