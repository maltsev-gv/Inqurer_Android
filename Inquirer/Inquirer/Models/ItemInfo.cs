using System;
using System.Collections.Generic;

namespace Inquirer.Models
{
    public class ItemInfo
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public ValueTypes ValueType { get; set; }

        public List<string> ValueList { get; set; } = new List<string>();

        public ItemTypes ItemType { get; set; }
    }

    public enum ValueTypes
    {
        Text,
        List,
        Stars,
        YesNo
    }
    public enum ItemTypes
    {
        Parameter,
        Header,
        SubHeader,
    }
}