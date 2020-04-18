Feature: Validate Posts
	In order to use all details from posts
	As a consumer of api
	I want to see all the posts details in response


	Scenario: Validate the get post with uri returns all post properties for PostId 1
	Given I have valid endpoint for resource "posts/1"
	When I do a "GET" request
	Then I should receive the success state as "true"
	And I should expect to see below details in response
	| userId | id | title                                                                      | body                                                                                                                                                              |
	| 1      | 1  | sunt aut facere repellat provident occaecati excepturi optio reprehenderit | quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto |  


	Scenario: Validate the get post with uri returns all post properties for PostId 2
	Given I have valid endpoint for resource "posts/2"
	When I do a "GET" request
	Then I should receive the status code as "200"
	And I should expect to see below details in response
	| userId | id | title        | body                                                                                                                                                                                                              |
	| 1      | 2  | qui est esse | est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla |  

	Scenario Outline: Validate all comments returned are related to the postId requested for
	Given I have valid endpoint for resource "posts/<postIdValue>/comments"
	When I do a "GET" request
	Then I should receive the status code as "200"
	And I should have "postId" as "<postIdValue>" in all comments
	Examples:
	 | postIdValue |
	 | 1           |
	 | 2           |
	 | 3           |  


	Scenario Outline: Validate all comments returned for requested postId by comparing the count in all comments uri(/comments)
	Given I have valid endpoint for resource "comments"
	And I do a "GET" request
	And I should receive the status code as "200"
	And I should expect to see "500" comments in the response
	And I have valid endpoint for resource "posts/<postId>/comments"
	When I do a "GET" request
	Then I should receive the status code as "200"
	And I should expect all comments for postId: "<postId>" are returned
	Examples:
	 | postId |
	 | 1      |
	 | 2      |
	 | 3      |    

	# Negative Error reponse scenarios

	Scenario Outline: Validate error reponse for invalid postId for posts
	Given I have valid endpoint for resource "posts/<postId>"
	When I do a "GET" request
	Then I should receive the status code as "404"
	
	Examples:
	| postId |
	| postId |
	| 12000  |
	| 1*)    |
	| 0      |


	Scenario Outline: Validate no results for invalid postId for comments
	Given I have valid endpoint for resource "posts/<postId>/comments"
	When I do a "GET" request
	#Then I should receive the status code as "404" , Arguably api should return 404 when resource is not found ,assumed 200 is expected hence validating 0 results
	Then I should receive the status code as "200"
	And I should expect to see "0" results in the response
	
	Examples:
	| postId |
	| postId |
	| 12000  |
	| 1*)    |
	| 0      |

	Scenario: Validate the post and post/comments uris return 403 with HTTP 
	Given I have valid endpoint for resource "posts/2"
	And I change the endpoint to be "http"
	When I do a "GET" request
	Then I should receive the status code as "403"
 