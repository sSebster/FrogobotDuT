using NetCord.Rest;

namespace Frogobot.Core.Utils;

public static class EmojiUtils
{
	/// <summary> Gets the emoji name + id from the emoji Markdown string. </summary>
	/// <param name="emojiName"> The name of the emoji, in the format "&lt;:name:id&gt;" </param>
	/// <returns></returns>
	public static (string, ulong) GetEmojiDataFromName(string emojiName)
	{
		if (!emojiName.StartsWith("<:"))
			throw new ArgumentException("The emoji name must start with '<:'");
		
		emojiName = emojiName
			.TrimStart('<')
			.TrimEnd('>');
		
		return (emojiName.Split(':')[1], ulong.Parse(emojiName.Split(':')[2]));
	}

	public static ReactionEmojiProperties GetReactionEmojiFrom(string emojiName)
	{
		var (name, id) = GetEmojiDataFromName(emojiName);
		return new ReactionEmojiProperties(name, id);
	}
}
