using RandomHelpers.API.ServiceModels.Types;
using System.Collections.Generic;

namespace RandomHelpers.API.Template.Interfaces.Managers
{
    public interface IRdlToHtmlConversionManager
    {
        List<Html> ConvertRdlToHtml(List<string> rdlFiles, bool saveToDisk, string htmlSavePath);
    }
}
