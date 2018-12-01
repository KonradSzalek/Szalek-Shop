using System.Collections.Generic;
using System.Drawing;
using System.Web;

namespace szalkszop.Services
{
	public interface IProductImageService
	{
		List<Image> ResizeImages(List<Image> images, int maxWidth, int maxHeight);
		List<Image> GetFromFiles(IEnumerable<HttpPostedFileBase> files);
		List<Image> ThumbnailImages(List<Image> images);
	}
}
