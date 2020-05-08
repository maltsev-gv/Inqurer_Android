using System.Collections.Generic;
using InquirerForAndroid.Services;
using Rcn.Interfaces.Inquirer;
using Xamarin.Forms;

namespace InquirerForAndroid.Models
{
    public class DiagramInfo : IDiagramInfo
    {
        private IDataStore _dataStore;

        public DiagramInfo()
        {
            _dataStore = DependencyService.Get<IDataStore>();
        }

        public int DiagramId { get; set; }
        public int SurveyReportId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DiagramTypes DiagramType { get; set; }
        public string DataJson { get; set; }
        public string LayoutJson { get; set; }
        public string ImageUrl { get; set; }
        public PublicationPlaces PublishOn { get; set; }
        public PublicationPlaces PublishDescriptionOn { get; set; }
        public List<IDiagramGroupInfo> Groups { get; set; }
        public int? TemplateId { get; set; }

        public ImageSource ImageSource => _dataStore.GetImageSource(ImageUrl);
    }
}
