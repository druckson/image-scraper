using System;
using System.Net;
using System.Collections.Generic;

namespace ImageScraper
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if (args.Length >= 2) {
				Console.WriteLine ("Downloading all images from website: ");
				Console.WriteLine (args[0]);
				Console.WriteLine ("Into directory:");
				Console.WriteLine (args[1]);

				var downloader = new FileDownloader ();
				var extensionFilter = new ExtensionFilter (
					new string[] {"jpg", "jpeg", "gif", "png", "tif", "tiff", 
					"jif", "jfif", "jp2", "jpx", "j2k", "j2c", "fpx", "gif", "raw"}
				);
				var scraper = new ImageScraper (downloader);

				// Download all images from the specified URL into the specified directory
				downloader.DownloadFiles (extensionFilter.Filter (scraper.Scrape (new Uri (args [0]))), args[1]);

				Console.WriteLine ("Done");
			}
		}
	}
}
