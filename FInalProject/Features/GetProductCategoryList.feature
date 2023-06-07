Feature: Get products by category

A short summary of the feature

@tag1
Scenario: Retrieve products by category
	Given An endpoint with the query parameter 'category' set to '5'
	When I send a get request
	Then The response should have a status code of 'OK'
