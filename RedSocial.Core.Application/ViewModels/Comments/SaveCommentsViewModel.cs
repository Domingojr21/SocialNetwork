
using System.ComponentModel.DataAnnotations;

namespace RedSocial.Core.Application.ViewModels.Comments
{
    public class SaveCommentsViewModel

    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int PublicationsId { get; set; }

        public string UserId {get; set; } 
    }
}
