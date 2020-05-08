using Rcn.Interfaces.Inquirer;

namespace InquirerForAndroid.Models
{
    public class WishOptionInfo : IWishOptionInfo
    {
        public int WishOptionId { get; set; }
        public int SurveyReportId { get; set; }
        public string WishPropertyName { get; set; }
        public PublicationPlaces PublishOn { get; set; }
    }
}
