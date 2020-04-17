Feature: Validate Posts
	In order to use all details from posts
	As a consumer of api
	I want to see all the posts details in response

@mytag
	Scenario: Validate the get post with uri returns all post properties for Post 1
	Given I have valid endpoint
	When I do a "GET" request for resource "posts/1"
	Then I should receive the success response
	And I should expect to see below details in response
	| userId | id |title | body |
	|   1    |1   |sunt aut facere repellat provident occaecati excepturi optio reprehenderit      |  quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto    |

	Scenario: Validate the get post with uri returns all post properties for Post 2 userId 1
	Given I have valid endpoint
	When I do a "GET" request for resource "posts/2"
	Then I should receive the status code as "200"
	And I should expect to see below details in response
	| userId | id |title | body |
	|   1    |2   |qui est esse      |  est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla    |

	Scenario Outline: Validate the all comments got same postId in the results
	Given I have valid endpoint
	When I do a "GET" request for resource "posts/<propertyValue>/comments"
	Then I should receive the status code as "200"
	And I should have "<propertyName>" as "<propertyValue>" in comments
	Examples:
	|propertyName|propertyValue|
	|postId      |1             |
	|postId      |2             |
	|postId      |3             |
