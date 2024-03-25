Feature: EcommerceTransaction
In order to enhance customer experience when purchasing an Item online
the CEO of the clothing website
wants to provide an order functionality 



Background: 
	Given I am on the login page
		And I have logged in using valid login credentials
	
@TestCaseOne
Scenario Outline: Applying a coupon
The test will login to an e-commerce site as a registered user, purchase an item of clothing, apply a 
discount code and check that the total is correct after the discount & shipping is applied.

	When I navigate to the Shop page to add '<item>' to my cart
		And I view cart to apply coupon '<coupon>'
	Then I should get <discount>% deducted off my selected item

Examples: 
| item   | coupon    | discount |
| Belt   | edgewords | 15       |
| Beanie | nfocus    | 25       |
 

@TestCaseTwo
Scenario Outline: Placing an order
The test will login to an e-commerce site as a registered user, purchase an item of clothing and go 
through checkout. It will capture the order number and check the order is also present in the ‘My 
Orders’ section of the site

	When I navigate to the Shop page to add '<item>' to my cart
		And I fill the billing details to place an order in checkout 
		| firstName | secondName | country             | address        | city   | postcode | phoneNo      | email					  |
		| Jane      | Doe        | United Kingdom (UK) | 149 Piccadilly | London | W1J 7NT  | 01632 907767 | hellogen@edgewords.co.uk |
	Then I should see an order summary of my latest order
		And View the order I made in my orders tab in my account

Examples: 
| item |
| Belt |
