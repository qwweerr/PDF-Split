using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Pdf;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;

namespace PDF_Split
{
    public class PDFToImages
    {
        DataTable OutputFilePath;
        public PDFToImages(string InputFilePath)
        {
            string OutPath = string.Empty;
            DataTable Paths = new DataTable();
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(InputFilePath);
            string Directory = "C:\\Users";
            DataColumn dc = new DataColumn("Image Path", typeof(string));
            Paths.Columns.Add(dc);
            int ClientIndex = 1;
            for (int i = 0; i < doc.Pages.Count; ++i)
            {
                if ((i + 1) % 2 == 0)
                {
                    OutPath = Directory + "\\" + "C" + ClientIndex + "P" + 2 + ".jpeg";
                    ++ClientIndex;
                }
                else
                {
                    OutPath = Directory + "\\" + "C" + ClientIndex + "P" + 1 + ".jpeg";
                }
                Image emf = doc.SaveAsImage(i, Spire.Pdf.Graphics.PdfImageType.Metafile);
                Image zoomImg = new Bitmap((int)(emf.Size.Width * 2), (int)(emf.Size.Height * 2));
                using (Graphics g = Graphics.FromImage(zoomImg))
                {
                    g.ScaleTransform(2.0f, 2.0f);
                    g.DrawImage(emf, new Rectangle(new Point(0, 0), emf.Size), new Rectangle(new Point(0, 0), emf.Size), GraphicsUnit.Pixel);
                    emf.Save(OutPath, ImageFormat.Jpeg);
                    DataRow dr = Paths.NewRow();
                    dr[0] = OutPath;
                }
            }
            OutputFilePath = Paths;
        }

        public DataTable ImagesPath()
        {
            return OutputFilePath;
        }
    }
}
