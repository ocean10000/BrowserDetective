using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

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
        public override string ToString()
        {
            var sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = Encoding.UTF32;
            settings.OmitXmlDeclaration = true;
            using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(sb, settings))
            {
                if (Type == BrowserType.Browser)
                {
                    writer.WriteStartElement("Browser");
                }
                else
                {
                    writer.WriteStartElement("Gateway");
                }
                writer.WriteAttributeString("Id", Id.ToString());
                writer.WriteAttributeString("ParentId", ParentId.ToString());
                writer.WriteAttributeString("Prority", Prority.ToString());
                writer.WriteAttributeString("Name", Name.ToString());
                #region Sample
                foreach (var item in Samples)
                {
                    writer.WriteStartElement("Samples");
                    writer.WriteAttributeString("Name", item.Name);
                    writer.WriteAttributeString("Value", item.Value);
                    writer.WriteEndElement();
                }
                #endregion
                #region Ident
                foreach (var item in Identifications)
                {
                    writer.WriteStartElement("Identification");
                    writer.WriteAttributeString("Type", item.Type.ToString());
                    writer.WriteAttributeString("Name", item.Name);
                    if (string.IsNullOrWhiteSpace(item.Match) == false)
                    {
                        writer.WriteAttributeString("Match", item.Match);
                    }
                    else if (string.IsNullOrWhiteSpace(item.NonMatch) == false)
                    {
                        writer.WriteAttributeString("NonMatch", item.NonMatch);
                    }
                    writer.WriteEndElement();
                }
                #endregion
                #region Captures
                foreach (var item in Captures)
                {
                    writer.WriteStartElement("Capture");
                    writer.WriteAttributeString("Type", item.Type.ToString());
                    writer.WriteAttributeString("Name", item.Name);
                    if (string.IsNullOrWhiteSpace(item.Match) == false)
                    {
                        writer.WriteAttributeString("Match", item.Match);
                    }
                    else if (string.IsNullOrWhiteSpace(item.NonMatch) == false)
                    {
                        writer.WriteAttributeString("NonMatch", item.NonMatch);
                    }
                    writer.WriteEndElement();
                }
                #endregion
                #region Capabilities
                foreach (var item in Capabilities)
                {
                    writer.WriteStartElement("Capability");
                    writer.WriteAttributeString("Name", item.Name);
                    writer.WriteAttributeString("Value", item.Value);
                    writer.WriteEndElement();
                }
                #endregion
                #region Children
                foreach (var item in InverseParent)
                {
                    writer.WriteStartElement("Child");
                    writer.WriteAttributeString("Id", item.Id.ToString());
                    writer.WriteAttributeString("Name", item.Name);
                    writer.WriteAttributeString("Type", item.Type.ToString());
                    writer.WriteEndElement();
                }
                #endregion
                writer.WriteEndElement();
                writer.Flush();
            }
            return sb.ToString();
        }
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
    //this is to allow us to track Resulting changes by Browser Id. 
    //for easier out when items changed easier.
    public partial class Trackitem
    {
        public virtual long BrowserID { get; set; }
        public virtual string? BrowserName { get; set; }
        public virtual BrowserType Type { get; set; }
        public virtual string Name { get; set; } = string.Empty;

        public virtual string Value { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{BrowserID}, {BrowserName}, {Name}, {Value}";
        }
    }
}
