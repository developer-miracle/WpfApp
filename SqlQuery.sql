CREATE PROCEDURE [dbo].[UpdateBooks]
	@pId INT,
	@pName NVARCHAR(200),
	@pPrice MONEY,
	@pAuthorId INT,
	@pCategoryId INT
AS
BEGIN
SET NOCOUNT ON
UPDATE [dbo].[Books]
SET	
[name] = @pName,
[price] = @pPrice,
[author_id] = @pAuthorId,
[category_id] = @pCategoryId
WHERE id = @pId
END


SELECT * FROM Books WHERE author_id = (SELECT id FROM Authors WHERE	[name] = N'Рэй Брэдбери');
SELECT * FROM Books WHERE author_id = (SELECT id FROM Authors WHERE	[name] = N'@pName')