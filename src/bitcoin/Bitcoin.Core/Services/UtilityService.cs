using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Bitcoin.Core.Services
{
    public class UtilityService
    {
        public UtilityService()
        {

        }

        public string ConvertToBase64String(string imagePath)
        {
            using (Image image = Image.FromFile(imagePath))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
    }
}
