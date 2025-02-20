Feature: API Login Testing

  Scenario Outline: Verify login with role-based authentication
    Given I send a login request with username "<username>" and password "<password>"
    Then I should get a successful response
    And I should see the role as "<role>"

    Examples:
      | username   | password  | role  |
      | emilys     | emilyspass   | admin |
      | sophiab     | sophiabpass  | user  |
