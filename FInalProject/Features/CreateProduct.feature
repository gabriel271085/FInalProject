Feature: Create Product



@tag1
Scenario: Create a new product
        Given I have an authentication endpoint to the site
        And endpoint to create a product
        When A POST request is sent
        Then valid response code is expected
       