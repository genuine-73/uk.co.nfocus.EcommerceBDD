Feature: EcommerceTransaction
In order to enhance customer experience when purchasing an Item online
the CEO of the clothing website
wants to provide an order functionality 

OT

Background: 
	Given I am on the login page
		And I have logged in using valid login credentials
		And I navigate to the Shop page
	
@TestCaseOne
Scenario Outline: Applying a coupon
	When I add '<item>' to my cart
		And I view cart to apply coupon '<coupon>'
	Then I should get '<discount>'% off my selected item

Examples: 
| item   | coupon    | discount |
| Belt   | edgewords | 15       |
| Beanie | nfocus    | 25       |
 

@TestCaseTwo
Scenario Outline: Placing an order
	When I add '<item>' to my cart
		And I fill the billing details to place an order in checkout 
		| firstName | secondName |        country      |     address    |  city  | postcode |    phoneNo   | 
		|   Jane    |     Doe    | United Kingdom (UK) | 149 Piccadilly | London | W1J 7NT  | 01632 907767 |
	Then I should see an order summary of my latest order with the order number
		And see the same order number when I navigate to my orders tab in my account

Examples: 
| item |
| Belt |
