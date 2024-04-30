using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nitrilon.DataAccess;
using Nitrilon.Entities;
using System.Linq;
using System.Runtime.InteropServices;

namespace Nitilon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : Controller
    {
        [HttpPost]
        public IActionResult AddMember(Member newMember)
        {
            Repository r = new Repository();
            r.AddMember(newMember);
            return Ok();
        }

        [HttpGet("all")]
        public IEnumerable<Member> GetAll()
        {
            Repository r = new Repository();
            var members = r.GetAllMembers();
            return members;
        }
    }
}
