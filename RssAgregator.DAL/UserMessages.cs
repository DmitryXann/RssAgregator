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
    
    public partial class UserMessages
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        public System.DateTime SendDateTime { get; set; }
        public bool Read { get; set; }
    
        public virtual User FromUser { get; set; }
        public virtual User ToUser { get; set; }
    }
}
