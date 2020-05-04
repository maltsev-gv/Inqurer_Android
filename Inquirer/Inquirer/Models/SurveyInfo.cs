using System;
using Rcn.Interfaces.Inquirer;

namespace InquirerForAndroid.Models
{
    public class SurveyInfo : ISurveyInfo
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public IBlockInfo FirstBlock { get; set; }
        public GlobalSurveyStatuses GlobalStatus { get; set; }
        public UserSurveyStatuses UserStatus { get; set; }
        public DateTime EndsAt { get; set; }
        public DateTime ProcessedAt { get; set; }
        public ISurveyReportInfo SurveyReportTemplate { get; set; }
        public int SurveyReportId { get; set; }
    }
}
