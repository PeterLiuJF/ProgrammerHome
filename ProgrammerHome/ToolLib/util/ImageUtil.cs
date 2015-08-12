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
 * Content: ͼƬ��
 ******************************************************************/

namespace ToolLib.util
{
    /// <summary>
    /// ͼƬ��
    /// </summary>
    public class ImageUtil
    {
        #region ����ͼƬ
        /// <summary>
        /// ����ͼƬ(Ĭ��ʹ�ø�������˫���β�ֵ��,������������ߵ�����ͼƬ)
        /// </summary>
        /// <param name="imageArry">ԭʼͼƬ����������</param>
        /// <param name="intWidth">ͼƬ���(��λ:px)</param>
        /// <param name="intHeight">ͼƬ�߶�(��λ:px)</param>   
        /// <param name="strImageType">ͼƬ����(jpg,jpeg,gif,bmp,png)</param>
        /// <returns>���ź�ͼƬ����������</returns>
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
                //��ֵ�㷨������
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
