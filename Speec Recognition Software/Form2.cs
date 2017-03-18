using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;
using System.Diagnostics;


namespace Speec_Recognition_Software
{
    public partial class Form2 : Form
    {
        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-IN"));
        Choices clist=new Choices();
        Grammar gr;


        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //Form1 f = new Form1();
            //f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = true;
            clist.Add( new string[]{ "Hello","How are you","z",
                "what is current time","chrome","thank you","close","time","firefox","face","you",
                "who are you","what is current date"
            });
            GrammarBuilder gb = new GrammarBuilder();
           gb.Append(clist);
            //gb.AppendDictation();
            //gb.AppendWildcard();
            //gb.AppendRuleReference();
            //Grammar gr = new Grammar(new GrammarBuilder(clist));
             gr = new Grammar(gb);
            ss.Rate = -2;

            try
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammarAsync(gr);
                sre.SpeechRecognized += Sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                // sre.QueryRecognizerSetting();
                sre.RecognizeAsync(RecognizeMode.Multiple);
            } catch (Exception ex) { }
        }

        private void Sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch((Convert.ToString(e.Result.Text)).ToLower())
            {
                case "hello":
                    ss.SpeakAsync("Hello Malik");
                    break;
                case "who are you":
                    ss.SpeakAsync("I am your Personal Assistant"); 
                    break;
                case "what is current date":
                    ss.SpeakAsync("The date is "+System.DateTime.Now.ToShortDateString());
                    break;
                case "how are you":
                     ss.SpeakAsync("I am doing great Malik what about you"); 
                    break;
                case "what is current time":
                    ss.SpeakAsync("The current time is "+DateTime.Now.ToShortTimeString());
                    break;
                case "thank you":
                    ss.SpeakAsync("my pleasure");
                    break;
                case "chrome":
                    Process.Start("chrome", "https://www.google.co.in/");
                    break;
                case "close":
                    Application.Exit();
                    break;
                case "firefox":
                    Process.Start("chrome", "https://www.google.co.in/");
                    break;
                case "face":
                    Process.Start("chrome", "https://www.facebook.com/");
                    break;
                case "you":
                    Process.Start("chrome", "https://www.youtube.com/");
                    break;
                default:
                    ss.SpeakAsync("not defined");
                    break;

            }
            //string b = textBox1.Text;
            textBox1.Text = e.Result.Text.ToString() + Environment.NewLine;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //stop button
            sre.RecognizeAsyncStop();
            button2.Enabled = true;
            button3.Enabled = false;
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            sre.UnloadAllGrammars();
            sre.Dispose();
            gr = null;

        }
    }
}
