using CopyTranslation.Properties;
using Google;
using Google.Cloud.Translation.V2;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WK.Libraries.SharpClipboardNS;
using static WK.Libraries.SharpClipboardNS.SharpClipboard;
namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {

        TranslationClient client;
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            ayarla();
            string credential_path = "My First Project-8c20196a55ae.json";
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);
            client = TranslationClient.Create();
        }
        void ayarla()
        {
            var clipboard = new SharpClipboard();
            clipboard.ObservableFormats.Texts = true;
            clipboard.ClipboardChanged += ClipboardChanged;
        }
        SoundPlayer player = new SoundPlayer();
     
        string path = "ses.wav"; //lmasini istediginiz ses dosyasinin yolu
        private void ClipboardChanged(Object sender, ClipboardChangedEventArgs e)
        {
            if (e.ContentType == SharpClipboard.ContentTypes.Text && calis)
            {
                player.SoundLocation = path;
                Translate(Clipboard.GetText());
                if (ses) player.Play();
            }
        }
        //
        public string TranslateText(string input, string languagePair, string sudildencevir = "auto") {
            string url = String.Format("https://translate.google.com/?hl="+languagePair+"&sl=" + sudildencevir+"&tl=tr&text="+ input + "&op=translate", input, languagePair);
            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.Default;
            string result = webClient.DownloadString(url);
            Clipboard.SetText(result);
            return result;
        }
        void Translate(string metin)
        {
            if(metin == "") {
                metin = Clipboard.GetText();
                if(metin == "") metin = "BOŞ METİN!!!!";
            }
            calis = false;
            if (bunifuCheckbox1.Checked)
            {
                var detection = client.DetectLanguage(text: metin);
                comboBox1.Text = detection.Language;
            }
            if (comboBox1.Text != comboBox2.Text)
            {
                Clipboard.SetText(client.TranslateText(metin, comboBox2.SelectedItem.ToString(), comboBox1.SelectedItem.ToString()).TranslatedText);
            }
          
            calis = true;
        }
        bool calis = false;
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            bunifuImageButton1.Enabled = false;
            bunifuImageButton1.Image = Resources.enabledplay;
            bunifuImageButton2.Enabled = true;
            bunifuImageButton2.Image = Resources.enabledwpause1;
            calis = true;
        }
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            bunifuImageButton1.Enabled = true;
            bunifuImageButton1.Image = Resources.enabledwplay1;
            bunifuImageButton2.Enabled = false;
            bunifuImageButton2.Image = Resources.enabledpause;
            calis = false;
        }
        private void bunifuCheckbox1_OnChange(object sender, EventArgs e)
        {
            if (bunifuCheckbox1.Checked)
            {
                comboBox1.Enabled = false;
                comboBox4.Enabled = false;
            }
            else
            {
                comboBox4.Enabled = true;
                comboBox1.Enabled = true;
            }
        }
        void load() {
            bunifuImageButton1.Enabled = true;
            bunifuImageButton1.Image = Resources.enabledwplay1;
            bunifuImageButton2.Enabled = false;
            bunifuImageButton2.Image = Resources.enabledpause;
            TranslationClient client = TranslationClient.Create();
            IList<Language> response = client.ListLanguages(target: "en");
            foreach (var language in response)
            {
                comboBox1.Items.Add(language.Code);
                comboBox4.Items.Add(language.Name);
                comboBox2.Items.Add(language.Code);
                comboBox3.Items.Add(language.Name);
            }
            if (bunifuCheckbox1.Checked)
            {
                comboBox1.Enabled = false;
                comboBox4.Enabled = false;
            }
            else
            {
                comboBox1.Enabled = true;
                comboBox4.Enabled = true;
            }
            RegionInfo LocalReg = System.Globalization.RegionInfo.CurrentRegion;
            comboBox2.Text = LocalReg.ToString();
            int a = Convert.ToInt32("12");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Thread baslangic = new Thread(new ThreadStart(load));
            baslangic.Start();
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = comboBox3.SelectedIndex;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = comboBox2.SelectedIndex;
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = comboBox4.SelectedIndex;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.SelectedIndex = comboBox1.SelectedIndex;
        }
        bool ses = true;
        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            if (!ses)
            {
                ses = true;
                bunifuImageButton3.Image = Resources._2298386_32;
            }
            else
            {
                ses = false;
                bunifuImageButton3.Image = Resources._2561215_32;
            }
        }
    
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
          
            if (richTextBox1.TextLength > 0)
            {
               panel4.Enabled = true;
                bunifuThinButton28.Padding = new Padding(0, 0, 0, 0);
                bunifuThinButton28.IdleFillColor = Color.White;
            }
            else
            {
                panel4.Enabled = false;
                bunifuThinButton28.Padding = new Padding(28, 10, 0, 0);
                bunifuThinButton28.IdleFillColor = Color.Silver;
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "en";
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "fr";
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "de";
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "tr";
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "zh";
        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "es";
        }
        void isimdegis()
        {
           
                }
        private void bunifuThinButton27_Click(object sender, EventArgs e)
        {
            if (richTextBox2.Text!="")
            {
                bool calis_Yedek = calis;
                calis = false;
                Clipboard.SetText(richTextBox2.Text);
                bunifuThinButton27.ButtonText = "Copied!";
                Thread.Sleep(1000);
                bunifuThinButton27.ButtonText = "Copy";
                calis = calis_Yedek;
            }
          
        }
        private void bunifuThinButton28_Click(object sender, EventArgs e)
        {
            ManuelTranslete(richTextBox1.Text);
           //MessageBox.Show( TranslateText(richTextBox1.Text, comboBox1.Text));
        }
        void ManuelTranslete(string CevrilecekMetin)
        {
            if (richTextBox1.Text!="")
            {
                try
                {
                    var CevrilecekMetindili = client.DetectLanguage(text: CevrilecekMetin);
                    if (CevrilecekMetindili.Language != comboBox2.Text)
                    {
                       richTextBox2.Text = client.TranslateText(CevrilecekMetin, comboBox2.Text, CevrilecekMetindili.Language).TranslatedText;
                    }
                    else
                    {
                        richTextBox2.Text = richTextBox1.Text;
                    }
                }
                catch (Google.GoogleApiException)
                {
                    MessageBox.Show("Çeviri limitine takılındı. Lütfen api anahtarınızı yenileyiniz.");
                }
            }
            else
            {
                MessageBox.Show("You have no entered text to translate!");
            }

        }
        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ManuelTranslete(richTextBox1.Text);
            }
        }

        private void richTextBox1_SizeChanged(object sender, EventArgs e)
        {
           
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox2.TextLength > 0)
            {
                panel5.Enabled = true;
                bunifuThinButton27.Padding = new Padding(0, 0, 0, 0);
                bunifuThinButton27.IdleFillColor = Color.White;
            }
            else
            {
                panel5.Enabled = false;
                bunifuThinButton27.Padding = new Padding(40, 10, 0, 0);
                bunifuThinButton27.IdleFillColor = Color.Silver;
            }
        }
        bool ctrl = false;

        void key_kaldir(Keys basilantus)
        {
            if (ctrl)
            {
                if (basilantus == Keys.V)
                {
                    richTextBox1.Text += Clipboard.GetText();
                    ManuelTranslete(richTextBox1.Text);
                }
                else if (basilantus != Keys.A)
                {
                    ctrl = false;
                }
            }
            if (basilantus == Keys.ControlKey)
            {
                ctrl = true;
            }
        }
        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Thread FirstThread = new Thread(() => key_kaldir(e.KeyCode));
            FirstThread.Start();
        }

        private void richTextBox2_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void richTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                ctrl = false;
            }
        }
    }
}
