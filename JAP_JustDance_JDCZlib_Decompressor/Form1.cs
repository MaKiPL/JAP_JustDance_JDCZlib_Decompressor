using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JAP_JustDance_JDCZlib_Decompressor
{
    public partial class Form1 : Form
    {
        [DllImport("JDCZlibForUnity")]
        private static extern IntPtr JDCZlibDeCompress([MarshalAs(UnmanagedType.LPArray)] byte[] source, int length);

        [DllImport("JDCZlibForUnity")]
        private static extern void JDCZlibInitialize();

        public Form1()
        {
            InitializeComponent();
            JDCZlibInitialize();
        }

        private class BasicFunc
        {
            public static string[] array = new string[]
            {
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "A",
                "B",
                "C",
                "D",
                "E",
                "F",
                "G",
                "H",
                "I",
                "J",
                "K",
                "L",
                "M",
                "N",
                "O",
                "P",
                "Q",
                "R",
                "S",
                "T",
                "U",
                "V",
                "W",
                "X",
                "Y",
                "Z"
            };
        }

        public static int stringConvertToNumber(string source, int format)
        {
            char[] array = source.ToCharArray();
            int num = 0;
            try
            {
                for (int i = 0; i < array.Length; i++)
                {
                    string value = array[i].ToString().ToUpper();
                    int num2 = Array.IndexOf<string>(BasicFunc.array, value);
                    if (num2 < 0)
                    {
                        throw new Exception();
                    }
                    double value2 = Math.Pow((double)format, Convert.ToDouble(array.Length - i - 1));
                    num += num2 * Convert.ToInt32(value2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return num;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog {Multiselect = false};
            if (ofd.ShowDialog() == DialogResult.OK)
                textBox1.Text = ofd.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                textBox2.Text = ofd.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Set paths!");
                return;
            }
            byte[] message = File.ReadAllBytes(textBox1.Text);
            string utf = Encoding.UTF8.GetString(message, 0, 5);
            int num2 = stringConvertToNumber(utf, 36);
            int num4 = num2 & 4194303;

            byte[] retbuf = new byte[num4];
            Array.Copy(message, 0 + 5, retbuf, 0, num4);
            string result = doDeCompress(retbuf, num4);
            File.WriteAllText(textBox2.Text, result);
            MessageBox.Show("DONE!");
        }

        public static string doDeCompress(byte[] source, int length)
        {
            string result = string.Empty;
            try
            {
                IntPtr ptr = JDCZlibDeCompress(source, length);
                result = Marshal.PtrToStringAnsi(ptr);
            }
            catch
            {
                
            }
            return result;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
    string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Set paths!");
                return;
            }


        }
    }
}
