

using System.ComponentModel.DataAnnotations;

namespace RedSocial.Core.Application.ViewModels.Friends
{
    public class SaveFriendsViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required(ErrorMessage = "Debe colocar algun Usuario")]
        public string FriendId { get; set; }

        public bool HasError { get; set; } = false;
        public string? Error { get; set; }

    }
}
