using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationSearch.Data.Entities
{
    public class Job : IEntity
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "The field {0} can contain up to lenght {1} characters.")]
        [Display(Name = "Job Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        public Status Status { get; set; }

        public User User { get; set; }
    }
}
