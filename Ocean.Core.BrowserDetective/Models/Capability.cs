using System;
using System.Collections.Generic;

namespace Ocean.Core.BrowserDetective.Models;

public partial class Capability
{
    public long Id { get; set; }

    public long BrowserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Value { get; set; }

    public virtual Browser Browser { get; set; } = null!;
}
