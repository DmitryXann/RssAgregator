//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RssAgregator.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserFeedback
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Header { get; set; }
        public string Email { get; set; }
    
        public virtual User User { get; set; }
    }
}
