using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Learning_Platform.ServiceInterfaces;

namespace Online_Learning_Platform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LifetimeController : ControllerBase
    {
        private readonly ISingleton _singleton;
        private readonly ITransient _transient;
        private readonly IScoped _scoped;

        public LifetimeController(ISingleton singleton,
            ITransient transient,
            IScoped scoped)
        {
            _singleton = singleton;
            _transient = transient;
            _scoped = scoped;
        }

        [HttpGet("/singleton")]
        public string GetIdFromSingletom()
        {
            return $"Singleton id {_singleton.GetId()}";
        }

        [HttpGet("/transient")]
        public string GetIdFromTransient()
        {
            return $"Transient id {_transient.GetId()}";
        }

        [HttpGet("/scoped")]
        public string GetIdFromScoped()
        {
            return $"Scoped id {_scoped.GetId()}";
        }
    }
}
