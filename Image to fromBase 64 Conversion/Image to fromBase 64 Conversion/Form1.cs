using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Image_to_fromBase_64_Conversion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           DialogResult dr =  openFileDialog1.ShowDialog();
           try
           {
               
               if (dr == DialogResult.OK)
               {
                   textBox1.Text = openFileDialog1.FileName;
                   string s = textBox1.Text;
                   if (s.Contains("jpg"))
                   {
                       textBox1.Text = openFileDialog1.FileName;
                   }
                   else
                   {
                       MessageBox.Show("Only Jpg images Please");
                       textBox1.Text = "";
                   }
               }
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
           }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("No Path Specified");
                textBox1.Focus();
            }
            else
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(textBox1.Text);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                richTextBox1.Text = base64ImageRepresentation;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(richTextBox1.Text.ToString());
            MessageBox.Show("Copied To Clipboard!!!");
            richTextBox1.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = Clipboard.GetText();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button5.Text = "Image From Base 64" + char.ConvertFromUtf32(8594);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
               pictureBox1.Image = Base64ToImage(richTextBox1.Text);
            }
            else
            {
                MessageBox.Show("Paste Base 64 String First");
                richTextBox1.Focus();
            }
        }

        public Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
    }
}
