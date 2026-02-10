using NetCord;
using NetCord.Services.ApplicationCommands;

namespace Frogobot.Core.Commands;

public class PossessPointCommands : ApplicationCommandModule<ApplicationCommandContext>
{
	[SlashCommand("possess", "Ajoute un Possess-Point à la personne choisie")]
	public string Possess(User user)
	{
		// TODO: Adds 1 possesspoint to the user
		
		
		return $"{user} a mentionné Possesslime !";
	}
	
	[SlashCommand("scoreboard", "Affiche le tableau des Possess-Points")]
	public string Scoreboard()
	{
		// TODO: Make a embed scoreboard of the possesspoints on this server
		
		return "Scoreboard";
	}

	[SlashCommand("points", "Affiche les Possess-Points de la personne choisie")]
	public string Points(User? user = null)
	{
		user ??= Context.User;
		int points = 0;
		
		// TODO: Query the number of possesspoints of the user
		
		return $"Mentions de Possesslime par {user} : {points}";
	}
}
