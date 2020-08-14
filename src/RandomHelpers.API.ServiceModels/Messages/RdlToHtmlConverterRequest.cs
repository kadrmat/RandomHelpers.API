using RandomHelpers.API.ServiceModels.Types;
using ServiceStack;

namespace RandomHelpers.API.ServiceModels.Messages
{
    [Route("/Converters/RdlToHtml/",
        Summary = "Converts RDL files from SSMS to Html.",
        Verbs = "POST")]
    public class RdlToHtmlConverterRequest : IReturn<RdlToHtmlConverterResponse>
    {
        public RdlInfo RdlInfo { get; set; }
    }
}
