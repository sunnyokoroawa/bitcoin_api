using Bitcoin.Core.Models;
using QRCoder;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

        public Response<string> GenerateQRCodeAsync(string address, string qrLogo)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                    QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(address, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qRCodeData);

                    // logo path
                    var logopath = Path.Combine(
                               Directory.GetCurrentDirectory(), $"{"Docs/QR"}", qrLogo);

                    Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Orange, Color.Black, (Bitmap)Bitmap.FromFile(logopath));

                    using (Bitmap bitmap = qrCode.GetGraphic(20))
                    {
                        Log.Information("About to save file stream");

                        bitmap.Save(memoryStream, ImageFormat.Png);

                        Log.Information("Saved file stream");

                        return new Response<string>
                        {
                            Success = true,
                            Message = "Request Successful",
                            Data = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray())
                        };
                    }
                }
            }

            catch (Exception ex)
            {
                Log.Information("Error saving file stream");

                Log.Error(ex, $"QRCode generation {ex.Message}");

                return new Response<string>
                {
                    Message = "Error generating QR Code",
                };
            }
        }
    }
}
