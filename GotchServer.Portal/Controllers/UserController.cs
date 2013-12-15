
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GoDutchServer.Model;

namespace GotchServer.Portal.Controllers
{
    public class UserController : ApiController
    {
        public List<MyUser> user = new List<MyUser>()
                                       {
                                           new MyUser() {ID = 1, UserName = "张三"},
                                           new MyUser() {ID = 2, UserName = "李四"}
                                       };

        public string Get()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            //return Newtonsoft.Json.JsonConvert.;
            return json;
        }
    }
}
