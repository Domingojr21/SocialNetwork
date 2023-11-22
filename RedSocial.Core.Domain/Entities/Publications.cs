
using RedSocial.Core.Domain.Common;

namespace RedSocial.Core.Domain.Entities
{
    public class Publications : CommonProperties
    {

        public string? Photo {get; set;}

        public string Content { get; set;}
      
        public DateTime Date {get; set;}

        //Navigation Properties
        public ICollection<Comments> Comments;


    }
    
}
