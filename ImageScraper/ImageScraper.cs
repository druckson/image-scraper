//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Net;
using System.Collections.Generic;
using CsQuery;

namespace ImageScraper
{
	public class ImageScraper
	{
		private IFileDownloader _downloader;

		// Inject dependencies
		public ImageScraper (IFileDownloader downloader)
		{
			this._downloader = downloader;
		}

		// Find any image tags and return their source URLs
		// This could be improved to scrape CSS files and other 
		// tag types to find background images, buttons, etc.
		public IEnumerable<Uri> Scrape(Uri uri)
		{
			string data = this._downloader.DownloadString(uri);
			CQ content = data;
			var elements = content ["img"].Elements;

			foreach (var element in content["img"].Elements) {
				yield return new Uri(uri, element.Attributes["src"]);
			}
		}

		// Scrape several URLs
		public void ScrapeEach(IEnumerable<Uri> uris)
		{
			foreach (var uri in uris) {
				this.Scrape (uri);
			}
		}
	}
}

