using JavaProgrammingContest.Web.Helpers;
using Microsoft.Web.WebPages.OAuth;

namespace JavaProgrammingContest.Web.App_Start{
    public static class AuthConfig{
        public static void RegisterAuth(){
            /*
            OAuthWebSecurity.RegisterTwitterClient(
              consumerKey: "YWIn1H8KULGObNr44RbBw",
                consumerSecret: "awZspGQZhvh4zNyydT7WfVC5VfM4M2Fua5UP4D4Ak");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "179944392143624",
                appSecret: "f3822e9fd6e2ac05f53d157e48567009");

             OAuthWebSecurity.RegisterGoogleClient();*/

           OAuthWebSecurity.RegisterLinkedInClient(
                 consumerKey: "tdodjkbahr83",
              consumerSecret: "l9EaWtPS4nhPG35d");

           // OAuthWebSecurity.RegisterClient(new LinkedInCustomClient("b08e9cc9-a08e-4842-a702-46fc5527905f",
            //    "54044dc3-35e8-4fb4-8f44-ff74ad7fc1be"), "LinkedIn", null);
        }
    }
}