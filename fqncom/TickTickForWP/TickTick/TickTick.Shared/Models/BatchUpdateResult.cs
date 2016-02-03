using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TickTick.Enums;

namespace TickTick.Models
{
    public class BatchUpdateResult
    {
        Dictionary<String, String> _idToEtag = new Dictionary<string, string>();

        [JsonProperty("id2etag")]
        public Dictionary<String, String> IdToEtag
        {
            get { return _idToEtag; }
            set { _idToEtag = value; }
        }

        Dictionary<String, ErrorType> _idToError = new Dictionary<string, ErrorType>();

        [JsonProperty("id2error")]
        public Dictionary<String, ErrorType> IdToError
        {
            get { return _idToError; }
            set { _idToError = value; }
        }
    }
}
