using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Service.VR;
using InquirerForAndroid.Models;
using InquirerForAndroid.Services;
using InquirerForAndroid.Views;
using Rcn.Common.ExtensionMethods;
using Rcn.Interfaces.Inquirer;
using Xamarin.Forms;

namespace InquirerForAndroid.ViewModels
{
    public class SurveySelectorViewModel : ViewModelBase
    {
        public SurveySelectorViewModel()
        {
            //GoToAuth();
            Debug.WriteLine($"SurveySelectorViewModel: ctor");
            Title = "Выбор опроса" + (Enterprise == null ? "" : $" ({Enterprise.ShortName})");
            IsBackButtonPresent = true;
            LoadReportsCommand = new Command(async isForced => await LoadReportsMethod(isForced as bool? ?? true));
            LoadReportsCommand.Execute(false);
            SurveySelectedCommand = new Command(SurveySelectedMethod);
        }

        public SurveySelectorViewModel(EnterpriseInfo info) : this()
        {
            Enterprise = info;
            Surveys = info.Surveys.Select(s => (SurveyInfo) s).ToList();
        }

        public EnterpriseInfo Enterprise { get; set; }

        private void SurveySelectedMethod(object obj)
        {
            var surveyInfo = (SurveyInfo) obj;
            if (surveyInfo.GlobalStatus == GlobalSurveyStatuses.Completed)
            {
                WrapperPage.GoToView(new ReportViewModel(this, surveyInfo));
            }
            else
            {
                WrapperPage.GoToView(new SurveyViewModel(this, surveyInfo));
            }
        }

        public int EnterpriseId => Enterprise?.EnterpriseId ?? 0;

        public ICommand LoadReportsCommand { get; set; }
        public ICommand SurveySelectedCommand { get; set; }

        protected override void OnBackButtonPressed()
        {
            WrapperPage.GoToView(new EnterpriseSelectorViewModel(), forward: false);
        }

        public override bool IsSameAs(ViewModelBase viewModel)
        {
            return viewModel is SurveySelectorViewModel otherSelectorViewModel
                   && otherSelectorViewModel.EnterpriseId == EnterpriseId;
        }

        public List<SurveyInfo> Surveys
        {
            get => GetVal<List<SurveyInfo>>();
            set => SetVal(value);
        }

        public string FilterText
        {
            get => GetVal<string>();
            set
            {
                SetVal(value);
                if (!value.IsNullOrEmpty())
                {
                    var lowerText = value.ToLower();
                    Surveys.ForEach(si => si.IsVisible = si.SurveyName.ToLower().Contains(lowerText));
                }
                else
                {
                    Surveys.ForEach(si => si.IsVisible = true);
                }
            }
        }

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
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                if (forceRefresh || Surveys.Count == 0)
                {
                    await AppShell.Alert("Ошибка связи", ex.Message, null, "Закрыть");
                }
            }
            finally
            {
                IsRefreshing = false;
            }
        }
    }
}
