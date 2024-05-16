namespace Lab8
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddDistributedMemoryCache();// добавляем IDistributedMemoryCache
			builder.Services.AddSession();  // добавляем сервисы сессии

			var app = builder.Build();

			app.UseSession();   // добавляем middleware для работы с сессиями

			app.Run(async (context) =>
			{
				if (context.Session.Keys.Contains("pet"))
					await context.Response.WriteAsync($"Your pet is {context.Session.GetString("pet")}");
				else
				{
					context.Session.SetString("pet", "Berry");
					await context.Response.WriteAsync("You don't have a pet");
				}
			});

			/*
			app.Use(async (context, next) =>
			{
				if (context.Request.Cookies.ContainsKey("username"))
				{
					string? name = context.Request.Cookies["username"];
					await context.Response.WriteAsync($"Hi {name}!\n");
				}
				else
				{
					context.Response.Cookies.Append("username", "user123");
					await context.Response.WriteAsync("Hello World!\n");
				}

				await next.Invoke();
			});

			app.Run(async (context) =>
			{
				await context.Response.WriteAsync($"HttpContext.Items\n");
				if (context.Items.ContainsKey("name"))
					await context.Response.WriteAsync($"Name: {context.Items["name"]}\n");
				else
					await context.Response.WriteAsync("No names\n");

				if (context.Items.ContainsKey("age"))
					await context.Response.WriteAsync($"Message: {context.Items["age"]}\n");
				else
					await context.Response.WriteAsync("No age\n");
			});
			*/
			app.Run();
		}
	}
}
