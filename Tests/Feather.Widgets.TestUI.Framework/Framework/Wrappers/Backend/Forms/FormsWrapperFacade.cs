﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Forms
{
    /// <summary>
    /// This is the entry point class for forms backend wrappers.
    /// </summary>
    public class FormsWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the FormsWrapper 
        /// </summary>
        /// <returns>Returns the FormsWrapper</returns>
        public FormsWrapper FormsWrapper()
        {
            return new FormsWrapper();
        }

        /// <summary>
        /// Provides unified access to the FormsContentScreenWrapper 
        /// </summary>
        /// <returns>Returns the FormsContentScreenWrapper</returns>
        public FormsContentScreenWrapper FormsContentScreenWrapper()
        {
            return new FormsContentScreenWrapper();
        }

        /// <summary>
        /// Provides unified access to the FormResponseWrapper 
        /// </summary>
        /// <returns>Returns the FormResponseWrapper</returns>
        public FormResponseWrapper FormResponseWrapper()
        {
            return new FormResponseWrapper();
        }
    }
}
