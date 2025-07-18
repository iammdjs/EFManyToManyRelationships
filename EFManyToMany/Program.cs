using EFManyToMany.Data;
using EFManyToMany.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
    options.UseInMemoryDatabase("ManyToManyDb"));

// Configure OData
builder.Services.AddControllers().AddOData(options =>
    options.Select().Expand().Filter().OrderBy().Count().SetMaxTop(null)
        .AddRouteComponents("odata", GetEdmModel()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataContext>();
    if (context.Database.EnsureCreated())
    {
        var tag1 = new Tag { Name = "C#" };
        var tag2 = new Tag { Name = ".NET" };
        var tag3 = new Tag { Name = "EF Core" };

        var post1 = new Post { Title = "Intro to C#", Tags = new List<Tag> { tag1 } };
        var post2 = new Post { Title = "What's new in .NET 8", Tags = new List<Tag> { tag1, tag2 } };
        var post3 = new Post { Title = "Many to Many Relationships in EF Core", Tags = new List<Tag> { tag1, tag2, tag3 } };

        context.Posts.AddRange(post1, post2, post3);
        context.SaveChanges();
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

Microsoft.OData.Edm.IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<Post>("Posts");
    builder.EntitySet<Tag>("Tags");
    return builder.GetEdmModel();
}