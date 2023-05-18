
using GraphQL;

namespace GraphQL_TestApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // GraphQL
            builder.Services.AddGraphQL(b => b.AddAutoSchema<TestQuery>().AddSystemTextJson());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseGraphQL("/graphql");
            app.UseGraphQLPlayground("/",
                new GraphQL.Server.Ui.Playground.PlaygroundOptions
                {
                    GraphQLEndPoint = "/graphql",
                    SubscriptionsEndPoint = "/graphql",
                });

            app.MapControllers();

            app.Run();
        }
    }
}