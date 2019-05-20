**CAUTION: The implementation was part of a PoC and has not been tested extensively nor has been used in a real project, yet.**



# Cypress For Sitecore

This implementation enables you to run Cypress tests for page items in preview and live mode out of the Sitecore backend.
For this to work you need this node.js webserver running, which will handle the cypress test execution and display of results: https://repo.deloitte.de/projects/DDSC/repos/deloitte.sitecore.cypress.webserver/browse

## Requirements
* Node.js Cypress Webserver: https://repo.deloitte.de/projects/DDSC/repos/deloitte.sitecore.cypress.webserver/browse
* Sitecore 9.1.0

## Installation
* Install the Sitecore package: 

## Configuration
* Use the *Deloitte.Sitecore.Foundation.Testing.UI.Cypress.config* file to change the Cypress configuration and to define the base url of the API.
* Change the Helix Folder ID settings in case you are not using the default folders in the *Deloitte.Sitecore.Foundation.Testing.UI.Helix.Settings.config*.

## Usage
After installing the Sitecore package and changing the configuration according to your set up:

1. Navigate to any page item
2. Select the "Review"-Ribbon
3. Start a test run by clicking on the "Test Live" or "Test Preview" Button



To write Sitecore Rendering specific test files, please take a look at the documentation of the node.js Cypress api webserver.




*Inspired by https://github.com/daveaftershok/sitecore-test-run-on-publish (Dave Leigh)*
