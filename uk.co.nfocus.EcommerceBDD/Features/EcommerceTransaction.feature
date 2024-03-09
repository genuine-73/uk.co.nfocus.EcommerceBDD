Feature: EcommerceTransaction


Background: 
	Given I am on the login page
	And I have logged in using valid credentials
	When I navigate to the Shop page


#Scenario: Applying a coupon code
#	When I add an item to my cart
#	And apply coupon 'edgewords'
#	Then I should get 10% off my selected item


@TestCaseOne
Scenario Outline: Applying a coupon
	When I add an '<item>' to my cart
	And and view cart to apply coupon '<coupon>'
	Then I should get '<discount>'% off my selected item
Examples: 
| item   | coupon    | discount |
| Cap    | edgewords |    10    |
| Beanie | edgewords |    15    |
| Belt   | nfocus    |    25    |


#@TestCaseTwo
#Scenario: Placing an order
#	When I add an 'Belt' to my cart
#	And fill out the billing details to place an order in checkout
#	Then I should see an order summary
#	And access it from my orders portal

@TestCaseTwo
Scenario: Placing an order
	When I add an '<item>' to my cart
	And I fill out my '<firstName>', '<lastName>', '<country>', '<address>', '<city>', '<postcode>' and '<phoneNo>' to place an order in checkout 
	Then I should see an order summary
	And access it from my orders portal

Examples: 
| item | firstName | lastName | country             | address        | city   | postcode | phoneNo      |
| Belt | Jane      | Doe      | United Kingdom (UK) | 149 Piccadilly | London | W1J 7NT  | 01632 907767 |