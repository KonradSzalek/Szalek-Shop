using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web;

namespace szalkszop.Services
{
	public class ProductImageService : IProductImageService
	{
		public List<Image> ThumbnailImages(List<Image> images)
		{
			List<Image> thumbnailImages = new List<Image>();

			foreach (Image image in images)
			{
				var img = image;

				double imgHeight = img.Size.Height;
				double imgWidth = img.Size.Width;

				double x = imgHeight / 124;
				int newWidth = (int)(imgWidth / x);
				int newHeight = (int)(imgHeight / x);

				Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
				Image myThumbnail = img.GetThumbnailImage(newWidth, newHeight, myCallback, IntPtr.Zero);

				thumbnailImages.Add(myThumbnail);
			}

			return thumbnailImages;
		}

		public bool ThumbnailCallback()
		{
			return false;
		}

		public List<Image> ResizeImages(List<Image> images, int biggerDimension, int smallerDimension)
		{
			List<Image> resizedImages = new List<Image>();
			int biggerValue;
			int smallerValue;
			bool verticalImage = false;

			foreach (Image image in images)
			{
				if (image.Width >= image.Height)
				{
					biggerValue = image.Width;
					smallerValue = image.Height;
				}

				else
				{
					biggerValue = image.Height;
					smallerValue = image.Width;
					verticalImage = true;
				}

				if (biggerValue <= biggerDimension && smallerValue <= smallerDimension)
				{
					resizedImages.Add(image);
					continue;
				}

				using (image)
				{
					int newHeight;
					int newWidth;
					double ratio = (double)biggerValue / biggerDimension;

					if (verticalImage)
					{
						newHeight = biggerDimension;
						newWidth = (int)(smallerValue / ratio);
					}
					else
					{
						newWidth = biggerDimension;
						newHeight = (int)(smallerValue / ratio);
					}

					Bitmap resizedImage = new Bitmap(newWidth, newHeight);

					using (var graphic = Graphics.FromImage(resizedImage))
					{
						graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
						graphic.SmoothingMode = SmoothingMode.HighQuality;
						graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
						graphic.CompositingQuality = CompositingQuality.HighQuality;
						graphic.DrawImage(image, 0, 0, newWidth, newHeight);
					}
					resizedImages.Add(resizedImage);
				}
			}
			return resizedImages;
		}

		public List<Image> GetFromFiles(IEnumerable<HttpPostedFileBase> files)
		{
			List<Image> images = new List<Image>();
			foreach (HttpPostedFileBase file in files)
			{
				var image = Image.FromStream(file.InputStream, true, true);
				images.Add(image);
			}
			
			return images;
		}
	}
}