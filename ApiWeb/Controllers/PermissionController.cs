using ApiWeb.DbContexts;
using ApiWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly ILogger<PermissionController> _logger;
        private readonly PermissionDbContext _context;

        public PermissionController(ILogger<PermissionController> logger, PermissionDbContext context)
        {
            _logger = logger;
            _context = context;

        }

        [HttpGet(Name = "list")]
        public IEnumerable<Permission> Get()
        {
            List<Permission> permissions = _context.GetPermissions();
            //buscar todos los permisos
            return permissions;
        }
        //solicitar permiso
        [HttpGet("{id}")]
        public Permission FindById(int id)
        {
           var permission = _context.Find(id);
            //buscar un permiso por el id
            return permission;
        }

        [HttpGet("types/list")]
        public IEnumerable<PermissionType> GetPermissionTypes()
        {
            List<PermissionType> permissions = _context.GetPermissionTypes();
            //buscar todos los permisos
            return permissions;
        }

        [HttpPost]
        public Permission Insert([FromBody] Permission model)
        {
            model.DatePermission = DateTime.Now;
            if (model.PermissionType.IdPermissionType != 0)
            {
                var existemPermission = _context.PermissionType.Where(x => x.IdPermissionType == model.PermissionType.IdPermissionType).Single();
                if(existemPermission != null)
                    model.PermissionType = existemPermission;
            }
            _context.Add(model);
            _context.SaveChanges();
            //logica ir a la base de datos, consultar si existe el permiso, si existe actualizamos, si no lanzamos un not found 404 
            return model;
        }
    }
}