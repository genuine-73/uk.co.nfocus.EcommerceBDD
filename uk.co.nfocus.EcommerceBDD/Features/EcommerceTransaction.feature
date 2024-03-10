Feature: EcommerceTransaction
In order to enhance customer experience when purchasing an Item online
the CEO of the clothing website
wants to provide an order functionality 

Background: 
	Given I have logged in using valid credentials
	When I navigate to the Shop page
	And I add an 'Belt' to my cart


@TestCaseOne
Scenario Outline: Applying a coupon
	When and view cart to apply coupon '<coupon>'
	Then I should get '<discount>'% off my selected item

Examples: 
| coupon    | discount |
| edgewords |    10    |
| edgewords |    15    |
| nfocus    |    25    |


@TestCaseTwo
Scenario Outline: Placing an order
	When I fill out my '<firstName>', '<lastName>', '<country>', '<address>', '<city>', '<postcode>' and '<phoneNo>' to place an order in checkout 
	Then I should see an order summary
	And access it from my orders portal

Examples: 
| firstName | lastName | country             | address        | city   | postcode | phoneNo      |
| Jane      | Doe      | United Kingdom (UK) | 149 Piccadilly | London | W1J 7NT  | 01632 907767 |