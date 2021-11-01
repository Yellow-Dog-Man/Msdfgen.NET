using BaseX;
using CodeX;
using System;
using System.IO;
using System.Security.Cryptography;
using Xunit;

namespace Msdfgen.NET.Tests
{
  public class ImageTest
  {
    public class ImageSource
    {
      public byte[] Image { get; }

      public ImageSource(byte[] image)
      {
        Image = image;
      }

      public override string ToString()
      {
        return $"hash: {BitConverter.ToString(SHA256.HashData(Image)).Replace("-", "")}";
      }
    }

    public static object[][] ProblemCharacters = new object[][]{
      new object[] { "neos_core.otf", "jp-u", 'う', new ImageSource(Resources.neos_core_otf_jp_u) },
      new object[] { "wizzta.ttf", "u", 'u', new ImageSource(Resources.wizzta_ttf_u) },
     };

    [Theory]
    [MemberData(nameof(ProblemCharacters))]
    public void TestKnownFunkyCharacters(string fontPath, string descriptive, char symbol, ImageSource expectedImage)
    {
      var font = FontX.Load(fontPath);
      var id = font.CharToGlyphId(symbol);
      var bitmap = new Bitmap2D(128, 128, TextureFormat.RGBA32, false);
      font.RenderGlyphMSDF(id, bitmap, new Rect(0, 0, 128, 128), 2, false);

      var imagePath = $"{fontPath}-{descriptive}.png";
      bitmap.Save(imagePath, 85);
      byte[] resultData = File.ReadAllBytes(imagePath);

      // If this fails, checkc out -result to see how that looks.
      Assert.Equal(expectedImage.Image, resultData);
    }
  }
}
