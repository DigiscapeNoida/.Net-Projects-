using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc2OpenXml
{
    internal class ImgProcess
    {
        public void ExtractImage(string docpath, string imgpath)
        {
            WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(docpath, true);
            Body body = wordprocessingDocument.MainDocumentPart.Document.Body;
            int count = 0;
            string para1 = "";
            foreach (Paragraph par in body.Descendants<Paragraph>())
            {
                para1 = par.InnerXml.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "");
                foreach (Run run in par.Descendants<Run>())
                {
                    try
                    {
                        DocumentFormat.OpenXml.Wordprocessing.Drawing image =
                        run.Descendants<DocumentFormat.OpenXml.Wordprocessing.Drawing>().FirstOrDefault();
                        if (image != null)
                        {
                            count = count + 1;
                            var imageFirst = image.Inline.Graphic.GraphicData.Descendants<DocumentFormat.OpenXml.Drawing.Pictures.Picture>().FirstOrDefault();
                            var blip = imageFirst.BlipFill.Blip.Embed.Value;
                            ImagePart img = (ImagePart)wordprocessingDocument.MainDocumentPart.Document.MainDocumentPart.GetPartById(blip);
                            string imageFileName = string.Empty;
                            string cnt = count.ToString();
                            using (System.Drawing.Image toSaveImage = Bitmap.FromStream(img.GetStream()))
                            {
                                if (cnt.Length == 1)
                                {
                                    cnt = "00" + cnt;
                                }
                                if (cnt.Length == 2)
                                {
                                    cnt = "0" + cnt;
                                }
                                imageFileName = imgpath + "\\" + Path.GetFileNameWithoutExtension(docpath) + "_" + cnt + ".tif";
                                try
                                {
                                    toSaveImage.Save(imageFileName, ImageFormat.Tiff);
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            para1 = para1.Replace(image.OuterXml.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", ""), "<w:t>||" + Path.GetFileNameWithoutExtension(imageFileName) + "||</w:t>");
                            //Dimension(image.InnerXml, "Fig_" + count, imgpath);
                        }
                    }
                    catch
                    {

                    }
                }
                par.InnerXml = para1;
            }
            wordprocessingDocument.Close();
        }
    }
}
