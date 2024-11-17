
# MovieCharacterAPI

## Introduction
This is a RESTful API for managing movies, characters, and franchises. The API supports full CRUD operations and includes additional endpoints for updating related data and generating reports.

## Features
- Full CRUD operations for Movies, Characters, and Franchises
- Endpoints to update characters in a movie and movies in a franchise
- Reporting endpoints to get all movies in a franchise, all characters in a movie, and all characters in a franchise
- Documentation with Swagger
- Clean code with services and repositories
  

Database Diagrams
This repository includes a database relationship diagram, diagrame.png, which visualizes the structure and relationships within the database. These diagrams help in understanding how the tables are related to each other and provide a clear overview of the database schema.

The diagram has been included in this repository for reference and to aid in database design and development.

## Endpoints


### Movies
- GET /api/movies: Get all movies
- GET /api/movies/{id}: Get a specific movie by ID
- POST /api/movies: Create a new movie
- PUT /api/movies/{id}: Update a movie by ID
- DELETE /api/movies/{id}: Delete a movie by ID
- PUT /api/movies/{id}/characters: Update characters in a movie

### Characters
- GET /api/characters: Get all characters
- GET /api/characters/{id}: Get a specific character by ID
- POST /api/characters: Create a new character
- PUT /api/characters/{id}: Update a character by ID
- DELETE /api/characters/{id}: Delete a character by ID

### Franchises
- GET /api/franchises: Get all franchises
- GET /api/franchises/{id}: Get a specific franchise by ID
- POST /api/franchises: Create a new franchise
- PUT /api/franchises/{id}: Update a franchise by ID
- DELETE /api/franchises/{id}: Delete a franchise by ID
- PUT /api/franchises/{id}/movies: Update movies in a franchise
- GET /api/franchises/{id}/movies: Get all movies in a franchise
- GET /api/franchises/{id}/characters: Get all characters in a franchise

## Models

### Movie
- Id (int): Unique identifier
- Title (string): Title of the movie
- Description (string): Description of the movie
- IsCompleted (bool): Indicates if the movie is completed
- Characters (ICollection<Character>): Characters in the movie
- FranchiseId (int?): ID of the franchise the movie belongs to
- Franchise (Franchise): The franchise the movie belongs to

### Character
- Id (int): Unique identifier
- FullName (string): Character's full name, required and max length of 100 characters
- Alias (string?): Alias of the character, optional and max length of 50 characters
- Gender (string?): Gender of the character, required and max length of 10 characters
- Picture (string?): URL to the character's picture, optional
- Movies (ICollection<Movie>): Movies the character appears in

### Franchise
- Id (int): Unique identifier
- Title (string): Name of the franchise, required and max length of 100 characters
- Description (string?): Description of the franchise, optional and max length of 500 characters
- Movies (ICollection<Movie>): Movies in the franchise

## DTOs
- MovieDto: Data transfer object for movies
- CharacterDto: Data transfer object for characters
- FranchiseDto: Data transfer object for franchises

## Services
- MovieService: Handles business logic for movies
- CharacterService: Handles business logic for characters
- FranchiseService: Handles business logic for franchises

## Setup and Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/mustafa3m/MovieCharacterAPI.git
   ```

Documentation
API documentation is available via Swagger. Once the application is running, navigate to /swagger to view the API documentation












   
   ```
