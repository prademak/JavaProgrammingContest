using JavaProgrammingContest.Web.Helpers;
using Microsoft.Web.WebPages.OAuth;

namespace JavaProgrammingContest.Web.App_Start{
    public static class AuthConfig{
        public static void RegisterAuth(){
       
           OAuthWebSecurity.RegisterLinkedInClient(
                 consumerKey: "tdodjkbahr83",
              consumerSecret: "l9EaWtPS4nhPG35d");

        }
    }
}