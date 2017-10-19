using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawHttpClient
{
    public class OktaUser
    {
        public string id { get; set; }
        public string status { get; set; }
        public DateTime created { get; set; }
        public DateTime? activated { get; set; }
        public DateTime? statusChanged { get; set; }
        public DateTime? lastLogin { get; set; }
        public DateTime lastUpdated { get; set; }
        public DateTime? passwordChanged { get; set; }
        public Profile profile { get; set; }
        public Credentials credentials { get; set; }
        public Links _links { get; set; }
    }

    public class Profile
    {
        public string lastName { get; set; }
        public string secondEmail { get; set; }
        public string primaryPhone { get; set; }
        public string mobilePhone { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string firstName { get; set; }
        public string displayName { get; set; }
        public bool? ECIAdmin { get; set; }
        public string streetAddress { get; set; }
        public string department { get; set; }
        public string countryCode { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string title { get; set; }
        public string EcicloudSN { get; set; }
        public string zipCode { get; set; }
        public string middleName { get; set; }
        public string honorificPrefix { get; set; }
        public string honorificSuffix { get; set; }
        public string SID { get; set; }
        public string upn { get; set; }
        public string LDAPDN { get; set; }
        public bool? SelfService { get; set; }
    }

    public class Provider
    {
        public string type { get; set; }
        public string name { get; set; }
    }

    public class Password
    {
    }

    public class RecoveryQuestion
    {
        public string question { get; set; }
    }

    public class Credentials
    {
        public Provider provider { get; set; }
        public Password password { get; set; }
        public RecoveryQuestion recovery_question { get; set; }
    }

    public class Activate
    {
        public string href { get; set; }
        public string method { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Suspend
    {
        public string href { get; set; }
        public string method { get; set; }
    }

    public class ResetPassword
    {
        public string href { get; set; }
        public string method { get; set; }
    }

    public class Deactivate
    {
        public string href { get; set; }
        public string method { get; set; }
    }

    public class Reactivate
    {
        public string href { get; set; }
        public string method { get; set; }
    }

    public class ExpirePassword
    {
        public string href { get; set; }
        public string method { get; set; }
    }

    public class ChangeRecoveryQuestion
    {
        public string href { get; set; }
        public string method { get; set; }
    }

    public class ForgotPassword
    {
        public string href { get; set; }
        public string method { get; set; }
    }

    public class ChangePassword
    {
        public string href { get; set; }
        public string method { get; set; }
    }

    public class ResetFactors
    {
        public string href { get; set; }
        public string method { get; set; }
    }

    public class Links
    {
        public Activate activate { get; set; }
        public Self self { get; set; }
        public Suspend suspend { get; set; }
        public ResetPassword resetPassword { get; set; }
        public Deactivate deactivate { get; set; }
        public Reactivate reactivate { get; set; }
        public ExpirePassword expirePassword { get; set; }
        public ChangeRecoveryQuestion changeRecoveryQuestion { get; set; }
        public ForgotPassword forgotPassword { get; set; }
        public ChangePassword changePassword { get; set; }
        public ResetFactors resetFactors { get; set; }
    }
}
