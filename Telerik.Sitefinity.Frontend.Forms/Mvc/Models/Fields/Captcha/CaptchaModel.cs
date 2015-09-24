﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Text;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Web;
using System.Web;
using Telerik.Sitefinity.Services.Comments.DTO;
using System.Reflection;
using Telerik.Sitefinity.Services.Comments;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.Captcha
{
    /// <summary>
    /// Implements API for working with form Captcha fields.
    /// </summary>
    public class CaptchaModel : FormElementModel, ICaptchaModel
    {
        /// <inheritDocs/>
        public bool DisplayOnlyForUnauthenticatedUsers
        {
            get;
            set;
        }

        /// <inheritDocs/>
        public override object Value
        {
            get
            {
                if (base.Value == null)
                    base.Value = System.Web.HttpContext.Current.Request[CaptchaModel.CaptchaAnswerFormKey];

                return base.Value;
            }
            set
            {
                base.Value = value;
            }
        }

        /// <inheritDocs/>
        public override object GetViewModel(object value)
        {
            return new CaptchaViewModel()
            {
                GenerateUrl = RouteHelper.ResolveUrl(CaptchaModel.CaptchaGetService, UrlResolveOptions.Rooted),
                CaptchaAnswerFormKey = CaptchaModel.CaptchaAnswerFormKey,
                CaptchaCorrectAnswerFormKey = CaptchaModel.CaptchaCorrectAnswerFormKey,
                CaptchaInitializationVectorFormKey = CaptchaModel.CaptchaInitializationVectorFormKey,
                CaptchaKeyFormKey = CaptchaModel.CaptchaKeyFormKey,
                CssClass = this.CssClass
            };
        }

        /// <inheritDocs/>
        public bool ShouldRenderCaptcha()
        {
            var isVisible = !(SystemManager.CurrentHttpContext.User != null &&
                    SystemManager.CurrentHttpContext.User.Identity != null &&
                    SystemManager.CurrentHttpContext.User.Identity.IsAuthenticated &&
                    this.DisplayOnlyForUnauthenticatedUsers &&
                    !SystemManager.IsDesignMode);

            return isVisible;
        }

        /// <inheritDocs/>
        public override bool IsValid(object value)
        {
            if (!this.ShouldRenderCaptcha())
                return true;

            var strValue = value as string;
            if (string.IsNullOrEmpty(strValue))
                return false;

            if (HttpContext.Current == null || HttpContext.Current.Request == null)
                return false;

            var correctAnswer = HttpContext.Current.Request[CaptchaModel.CaptchaCorrectAnswerFormKey];
            var initializationVector = HttpContext.Current.Request[CaptchaModel.CaptchaInitializationVectorFormKey];
            var key = HttpContext.Current.Request[CaptchaModel.CaptchaKeyFormKey];

            if (string.IsNullOrEmpty(correctAnswer) || string.IsNullOrEmpty(initializationVector) || string.IsNullOrEmpty(key))
                return false;

            var isValid = this.ValidateCaptcha(strValue, correctAnswer, initializationVector, key);

            return isValid;
        }

        private bool ValidateCaptcha(string answer, string correctAnswer, string initializationVector, string key)
        {
            var commentWebServiceType = Type.GetType("Telerik.Sitefinity.Services.Comments.CommentWebService, Telerik.Sitefinity");
            var commentWebServiceInstance = Activator.CreateInstance(commentWebServiceType);
            var validateMethodInfo = commentWebServiceType.GetMethod("Validate", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(CaptchaInfo) }, null);

            var captchaInfo = new CaptchaInfo() { Answer = answer, CorrectAnswer = correctAnswer, InitializationVector = initializationVector, Key = key };
            var result = validateMethodInfo.Invoke(commentWebServiceInstance, new object[] { captchaInfo }) as bool?;

            return result.HasValue && result.Value;
        }

        private const string CaptchaGetService = "RestApi/comments-api/";

        private const string CaptchaAnswerFormKey = "captcha-a";
        private const string CaptchaCorrectAnswerFormKey = "captcha-ca";
        private const string CaptchaInitializationVectorFormKey = "captcha-iv";
        private const string CaptchaKeyFormKey = "captcha-k";
    }
}
