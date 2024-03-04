Feature: EcommerceTransaction

Background: 
	Given I am on the login page
	And I have logged in using valid credentials
	When I navigate to the Shop page


Scenario: Applying a coupon code
	When I add an item to my cart
	And apply coupon 'edgewords'
	Then I should get ten percent off my selected item

Scenario: Placing an order
	When I add an item to my cart
	And fill out the billing details to place an order in checkout
	Then I should see an order summary
	And access it from my orders portal

