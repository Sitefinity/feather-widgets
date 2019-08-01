using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Mvc.Proxy;

namespace Telerik.Sitefinity.Frontend.Forms
{
    /// <summary>
    /// Defines methods for decorating forms markup in MVC
    /// </summary>
    internal interface IFormRulesDecorator
    {
        /// <summary>Sets the form.</summary>
        /// <param name="form">The form.</param>
        void SetForm(FormDescription form);

        /// <summary>
        /// Wraps the form markup.
        /// </summary>
        /// <param name="formMarkup">The form markup.</param>
        /// <returns>Razor syntax markup</returns>
        string WrapFormMarkup(string formMarkup);

        /// <summary>
        /// Gets the markup to be inserted inside the form.
        /// </summary>
        /// <returns>Razor syntax markup</returns>
        string GetInFormMarkup();

        /// <summary>
        /// Wraps the field markup.
        /// </summary>
        /// <param name="mvcControllerProxy">The field controller.</param>
        /// <param name="fieldMarkup">The field markup.</param>
        /// <returns>Razor syntax markup</returns>
        string WrapFieldMarkup(MvcControllerProxy mvcControllerProxy, string fieldMarkup);
    }
}