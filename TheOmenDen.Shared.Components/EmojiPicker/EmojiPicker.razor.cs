using Microsoft.AspNetCore.Components;
using TheOmenDen.Shared.Components.EmojiPicker.Models;
using TheOmenDen.Shared.Components.EmojiPicker.Services;

namespace TheOmenDen.Shared.Components.EmojiPicker;
public partial class EmojiPicker : ComponentBase
{
    private Emoji[] _displayEmojis = Array.Empty<Emoji>();

    protected override async Task OnInitializedAsync()
    {
        var emojiLoader = await EmojiLoaderAsyncFactory.GetInstanceAsync();

        _displayEmojis = await emojiLoader.LoadAllEmoji()
            .ToArrayAsync();
    }
}