using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services;
using NetCord.Hosting.Services.ApplicationCommands;

namespace Frogobot.Core;

internal class Program
{
	public static async Task Main(string[] args)
	{
		var builder = Host.CreateApplicationBuilder(args);

		if (builder.Environment.IsDevelopment())
		{
			builder.Configuration.AddUserSecrets<Program>();
		}

		builder.Services
			.AddDiscordGateway()
			.AddApplicationCommands();
		
		var app = builder.Build();

		app.AddModules(typeof(Program).Assembly);

		await app.RunAsync();
	}
}
