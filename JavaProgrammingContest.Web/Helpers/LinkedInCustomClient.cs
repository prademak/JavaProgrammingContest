using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OAuth.Messages;

namespace JavaProgrammingContest.Web.Helpers{
    public class LinkedInCustomClient : OAuthClient{
        private static XDocument LoadXDocumentFromStream(Stream stream){
            var settings = new XmlReaderSettings{
                MaxCharactersInDocument = 65536L
            };
            return XDocument.Load(XmlReader.Create(stream, settings));
        }

        /// Describes the OAuth service provider endpoints for LinkedIn.
        private static readonly ServiceProviderDescription LinkedInServiceDescription =
            new ServiceProviderDescription{
                AccessTokenEndpoint =
                    new MessageReceivingEndpoint("https://api.linkedin.com/uas/oauth/accessToken",
                        HttpDeliveryMethods.PostRequest),
                RequestTokenEndpoint =
                    new MessageReceivingEndpoint("https://api.linkedin.com/uas/oauth/requestToken",
                        HttpDeliveryMethods.PostRequest),
                UserAuthorizationEndpoint =
                    new MessageReceivingEndpoint("https://www.linkedin.com/uas/oauth/authorize",
                        HttpDeliveryMethods.PostRequest),
                TamperProtectionElements =
                    new ITamperProtectionChannelBindingElement[]{new HmacSha1SigningBindingElement()},
                ProtocolVersion = ProtocolVersion.V10a
            };

        public LinkedInCustomClient(string consumerKey, string consumerSecret)
            :
                base("linkedIn", LinkedInServiceDescription, consumerKey, consumerSecret) {}

        /// Check if authentication succeeded after user is redirected back from the service provider.
        /// The response token returned from service provider authentication result. 
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "We don't care if the request fails.")]
        protected override AuthenticationResult VerifyAuthenticationCore(AuthorizedTokenResponse response){
            // See here for Field Selectors API http://developer.linkedin.com/docs/DOC-1014
            const string profileRequestUrl =
                "https://api.linkedin.com/v1/people/~:(id,first-name,last-name,headline,industry,summary)";

            string accessToken = response.AccessToken;

            var profileEndpoint =
                new MessageReceivingEndpoint(profileRequestUrl, HttpDeliveryMethods.GetRequest);
            HttpWebRequest request =
                WebWorker.PrepareAuthorizedRequest(profileEndpoint, accessToken);

            try{
                using (WebResponse profileResponse = request.GetResponse())
                    using (Stream responseStream = profileResponse.GetResponseStream()){
                        XDocument document = LoadXDocumentFromStream(responseStream);
                        string userId = document.Root.Element("id").Value;

                        string firstName = document.Root.Element("first-name").Value;
                        string lastName = document.Root.Element("last-name").Value;
                        string userName = firstName + " " + lastName;

                        var extraData = new Dictionary<string, string>{
                            {"accesstoken", accessToken},
                            {"name", userName}
                        };

                        return new AuthenticationResult(
                            isSuccessful: true,
                            provider: ProviderName,
                            providerUserId: userId,
                            userName: userName,
                            extraData: extraData);
                    }
            } catch (Exception exception){
                return new AuthenticationResult(exception);
            }
        }
    }
}