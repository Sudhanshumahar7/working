using api.Services;
using api.models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Workflow API", Version = "v1" });
});

builder.Services.AddSingleton<WorkflowService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Workflow API V1");
});

app.UseHttpsRedirection();


app.MapPost("/workflows", (WorkflowDefinition def, WorkflowService service) =>
{
    if (service.CreateWorkflowDefinition(def, out var error))
        return Results.Ok(new { message = "Workflow created" });

    return Results.BadRequest(new { error });
});

app.MapGet("/workflows/{id}", (string id, WorkflowService service) =>
{
    var def = service.GetDefinition(id);
    return def is not null ? Results.Ok(def) : Results.NotFound(new { error = "Definition not found" });
});

app.MapPost("/instances/{workflowId}", (string workflowId, WorkflowService service) =>
{
    var instance = service.StartWorkflowInstance(workflowId);
    return instance is not null ? Results.Ok(instance) : Results.NotFound(new { error = "Definition not found" });
});

app.MapPost("/instances/{id}/actions/{actionId}", (string id, string actionId, WorkflowService service) =>
{
    if (service.ExecuteAction(id, actionId, out var error))
        return Results.Ok(new { message = "Action executed" });

    return Results.BadRequest(new { error });
});

app.MapGet("/instances/{id}", (string id, WorkflowService service) =>
{
    var instance = service.GetInstance(id);
    return instance is not null ? Results.Ok(instance) : Results.NotFound(new { error = "Instance not found" });
});


app.Run();