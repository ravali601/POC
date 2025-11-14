# ------------------------------------------------------------

# TextResponder – Project README

# ------------------------------------------------------------

## 1. Project Overview

TextResponder is a simple ASP.NET Core MVC project that demonstrates how an MVC application can communicate with a Web API using HttpClient.
The user enters text in a form, the MVC application validates the input, sends it to the API, and displays the response returned by the API.

The project focuses on:

* MVC UI with validation
* A Web API endpoint
* HttpClient service integration
* Clean separation of concerns
* Unit test coverage across all layers
* Simple, clear organization suitable for an interview assessment

The project uses .NET 9.

No database is used.

---

## 2. Application Flow

1. User enters text on the MVC page.
2. The input is validated using DataAnnotations.
3. If valid, the MVC app calls the internal API using an HttpClient service.
4. The API validates the same input again.
5. If valid, the API returns the same text in a MessageResult object.
6. The MVC app displays the response on the page.

---

## 3. Features

* MVC form with:

  * Required field validation
  * Regex validation allowing only alphanumeric characters
* Web API endpoint that:

  * Accepts MessageInput
  * Performs server-side validation
  * Returns MessageResult
* Service layer with HttpClient
* Clean controller structure
* Unit tests for:

  * HomeController
  * MessageController (API)
  * MessageService
  * Model validation

---

## 4. Project Structure

Controllers:

* HomeController (MVC)
* MessageController (API)

Models:

* MessageInput
* MessageResult

Services:

* IMessageService
* MessageService

Views:

* Views/Home/Index.cshtml (main UI)

Tests:

* HomeControllerTests
* MessageControllerTests
* MessageServiceTests
* ModelValidationTests

---

## 5. API Details

Endpoint: POST /api/message/send
Request Body:
{ "text": "<value>" }

Response:

* 200 OK with MessageResult when valid
* 400 BadRequest when validation fails

---

## 6. Running the Project

1. Open the solution in Visual Studio 2022.
2. Make sure the project targets .NET 9.
3. Run the project.
4. The MVC UI should open automatically.
5. The API can be accessed using Swagger at:
   [https://localhost](https://localhost):<port>/swagger/index.html

If you want the project to always open MVC UI instead of Swagger, do not set launchUrl to "swagger" in launchSettings.

---

## 7. Running the Test Suite

1. Open Test Explorer in Visual Studio.
2. Run all tests.
3. All tests should pass.

The tests cover:

* MVC controller behavior
* API controller responses
* HttpClient communication handling
* Validation logic

---

## 8. Manual Test Cases

The complete set of manual test cases is located in the accompanying test document.

These cover:

* UI interactions
* API behavior
* Service layer
* Input validation

---

## 9. Technologies Used

* ASP.NET Core MVC
* ASP.NET Core Web API
* .NET 9
* C#
* HttpClient factory
* DataAnnotations validation
* xUnit for unit testing
* Moq and Moq.Protected for service mocking
* Swagger/OpenAPI for API testing

---

## 10. Notes

* This project is intended as a POC or interview assessment.
* It demonstrates clean architecture, readable code, separation of concerns, and testability.
* HttpClient is registered using AddHttpClient for best practices.