using Frogobot.Core.Utils;
using Frogobot.Data.Services;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;

namespace Frogobot.Core.Events;

public class PossessPointMessageHandler : IMessageCreateGatewayHandler
{
	private readonly IServiceScopeFactory _scopeFactory;
	private readonly DiscordEmojiOptions _emojiOptions;

	[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
	public PossessPointMessageHandler(IServiceScopeFactory scopeFactory, IOptions<DiscordEmojiOptions> emojiOptions)
	{
		_scopeFactory = scopeFactory;
		_emojiOptions = emojiOptions.Value;
	}

	public async ValueTask HandleAsync(Message message)
	{
		// We don't want to react to our own messages
		if (message.Author.IsBot) 
			return;
		
		var result = MessageContainsMagicWord(message.Content);
		
		if (!result.HasMatch) 
			return;
		
		using var scope = _scopeFactory.CreateScope();
		var possessPointService = scope.ServiceProvider.GetRequiredService<IPossessPointService>();

		if (result.ContainsPossessSlime)
		{
			await message.AddReactionAsync(EmojiUtils.GetReactionEmojiFrom(_emojiOptions.PossessPoint));
			await possessPointService.AddPossessPointAsync(message.Author.Id);
		}
		else if (result.ContainsSlime)
		{
			await message.AddReactionAsync(EmojiUtils.GetReactionEmojiFrom(_emojiOptions.Slime));
		}
		
		if (result.ContainsAnakin)
			await message.AddReactionAsync(EmojiUtils.GetReactionEmojiFrom(_emojiOptions.Anakin));
	}

	public static MessageResult MessageContainsMagicWord(string message)
	{
		var msg = message.ToLowerInvariant();
		var result = new MessageResult();

		if (msg.Contains("possesslime") || msg.Contains("posseslime") || msg.Contains("possessslime"))
			result.ContainsPossessSlime = true;
		
		// We don't want to detect the slime part of possesslime,
		// And we don't care about putting a slime reaction if it already contains possesslime
		else if (msg.Contains("slime"))
			result.ContainsSlime = true;

		if (msg.Contains("anakin"))
			result.ContainsAnakin = true;

		return result;
	}

	public struct MessageResult
	{
		/// <summary> Whether the message contains any of the magic words. </summary>
		public bool HasMatch => ContainsSlime || ContainsPossessSlime || ContainsAnakin;
		
		public bool ContainsSlime { get; set; }
		public bool ContainsPossessSlime { get; set; }
		public bool ContainsAnakin { get; set; }
	}
}
