using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using InquirerForAndroid.Models;
using InquirerForAndroid.Views;
using Xamarin.Forms;

namespace InquirerForAndroid.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        private SurveySelectorViewModel _surveySelectorViewModel;
        private readonly int _reportId;

        public ReportViewModel()
        {
            IsBackButtonPresent = true;
        }

        public ReportViewModel(SurveySelectorViewModel surveySelectorViewModel, SurveyInfo surveyInfo) : this()
        {
            Title = surveyInfo.SurveyName;
            _reportId = surveyInfo.SurveyReportId;
            _surveySelectorViewModel = surveySelectorViewModel;
            LoadReportsCommand = new Command(async isForced => await LoadReportsMethod(isForced as bool? ?? true));
            LoadReportsCommand.Execute(false);
        }

        public ICommand LoadReportsCommand { get; set; }

        public async Task LoadReportsMethod(bool forceRefresh)
        {
            if (IsRefreshing)
            {
                return;
            }

            try
            {
                IsRefreshing = true;
                var reports = await DataStore.GetReports(forceRefresh);
                Report = reports.Single(r => r.SurveyReportId == _reportId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                if (forceRefresh || Report == null)
                {
                    await AppShell.Alert("Ошибка связи", ex.Message, null, "Закрыть");
                }
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        public SurveyReportInfo Report
        {
            get => GetVal<SurveyReportInfo>();
            set => SetVal(value);
        }

        public string WishesCountStr => Report?.Wishes == null
            ? ""
            : $"Пожелания сотрудников: {Report?.Wishes.Count}";

        protected override void OnBackButtonPressed()
        {
            WrapperPage.GoToView(_surveySelectorViewModel, forward: false);
        }
    }
}
