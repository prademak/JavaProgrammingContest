using Microsoft.Web.WebPages.OAuth;

namespace JavaProgrammingContest.Web.App_Start {
    public static class AuthConfig {
        public static void RegisterAuth() {
         
            OAuthWebSecurity.RegisterTwitterClient(
              consumerKey: "YWIn1H8KULGObNr44RbBw",
                consumerSecret: "awZspGQZhvh4zNyydT7WfVC5VfM4M2Fua5UP4D4Ak");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "179944392143624",
                appSecret: "f3822e9fd6e2ac05f53d157e48567009");

             OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
