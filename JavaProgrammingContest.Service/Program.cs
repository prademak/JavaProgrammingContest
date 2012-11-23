using System;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace JavaProgrammingContest.Service{
    internal class Program{
        public static void Main(string[] args){
            //Create a host configuration
            var selfHostConfiguraiton = new HttpSelfHostConfiguration("http://localhost:8080");

            //Setup the routes
            selfHostConfiguraiton.Routes.MapHttpRoute(
                name: "DefaultApiRoute",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {controller = "Assignments", id = RouteParameter.Optional }
                );

            //Create Server & Wait for new connections
            using (var server = new HttpSelfHostServer(selfHostConfiguraiton)){
                server.OpenAsync().Wait();
                Console.WriteLine("Now Hosting at http://localhost:8080/api/{controller}");
                Console.ReadLine();
            }
        }
    }
}