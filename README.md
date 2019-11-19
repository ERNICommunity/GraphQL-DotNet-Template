# GraphQL .Net Template
This is a simple [GraphQL](https://graphql.org/) template project written in C#.

## Context
The project exposes a simplified webshop api in order to demonstrate some of GraphQL's features.

There are three types of product ([Book](GraphQl/Types/BookType.cs), [Film](GraphQl/Types/FilmType.cs), [Shoe](GraphQl/Types/ShoeType.cs)) which share [common data](GraphQl/Types/ProductInterface.cs), but also have distinct data fields.
Users have the ability to commit reviews for each product.
Further, if a user is interested in a product and would like to be notified when a new review is added, he or she can subscribe to new reviews being added and will be notified through push notifications when new reviews are added. This demonstrates the publish-subscribe model that GraphQL offers out of the box.

## Why GraphQL?
Currently (2019) many new apis are realised using RESTful services. Before REST the dominant technology was SOAP, which still accounts for many existing web services. GraphQL was published as an open specification in 2016 by Facebook and has since gained considerable adoption in the community and is generally liked by many developers for its user friendly design.

Hence, exploring GraphQL seemed a good idea as it is a promising piece of technology.

Naturally, there are also some technical nuggets that justify a deeper look at this comparatively new piece of technology.

### Technical Nuggets
- Clients can control precisely what data and also how much data they'd like to receive from a server. Hence over- and under- fetching issues are largely reduced.
- Even though this sample project uses http as transport technology, GraphQL can in principle be used with any transport layer.
- GraphQL is designed to be type safe. This can be explored nicely using [GraphiQL](https://gitub.com/graphql/graphiql) (which is used in this sample application, too.)
- A built in publish-subscribe mechanism. In GraphQL lingo simply "subscriptions". Implemented here using web sockets.

## Design Overview
The template project is a C# .Net Core application built with [graphql-dotnet](https://github.com/graphql-dotnet/graphql-dotnet).
![Project Architecture](img/arch.svg)

The highlighted **GraphQL** component contains almost all logic related to GraphQl. As everything is stitched together in the [Startup.cs](Startup.cs) file, that is listed explicitly as well.

The Products and Reviews are modelled as separate tables in some db, where the reviews have a foreign key to the corresponding product in the products table.

With this architecture it is possible to demonstrate the most important aspects of an api built using GraphQL.

- **queries**
  - aggregation
  - filtering
  - selecting
  - range limiting (hint to pagination)
- **mutations**
  - add data
  - *not implemented*
    - delete data
    - change data
- **subscriptions**
  - subscription to events

### Points Of Improvement

- The publicly exposed UUIDs should not be used internally to relate reviews to products.
- GraphQL offers a caching mechanism using a so called [DataLoader](https://github.com/graphql/dataloader). This could be added to this project
- Even though GraphQLs query language is type safe, the implementation of the types under the directory "[Types](GraphQL/Types/)" is not type safe with regard to the .Net type system
- Some solutions can be considered clumsy, such as
  - Needing to register the product interface definition separately in each [product](GraphQL/Types/ProductType).
  - The schema [implementation](GraphQL/ProductSchema.cs) has to be told explicitly what concrete types are available.
  - Similarly, the [mutation](GraphQL/ProductMutation.cs) has to know about every product type, too.
- For some reason the GraphiQL documentation does not load when subscriptions are enabled. (Comment out the subscriptions in the [schema](GraphQL/ProductSchema.cs) in order to get the docs.)

## Running
Run the command `dotnet run` in a terminal in the toplevel directory of the repo and then open a browser at the projects ui [playground](https://localhost:5001/ui/playground).

Use that web frontend of the api to explore the (generated) GraphQL schema and documentation. You can also interact with the server through the web interface to submit queries and mutation requests. Note that the interface comes with intellisense!

Further, if two browser pages are opened, GraphQLs subscription feature can also be tested.

### Sample Messages
Feel free to use the [playground](https://localhost:5001/ui/playground) on the following examples as a starting point to generate your own messages. With the intellisense feature, it is not even necessary to consult the documentation.

#### Queries
```
{
  product(id: <some id>){
    name
    type
    stock
  }
}
```
->
```json
{
  "data": {
    "product": {
      "name": "The Lord of the Rings",
      "type": "Film",
      "stock": 12
    }
  }
}
```
---
```
{
  products(first: 3) {
    name
    type
    ... on Film {
      director
    }
    ... on Book {
      author
    }
    ... on Shoe {
      size
    }
  }
}
```
-->
```json
{
  "data": {
    "products": [
      {
        "name": "The Lord of the Rings",
        "type": "Book",
        "author": "J. R. R. Tolkien"
      },
      {
        "name": "The Linux Programming Interface",
        "type": "Book",
        "author": "Michael Kerrisk"
      },
      {
        "name": "The Lord of the Rings",
        "type": "Film",
        "director": "Peter Jackson"
      }
    ]
  }
}
```

#### Mutations
```
mutation {
  createReview(
    review: {
      productId: <some id>
      title: "My precious book."
      text: "I really enjoyed the long read!"
    }
  ) {
    title
  }
}
```
-->
```json
{
  "data": {
    "createReview": {
      "title": "My precious book."
    }
  }
}
```

#### Subscriptions
```
subscription{
  reviewAdded{
    productName
    title
  }
}
```
--> On new review added, we get an event (push notification) similar to:
```json
{
  "data": {
    "reviewAdded": {
      "productName": "The Lord of the Rings",
      "title": "My precious book."
    }
  }
}
```

## Resources
[1] Pluralsight course [Building GraphQL APIs with ASP.NET Core](https://app.pluralsight.com/library/courses/building-graphql-apis-aspdotnet-core/table-of-contents)
