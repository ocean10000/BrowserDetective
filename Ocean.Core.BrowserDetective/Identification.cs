using Ocean.Core.BrowserDetective.Data.Models;
using System.Text.RegularExpressions;

namespace Ocean.Core.BrowserDetective
{

    public class Identification
    {
        private ICapture _Capture;
        private Regex _RegexPattern;
        private Match? _PatternMatch = null;
#pragma warning disable CS8618
        public Identification(ICapture Capture)
        {
            _Capture = Capture;
            if (string.IsNullOrEmpty(_Capture.Match) == false)
            {
                _RegexPattern = new Regex(_Capture.Match, RegexOptions.None);
            }
            else if (string.IsNullOrEmpty(_Capture.NonMatch) == false)
            {
                _RegexPattern = new Regex(_Capture.NonMatch, RegexOptions.None);
            }
        }
#pragma warning restore CS8618
        /// <summary>
        /// Builds a Match Object from the result of the regular expression
        /// </summary>
        /// <param name="header">Header Value which the regular expression will evaluate.</param>
        /// <returns>A Match object created from the regular expression and the passed in header.</returns>
        public Match Match(IDictionary<string, string> header, ref Result result)
        {
            string value = string.Empty;

            if (_Capture.Type == Ocean.Core.BrowserDetective.CaptureType.Header && header.ContainsKey(_Capture.Name))
            {
                value = header[_Capture.Name];
            }
            else if (_Capture.Type == Ocean.Core.BrowserDetective.CaptureType.capability && result.ContainsKey(_Capture.Name))
            {
                value = result[_Capture.Name];
            }

            _PatternMatch = _RegexPattern.Match(value);

            return _PatternMatch;
        }
        /// <summary>
        /// Returns true, if a match is made, false when unsucessfull.
        /// </summary>
        public bool Success
        {
            get
            {
                if (_PatternMatch == null)
                {
                    //------------------------------------------------------------
                    //CA1065: Do not raise exceptions in unexpected locations
                    //http://msdn.microsoft.com/library/bb386039%28VS.100%29.aspx
                    //------------------------------------------------------------
                    return false;
                }
                if (string.IsNullOrEmpty(_Capture.Match) == false)
                {
                    return _PatternMatch.Success;
                }
                else
                {
                    return !_PatternMatch.Success;
                }
            }
        }
        /// <summary>
        /// Returns true if the regular expression resulted in groups being captured.
        /// </summary>
        public bool HasCaptureGroups
        {
            get
            {
                if (_PatternMatch == null)
                {
                    //------------------------------------------------------------
                    //CA1065: Do not raise exceptions in unexpected locations
                    //http://msdn.microsoft.com/library/bb386039%28VS.100%29.aspx
                    //------------------------------------------------------------
                    return false;
                }
                if (_PatternMatch.Groups.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// Returns the expansion of the specified replacement pattern. 
        /// </summary>
        /// <param name="template">The replacement parameter is a standard regular expression replacement pattern. It can consist of literal characters and regular expression substitutions.</param>
        /// <returns>Returns the expansion of the specified replacement pattern. </returns>
        /// <exception cref="nBrowser.Exception">Missing Regular Expression Value, Match Function was not previously called</exception>
        public string Result(string? template)
        {
            if (_PatternMatch == null)
            {
                throw new Exception("Missing Regular Expression Value, Match Function was not previously called");
            }
            if (HasCaptureGroups)
            {
                if (string.IsNullOrEmpty(_Capture.Match) == false && _PatternMatch.Success == true && string.IsNullOrEmpty(template) == false)
                {
                    if (_PatternMatch.Result(template) != template)
                        return _PatternMatch.Result(template);
                    else
                        return null;
                }
            }
            return string.Empty;
        }
    }
}
