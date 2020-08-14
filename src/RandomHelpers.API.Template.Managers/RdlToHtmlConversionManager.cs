using RandomHelpers.API.ServiceModels.Types;
using RandomHelpers.API.Template.Interfaces.Managers;
using RandomHelpers.API.Template.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web.Services.Protocols;
using RES2005 = RandomHelpers.API.Template.Managers.com.chrobinson.chrssrs1;
using RS2005 = RandomHelpers.API.Template.Managers.com.chrobinson.chrssrs;

namespace RandomHelpers.API.Template.Managers
{
    public class RdlToHtmlConversionManager : IRdlToHtmlConversionManager
    {
        private readonly IFileSaveRepository fileSaveRepository;

        public RdlToHtmlConversionManager(IFileSaveRepository fileSaveRepository)
        {
            this.fileSaveRepository = fileSaveRepository;
        }

        public List<Html> ConvertRdlToHtml(List<string> rdlFiles, bool saveToDisk, string htmlSavePath)
        {
            RS2005.ReportingService2005 rs = new RS2005.ReportingService2005();
            RES2005.ReportExecutionService rsExec = new RES2005.ReportExecutionService();

            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rsExec.Credentials = System.Net.CredentialCache.DefaultCredentials;

            rs.Url = "https://chrssrs.chrobinson.com/reportserver/reportservice2005.asmx";
            rsExec.Url = "https://chrssrs.chrobinson.com/reportserver/ReportExecution2005.asmx";

            bool threwException = false;
            StringBuilder sb = new StringBuilder();
            string historyID = null;
            string deviceInfo = null;
            string format = "HTML5.0";
            byte[] results;
            string encoding = string.Empty;
            string mimeType = string.Empty;
            string extension = string.Empty;
            RES2005.Warning[] warnings = null;
            string[] streamIDs = null;
            List<Html> documentsHtml = new List<Html>();
            StringBuilder fileSavePath = new StringBuilder();
            string filePath = string.Empty;

            foreach (string rdlFile in rdlFiles)
            {
                fileSavePath.Append(!string.IsNullOrEmpty(htmlSavePath) ? htmlSavePath : ConfigurationManager.AppSettings["FileSavePath"].ToString());
                fileSavePath.Append(rdlFile.Contains("IMS") ? @"\IMS\" : @"\Compass\");
                filePath = fileSavePath.ToString();
                fileSavePath.Clear();
                string reportPath = rdlFile.Contains("IMS") ? ConfigurationManager.AppSettings["SSRSIMSReportPath"] : ConfigurationManager.AppSettings["SSRSCompassReportPath"];
                string fileName = rdlFile.Substring(rdlFile.LastIndexOf(@"\") + 1, rdlFile.LastIndexOf(@".") - (rdlFile.LastIndexOf(@"\") + 1));
                string _reportName = string.Concat(reportPath, fileName);
                string _historyID = null;
                bool _forRendering = false;

                RS2005.ParameterValue[] _values = null;
                RS2005.DataSourceCredentials[] _credentials = null;
                RS2005.ReportParameter[] _parameters = null;
                try
                {
                    _parameters = rs.GetReportParameters(_reportName, _historyID, _forRendering, _values, _credentials);
                    RES2005.ExecutionInfo ei = rsExec.LoadReport(_reportName, historyID);

                    RES2005.ParameterValue[] parameters = new RES2005.ParameterValue[1];
                    if (_parameters.Length > 0)
                    {
                        foreach (RS2005.ReportParameter parameter in _parameters)
                        {
                            parameters[0] = new RES2005.ParameterValue { Name = parameter.Name, Value = "123" };
                        }
                    }

                    rsExec.SetExecutionParameters(parameters, "en-us");

                    results = rsExec.Render(format, deviceInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);
                    System.Text.Encoding enc = System.Text.Encoding.UTF8;
                    string tmpReport = enc.GetString(results);

                    if (saveToDisk)
                    {
                        fileSaveRepository.SaveFileToDisk(results, filePath, fileName, false);
                    }

                    documentsHtml.Add(new Html { HtmlData = tmpReport, RdlFileName = fileName });
                }
                catch (SoapException e)
                {
                    threwException = true;
                    sb.Append(string.Concat(fileName, 
                        Environment.NewLine, 
                        "---", 
                        e.Message, 
                        Environment.NewLine, 
                        "---", e.StackTrace, 
                        Environment.NewLine, 
                        "---", 
                        e.InnerException, 
                        Environment.NewLine, 
                        Environment.NewLine));

                    continue;
                }
            }

            if (threwException)
            {
                fileSaveRepository.SaveFileToDisk(Encoding.UTF8.GetBytes(sb.ToString()), filePath, null, threwException);
            }

            return documentsHtml;
        }
    }
}
