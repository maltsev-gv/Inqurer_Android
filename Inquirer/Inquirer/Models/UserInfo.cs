using Rcn.Interfaces.Inquirer;

namespace InquirerForAndroid.Models
{
    public class UserInfo: IUserInfo
    {
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsTester { get; set; }
        public string PersonnelNumber { get; set; }
    }
}
