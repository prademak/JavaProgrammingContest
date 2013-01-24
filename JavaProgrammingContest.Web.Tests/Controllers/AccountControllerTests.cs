using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.DataAccess.TestSupport;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Web.App_Start;
using JavaProgrammingContest.Web.Controllers;
using JavaProgrammingContest.Web.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using System.Web.Mvc;
using System.Web.Routing;


namespace JavaProgrammingContest.Web.Tests.Controllers
{
    [TestFixture]
    class AccountControllerTests
    {
        private Mock<IDbContext> _contextMock;
        private AccountController _controller; 
        private LogonModel _logmod;
        private AccountController Controller { get; set; } 
        private Mock<IWebSecurity> _WebSecurity { get; set; } 

                [SetUp]
                public void Init()
                {
                        _contextMock = new Mock<IDbContext>();
                        _WebSecurity = new Mock<IWebSecurity>();
                        _logmod = new LogonModel();
                        _controller = new AccountController(_contextMock.Object, _WebSecurity.Object);

                }
                [Test]
                public void Login_ViewTest()
                {
                    var result = _controller.Logon("/Home/Index") as ViewResult;

                    Assert.IsNotNull(result);
                }
                [Test]
                public void Register_ViewTest()
                {
                    var result = _controller.Logon("/Home/Register") as ViewResult;

                    Assert.IsNotNull(result);
                }
                [Test]
                public void Login_UserCanLogin()
                {
                    string returnUrl = "/Home/Index";
                    string userName = "user";
                    string password = "password";

                    _WebSecurity.Setup(s => s.Login(userName, password, false)).Returns(true);
                    SetupControllerForTests(_controller);
                    var model = new LogonModel
                    {
                        UserName = userName,
                        Password = password
                    };

                    var result = _controller.Logon(model, returnUrl) as RedirectResult;
                    Assert.NotNull(result);
                    Assert.AreEqual(returnUrl, result.Url);
                }


                [Test]
                public void Register_UserCanCreateAAccount()
                {
                    string userName = "user";
                    string password = "password";

                    _WebSecurity.Setup(s => s.Login(userName, password, false)).Returns(true);
                    SetupControllerForTests(_controller);
                    var model = new RegisterModel
                    {   Name = "naam",
                        UserName = userName,
                        Password = password,
                        ConfirmPassword = password
                    };

                    var result = _controller.Register(model) as RedirectToRouteResult;
                    Assert.AreEqual("Editor", result.RouteValues["controller"]);
                    Assert.AreEqual("Index", result.RouteValues["action"]);
                }
         

                private static void SetupControllerForTests(Controller controller)
                {
                    var config = new HttpConfiguration();
                    var Routes = new RouteCollection();
                    RouteConfig.RegisterRoutes(Routes);

                    var Request = new Mock<HttpRequestBase>(MockBehavior.Strict);
                    Request.SetupGet(x => x.ApplicationPath).Returns("/");
                    Request.SetupGet(x => x.Url).Returns(new Uri("http://localhost/account", UriKind.Absolute));
                    Request.SetupGet(x => x.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection());

                    var Response = new Mock<HttpResponseBase>(MockBehavior.Strict);

                    var Context = new Mock<HttpContextBase>(MockBehavior.Strict);
                    Context.SetupGet(x => x.Request).Returns(Request.Object);
                    Context.SetupGet(x => x.Response).Returns(Response.Object);
                    controller.Url = new System.Web.Mvc.UrlHelper(new RequestContext(Context.Object, new RouteData()), Routes);


                           }
              
             

             
         
    }
}
