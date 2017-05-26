using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XOR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Verschlüsselung auf Button 1

        private void button1_Click(object sender, EventArgs e)
        {
            // Strings deklarieren
            string klarText = richTextBox1.Text;
            string Passwort = richTextBox3.Text;
            string Verschlüsselung = "";

            
            // Ascii zu Bit Array

            byte[] klarTextArray = System.Text.Encoding.ASCII.GetBytes(klarText);
            byte[] PasswortTextArray = System.Text.Encoding.ASCII.GetBytes(Passwort);
                                   
            // klarText in Binär

            string klarTextBinaer = string.Empty;

            // Für jeden Buchstaben des klarText konvertiere Ascii in Binär (8 Bit Blöcke)

            foreach (char KlarTextBuchstabe in klarText)
            {
                klarTextBinaer += Convert.ToString((int)KlarTextBuchstabe, 2).PadLeft(8, '0');
            }

            // Passwort in binär

            string PasswortBinaer = string.Empty;

            // Für jeden Buchstaben des Passwort konvertiere Ascii in Binär (8 Bit Blöcke)

            foreach (char PasswortBuchstabe in Passwort)
            {
                PasswortBinaer += Convert.ToString((int)PasswortBuchstabe, 2).PadLeft(8, '0');
            }
            
            // Deklariere einen neuen Index und zähle mit dem das Passwort auf die Länge des klarText hoch

            int j = 0;

            // Verschlüsselung XOR (mit xor verschlüssel jedes Bit des klarText mit jedem Bit des Passworts, erweitere das Passwort auf jeden Buchstaben (Binär) im Text)

            for (int i = 0; i < klarTextBinaer.Length; i++)
            {
                
                // Verschlüsselung

                int AsciiKlarTextIndex = Convert.ToInt32(klarTextBinaer[i]);
                int AsciiPasswortIndex = Convert.ToInt32(PasswortBinaer[j]);

                // Passwort erweitern

                j = j + 1 == PasswortBinaer.Length ? 0 : j + 1;

                int SchluesselString = AsciiKlarTextIndex ^= AsciiPasswortIndex;

                Verschlüsselung += SchluesselString;

                // Ausgabe

                richTextBox2.Text = Verschlüsselung;
            }
        }

        // Entschlüsselung auf Button 1
         
        private void button2_Click(object sender, EventArgs e)

        {
            //Strings deklarieren

            string Cipher = richTextBox2.Text;
            string Passwort = richTextBox3.Text;

            string ReverseStringString = "";

            // Passwort Array

            byte[] PasswortTextArray = System.Text.Encoding.ASCII.GetBytes(Passwort);

            // Passwort in Binär in 8 Bit Blöcke

            string PasswortBinaer = string.Empty;
            foreach (char PasswortBuchstabe in Passwort)
            {
                PasswortBinaer += Convert.ToString((int)PasswortBuchstabe, 2).PadLeft(8, '0');
            }
            int j = 0;

            // Entschlüsselung -> xor cipher mit Passwort und erhalte klarText
             
            for (int i = 0; i < Cipher.Length; i++)

            {

                int CipherIndex = Convert.ToInt32(Cipher[i]);
                int AsciiPasswortIndex = Convert.ToInt32(PasswortBinaer[j]);

                j = j + 1 == PasswortBinaer.Length ? 0 : j + 1;

                int ReverseString = CipherIndex ^= AsciiPasswortIndex;

                ReverseStringString += ReverseString;

                // Ausgabe

                richTextBox1.Text = ReverseStringString;
            }

            richTextBox1.Text = aufteilen(ReverseStringString);
        }

        // Teile die Bit des neuen Schlüsselstring auf
         
        public string aufteilen(string ReverseStringString) 
        {
            string cipher = richTextBox2.Text;
            string ziffer = "";
            int[] BuchstabenBin = new int[8];
            int z = 0;

            for (int p = 0; p < ReverseStringString.Length; p++)
            {
                for (int i = 0; i < 8; i++)
                {
                    BuchstabenBin[i] = ReverseStringString[z % ReverseStringString.Length] - 48;

                    z++;

                    richTextBox1.Text = ziffer;
                }

                ziffer += umwandel(BuchstabenBin);
                
            }

            return ziffer;
        }

        // Wandel die Bit Blöcke des entschlüsselten und aufgeteilten String wieder in Buchstaben um 

        public char umwandel(int[] BuchstabenBin)
        {
            int zahl = 0;
            int g = 7;
            char ziffer;

            for (int c = 0; c < BuchstabenBin.Length; c++)
            {
                if (BuchstabenBin[c] == 1)
                    BuchstabenBin[c] += 1;
            }

            for (int i = 0; i < 8; i++)
            {
                zahl += (int)Math.Pow(BuchstabenBin[i], g);
                g--;                             
            }            

            ziffer = (char)zahl;
            return ziffer;
        }        
    }
}
