using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWeb.Models
{
    public class Permission
    {
        [Key]
        public int IdPermission { get; set; }
        public string PersonName { get; set; }
        public string LastName { get; set; }
        public PermissionType PermissionType { get; set; }
        public DateTime? DatePermission { get; set; }
    }

    public class PermissionType
    {

        [Key]
        [ForeignKey("Permission")]
        public int IdPermissionType { get; set; }
        public string DescriptionPermission { get; set; }

    }
}
