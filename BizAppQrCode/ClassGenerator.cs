using QRCoder;
using System;
using System.Drawing;
using System.IO;

namespace BizAppQrCode
{
	public class ClassGenerator
	{
		public byte[] Generate(string txtQRCode)
		{
			QRCodeGenerator _qrCode = new QRCodeGenerator();
			QRCodeData _qrCodeData = _qrCode.CreateQrCode(txtQRCode, QRCodeGenerator.ECCLevel.Q);
			QRCode qrCode = new QRCode(_qrCodeData);
			Bitmap qrCodeImage = qrCode.GetGraphic(20);
			return BitmapToBytesCode(qrCodeImage);
		}
		private static Byte[] BitmapToBytesCode(Bitmap image)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
				return stream.ToArray();
			}
		}
	}
}
