using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using CryptoPrivacy;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace MineHackingTools
{
    public partial class HashCrackingForm : Form
    {
        public HashCrackingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog GetWordList = new OpenFileDialog();
            GetWordList.Filter = "Wordlist Text Files (*.txt) | *.txt";
            if(GetWordList.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = GetWordList.FileName;
            }
        }

        private void HashingThread()
        {
            try
            {
                textBox1.Enabled = false;
                textBox3.Enabled = false;
                button1.Enabled = false;
                HashingAlgorithms CrackHash = new HashingAlgorithms();
                string[] ToHash = File.ReadAllLines(textBox2.Text);
                bool IsFound = false;
                label4.Text = "Status: Cracking....";
                switch (comboBox1.Text)
                {
                    case "MD5":
                        foreach (string HashingText in ToHash)
                        {
                            string Text = HashingText;
                            string Hashed = CrackHash.HashingMD5(Text);
                            if (Hashed == textBox1.Text)
                            {
                                IsFound = true;
                                textBox1.Enabled = true;
                                button1.Enabled = true;
                                label4.Text = "Status: Cracked.";
                                MessageBox.Show("Cracked Successfully, it's are: " + Text, "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case "SHA1":
                        foreach (string HashingText in ToHash)
                        {
                            string Text = HashingText;
                            string Hashed = CrackHash.HashingSHA1(Text);
                            if (Hashed == textBox1.Text)
                            {
                                IsFound = true;
                                textBox1.Enabled = true;
                                button1.Enabled = true;
                                label4.Text = "Status: Cracked.";
                                MessageBox.Show("Cracked Successfully, it's are: " + Text, "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case "SHA256":
                        foreach (string HashingText in ToHash)
                        {
                            string Text = HashingText;
                            string Hashed = CrackHash.HashingSHA256(Text);
                            if (Hashed == textBox1.Text)
                            {
                                IsFound = true;
                                textBox1.Enabled = true;
                                button1.Enabled = true;
                                label4.Text = "Status: Cracked.";
                                MessageBox.Show("Cracked Successfully, it's are: " + Text, "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case "SHA384":
                        foreach (string HashingText in ToHash)
                        {
                            string Text = HashingText;
                            string Hashed = CrackHash.HashingSHA384(Text);
                            if (Hashed == textBox1.Text)
                            {
                                IsFound = true;
                                textBox1.Enabled = true;
                                button1.Enabled = true;
                                label4.Text = "Status: Cracked.";
                                MessageBox.Show("Cracked Successfully, it's are: " + Text, "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case "SHA512":
                        foreach (string HashingText in ToHash)
                        {
                            string Text = HashingText;
                            string Hashed = CrackHash.HashingSHA512(Text);
                            if (Hashed == textBox1.Text)
                            {
                                IsFound = true;
                                textBox1.Enabled = true;
                                button1.Enabled = true;
                                label4.Text = "Status: Cracked.";
                                MessageBox.Show("Cracked Successfully, it's are: " + Text, "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case "HMAC-MD5":
                        foreach (string HashingText in ToHash)
                        {
                            string Text = HashingText;
                            string Hashed = CrackHash.HashingHMACMD5(Text, textBox3.Text);
                            if (Hashed == textBox1.Text)
                            {
                                IsFound = true;
                                textBox1.Enabled = true;
                                textBox3.Enabled = true;
                                button1.Enabled = true;
                                label4.Text = "Status: Cracked.";
                                MessageBox.Show("Cracked Successfully, it's are: " + Text, "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case "HMAC-SHA1":
                        foreach (string HashingText in ToHash)
                        {
                            string Text = HashingText;
                            string Hashed = CrackHash.HashingHMACSHA1(Text, textBox3.Text);
                            if (Hashed == textBox1.Text)
                            {
                                IsFound = true;
                                textBox1.Enabled = true;
                                textBox3.Enabled = true;
                                button1.Enabled = true;
                                label4.Text = "Status: Cracked.";
                                MessageBox.Show("Cracked Successfully, it's are: " + Text, "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case "HMAC-SHA256":
                        foreach (string HashingText in ToHash)
                        {
                            string Text = HashingText;
                            string Hashed = CrackHash.HashingHMACSHA256(Text, textBox3.Text);
                            if (Hashed == textBox1.Text)
                            {
                                IsFound = true;
                                textBox1.Enabled = true;
                                textBox3.Enabled = true;
                                button1.Enabled = true;
                                label4.Text = "Status: Cracked.";
                                MessageBox.Show("Cracked Successfully, it's are: " + Text, "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case "HMAC-SHA384":
                        foreach (string HashingText in ToHash)
                        {
                            string Text = HashingText;
                            string Hashed = CrackHash.HashingHMACSHA384(Text, textBox3.Text);
                            if (Hashed == textBox1.Text)
                            {
                                IsFound = true;
                                textBox1.Enabled = true;
                                textBox3.Enabled = true;
                                button1.Enabled = true;
                                label4.Text = "Status: Cracked.";
                                MessageBox.Show("Cracked Successfully, it's are: " + Text, "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case "HMAC-SHA512":
                        foreach (string HashingText in ToHash)
                        {
                            string Text = HashingText;
                            string Hashed = CrackHash.HashingHMACSHA512(Text, textBox3.Text);
                            if (Hashed == textBox1.Text)
                            {
                                IsFound = true;
                                textBox1.Enabled = true;
                                textBox3.Enabled = true;
                                button1.Enabled = true;
                                label4.Text = "Status: Cracked.";
                                MessageBox.Show("Cracked Successfully, it's are: " + Text, "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            }
                        }
                        break;
                }
                if(IsFound == false)
                {
                    textBox1.Enabled = true;
                    button1.Enabled = true;
                    textBox3.Enabled = true;
                    label4.Text = "Status: The WordList You Provided Doesn't match the hash you inputed.";
                    button2.Visible = false;
                }
            }
            catch
            {
                textBox1.Enabled = true;
                button1.Enabled = true;
                textBox3.Enabled = true;
                label4.Text = "Status: Exception (Error) Occured.";
            }
        }

        string OriginalButtonLocation;

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("You can't leave the wordlist text empty or the hash.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
                Thread Hashing = new Thread(new ThreadStart(HashingThread));
                Hashing.Priority = ThreadPriority.Highest;
                Hashing.Start();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text == "HMAC-MD5" || comboBox1.Text == "HMAC-SHA1" || comboBox1.Text == "HMAC-SHA256" || comboBox1.Text == "HMAC-SHA384" || comboBox1.Text == "HMAC-SHA512")
            {
                comboBox1.Location = new Point(72, 100);
                label3.Location = new Point(9, 105);
                button2.Location = new Point(215, 140);
                label4.Location = new Point(8, 140);
                label5.Visible = true;
                textBox3.Visible = true;
                this.Size = new Size(549, 210);
            }
            else
            {
                comboBox1.Location = new Point(72, 73);
                label3.Location = new Point(9, 75);
                button2.Location = new Point(215, 103);
                label4.Location = new Point(8, 108);
                label5.Visible = false;
                textBox3.Visible = false;
                this.Size = new Size(549, 173);
            }
        }

        private void HashCrackingForm_Load(object sender, EventArgs e)
        {
            label5.Visible = false;
            textBox3.Visible = false;
        }
    }
}