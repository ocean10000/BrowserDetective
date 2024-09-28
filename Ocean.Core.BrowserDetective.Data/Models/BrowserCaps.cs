using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Ocean.Core.BrowserDetective.Data.Models
{
    public partial class Browser
    {
        public virtual long Id { get; set; }

        public virtual long? ParentId { get; set; }

        [NotMapped]
        public ILogger? _logger { get; set; } = null;

        [NotMapped]
        public virtual string parentID { get; set; } = string.Empty;

        public virtual BrowserType Type { get; set; }

        public virtual long? Prority { get; set; }

        public virtual string Name { get; set; } = null!;

        public virtual ICollection<Capability> Capabilities { get; set; } = new List<Capability>();

        public virtual ICollection<Capture> Captures { get; set; } = new List<Capture>();

        public virtual ICollection<Identification> Identifications { get; set; } = new List<Identification>();

        public virtual ICollection<SampleHeader> Samples { get; set; } = new List<SampleHeader>();

        public virtual ICollection<Browser> InverseParent { get; set; } = new List<Browser>();

        public virtual Browser? Parent { get; set; }
    }
    public partial class Capability
    {
        public virtual long Id { get; set; }

        public virtual long BrowserId { get; set; }

        public virtual string Name { get; set; } = null!;

        public virtual string? Value { get; set; }

        public virtual Browser Browser { get; set; } = null!;
    }
    public partial class Capture : ICapture
    {
        public virtual long Id { get; set; }

        public virtual long BrowserId { get; set; }

        public virtual CaptureType Type { get; set; }

        public virtual string Name { get; set; } = string.Empty;

        public virtual string? Match { get; set; } = null!;

        public virtual string? NonMatch { get; set; } = null!;

        public virtual Browser Browser { get; set; } = null!;
    }
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
    public partial class Identification : ICapture
    {
        public virtual long Id { get; set; }

        public virtual long BrowserId { get; set; }

        public virtual CaptureType Type { get; set; }

        public virtual string Name { get; set; } = string.Empty;

        public virtual string? Match { get; set; } = null!;

        public virtual string? NonMatch { get; set; } = null!;

        public virtual Browser Browser { get; set; } = null!;
    }
    public partial class SampleHeader
    {
        public virtual long Id { get; set; }

        public virtual long BrowserId { get; set; }

        public virtual string? Name { get; set; }

        public virtual string? Value { get; set; }

    }
}
