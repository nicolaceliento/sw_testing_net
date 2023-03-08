# Software Testing .NET

**Software Testing exam project at Federico II University to explain build and test automation with GitHub Actions, in a CI/CD context.**

The solution contains an hypothetical MVC API Shopping Cart Service that expose basic HTTP Methods (GET, POST, DELETE) with a fake implementation and a NUnit Test project to test API service.

The Continuous Integration workflow pipeline job consists of the following steps:

- Checkout Source Repo  
- Setup .NET SDK
- Install dependencies package from NuGet Repo
- Build project solution 
- Run Unit Test
- Publish Test Report