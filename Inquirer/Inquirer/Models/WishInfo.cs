using System;
using Rcn.Interfaces.Inquirer;

namespace InquirerForAndroid.Models
{
    public class WishInfo : IWishInfo
    {
        public int WishId { get; set; }
        public int SurveyReportId { get; set; }
        public string Text { get; set; }
        public string Responsible { get; set; }
        public string Term { get; set; }
        public string Comment { get; set; }
        public WishStatuses Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
