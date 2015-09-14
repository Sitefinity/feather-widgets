using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models
{
    /// <summary>
    /// Enumerates possible outcomes after form submission.
    /// </summary>
    public enum SubmitStatus
    {
        /// <summary>
        /// Successful submit.
        /// </summary>
        Success,

        /// <summary>
        /// Unsuccessful submit due to invalid entry.
        /// </summary>
        InvalidEntry,

        /// <summary>
        /// Unsuccessful submit due to violation of form submit restrictions.
        /// </summary>
        RestrictionViolation
    }
}
