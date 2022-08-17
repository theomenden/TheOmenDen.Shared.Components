using Microsoft.AspNetCore.Components;
using TheOmenDen.Shared.Components.Models;

namespace TheOmenDen.Shared.Components;

public partial class Audio : ComponentBase
{
    private SongInformation Song;

    [Parameter] public Boolean Muted { get; set; } = true;
    
    [Parameter] public Boolean Autoplay { get; set; } = true;

    [Parameter] public AudioSource? Source { get; set; }

    [Parameter] public String CodeNotSupportedMessage { get; set; } = "Your browser does not support the audio element.";

    protected override void OnParametersSet()
    {
        if (Source is not null)
        {
            Song = Source.Song;
        }
    }
}