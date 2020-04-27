using System;
using System.Collections.Generic;
using System.Linq;
using InquirerForAndroid.Models;
using Rcn.Common.ExtensionMethods;
using Xamarin.Forms;

namespace InquirerForAndroid.Services
{
    public static class TreeExtensions
    {
        public static T GetParentOfType<T>(this Element element) where T : Element
        {
            var parent = element.Parent;
            while (parent != null && parent.GetType() != typeof(T))
            {
                parent = parent.Parent;
            }

            return (T)parent;
        }

        public static List<EnterpriseInfo> GetEnterprisesByFilter(this List<EnterpriseInfo> enterprises, string namePattern, bool? isDefault = null)
        {
            var result = new List<EnterpriseInfo>();
            namePattern = namePattern?.ToLower() ?? "";
            FindEnterprises(enterprises, namePattern, isDefault, result);
            result = result.OrderBy(ei => ei.Name).ToList();
            if (!namePattern.IsNullOrEmpty())
            {
                var startsFromPattern = result.Where(ei => ei.Name.ToLower().StartsWith(namePattern)).ToArray();
                result.RemoveAll(ei => startsFromPattern.Contains(ei));
                result = startsFromPattern.Concat(result).ToList();
            }
            return result;
        }

        public static List<EnterpriseInfo> GetFlatEnterprisesList(this List<EnterpriseInfo> rawEnterprises)
        {
            var result = new List<EnterpriseInfo>();
            FindEnterprises(rawEnterprises, "", null, result);
            return result;
        }

        private static void FindEnterprises(List<EnterpriseInfo> enterprises, string namePattern, bool? isDefault, List<EnterpriseInfo> result, EnterpriseInfo parent = null)
        {
            foreach (var enterpriseInfo in enterprises)
            {
                if (namePattern != "" && enterpriseInfo.Name.ToLower().Contains(namePattern) 
                    || isDefault != null && enterpriseInfo.IsDefault == isDefault
                    || namePattern == string.Empty && isDefault == null)
                {
                    result.Add(enterpriseInfo);
                    enterpriseInfo.Parent = parent;
                    enterpriseInfo.IsVisible = parent == null;
                }
                FindEnterprises(enterpriseInfo.Children.OfType<EnterpriseInfo>().ToList(), namePattern, isDefault, result, enterpriseInfo);
            }
        }
    }
}
