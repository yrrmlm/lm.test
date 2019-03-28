using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lm.tool
{
    public class PdfHelper
    {
        private readonly static Lazy<PdfHelper> LInstance = new Lazy<PdfHelper>(() => { return new PdfHelper(); });

        public static PdfHelper Instance
        {
            get
            {
                return LInstance.Value;
            }
        }

        public void Create(string filePath)
        {
            var document = new Document(PageSize.A4);
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);
                    document.Open();
                    PdfPTable table = new PdfPTable(1);                
                    table.AddCell(LineCell());
                    var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.png");
                    table.AddCell(ImageCell(imagePath, 200));
                    var content = "testsateaesta";
                    table.AddCell(ParagraphCell(content));
                    document.Add(table);
                    document.Close();
                }
            }
            catch (DocumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 生成一条线
        /// </summary>
        /// <returns></returns>
        private PdfPCell LineCell()
        {
            var lineCell = new PdfPCell();
            lineCell.BorderWidth = 0f;
            lineCell.BorderWidthBottom = 0.2f;
            return lineCell;
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        private PdfPCell ImageCell(string imagePath,float size)
        {
            var imageCell = new PdfPCell();
            var image = Image.GetInstance(imagePath);
            image.ScaleAbsoluteWidth(size);
            imageCell.AddElement(image);
            return imageCell;
        }

        /// <summary>
        /// 插入内容
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private PdfPCell ParagraphCell(string content)
        {
            var paragraphCell = new PdfPCell();
            paragraphCell.AddElement(new Paragraph(content));
            return paragraphCell;
        }

        /// <summary>
        /// 生成水印
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="filePath"></param>
        /// <param name="waterMarkName"></param>
        /// <param name="waterMarkAddr"></param>
        /// <returns></returns>
        private static MemoryStream SetWaterMark(MemoryStream ms, string filePath, string waterMarkName, string waterMarkAddr = null)
        {
            MemoryStream msWater = new MemoryStream();
            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            try
            {
                pdfReader = new PdfReader(filePath);
                pdfStamper = new PdfStamper(pdfReader, msWater);

                int total = pdfReader.NumberOfPages + 1;//获取PDF的总页数
                iTextSharp.text.Rectangle psize = pdfReader.GetPageSize(1);//获取第一页
                float width = psize.Width;//PDF页面的宽度，用于计算水印倾斜
                float height = psize.Height;
                PdfContentByte waterContent;
                BaseFont basefont = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\SIMFANG.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                PdfGState gs = new PdfGState();
                for (int i = 1; i < total; i++)
                {
                    waterContent = pdfStamper.GetOverContent(i);//在内容上方加水印
                                                                //透明度
                    waterContent.SetGState(gs);
                    //开始写入文本
                    waterContent.BeginText();
                    waterContent.SetColorFill(BaseColor.RED);
                    waterContent.SetFontAndSize(basefont, 18);
                    waterContent.SetTextMatrix(0, 0);
                    if (waterMarkAddr == null || waterMarkAddr == "")
                    {
                        waterContent.ShowTextAligned(Element.ALIGN_CENTER, waterMarkName, width / 2, height / 2, 55);
                    }
                    else
                    {
                        waterContent.ShowTextAligned(Element.ALIGN_CENTER, waterMarkName, width / 2, height / 2 + 100, 55);
                        waterContent.ShowTextAligned(Element.ALIGN_CENTER, waterMarkAddr, width / 2, height / 2 - 100, 55);
                    }
                    waterContent.EndText();
                }
            }
            catch (Exception ex)
            {
                return ms;
            }
            finally
            {
                if (pdfStamper != null)
                    pdfStamper.Close();

                if (pdfReader != null)
                    pdfReader.Close();
            }
            return msWater;
        }
    }
}
