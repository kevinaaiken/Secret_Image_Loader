using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Secret_Image_Loader
{
    class SecretClass
    {
        
        public string DisplayMSG(string[] info)
        {
            int len = 0;
            bool start_point = true;
            bool get_len = false;
            bool get_msg = false;
            string message = "";

            //start at 4 (0-3 are header) for R channel, +1 = G, +2 = B: info is in blue so start at 6 (4+2)
            for (int i = 6; i < info.Length; i += 3){
                
                //FIRST: start point is first even pixel, length is in next blue channel
                if (start_point){
                    if (Convert.ToInt16(info[i]) % 2 == 0)
                    {
                        start_point = false;
                        get_len = true;
                    }
                //SECOND: get length of message
                }else if (get_len){
                    len = Convert.ToInt16(info[i]);
                    get_len = false;
                    get_msg = true;
                }else if (get_msg){
                //THIRD: each B channel info is a char that makes up the secret message
                    if (message.Length < len)
                    {
                        message += (char)Convert.ToInt16(info[i]);
                    }else{
                        return message;
                    }
                }
                
            }//end foreach line in file

            return message;
        }//end display message 


        public string[] ConvertBMP(Bitmap pic)
        {
            string[] info = new string[((pic.Width * pic.Height) * 3) + 4];

            int i = 4;
            for (int y = 0; y < pic.Height; y ++){
                for (int x = 0; x < pic.Width; x ++){

                    Color col_pixel = pic.GetPixel(x, y);

                    info[i]     = Convert.ToString(col_pixel.R);
                    info[i + 1] = Convert.ToString(col_pixel.G);
                    info[i + 2] = Convert.ToString(col_pixel.B);

                    i += 3;
                }//end for width
            }//end for height

            return info;
        }//end comvert bitmap


    }//class
}//name
