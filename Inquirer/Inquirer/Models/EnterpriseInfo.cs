using System.Collections.Generic;
using Rcn.Interfaces.Inquirer;

namespace InquirerForAndroid.Models
{
    public class EnterpriseInfo : IEnterpriseInfo
    {
        public int EnterpriseId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public List<ISurveyInfo> Surveys { get; set; }
        public string ImageUrl { get; set; }
        public List<IEnterpriseInfo> Children { get; set; }
        public bool IsDefault { get; set; }
    }
}
