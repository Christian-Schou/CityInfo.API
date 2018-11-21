# CityInfo.API
> Little project making use of technics learned from education


The solution let's you create a project that can handle api requests about cities and points of interest for the specific cities. See the API documentation for information, on how to use the API.


### Migrate Database in your own project

In the console type the following. Remember to change <NameOfMigration> to your own description.

```sh
Add-Migration <NameOfMigration>
```
Example:

```sh
Add-Migration CityInfoDbInitialMigration
```

## Usage example

1. Clone the project

2. Rebuild the solution

3. Hit F5 to run the project - you will get an 404 Error at first. This is because the app makes use of UseStatusCodePages(); and their haven't been added a startup page or default text.

4. Go to http://localhost:port/swagger (change localhost and port with your projects URL)

5. Test and explore the application. You can test the API through swagger. The database will be seeded on startup, so you have something to play around with.

## Release History

* 1.0.0
    * Initial

## Meta

Christian Schou – [@YourTwitter](https://twitter.com/)
