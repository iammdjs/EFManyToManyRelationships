# EF Many-to-Many Relationships with OData

This project demonstrates how to implement many-to-many relationships using Entity Framework Core and expose them via OData endpoints, effectively handling circular reference issues.

## Project Structure

- `Models/Post.cs`: Represents a blog post.
- `Models/Tag.cs`: Represents a tag that can be associated with posts.
- `Data/DataContext.cs`: The Entity Framework Core DbContext for interacting with the database.
- `Controllers/PostsController.cs`: OData controller for managing Posts.
- `Controllers/TagsController.cs`: OData controller for managing Tags.

## Many-to-Many Relationship

The `Post` and `Tag` entities have a many-to-many relationship. A `Post` can have multiple `Tags`, and a `Tag` can be associated with multiple `Posts`.

```csharp
// Post.cs
public class Post
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public ICollection<Tag>? Tags { get; set; }
}

// Tag.cs
public class Tag
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Post>? Posts { get; set; }
}
```

Entity Framework Core automatically configures the many-to-many relationship based on these navigation properties.

## How to Run the Application

Run the application from the root directory:
    ```bash
    dotnet run --project EFManyToMany
    ```

    The application will start, typically on `http://localhost:5177`.

## OData Endpoints

This application exposes OData endpoints, allowing flexible querying and expansion of related entities. The base URL for OData is `http://localhost:5177/odata`.

### Get All Posts

```
GET http://localhost:5177/odata/Posts
```

### Get All Tags

```
GET http://localhost:5177/odata/Tags
```

### Get Posts with Related Tags (using $expand)

```
GET http://localhost:5177/odata/Posts?$expand=Tags
```

### Get Tags with Related Posts (using $expand)

```
GET http://localhost:5177/odata/Tags?$expand=Posts
```

### Other OData Query Options

You can combine `$expand` with other OData query options like `$filter`, `$select`, `$orderby`, and `$top`.

- **Filter Posts by Title and expand Tags:**
  ```
  GET http://localhost:5177/odata/Posts?$filter=contains(Title, 'C#')&$expand=Tags
  ```

- **Select specific fields from Tags and expand Posts:**
  ```
  GET http://localhost:5177/odata/Tags?$select=Id,Name&$expand=Posts
  ```

## Using `EFManyToMany.http`

The `EFManyToMany.http` file contains example requests that you can run directly from compatible IDEs (like Visual Studio Code with the REST Client extension) to test the API endpoints.