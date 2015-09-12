using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.FileField
{
    public interface IFileFieldModel : IFormFieldModel
    {
        bool AllowMultipleFiles { get; set; }
    }
}
