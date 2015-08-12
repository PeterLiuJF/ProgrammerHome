using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

/******************************************************************
 * Author: hehl 
 * Date: 2012-02-17 
 * Content: 图片类
 ******************************************************************/

namespace ToolLib.util
{
    /// <summary>
    /// 图片类
    /// </summary>
    public class ImageUtil
    {
        #region 缩放图片
        /// <summary>
        /// 缩放图片(默认使用高质量的双三次插值法,将产生质量最高的缩放图片)
        /// </summary>
        /// <param name="imageArry">原始图片二进制数组</param>
        /// <param name="intWidth">图片宽度(单位:px)</param>
        /// <param name="intHeight">图片高度(单位:px)</param>   
        /// <param name="strImageType">图片类型(jpg,jpeg,gif,bmp,png)</param>
        /// <returns>缩放后图片二进制数组</returns>
        public static byte[] ZoomImage(byte[] imageArry, int intWidth, int intHeight, string strImageType)
        {
            byte[] byteArrReturn = null;

            if (imageArry == null)
            {
                return imageArry;
            }

            if (imageArry.Length <= 0)
            {
                return imageArry;
            }

            if (intWidth <= 0 || intHeight <= 0)
            {
                return imageArry;
            }

            if (string.IsNullOrEmpty(strImageType))
            {
                strImageType = "jpg";
            }

            MemoryStream ms_Img = null;
            MemoryStream ms_Img_Type = null;
            Bitmap bmp = null;
            Bitmap bmpNew = null;
            Graphics g = null;

            try
            {
                ms_Img_Type = new MemoryStream();
                ms_Img = new MemoryStream(imageArry);
                bmp = new Bitmap(ms_Img);

                bmpNew = new Bitmap(intWidth, intHeight);

                g = Graphics.FromImage(bmpNew);
                //插值算法的质量
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(bmp, new Rectangle(0, 0, intWidth, intHeight), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);

                switch (strImageType.Trim().ToLower())
                {
                    case "jpg":
                        bmpNew.Save(ms_Img_Type, ImageFormat.Jpeg);
                        break;
                    case "jpeg":
                        bmpNew.Save(ms_Img_Type, ImageFormat.Jpeg);
                        break;
                    case "gif":
                        bmpNew.Save(ms_Img_Type, ImageFormat.Gif);
                        break;
                    case "bmp":
                        bmpNew.Save(ms_Img_Type, ImageFormat.Bmp);
                        break;
                    case "png":
                        bmpNew.Save(ms_Img_Type, ImageFormat.Png);
                        break;
                    default:
                        bmpNew.Save(ms_Img_Type, ImageFormat.Jpeg);
                        break;
                }

                byteArrReturn = ms_Img_Type.ToArray();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ms_Img_Type != null)
                {
                    ms_Img_Type.Close();
                    ms_Img_Type.Dispose();
                }

                if (ms_Img != null)
                {
                    ms_Img.Close();
                    ms_Img.Dispose();
                }

                if (g != null)
                {
                    g.Dispose();
                }
            }

            return byteArrReturn;
        }
        #endregion
    }
}
