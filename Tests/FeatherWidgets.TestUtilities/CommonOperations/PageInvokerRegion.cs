using System;
using System.Net;
using System.Web;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.TestIntegration.Helpers;

namespace FeatherWidgets.TestUtilities.CommonOperations
{
    public class PageInvokerRegion : IDisposable
    {
        /// <summary>
        /// Creates a new PageInvokerRegion with empty values and set it as PageInvokerRegion.Current
        /// </summary>
        public PageInvokerRegion()
        {
            this.SetRegionValues(null);
            PageInvokerRegion.Current = this;
        }

        /// <summary>
        /// Creates a new PageInvokerRegion with values taken from the provided HttpContext object and set it as PageInvokerRegion.Current
        /// </summary>
        /// <param name="context">The user.</param>
        public PageInvokerRegion(HttpContext context)
        {
            this.SetRegionValues(context);
            PageInvokerRegion.Current = this;
        }

        /// <summary>
        /// Creates a new PageInvokerRegion with autorization data for the provied user and set it as PageInvokerRegion.Current
        /// </summary>
        /// <param name="user">The user.</param>
        public PageInvokerRegion(User user)
        {
            using (new AuthenticateUserRegion(user))
            {
                this.SetRegionValues(HttpContext.Current);
            }

            PageInvokerRegion.Current = this;
        }

        /// <summary>
        /// Get the current PageInvokerRegion instance if it set 
        /// </summary>
        public static PageInvokerRegion Current { get; private set; }

        /// <summary>
        /// Collects all cookies from initialization and between the requests
        /// </summary>
        public CookieCollection Cookies { get; private set; }

        /// <summary>
        /// Holds the HTTP Referer value from the last request
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string Referer { get; set; }

        /// <summary>
        /// Updates all cookies with the ones in the provided CookieCollection. Clears all cookies if the parameter is null.
        /// </summary>
        /// <param name="cookies">Collection with new cookies</param>
        public void UpdateCookies(CookieCollection cookies)
        {
            if (cookies == null)
            {
                this.Cookies = new CookieCollection();
            }
            else
            {
                this.Cookies = cookies;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PageInvokerRegion()
        {
            //// Finalizer calls Dispose(false)
            this.Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //// free managed resources here
                PageInvokerRegion.Current = null;
            }

            //// free native resources if there are any.
        }

        private void SetRegionValues(HttpContext context)
        {
            this.Cookies = new CookieCollection();

            if (context != null)
            {
                Cookie cookie;
                foreach (HttpCookie httpCookie in context.Response.Cookies)
                {
                    cookie = new Cookie();
                    cookie.Domain = httpCookie.Domain;
                    cookie.Expires = httpCookie.Expires;
                    cookie.Name = httpCookie.Name;
                    cookie.Path = httpCookie.Path;
                    cookie.Secure = httpCookie.Secure;
                    cookie.Value = httpCookie.Value;

                    this.Cookies.Add(cookie);
                }
            }
        }
    }
}
