using System.Diagnostics.CodeAnalysis;

namespace Frogobot.Core;

// We disable the get-only check because the emojis need to be set-able for the configuration to work
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class DiscordEmojiOptions
{
	public const string SectionName = "DiscordEmoji";
	
	public string PossessPoint { get; set; } = "";
	public string Anakin { get; set; } = "";
	public string Slime { get; set; } = "";
}
