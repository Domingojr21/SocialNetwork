

using Microsoft.AspNetCore.Http;
using RedSocial.Core.Application.ViewModels.Comments;
using RedSocial.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RedSocial.Core.Application.ViewModels.Publications
{
    public class SavePublicationsViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar algo en la publicacion")]     
        public string Content { get; set; }

        public string? Photo { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public List<CommentsViewModel>? Comments { get; set; }

     
       

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }

    }
}
