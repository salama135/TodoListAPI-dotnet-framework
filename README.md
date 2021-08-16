# TodoListAPI-dotnet-framework

 A simple todo list api using dotnet framework 
 
![The Clean Architecture](images/image1.png "image_tooltip")

### Clean Architecture

Here we are using a clean architecture approach which states that there are three main layers in any system and they must be completely separated from each other to allow for a concept called **<span style="text-decoration:underline;">the separation of concern</span>**.

This lead to producing systems that are:



1. Independent of Frameworks.
2. Testable.
3. Independent of UI.
4. Independent of Database.
5. Independent of any external agency.


### The Dependency Rule

This is a rule that makes clean architecture possible, the rule says **<span style="text-decoration:underline;">that source code dependencies can only point inwards</span>**. Meaning that functions, classes. variables, or any other named software entity declared in an outer circle **<span style="text-decoration:underline;">MUST NOT</span>** be mentioned by an outer circle.

