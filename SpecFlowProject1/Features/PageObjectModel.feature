Feature: PageObjectModel

Search testers talk in YouTube

@TestersTalk
Scenario: PageObjectModel
	Given Enter the youTube URL
	When Search for Testers Talk
	And Navigate to Channel
	Then Verify the title of page
