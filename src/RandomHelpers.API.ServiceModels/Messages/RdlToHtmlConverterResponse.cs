using RandomHelpers.API.ServiceModels.Types;
using ServiceStack;
using System.Collections.Generic;

namespace RandomHelpers.API.ServiceModels.Messages
{
    public class RdlToHtmlConverterResponse : IHasResponseStatus
    {
        public List<Html> ConvertedFiles { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
