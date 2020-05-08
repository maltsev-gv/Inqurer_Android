using InquirerForAndroid.Models;
using InquirerForAndroid.Views;

namespace InquirerForAndroid.ViewModels
{
    public class SurveyViewModel : ViewModelBase
    {
        private SurveySelectorViewModel _surveySelectorViewModel;

        public SurveyViewModel()
        {
            IsBackButtonPresent = true;
        }

        public SurveyViewModel(SurveySelectorViewModel surveySelectorViewModel, SurveyInfo surveyInfo) : this()
        {
            Title = surveyInfo.SurveyName;
            _surveySelectorViewModel = surveySelectorViewModel;
        }

        protected override void OnBackButtonPressed()
        {
            WrapperPage.GoToView(_surveySelectorViewModel, forward: false);
        }
    }
}
