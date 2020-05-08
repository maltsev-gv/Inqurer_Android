using System;
using System.Collections.Generic;
using Rcn.Common;
using Rcn.Common.Exceptions;
using Rcn.Interfaces;
using Rcn.Interfaces.Inquirer;

namespace InquirerForAndroid.Models
{
    public class SurveyInfo : ObservableObject, ISurveyInfo
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

        [InitialValue(true)]
        public bool IsVisible
        {
            get => GetVal<bool>();
            set => SetVal(value);
        }

        public string SurveyStatus
        {
            get
            {
                switch (GlobalStatus)
                {
                    case GlobalSurveyStatuses.Planned:
                        return "Опрос ещё не начался";
                    case GlobalSurveyStatuses.Active:
                        return $"Опрос завершится: {EndsAt.ToString(Globals.DateFormat)}";
                    case GlobalSurveyStatuses.Processing:
                        return $"Обработка завершится: {ProcessedAt.ToString(Globals.DateFormat)}";
                    case GlobalSurveyStatuses.Completed:
                        return "Статистика доступна для просмотра";
                    case GlobalSurveyStatuses.Expires:
                        return "Опрос устарел";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public PictureTypes PictureType =>
            GlobalStatus == GlobalSurveyStatuses.Completed
                ? PictureTypes.Completed
                : GlobalStatus == GlobalSurveyStatuses.Processing
                    ? PictureTypes.Processing
                    : UserStatus == UserSurveyStatuses.Started
                        ? PictureTypes.Paused
                        : UserStatus == UserSurveyStatuses.Finished
                            ? PictureTypes.Finished
                            : PictureTypes.None;

        public bool IsEnabled => GlobalStatus != GlobalSurveyStatuses.Processing;
    }
}