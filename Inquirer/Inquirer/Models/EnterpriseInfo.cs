using System.Collections.Generic;
using System.Linq;
using Rcn.Common;
using Rcn.Interfaces.Inquirer;

namespace InquirerForAndroid.Models
{
    public class EnterpriseInfo : ObservableObject, IEnterpriseInfo
    {
        public int EnterpriseId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public List<ISurveyInfo> Surveys { get; set; }
        public string ImageUrl { get; set; }
        public List<IEnterpriseInfo> Children { get; set; }
        public bool IsDefault { get; set; }

        public EnterpriseInfo Parent { get; set; }

        public bool IsVisible
        {
            get => GetVal<bool>();
            set
            {
                SetVal(value, RaiseAll);
                if (!value)
                {
                    Children?.ForEach(ei => ((EnterpriseInfo)ei).IsVisible = false);
                }
                else if (Parent != null)
                {
                    Parent.IsVisible = true;
                }
            }
        }

        public void RaiseAll()
        {
            RaisePropertyChanged(nameof(IsVisible));
            RaisePropertyChanged(nameof(IsExpanded));
            RaisePropertyChanged(nameof(IsExpandable));
            RaisePropertyChanged(nameof(NestingLevel));
        }

        public bool IsExpanded => Children?.Any(e => ((EnterpriseInfo)e).IsVisible) == true;

        public bool IsExpandable => Children?.Any() == true;

        private int? _nestingLevel;
        public int NestingLevel
        {
            get
            {
                if (_nestingLevel == null)
                {
                    _nestingLevel = 0;
                    var curInfo = this;
                    while (curInfo.Parent != null)
                    {
                        _nestingLevel++;
                        curInfo = curInfo.Parent;
                    }
                }

                return _nestingLevel.Value;
            }
        }

        public int CellHeight => IsVisible ? -1 : 0;

    }
}
