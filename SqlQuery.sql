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


CREATE PROCEDURE [dbo].[UpdateBooksAuthorsCategories]
@pId INT,
@pNameBook NVARCHAR(200),
@pNameAuthor NVARCHAR(200),
@pNameCategory NVARCHAR(200),
@pPrice MONEY
AS
BEGIN
SET NOCOUNT ON
UPDATE Books
SET	
Books.[name] = @pNameBook,
Books.price = @pPrice
WHERE Books.id = @pId
UPDATE Authors
SET	
Authors.[name] = @pNameAuthor
WHERE Authors.id = (SELECT author_id FROM Books WHERE id = @pId)
UPDATE Categories
SET	
Categories.[name] = @pNameCategory
WHERE Categories.id = (SELECT category_id FROM Books WHERE id = @pId)
END


SELECT * FROM Books WHERE author_id = (SELECT id FROM Authors WHERE	[name] = N'Рэй Брэдбери');
SELECT * FROM Books WHERE author_id = (SELECT id FROM Authors WHERE	[name] = N'@pName')

SELECT * FROM Books WHERE author_id = (SELECT id FROM Authors WHERE	[name] = N'@pName')

SELECT * FROM Books 
JOIN Authors
ON Books.Author_id = Authors.Id

SELECT Books.Id, Books.[name], Books.price, Authors.[name], Categories.[name] FROM Books 
JOIN Authors
ON Books.Author_id = Authors.Id
JOIN Categories
ON Books.category_id = Categories.Id 
WHERE Authors.[name] = N'Рэй Брэдбери'