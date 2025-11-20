using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApi.Controllers
{
    [Route("api/tasks/[controller]")]
    [ApiController]
    public class TaskApiController : ControllerBase
    {
    }
}
