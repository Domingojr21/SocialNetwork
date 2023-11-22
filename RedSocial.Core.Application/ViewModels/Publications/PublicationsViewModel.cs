
using RedSocial.Core.Application.ViewModels.Comments;

namespace RedSocial.Core.Application.ViewModels.Publications
{
   public class PublicationsViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string? Photo { get; set; }

        public string FriendPhoto { get; set; }

        public string FriendUsername { get; set; }

        public DateTime Date { get; set; }

        public string? Comment { get; set; }

        public string UserId { get; set; }

        public List<CommentsViewModel>? Comments { get; set; }
        
    }
}
