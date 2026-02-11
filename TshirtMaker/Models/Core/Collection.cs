using System.ComponentModel.DataAnnotations;

namespace TshirtMaker.Models.Core
{
    public class Collection : BaseEntity
    {

        public Guid UserId { get; set; }
        [Required]
        public Guid CopiedDesignId { get; set; }
        [Required]
        public Design? CopiedDesign { get; set; }

        public User? User { get; set; }

    }
}
