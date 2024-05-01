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
            MemberRepository r = new ();
            r.AddMember(newMember);
            return Ok();
        }

        [HttpGet("all")]
        public IEnumerable<Member> GetAll()
        {
            MemberRepository r = new ();
            var members = r.GetAllMembers();
            return members;
        }

        [HttpGet("{name}")]
        public IEnumerable<Member> Get(string name)
        {
            MemberRepository r = new ();
            var m = r.GetMember(name);
            return m;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            MemberRepository r = new ();
            r.DeleteMember(id);
            return Ok();
        }
    }
}
