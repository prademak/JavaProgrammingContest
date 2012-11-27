using System;
using System.Net.Http;
using System.Web.Http;

namespace JavaProgrammingContest.Web.API{
    public class BuildController : ApiController{
        public BuildController() {}

        public HttpResponseMessage Post(BuildJob buildJob){
            throw new NotImplementedException();
        }
    }

    public class BuildJob{
        public string Code { get; set; }
    }
}