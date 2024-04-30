# Shop.API Documentation
## Folder Structure
### Entities
The `Entities` folder contains the classes that represent the data model of the application. 
These classes are used to interact with the database and are used to pass data between the different layers of the application.

#### Dependencies
- Microsoft.EntityFrameworkCore

EntityFramework is a NuGet package that allows us to create the database using a code first approach. 
The classes in this folder are used to define the tables in the database.

#### Classes
- `Product`
- `ProductCategory`
- `Cart`
- `CartItem`
- `User`

Each of these classes represents a table in the database.

#### Relationships
![Entity Relationship Diagram](./EntityRelationshipDiagram.png)
- `Cart` has a one-to-many relationship with the `CartItem` entity.
- `Product` has a one-to-many relationship with the `CartItem` entity.
- `ProductCategory` has a one-to-many relationship with the `Product` entity.
- `User` has a one-to-many relationship with the `Cart` entity.