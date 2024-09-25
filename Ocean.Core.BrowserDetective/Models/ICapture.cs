using System;
using System.Collections.Generic;

namespace Ocean.Core.BrowserDetective.Models;

public interface ICapture
{
    public long Id { get; set; }

    public long BrowserId { get; set; }

    public CaptureType Type { get; set; }

    public string Name { get; set; }

    public string? Match { get; set; } 

    public string? NonMatch { get; set; }

    public Browser Browser { get; set; }
}
