using System.Collections.Generic;
using Rcn.Interfaces.Inquirer;

namespace InquirerForAndroid.Models
{
    public class BlockInfo : IBlockInfo
    {
        public int BlockId { get; set; }
        public string BlockName { get; set; }
        public List<IBlockInfo> SubBlocks { get; set; }
        public int Order { get; set; }
        public List<IQuestionInfo> Questions { get; set; }
        public int? UnlockerQuestionId { get; set; }
        public List<string> UnlockerAnswerVariants { get; set; }
    }
}
