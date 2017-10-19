# OktaUserApiClient
Since the Okta SDK for .Net is not stable, (crashing for when fetching too many users one time) I created this raw client to get users using pagination by directly calling the [Okta REST API](https://developer.okta.com/docs/api/resources/users.html#list-all-users), which fetches 200 users one time. I used [this](http://json2csharp.com/#) to generate the model classes.
