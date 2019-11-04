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
    public partial class Form1 : Form
    {
        //fields
        SecretClass getSecret;
        string[] info;


        //events
        public Form1()
        {
            InitializeComponent();
            getSecret = new SecretClass();  //initialize class
        }


        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog()){

                openFile.InitialDirectory = "C:\\temp\\";
                openFile.Filter ="Portable PicMap (*.ppm)|*.ppm|Bitmap Image (*.bmp)|*.bmp";
                openFile.FilterIndex = 1;
                openFile.RestoreDirectory = true;

                if(openFile.ShowDialog() == DialogResult.OK)
                {
                    //encryption can save as PPM and BMP formats, so decrypter can load both
                    switch (openFile.FilterIndex)
                    {
                        case 1:
                            info = File.ReadAllLines(openFile.FileName).ToArray();
                            button1.Enabled = true;
                            break;

                        case 2:
                            Bitmap pic = new Bitmap(openFile.FileName);
                            info = getSecret.ConvertBMP(pic);
                            button1.Enabled = true;
                            break;  
                    }
                }//end if dialog result is ok
            }
        }//end import file event


        private void button1_Click_1(object sender, EventArgs e)
        {
            string message = getSecret.DisplayMSG(info);
            textBox1.Text = message;
        }//end button1 click event
        


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }//class
}//name
