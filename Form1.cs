using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace FileEncryption
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private byte[] XOREncryptDecrypt(byte[] data, string key)
        {
            if (string.IsNullOrEmpty(key)) return data;

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            int keyLength = keyBytes.Length;

            for (int i = 0; i < data.Length; i++)
            {
                data[i] ^= keyBytes[i % keyLength]; // XOR each byte
            }
            return data;
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    byte[] fileData = File.ReadAllBytes(filePath);

                    string key = Interaction.InputBox("Enter an encryption key:", "Encryption Key", "", -1, -1);
                    if (string.IsNullOrEmpty(key))
                    {
                        MessageBox.Show("Encryption key is required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    byte[] encryptedData = XOREncryptDecrypt(fileData, key);
                    File.WriteAllBytes(filePath, encryptedData); // Overwrite file
                    MessageBox.Show("File encrypted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    byte[] fileData = File.ReadAllBytes(filePath);

                    string key = Interaction.InputBox("Enter the decryption key:", "Decryption Key", "", -1, -1);
                    if (string.IsNullOrEmpty(key))
                    {
                        MessageBox.Show("Decryption key is required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    byte[] decryptedData = XOREncryptDecrypt(fileData, key);
                    File.WriteAllBytes(filePath, decryptedData); // Overwrite file
                    MessageBox.Show("File decrypted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}