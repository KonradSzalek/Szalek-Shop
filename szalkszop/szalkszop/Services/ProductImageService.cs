using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web;

namespace szalkszop.Services
{
	public class ProductImageService : IProductImageService
	{
		public List<Image> ResizeImages(IEnumerable<HttpPostedFileBase> files, int biggerDimension, int smallerDimension)
		{
			List<Image> resizedImages = new List<Image>();
			int biggerValue;
			int smallerValue;
			bool verticalImage = false;

			foreach (HttpPostedFileBase file in files)
			{
				var image = Image.FromStream(file.InputStream, true, true);

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

					resizedImages.Add(resizedImage);
				}

			}
			return resizedImages;
		}

		public List<Image> CropImage(IEnumerable<HttpPostedFileBase> files, int width, int height)
		{
			List<Image> cropedImages = new List<Image>();
			var resizedImages = ResizeImages(files, 530, 300);

			foreach (var image in resizedImages)
			{
				int x = (int)(image.Width / 2 - 0.5 * width);
				int y = (int)(image.Height / 2 - 0.5 * height);

				Rectangle fromRectangle = new Rectangle(x, y, width, height);

				Bitmap target = new Bitmap(fromRectangle.Width, fromRectangle.Height);
				using (Graphics g = Graphics.FromImage(target))
				{
					Rectangle croppedImageDimentions = new Rectangle(0, 0, target.Width, target.Height);
					g.DrawImage(image, croppedImageDimentions, fromRectangle, GraphicsUnit.Pixel);
				}
				cropedImages.Add(target);

			}
			return cropedImages;
		}
	}
}