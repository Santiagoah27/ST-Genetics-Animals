# ST Genetics API Documentation

This document provides information on how to use the ST Genetics API.

## Base URL

All URLs referenced in the documentation have the following base:
`https://localhost:5001/api`

## Authentication

All endpoints require a JSON Web Token (JWT) for authentication. You can obtain a token by sending a `GET` request to `/api/Animals/token`.

## Endpoints

### Animals

#### Create a new animal

- **URL:** `/api/Animals`
- **Method:** `POST`
- **Auth required:** Yes
- **Body Params:**
  - `Name`
  - `Breed`
  - `BirthDate`
  - `Sex`
  - `Price`
  - `Status`

#### Update an existing animal

- **URL:** `/api/Animals/{id}`
- **Method:** `PUT`
- **Auth required:** Yes
- **URL Params:**
  - `id` (the ID of the animal to be updated)
- **Body Params:**
  - `Name`
  - `Breed`
  - `BirthDate`
  - `Sex`
  - `Price`
  - `Status`

#### Delete an animal

- **URL:** `/api/Animals/{id}`
- **Method:** `DELETE`
- **Auth required:** Yes
- **URL Params:**
  - `id` (the ID of the animal to be deleted)

#### Filter animals

- **URL:** `/api/Animals/filter`
- **Method:** `GET`
- **Auth required:** Yes
- **Query Params:**
  - `id`
  - `Name`
  - `Sex`
  - `Status`

### Orders

#### Create a new order

- **URL:** `/api/Animals/order`
- **Method:** `POST`
- **Auth required:** Yes
- **Body Params:**
  - `AnimalId`
  - `Quantity`

## Errors

The API uses conventional HTTP response codes to indicate the success or failure of an API request. Codes in the 2xx range indicate success. Codes in the 4xx range indicate an error that failed given the information provided (e.g., a required parameter was omitted, an order failed, etc.). Codes in the 5xx range indicate a server-side error.
