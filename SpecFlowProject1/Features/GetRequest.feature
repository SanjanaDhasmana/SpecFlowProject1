Feature: To Test the GET Request

A short summary of the feature

@tag1
Scenario: GET Request Testing
	Given The user sends a GET request with URL "https://reqres.in/api/users?page=2"
	Then Request should be a success with 200 Status Code
