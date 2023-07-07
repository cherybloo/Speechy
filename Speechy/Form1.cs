using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace turbo
{   
    public partial class Form1 : Form
    {
        private const string URL = "http://localhost:8000/about"; //change your json link here

        SpeechSynthesizer ss;

        public Form1()
        {
            InitializeComponent();
            ss = new SpeechSynthesizer();
            ss.Volume=100;
        }

        async Task<string> waitingBullshit()
        {
            using(WebClient client = new WebClient())
            {
                string userInput = "";
                var response = await client.DownloadStringTaskAsync(URL);
                dynamic dobj = JsonConvert.DeserializeObject<dynamic>(response);
                userInput = dobj["user"];
                return userInput;
            }
        }


        async void Form1_Load(object sender, EventArgs e)
        {
            string prevInput = "";
            while (true)
            {
                var response = await waitingBullshit();
                if (prevInput != response)
                {
                    outputBox.Text = response;
                    prevInput = response;
                    response = "";
                }
            }
           
        }
        
        private void outputBox_TextChanged(object sender, EventArgs e)
        {
            if (outputBox.Text != "")
            {
                
                ss.Speak(outputBox.Text);
            }
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            outputBox.Text = "";
        }
    }
}
