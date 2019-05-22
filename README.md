**CAUTION: The implementation was part of a PoC and has not been tested extensively nor has been used in a real project, yet.**



# Cypress For Sitecore

This implementation enables you to run Cypress tests for page items in preview and live mode out of the Sitecore backend.
For this to work you need this node.js webserver running, which will handle the cypress test execution and display of results: https://github.com/sebastianbienko/CypressForSitecore.Webserver



Read my blog post to get some background information: https://sebastianbienko.github.io/2019-05-21-cypress-for-sitecore/

## Requirements
* Node.js Cypress Webserver: https://github.com/sebastianbienko/CypressForSitecore.Webserver
* Sitecore 9.1.0

## Installation
* Install the Sitecore package: https://github.com/sebastianbienko/CypressForSitecore/blob/master/SitecorePackages/Cypress%20for%20Sitecore-0.1.zip
* Set up a *site*, which uses the *master* database instead of the *web* database to test the preview.

## Configuration
* Use the *Deloitte.Sitecore.Foundation.Testing.UI.Cypress.config* file to change the Cypress configuration and to define the base url of the API.
* Change the Helix Folder ID settings in case you are not using the default folders in the *Deloitte.Sitecore.Foundation.Testing.UI.Helix.Settings.config*.

## Usage
After installing the Sitecore package and changing the configuration according to your set up:

1. Navigate to any page item
2. Select the "Review"-Strip in the Ribbon
3. Start a test run by clicking on the "Test Live" or "Test Preview" Button



To write Sitecore Rendering specific test files, please take a look at the documentation of the node.js Cypress api webserver.

## Issues and missing features

- When using multiple test files, the test results are not being merged.
- There is just one test result at a time accessible, right now and it can not actually be differentiated between the "Preview" or "Live" test result, yet.
- The test files are not pre-filtered by the Helix location, yet.




*Inspired by https://github.com/daveaftershok/sitecore-test-run-on-publish (Dave Leigh)*
