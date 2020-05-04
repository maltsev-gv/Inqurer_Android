using System.Collections.Generic;
using Rcn.Interfaces.Inquirer;

namespace InquirerForAndroid.Models
{
    public class QuestionInfo : IQuestionInfo
    {
        public int QuestionId { get; set; }
        public int ParentBlockId { get; set; }
        public string Title { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public List<string> AnswerVariants { get; set; }
        public int Order { get; set; }
    }
}
