using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace convertirSvgIco;
public static class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Ingrese el nombre del archivo .svg: ");
        string svgFile = "/home/nico/Imágenes/svgs/trash-can.svg";

        if (!File.Exists(svgFile))
        {
            Console.WriteLine("El archivo no existe.");
            return;
        }

        string icoFile = Path.ChangeExtension(svgFile, "ico");
        Bitmap bitmap;

        try
        {
            bitmap = new Bitmap(svgFile);
        }
        catch (Exception ex)
        {
            Console.WriteLine("intento new bitmap:" + ex.Message);
            return;
        }

        if (bitmap.Width == 0 || bitmap.Height == 0)
        {
            Console.WriteLine("El archivo .svg es vacío.");
            return;
        }

        using (var ms = new MemoryStream())
        {
            bitmap.Save(ms, ImageFormat.Icon);
            ms.Position = 0;

            var icoData = new byte[ms.Length];
            ms.Read(icoData, 0, (int)ms.Length);

            File.WriteAllBytes(icoFile, icoData);
        }

        Console.WriteLine("El archivo .ico se ha creado correctamente.");
    }
}