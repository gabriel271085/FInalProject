Feature: Update a product

A short summary of the feature

@tag1
Scenario: Update an existing product
	Given A valid authentication endpoint to the site  
	And An endpoint and a body information to update a product
	When A post request is sent
	Then A valid http response code is expected
