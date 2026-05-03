using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace LinkedinOutReach.parsers
{
    internal class JobCompanyHTMLParser
    {
        private HtmlDocument _htmlDocument;
        private string _htmlContent;
        public string? CompanyName { get; private set; }
        public string? CurrentJob { get; private set; }

        public JobCompanyHTMLParser(string htmlContent)
        {
            this._htmlDocument = new HtmlDocument();
            this._htmlContent = htmlContent;
        }

        public void updateHtmlContent(string htmlContent)
        {
            // First reset content 
            this.resetContent();

            // Then update the HTML content and reload the document
            this._htmlContent = htmlContent;
            this._htmlDocument.LoadHtml(this._htmlContent);
        }

        public void fetchContent()
        {
            string docInnerText;
            string jobCompanyInfoStr;
            string[] jobCompanyInfo;

            // If there is no job/company information, reset the content and return
            docInnerText = this._htmlDocument.DocumentNode.InnerText;
            if (!docInnerText.Contains("Current: ") || !docInnerText.Contains(" at "))
            {
                this.resetContent();
                return;
            }

            // If there is job/company information, Update the content
            this.updateContent();
        }

        private void updateContent()
        {
            string jobCompanyInfoStr = this._htmlDocument.DocumentNode
                .SelectSingleNode("//p[contains(string(), ' at ')]").InnerText;

            string[] jobCompanyInfo = jobCompanyInfoStr.Split(" at ");

            this.CurrentJob = jobCompanyInfo[0].Replace("Current: ", "").Trim();
            this.CompanyName = jobCompanyInfo[1].Trim();
        }

        private void resetContent()
        {
            this.CompanyName = null;
            this.CurrentJob = null;
        }
    }
}
