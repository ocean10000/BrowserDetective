using System;
using System.Collections.Generic;

namespace Ocean.Core.BrowserDetective.Models;

public partial class SampleHeader
{
    public long Id { get; set; }

    public long BrowserId { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

}
