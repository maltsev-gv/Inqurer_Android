using System.Collections.Generic;
using Rcn.Interfaces.Inquirer;

namespace InquirerForAndroid.Models
{
    public class AnswerInfo : IAnswerInfo
    {
        public int AnswerId { get; set; }
        public int UserId { get; set; }
        public int EnterpriseId { get; set; }
        public List<string> Answers { get; set; }
        public int QuestionId { get; set; }
        public int SurveyId { get; set; }
    }
}