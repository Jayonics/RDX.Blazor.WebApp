# Shop.Models Documentation

## Concepts

### DTOs (Data Transfer Objects)

DTOs are simple objects that are used to transfer data between processes or layers of an application. They do not
contain any business logic but may contain serialization and deserialization logic. In this project, DTOs are defined
under the `Shop.Models.Dtos` namespace.

For instance, the `CartItemDto` class is a DTO that represents a cart item in the application. It includes properties
such as `Id`, `ProductId`, `CartId`, `ProductName`, `ProductDescription`, `ProductImageURL`, `Price`, `TotalPrice`,
and `Quantity`.

```csharp
public class CartItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    // Other properties...
}
```

Entities, on the other hand, are classes that map to database tables. They represent the data structure of the objects
that the application will be working with. Entities usually contain business logic and are used in the Data Access Layer
of an application. In this project, Entities are defined under the `Shop.API.Entities` namespace.

For example, the `Product` class is an Entity that represents a product in the shop. It includes properties such
as `Id`, `Name`, `Description`, `ImageURL`, `Price`, `Quantity`, and `CategoryId`.

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    // Other properties...
}
```

In many applications, DTOs and entities have a close relationship. DTOs are often used to safely transfer data from
entities to the client. This is because directly exposing entities to the client can be a security risk, as entities may
contain sensitive data. DTOs allow you to control exactly what data is exposed to the client.

For example, when sending a product to the client, you would map the `Product` entity to a `ProductDto`, and send the
DTO instead. The `ProductDto` only includes the properties that are safe to expose to the client.

```csharp
public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    // Other properties...
}
```

This pattern of using DTOs and Entities is a common practice in C# applications to ensure the separation of concerns and
data security.