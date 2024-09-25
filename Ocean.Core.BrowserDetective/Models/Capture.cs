using System;
using System.Collections.Generic;

namespace Ocean.Core.BrowserDetective.Models;

public partial class Capture : ICapture
{
    public long Id { get; set; }

    public long BrowserId { get; set; }

    public CaptureType Type { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Match { get; set; } = null!;

    public string? NonMatch { get; set; } = null!;

    public virtual Browser Browser { get; set; } = null!;
}
