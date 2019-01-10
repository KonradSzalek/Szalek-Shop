﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace szalkszop.Services
{
	public class CustomValidations
	{
		public class MaximumImageSizeAttribute : ValidationAttribute
		{
			private readonly double _fileSize;

			public MaximumImageSizeAttribute(double maxfileSizeInBytes)
			{
				_fileSize = maxfileSizeInBytes;
			}

			protected override ValidationResult IsValid(object value, ValidationContext validationContext)
			{
				var files = value as IEnumerable<HttpPostedFileBase>;

				foreach (HttpPostedFileBase file in files)
				{
					if (file == null) return ValidationResult.Success;
					if (file.ContentLength > _fileSize)
					{
						var errorMessage = $"Maximum single file size is {_fileSize / 1000 / 1000} MB";
						return new ValidationResult(errorMessage);
					}
				}

				return ValidationResult.Success;	
			}
		}

		public class ImageFormatAttribute : ValidationAttribute
		{
			public string[] _supportedFormats { get; set; }

			public ImageFormatAttribute(params string[] supportedFormats)
			{
				_supportedFormats = supportedFormats;
			}

			protected override ValidationResult IsValid(object value, ValidationContext validationContext)
			{
				var files = value as IEnumerable<HttpPostedFileBase>;

				foreach (HttpPostedFileBase file in files)
				{
					if (file == null) return ValidationResult.Success;
					string ext = System.IO.Path.GetExtension(file.FileName);

					if (!_supportedFormats.Contains(ext.ToLower()))
					{
						var errorMessage = "Image format is not supported";
						return new ValidationResult(errorMessage);
					}
				}
				return ValidationResult.Success;
			}
		}

		public class MaximumImageFormatAttribute : ValidationAttribute
		{
			private readonly int _biggerDimension;
			private readonly int _smallerDimension;

			public MaximumImageFormatAttribute(int biggerDimension, int smallerDimension)
			{
				_biggerDimension = biggerDimension;
				_smallerDimension = smallerDimension;
			}

			protected override ValidationResult IsValid(object value, ValidationContext validationContext)
			{
				var files = value as IEnumerable<HttpPostedFileBase>;

				foreach (var file in files)
				{
					if (file == null) return ValidationResult.Success;
					try
					{
						int biggerValue;
						int smallerValue;
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
						}

						if (biggerValue >= _biggerDimension || smallerValue >= _smallerDimension)
						{

							var errorMessage = $"Maximum image format is {_biggerDimension} x {_smallerDimension}";
							return new ValidationResult(errorMessage);
						}
					}

					catch (ArgumentException)
					{
						return new ValidationResult("Image format is not supported");
					}		
				}
				return ValidationResult.Success;
			}
		}

		public class MaximumAmountOfFiles : ValidationAttribute
		{
			private readonly int _maxAmountOfFiles;

			public MaximumAmountOfFiles(int maxAmountOfFiles)
			{
				_maxAmountOfFiles = maxAmountOfFiles;
			}

			protected override ValidationResult IsValid(object value, ValidationContext validationContext)
			{
				var files = value as IEnumerable<HttpPostedFileBase>;

				if (files.Count() > _maxAmountOfFiles)
				{
					var errorMessage = $"Maximum amount of images per product is {_maxAmountOfFiles}";
					return new ValidationResult(errorMessage);
				}

				return ValidationResult.Success;
			}
		}
	}
}
