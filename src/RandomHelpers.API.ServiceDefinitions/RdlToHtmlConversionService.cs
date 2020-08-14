using RandomHelpers.API.ServiceModels.Messages;
using RandomHelpers.API.ServiceModels.Types;
using RandomHelpers.API.Template.Interfaces.Managers;
using ServiceStack;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RandomHelpers.API.ServiceDefinitions
{
    public class RdlToHtmlConversionService : Service
    {
        private readonly IRdlToHtmlConversionManager rdlToHtmlConversionManager;

        public RdlToHtmlConversionService(IRdlToHtmlConversionManager rdlToHtmlConversionManager)
        {
            this.rdlToHtmlConversionManager = rdlToHtmlConversionManager;
        }

        public RdlToHtmlConverterResponse Post(RdlToHtmlConverterRequest request)
        {
            string folderPath = Path.GetDirectoryName(request.RdlInfo.RdlFolderPath);
            Directory.SetCurrentDirectory(folderPath);
            List<string> fileNames = Directory.EnumerateFiles($"{folderPath}", "*.rdl", SearchOption.AllDirectories).ToList();

            RdlToHtmlConverterResponse response = new RdlToHtmlConverterResponse();

            List<Html> convertedFiles = rdlToHtmlConversionManager.ConvertRdlToHtml(fileNames, request.RdlInfo.SaveToDisk, request.RdlInfo?.HtmlSavePath);

            if (!request.RdlInfo.SaveToDisk)
            {
                response.ConvertedFiles = convertedFiles;
            }

            return response;
        }
    }
}
