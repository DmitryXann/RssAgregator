using RssAgregator.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssAgregator.DAL
{
    public partial class Template
    {
        public TemplateModel GetModel()
        {
            return new TemplateModel 
            {
                Name = Name,
                View = View,
                Type = (byte)Type
            };
        }
    }
}
