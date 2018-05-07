using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSupply.Web.Models
{
    public class ErrorMessageViewModel
    {
        public ErrorMessageViewModel() { }

        public ErrorMessageViewModel(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
