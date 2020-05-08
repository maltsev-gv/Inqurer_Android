using System.Collections.Generic;
using Rcn.Interfaces.Inquirer;

namespace InquirerForAndroid.Models
{
    public class SurveyReportInfo : ISurveyReportInfo
    {
        public int SurveyReportId { get; set; }
        public int? EnterpriseToSurveyId { get; set; }
        public int? SurveyId { get; set; }
        public bool IsTemplate { get; set; }
        public string Title { get; set; }
        public int RespondentsNumber { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }
        public List<IDiagramInfo> Diagrams { get; set; }
        public int? TemplateId { get; set; }
        public List<IWishInfo> Wishes { get; set; }
        public List<IWishOptionInfo> WishOptions { get; set; }
    }
}
