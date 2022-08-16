using Microsoft.AspNetCore.Components;

namespace TheOmenDen.Shared.Components;

public partial class Audio : ComponentBase
{
    [Parameter] public Boolean Muted { get; set; } = true;
    [Parameter] public Boolean Autoplay { get; set; } = true;

    [Parameter] public IEnumerable<Uri> Sources { get; set; } = Enumerable.Empty<Uri>();

    [Parameter] public String CodeNotSupportedMessage { get; set; } = "Your browser does not support the audio element.";



}