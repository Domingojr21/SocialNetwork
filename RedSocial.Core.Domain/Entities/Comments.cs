
using RedSocial.Core.Domain.Common;

namespace RedSocial.Core.Domain.Entities
{
    public class Comments : CommonProperties
    {
       public int PublicationsId { get; set; }

       public string Content { get; set; }

        
        
        
        //Navigations properties 
        public Publications Publications { get; set; }

    }
}
