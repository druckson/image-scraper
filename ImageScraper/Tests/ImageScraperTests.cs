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
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;

namespace ImageScraper
{
	[TestFixture]
	public class ImageScraperTests
	{
		[Test]
		public void DownloadsCorrectURL() {
			var client = new Mock<IFileDownloader>();
			var uri = new Uri ("http://test.com/");
			var scraper = new ImageScraper(client.Object);
			var results = scraper.Scrape (uri).ToList ();
			client.Verify (x => x.DownloadString(uri), Times.Once);
		}

		[Test]
		public void FindsImageTag() {
			var client = new Mock<IFileDownloader> ();
			var uri = new Uri ("http://test.com/");
			client.Setup (x => x.DownloadString (uri)).Returns ("<html><img src=\"/test\"></img></html>");
			var scraper = new ImageScraper(client.Object);
			var results = scraper.Scrape (uri).ToArray ();
			Assert.AreEqual (results[0].AbsolutePath, "/test");
		}

		[Test]
		public void FindsMultipleImageTags() {
			var client = new Mock<IFileDownloader> ();
			var uri = new Uri ("http://test.com/");
			client.Setup (x => x.DownloadString (uri)).Returns ("<html><img src=\"/test1\"></img><img src=\"/test2\"></img></html>");
			var scraper = new ImageScraper(client.Object);
			var results = scraper.Scrape (uri).ToArray ();
			Assert.AreEqual (results[0].AbsolutePath, "/test1");
			Assert.AreEqual (results[1].AbsolutePath, "/test2");
		}
	}
}