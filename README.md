# codingTest
Web API Coding Test

For this solution I used EF6 CodeFirst, WebAPI, and KnockoutJS. I built a simple DAL with repositories and used dependency injection to ensure that they are testable. I did the same for the controllers. I used WebAPI and wired up to it with KnockoutJS view models. The solution's database is hosted on Azure and the connection string should work. Since CodeFirst is being used, the connection string can be changed and running an update-database command from the package manager console should stand up a new database. All the solution requires is Visual Studio 2015 Community Edition (run as Administrator). The solution can be safely converted for IIS Express if necessary.

Choice of data format: 

I chose JSON because it's standard issue. I like JSON better than XML because it's simpler and works well with everything. 

Request (for GetItems): 

GET /CodingTest.WebAPI/api/Items HTTP/1.1

Response (for GetItems) (JSON):

[{"ItemId":1,"QtyInStock":96,"Name":"NOS","Description":"Energy Drink","Price":5},{"ItemId":2,"QtyInStock":200,"Name":"Samsung Galaxy","Description":"Phone","Price":200},{"ItemId":3,"QtyInStock":10,"Name":"Tesla Coil","Description":"Powerful Energy Transmitter","Price":50000},{"ItemId":4,"QtyInStock":30,"Name":"Dog Food","Description":"Mad Max' Favourite","Price":15},{"ItemId":5,"QtyInStock":198,"Name":"Fish Taco","Description":"Crispy and delicious","Price":12}]

Authentication mechanism:

Local authentication. You must have a user account on the server. You can register for free. This was simpler than a full AD integration and works with the web. User creation and logging in are done via WebAPI calls using KnockoutJS. The 'Athenticate' attribute on the post controller action ensures you must be authenticated to purchase. (If you try without, you naturally get a 401 back from the server.)
