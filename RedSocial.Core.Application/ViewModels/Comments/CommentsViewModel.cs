
namespace RedSocial.Core.Application.ViewModels.Comments
{
    public class CommentsViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int PublicationsId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string PhotoUrl { get; set; }    
    }
}
