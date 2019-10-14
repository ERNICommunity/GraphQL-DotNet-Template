# GraphQL .Net Template
This is a simlpe [GraphQL](https://graphql.org/) template project written in C#.

## Context
The project exposes a simplified webshop api. There are three types of product ([Book](GraphQl\Types\BookType.cs), [Film](GraphQl\Types\FilmType.cs), [Shoe](GraphQl\Types\ShoeType.cs)) which share [common data](GraphQl\Types\ProductInterface.cs), but also have distinct data fields.
Users have the ability commit reviews to each product. Further, if a user is interested in a product and would like to be notified when a new review is added, he or she can subscribe to new reviews being added.

## Why GraphQL?

## Architecture Overview

## Running
Run the command `dotnet run` in a terminal and then open a browser at the projects ui [playground](https://localhost:5001/ui/playground).

Use that web frontend of the api to explore the (generated) GraphQL schema and documentation to explore the api. You can also interact with the server through a webinterface that comes with intellisense to submit queries and mutation requests. If two browser pages are opened, GraphQLs subscription feature can also be tested.

### Sample Messages
The [playground](https://localhost:5001/ui/playground) ui has built in intellisense, so feel free to use the following examples as a starting point to generate your own messages.
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
-->
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
