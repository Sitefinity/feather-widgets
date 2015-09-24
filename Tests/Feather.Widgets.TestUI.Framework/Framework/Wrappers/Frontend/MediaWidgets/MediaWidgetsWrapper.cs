﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    /// <summary>
    /// This is the entry point class for MediaWidgetsWrapper on the frontend.
    /// </summary>
    public class MediaWidgetsWrapper : BaseWrapper
    {
        /// <summary>
        /// Verifies image on the frontend.
        /// </summary>
        /// <param name="title">The image title.</param>
        /// <param name="altText">The image alt text.</param>
        /// <param name="src">The image src.</param>
        public void VerifyImage(string title, string altText, string src)
        {
            HtmlImage image = ActiveBrowser.Find.ByExpression<HtmlImage>("title=" + title, "alt=" + altText)
                .AssertIsPresent("image");

            Assert.IsTrue(image.Src.StartsWith(src), "src is not correct");
        }

        /// <summary>
        /// Verifies the video.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void VerifyVideo(string src, int width = 0, int height = 0)
        {
            HtmlVideo video = ActiveBrowser.Find.ByExpression<HtmlVideo>("src=~" + src)
                .AssertIsPresent("video");
            if (width != 0 && height != 0)
            {
                Assert.IsTrue(video.Width.Equals(width), "width is not correct");
                Assert.IsTrue(video.Height.Equals(height), "height is not correct");
            }
        }

        /// <summary>
        /// Verifies the video CSS class.
        /// </summary>
        /// <param name="cssClass">The CSS class.</param>
        /// <param name="src">The SRC.</param>
        public void VerifyVideoCssClass(string cssClass, string src)
        {
            HtmlDiv documentDiv = this.ActiveBrowser.Find.ByExpression<HtmlDiv>("class=" + cssClass);
            documentDiv.AssertIsNotNull("div is not found");

            documentDiv.Find.ByExpression<HtmlVideo>("src=~" + src).AssertIsPresent("video");
        }

        /// <summary>
        /// Verifies the document.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="href">The href.</param>
        public void VerifyDocument(string title, string href)
        {
            HtmlAnchor doc = ActiveBrowser.Find.ByExpression<HtmlAnchor>("title=" + title)
                .AssertIsPresent("document");

            Assert.IsTrue(doc.HRef.StartsWith(href), "href is not correct");
        }

        /// <summary>
        /// Verifies the document.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="href">The href.</param>
        public void VerifyDocumentFromWidget(string title, string href)
        {
            HtmlAnchor doc = ActiveBrowser.Find.ByExpression<HtmlAnchor>("innertext=" + title)
                .AssertIsPresent("document");

            Assert.IsTrue(doc.HRef.StartsWith(href), "href is not correct");
        }

        /// <summary>
        /// Verify css class of div element containing document
        /// </summary>
        /// <param name="cssClass">css class</param>
        /// <param name="title">The title.</param>
        public void VerifyDocumentCssClass(string cssClass, string title)
        {
            HtmlDiv documentDiv = this.ActiveBrowser.Find.ByExpression<HtmlDiv>("class=" + cssClass);
            documentDiv.AssertIsNotNull("div is not found");

            documentDiv.Find.ByExpression<HtmlAnchor>("innertext=" + title).AssertIsNotNull("document is not found");
        }

        /// <summary>
        /// Gets the image source.
        /// </summary>
        /// <param name="isBaseUrlIncluded">The is base URL included.</param>
        /// <param name="libraryUrl">The library URL.</param>
        /// <param name="imageType">Type of the image.</param>
        /// <param name="baseUrl">The base URL.</param>
        /// <returns></returns>
        public string GetMediaSource(bool isBaseUrlIncluded, string libraryUrl, string imageUrl, string baseUrl, string contentType = "images", string providerUrl = "default-source", string culture = null)
        {
            if (culture != null)
            {
                if (isBaseUrlIncluded)
                {
                    return baseUrl + culture.ToLower() + "/" + contentType + "/" + providerUrl + "/" + libraryUrl + "/" + imageUrl;
                }
                else
                {
                    return "/" + contentType + "/" + providerUrl + "/" + libraryUrl + "/" + imageUrl;
                }
            }
            else 
            {
                if (isBaseUrlIncluded)
                {
                    return baseUrl + contentType + "/" + providerUrl + "/" + libraryUrl + "/" + imageUrl;
                }
                else
                {
                    return "/" + contentType + "/" + providerUrl + "/" + libraryUrl + "/" + imageUrl;
                }
            }
        }

        /// <summary>
        /// Gets the download source.
        /// </summary>
        /// <param name="isBaseUrlIncluded">The is base URL included.</param>
        /// <param name="libraryUrl">The library URL.</param>
        /// <param name="imageType">Type of the image.</param>
        /// <param name="baseUrl">The base URL.</param>
        /// <returns></returns>
        public string GetDownloadButtonSource(bool isBaseUrlIncluded, string libraryUrl, string imageUrl, string baseUrl, string contentType = "images", string providerUrl = "default-source")
        {
            if (isBaseUrlIncluded)
            {
                return baseUrl + contentType + "/" + providerUrl + "/" + libraryUrl + "/" + imageUrl;
            }
            else
            {
                return "/" + contentType + "/" + providerUrl + "/" + libraryUrl + "/" + imageUrl;
            }
        }

        /// <summary>
        /// Verifies the style attribute value.
        /// </summary>
        /// <param name="style">Expected style attribute value.</param>
        /// <param name="title">Image title</param>
        /// <param name="altText">Image alternative text</param>
        public void VerifyImageStyle(string style, string title, string altText)
        {
            ActiveBrowser.Find.ByExpression<HtmlImage>("title=" + title, "alt=" + altText, "style=" + style).AssertIsPresent("image");
        }

        /// <summary>
        /// Verifies the thumbnail string in source attribute.
        /// </summary>
        /// <param name="thumbnail">Thumbnail string in source attribute</param>
        /// <param name="title">Image title</param>
        /// <param name="altText">Image alternative text</param>
        public void VerifyImageThumbnail(string thumbnail, string title, string altText)
        {
            HtmlImage image = ActiveBrowser.Find.ByExpression<HtmlImage>("title=" + title, "alt=" + altText)
                           .AssertIsPresent("image");

            Assert.IsTrue(image.Src.Contains(thumbnail), "src does not contain thumbnail substring");
        }
    }
}
