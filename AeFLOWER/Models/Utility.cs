using System.Text;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;
using System.Security.Claims;
using System.Net.Mail;
using System.Net;
using System.Security.Principal;

namespace AeFLOWER.Models
{
    public class Utility
    {
        public Utility() { }

        public void CreateUser(Account account)
        {
            FlowershopContext db = new FlowershopContext();

            if (account.AccountId == null)
                account.AccountId = Guid.NewGuid().ToString();

            account.Password = HashingPassword(account.Password); 
            db.Accounts.Add(account);
            db.SaveChanges();
        }
        public static string HashingPassword(string password)
        {
            //Hashing Password
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(password));
            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
            //
        }

        public static List<string>CountryList()
        {
            List<string> CultureList = new List<string>();
            CultureInfo[] GetCultureInfos = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (CultureInfo item in GetCultureInfos)
            {
                RegionInfo regionInfo = new RegionInfo(item.LCID);
                if(!(CultureList.Contains(regionInfo.EnglishName)))
                {
                    CultureList.Add(regionInfo.EnglishName);
                }
            }
            CultureList.Sort();
            return CultureList;
        }
        public static string CreateRandomPassword(int length = 15)
        {
            // Create a string of characters, numbers, and special characters that are allowed in the password
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string
            // and create an array of chars
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        public static void SendEmail(Account getAccount,string newPass)
        {
            MailMessage mm = new MailMessage("tahoanggia810@gmail.com", getAccount.Email.Trim());
            mm.Subject = "Password Recovery";
            mm.Body = string.Format("Hi {0},<br /><br />Your new password is {1}<br /><br />Thank You.", getAccount.Username, newPass.ToString());
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("tahoanggia810@gmail.com", "reuznhrjgiemzenu");
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Send(mm);
        }
    }
}
