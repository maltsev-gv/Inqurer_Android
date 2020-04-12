using System;
using Rcn.Common;
using Rcn.Interfaces.Inquirer;

namespace InquirerForAndroid.Models
{
    public class NewsBlockInfo : ObservableObject, INewsBlockInfo
    {
        public int NewsBlockId { get; set; }
        public int EnterpriseId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }

        public string CreatedDateStr => CreatedDate.ToString(ConstantsCommon.DateAndTimeFormat);
        public bool CanBeExpanded
        {
            get => GetVal<bool>();
            set => SetVal(value, RaiseAll);
        }

        public bool IsNeedToCollapse => CanBeExpanded && IsExpanded;
        public bool IsNeedToExpand => CanBeExpanded && !IsExpanded;

        public bool IsExpanded
        {
            get => GetVal<bool>();
            set => SetVal(value, RaiseAll);
        }

        private void RaiseAll()
        {
            RaisePropertyChanged(nameof(IsNeedToCollapse));
            RaisePropertyChanged(nameof(IsNeedToExpand));
        }
    }
}
