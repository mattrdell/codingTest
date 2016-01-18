# codingTest
Web API Coding Test

For this solution I used EF6 CodeFirst, WebAPI, and KnockoutJS. I built a simple DAL with repositories and used dependency injection to ensure that they are testable. I did the same for the controllers. I used WebAPI and wired up to it with KnockoutJS view models.

Choice of data format: 

I chose JSON because it's standard issue. I like JSON better than XML because it's simpler and works well with everything. 

Request: 

Response:

Authentication mechanism:

Local authentication. You must have a user account on the server. You can register for free. This was simpler than a full AD integration and works with the web. User creation and logging in are done via WebAPI calls using KnockoutJS. The 'Athenticate' attribute on the post controller action ensures you must be authenticated to purchase. 
