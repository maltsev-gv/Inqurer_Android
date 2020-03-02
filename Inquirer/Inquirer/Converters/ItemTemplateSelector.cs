using System;
using Inquirer.Models;
using Xamarin.Forms;

namespace Inquirer.Converters
{
    public class ItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate HeaderTemplate { get; set; }
        public DataTemplate SubHeaderTemplate { get; set; }
        public DataTemplate ListTemplate { get; set; }
        public DataTemplate TextTemplate { get; set; }
        public DataTemplate StarsTemplate { get; set; }
        public DataTemplate YesNoTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (!(item is ItemInfo itemInfo))
            {
                throw new ArgumentOutOfRangeException(
                    $"{nameof(ItemTemplateSelector)}: item is not {nameof(ItemInfo)}");
            }

            switch (itemInfo.ItemType)
            {
                case ItemTypes.Header:
                    return HeaderTemplate;
                case ItemTypes.SubHeader:
                    return SubHeaderTemplate;
                case ItemTypes.Parameter:
                {
                    switch (itemInfo.ValueType)
                    {
                        case ValueTypes.Text:
                            return TextTemplate;
                        case ValueTypes.List:
                            return ListTemplate;
                        case ValueTypes.Stars:
                            return StarsTemplate;
                        case ValueTypes.YesNo:
                            return YesNoTemplate;
                    }

                    throw new ArgumentOutOfRangeException(
                        $"{nameof(ItemTemplateSelector)}: no template for {itemInfo.ValueType}");
                }
            }

            throw new ArgumentOutOfRangeException(
                $"{nameof(ItemTemplateSelector)}: no template for {itemInfo.ItemType}");

        }
    }
}
