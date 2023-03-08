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

The Code Analysis workflow pipelint job, instead, consists of the following steps:
- Set up JDK 11      
- Checkout Source Repo        
- Cache SonarCloud packages       
- Install SonarCloud scanners
- Build and Analyze

**This is the code quality result badge**
[![Quality gate](https://sonarcloud.io/api/project_badges/quality_gate?project=nicolaceliento_sw_testing_net)](https://sonarcloud.io/summary/new_code?id=nicolaceliento_sw_testing_net)