## Simple Github Api client and server

Simple Github Api client implements and caches the most popular assembly-language repositories. The data is periodically fetched from Github API using a backgound worker thread and placed in an in-memory cache. There are three main components of the program:


1. An in-memory cache for storing some interesting facts about Github repositories. A thread safe global hash table is used for this purpose.

2. A background worker thread that periodically connects to Github REST API v3, and uses a sync algorithm to update the in-memory cache. Please note, Github API has a limit of only 11 requests per minute for unauthorized calls. For authorized calls, Github API has a limit of 30 requests per minute. To get this benefit, the program makes authorized calls every five seconds by passing an authorization token in the HTTP request header.

3. A simple Rest API that provides concurrent access to some facet of the cached data.


![image](https://user-images.githubusercontent.com/44266076/82735168-abb26980-9ced-11ea-979c-c547e4390f70.png)


## Steps to run
1. Clone the repository. https://github.com/shaahryarrazzak/Github.Api.git
2. Open the Github.Api.sln file in Visual Studio 2019 Version 16.4+
3. Set Git.Data.Api as a startup project.
4. Add your personal access token to appsettings.json.
5. Build and Run.
6. Use postman to test the following GET end point. 
      GET https://localhost:5001/v1/api/Repos?description=Apollo 
      Note: Change port from 5001 to your port if necessary.
      
For more information please click: [Simple Git Server](https://docs.google.com/document/d/1yRbB4DANjGWs1yv3CSD8tttNOoiquzbo_UNAZkeuMgY/edit?usp=sharing)

## Some non-functional features:
1. The program is easy to understand.
2. It is easy to enhance, extend and maintain.
3. It is written so that merge conflicts are minimal.
4. It is fast, querying in-memory cache is 200+ times faster than the Github API call.
5. It is platform independent. 
6. It is easy to test and debug.
7. Supports logging and elegant error handling.

## Absent features and possible disadvantages:
1. In-Memory space is limited. 
2. Cache cannot be distributed over multiple nodes.
3. Cache could go stale. Refresh happens every 5 seconds.
4. Cache maintains only 30 most popular repositories of assembly language.
API is exposing only one simple GET endpoint for demonstration purposes only but capable of extending to more functionality as needed. 
5. Token is kept in the configuration.

## Program uses some clean code principles like:
1. Dependency injection.
2. Interface segregation.
3. Single responsibility.
4. Separation of concerns.
5. Generality and componentization.







