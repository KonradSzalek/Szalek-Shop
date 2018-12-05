using System.Collections.Generic;
using System.Drawing;
using System.Web;

namespace szalkszop.Services
{
	public interface IProductImageService
	{
		List<Image> ResizeImages(IEnumerable<HttpPostedFileBase> images, int maxWidth, int maxHeight);
		List<Image> CropImage(IEnumerable<HttpPostedFileBase> images, int width, int height);
	}
}
