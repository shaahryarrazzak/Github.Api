## Simple Github Api

Simple Github Api consumes the Github REST API implements and caches the most popular assembly language repositories. The data is periodically fetched from Github API using a backgound worker thread and placed in an in-memory cache. There are three main components of the program:


1. An in-memory cache for storing some interesting facts about Github repositories. A thread safe global hash table is used for this purpose.

2. A background worker thread that periodically connects to Github REST API v3, and uses a sync algorithm to update the in-memory cache. Please note, Github API has a limit of only 11 requests per minute for unauthorized calls. For authorized calls, Github API has a limit of 30 requests per minute. To get this benefit, the program makes authorized calls every five seconds by passing an authorization token in the HTTP request header.

3. A simple Rest API that provides concurrent access to some facet of the cached data.


![image](https://user-images.githubusercontent.com/44266076/82735168-abb26980-9ced-11ea-979c-c547e4390f70.png)


## Steps to run
1. Clone repository.
2. Open the Github.Api.sln file in Visual Studio 2019 Version 16.4+
3. Set Git.Data.Api as startup project.
4. Add your personal access token to appsettings.json.
5. Build and Run.
6. Open post man. 
7. GET https://localhost:5001/v1/api/Repos?description=Apollo 
8. Change port from 5001 to your port if necessary.







