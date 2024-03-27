# README: SpecFlow Automation Testing on an E-Commerce Website

## Introduction
A Specflow-based project that seeks to demonstrate my knowledge and application of automation testing using Gherkin, SpecFlow and Selenium WebDriver.
I was tasked to write two automation scripts (scenarios in Gherkin terminology) to test the following test cases on an E-Commerce Website...

Test Case One: The test will login to an e-commerce site as a registered user, purchase an item of clothing, apply a 
discount code and check that the total is correct after the discount & shipping is applied.

Test Case Two: The test will login to an e-commerce site as a registered user, purchase an item of clothing and go 
through checkout. It will capture the order number and check the order is also present in the ‘My 
Orders’ section of the site 

## Techn Stack
- C#
- Gherkin
- .NET framework 6.0
- SpecFlow
- Selenium WebDriver
- Gherkin

## Prerequisites

- Visual Studio

## Running Test

1. Install by cloning the project to your local repo using the command <git clone https://github.com/genuine-73/uk.co.nfocus.EcommerceBDD.git>
2. Open terminal and navigate to uk.co.nfocus.EcommerceBDD/uk.co.nfocus.EcommerceBDD.csproj
3. Run the command [dotnet test --settings "local.runsettings"] or simply [dotnet test]

