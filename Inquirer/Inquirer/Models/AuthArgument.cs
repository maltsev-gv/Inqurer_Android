using Rcn.Interfaces.Inquirer;

namespace InquirerForAndroid.Models
{
    public class AuthArgument : IAuthArgument
    {
        public string PersonnelNumber { get; set; }
        public string Password { get; set; }
    }
}
