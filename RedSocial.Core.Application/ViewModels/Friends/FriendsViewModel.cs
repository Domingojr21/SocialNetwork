

using RedSocial.Core.Application.ViewModels.Publications;

namespace RedSocial.Core.Application.ViewModels.Friends
{
    public class FriendsViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string FriendId { get; set; }

        public string UserName { get; set; }

        public string FriendName { get; set; }
        
        public string FriendLastName { get; set;}

        public string FriendPhoto {get; set;}

        public List<PublicationsViewModel> FriendPublications { get; set; }
    }
   }

