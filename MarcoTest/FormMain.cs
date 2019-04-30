using log4net.Config;
using SanwaMarco;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using SanwaMarco.Comm;

namespace MarcoTest
{
    public partial class FormMain : Form
    {
        int indexOfSearchText = 0;
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string result = Marco.RunMarco("marco\\Kuma Marco.vb");
            setResult(result);
        }
        private void setResult(string result)
        {
            rtMsg.Text = result;
            setColor("RETURN", Color.Blue);
            setColor("\"", Color.Blue);
            setColor("'", Color.OrangeRed);
            setColor("SETVAR", Color.Green);
            setColor("DELAY", Color.Green);
            setColor("DECODE", Color.Green);
            setColor("API", Color.Green);
            setColor("PRINT", Color.Green);
            setColor("IF", Color.Blue);
            setColor("ELSE", Color.Blue);
            setColor("ENDIF", Color.Blue);
            setColor("ELSEIF", Color.Blue);
            setColor("Function", Color.Blue);
            setColor("End", Color.Blue);
            setColor("true", Color.Red);
            setColor("false", Color.Red);
            setColor("1", Color.Red);
            setColor("2", Color.Red);
            setColor("3", Color.Red);
            setColor("4", Color.Red);
            setColor("5", Color.Red);
            setColor("6", Color.Red);
            setColor("7", Color.Red);
            setColor("8", Color.Red);
            setColor("9", Color.Red);
            setColor("0", Color.Red);
            setColor("@", Color.Red);
        }
        public void setColor(string key, Color color)
        {
            setColor(key, color, 0);
        }
        public void setColor(string key, Color color, int start)
        {
            int laststart = start;
            int startindex = 0;
            indexOfSearchText = 0;

            if (key.Length > 0)
                startindex = FindMyText(key, start, rtMsg.Text.Length);

            // If string was found in the RichTextBox, highlight it
            if (startindex >= 0)
            {
                // Set the highlight color as red
                rtMsg.SelectionColor = color;
                rtMsg.SelectionFont = new Font(rtMsg.SelectionFont, FontStyle.Bold);
                // Find the end index. End Index = number of characters in textbox
                int endindex = key.Length;
                // Highlight the search string
                rtMsg.Select(startindex, endindex);
                // mark the start position after the position of
                // last search string
                start = startindex + endindex;
            }
            if(laststart == start)
            {
                return;
            }else if(start < rtMsg.Text.Length)
            {
                setColor(key, color, start);
            }
        }
        public int FindMyText(string txtToSearch, int searchStart, int searchEnd)
        {
            // Unselect the previously searched string
            //if (searchStart > 0 && searchEnd > 0 && indexOfSearchText >= 0)
            //{
            //    rtMsg.Undo();
            //}

            // Set the return value to -1 by default.
            int retVal = -1;

            // A valid starting index should be specified.
            // if indexOfSearchText = -1, the end of search
            if (searchStart >= 0 && indexOfSearchText >= 0)
            {
                // A valid ending index
                if (searchEnd > searchStart || searchEnd == -1)
                {
                    // Find the position of search string in RichTextBox
                    indexOfSearchText = rtMsg.Find(txtToSearch, searchStart, searchEnd, RichTextBoxFinds.MatchCase);
                    // Determine whether the text was found in richTextBox1.
                    if (indexOfSearchText != -1)
                    {
                        // Return the index to the specified search text.
                        retVal = indexOfSearchText;
                    }
                }
            }
            return retVal;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            try
            {
                if (openFileDialog.RestoreDirectory)
                {
                    openFileDialog.InitialDirectory = System.Environment.CurrentDirectory + "\\marco";
                    openFileDialog.RestoreDirectory = false;
                }
                //openFileDialog1.Filter = "json files (*.json)|*.json";
                //openFileDialog1.FilterIndex = 2;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //string fileName = Path.GetFileName(openFileDialog.FileName);
                    //MessageBox.Show(fileName);
                    string result = Marco.RunMarco(openFileDialog.FileName);
                    setResult(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + ":" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int[] items = new int[] { 1,15,3,66,5,8};
            //for(int i=0; i< items.Length - 1; i++)
            //{
            //    for (int j = i + 1; j < items.Length ; j++)
            //    {
            //        if(items[j] < items[i])
            //        {
            //            int temp = items[j];
            //            items[j] = items[i];
            //            items[i] = temp;
            //        }
            //    }
            //    Console.Write(items.ToString());
            //}
            //Console.Write(items.ToString());
            byte byteTemp = Byte.Parse("170");
            //int r1 = ActiveBoardNo & 0x1 >> 0; //與 00000001 做 AND 運算,向右位移0碼,得出第1位 IO 結果 
            //int r2 = ActiveBoardNo & 0x2 >> 1; //與 00000010 做 AND 運算,向右位移1碼,得出第2位 IO 結果  
            //int r3 = ActiveBoardNo & 0x4 >> 2; //與 00000100 做 AND 運算,向右位移2碼,得出第3位 IO 結果 
            //int r4 = ActiveBoardNo & 0x8 >> 3; //與 00001000 做 AND 運算,向右位移3碼,得出第4位 IO 結果 
            //int r5 = ActiveBoardNo & 0x10 >> 4;//與 00010000 做 AND 運算,向右位移4碼,得出第5位 IO 結果 
            //int r6 = ActiveBoardNo & 0x20 >> 5;//與 00100000 做 AND 運算,向右位移5碼,得出第6位 IO 結果 
            //int r7 = ActiveBoardNo & 0x40 >> 6;//與 01000000 做 AND 運算,向右位移6碼,得出第7位 IO 結果 
            //int r8 = ActiveBoardNo & 0x80 >> 7;//與 10000000 做 AND 運算,向右位移7碼,得出第8位 IO 結果 


            int r1 = (byteTemp >> 0) & 1; //與 00000001 做 AND 運算,向右位移0碼,得出第1位 IO 結果 
            int r2 = (byteTemp >> 1) & 1; //與 00000010 做 AND 運算,向右位移1碼,得出第2位 IO 結果  
            int r3 = (byteTemp >> 2) & 1; //與 00000100 做 AND 運算,向右位移2碼,得出第3位 IO 結果 
            int r4 = (byteTemp >> 3) & 1; //與 00001000 做 AND 運算,向右位移3碼,得出第4位 IO 結果 
            int r5 = (byteTemp >> 4) & 1;//與 00010000 做 AND 運算,向右位移4碼,得出第5位 IO 結果 
            int r6 = (byteTemp >> 5) & 1;//與 00100000 做 AND 運算,向右位移5碼,得出第6位 IO 結果 
            int r7 = (byteTemp >> 6) & 1;//與 01000000 做 AND 運算,向右位移6碼,得出第7位 IO 結果 
            int r8 = (byteTemp >> 7) & 1;//與 10000000 做 AND 運算,向右位移7碼,得出第8位 IO 結果 
            string temp = "" + r8 + r7 + r6 + r5 + r4 + r3 + r2 + r1 ;
            Console.WriteLine(temp);
            //Console.WriteLine(string.Format("{0:000.000}", ActiveBoardNo));
            int i = Convert.ToInt32(temp, 2);
            // Set 5th bit
            i = 0;
            //i |= 1 << 5 - 1;
            Console.WriteLine(); 
            Console.WriteLine(Convert.ToString(i | BIT(1), 2).PadLeft(8, '0')); // set bit 1
            Console.WriteLine(Convert.ToString(i | BIT(2), 2).PadLeft(8, '0')); // set bit 2
            Console.WriteLine(Convert.ToString(i | BIT(3), 2).PadLeft(8, '0')); // set bit 3
            Console.WriteLine(Convert.ToString(i | BIT(4), 2).PadLeft(8, '0')); // set bit 4
            Console.WriteLine(Convert.ToString(i | BIT(5), 2).PadLeft(8, '0')); // set bit 5
            Console.WriteLine(Convert.ToString(i | BIT(6), 2).PadLeft(8, '0')); // set bit 6
            Console.WriteLine(Convert.ToString(i | BIT(7), 2).PadLeft(8, '0')); // set bit 7
            Console.WriteLine(Convert.ToString(i | BIT(8), 2).PadLeft(8, '0')); // set bit 8
            i = Convert.ToInt32(temp, 2);
            i = 255;
            // Clear 5th bit
            //i &= ~BIT(5);
            Console.WriteLine();
            Console.WriteLine(Convert.ToString(i & ~BIT(1), 2).PadLeft(8, '0')); // set bit 1
            Console.WriteLine(Convert.ToString(i & ~BIT(2), 2).PadLeft(8, '0')); // set bit 2
            Console.WriteLine(Convert.ToString(i & ~BIT(3), 2).PadLeft(8, '0')); // set bit 3
            Console.WriteLine(Convert.ToString(i & ~BIT(4), 2).PadLeft(8, '0')); // set bit 4
            Console.WriteLine(Convert.ToString(i & ~BIT(5), 2).PadLeft(8, '0')); // set bit 5
            Console.WriteLine(Convert.ToString(i & ~BIT(6), 2).PadLeft(8, '0')); // set bit 6
            Console.WriteLine(Convert.ToString(i & ~BIT(7), 2).PadLeft(8, '0')); // set bit 7
            Console.WriteLine(Convert.ToString(i & ~BIT(8), 2).PadLeft(8, '0')); // set bit 8

            //int x = 1;
            //i = i | BIT(x);//set bit
            //i = i & ~BIT(x);//clear bit
        }
        
        private int BIT(int x)
        {
            int idx = x - 1;
            return 1 << idx;
        }
        private void btnConn_Click(object sender, EventArgs e)
        {
            Marco.ConnDevice(); 
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            XmlConfigurator.Configure();//for log4NET 設定
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Marco.SendMessage("Robot01", tbMsg.Text);
            rtMsg.AppendText("\nSend" + tbMsg.Text);
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            Marco.Init("Robot01");
        }
    }
}
