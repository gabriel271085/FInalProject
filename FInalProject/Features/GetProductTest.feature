Feature: Get a Product Test

A short summary of the feature

@tag1
Scenario: Get a HTTP request by ID
	Given An endpoint with id value 17
	When A GET request is sent
	Then A valid response code is expected
