using System;
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
				: base($"Maximum single file size is {maxfileSizeInBytes / 1000 / 1000} MB")
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
                        //CR5 nie lepiej tutaj dac error message, zamiast podawac ja do bazy i tutaj wyciagać ja z  validationContextu?
						var errorMessage = FormatErrorMessage((validationContext.DisplayName));
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
				: base("Image format is not supported")
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
                        //CR5 to samo tu
						var errorMessage = FormatErrorMessage((validationContext.DisplayName));
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
			: base($"Maximum image format is {biggerDimension} x {smallerDimension}")
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

                        //CR5 a co jezeli tylko np. biggerValue bedzie wieksze? Powinien to puscic?
						if (biggerValue >= _biggerDimension && smallerValue >= _smallerDimension)
						{
                            //CR5 i tu
							var errorMessage = FormatErrorMessage((validationContext.DisplayName));
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
				: base($"Maximum amount of images per product is {maxAmountOfFiles}")
			{
				_maxAmountOfFiles = maxAmountOfFiles;
			}

			protected override ValidationResult IsValid(object value, ValidationContext validationContext)
			{
				var files = value as IEnumerable<HttpPostedFileBase>;

                //CR5 to sie nigdy nie stanie bo masz przeciez jeden textbox do dodawania zdjec
				if (files.Count() > _maxAmountOfFiles)
				{
                    //cr5 i tu
					var errorMessage = FormatErrorMessage((validationContext.DisplayName));
					return new ValidationResult(errorMessage);
				}

				return ValidationResult.Success;
			}
		}
	}
}
