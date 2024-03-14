using TaskManager.Persistence;
using TaskManager.Repository;
using TaskManager.Services.Tasks;
using TaskManager.Validators;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddMemoryCache();
    builder.Services.AddDbContext<TaskManagerDbContext>();
    builder.Services.AddScoped<ITaskService, TaskService>();
    builder.Services.AddScoped<IUserTaskValidator, UserTaskValidator>();
    builder.Services.AddScoped<IUserTaskRepository, UserTaskRepository>();
    builder.Logging.AddLog4Net("./log4net.config");

}

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




