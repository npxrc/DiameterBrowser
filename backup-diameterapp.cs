using System;
using System.Drawing;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace DiameterBrowser
{
    public partial class DiameterApp : Form
    {
        private string appDataFolder = "Diameter";
        private string baseUrl = "https://www.perplexity.ai/?q=";
        private bool control = false;

        public DiameterApp()
        {
            InitializeComponent();
            OnResize();
            try
            {
                baseUrl = ReadFromFile("baseurl");
            } catch (Exception ex) { 
                Console.Error.WriteLine(ex.Message);
                WriteToFile("baseurl", "https://perplexity.ai/?q=");
                Application.Exit();
            }
            InitializeCefSharp();
            this.Resize += (sender, e) => OnResize();
            navigate.Click += (sender, e) => Navigate();
            back.Click += (sender, e)=> webbrowser.Back();
            forward.Click += (sender, e) => webbrowser.Forward();
            refresh.Click += (sender, e) => webbrowser.Reload();
            // Assuming this code is in the constructor or initialization method of your form
            this.KeyDown += (sender, e) => KeyDownEvent(sender, e);
            this.KeyUp += (sender, e) => KeyUpEvent(sender, e);

            url.KeyDown += (sender, e) => {
                if (e.KeyCode == Keys.Enter)
                {
                    Navigate();
                    webbrowser.Focus();
                }
            };
        }

        private void KeyDownEvent(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.ControlKey)
                {
                    MessageBox.Show("AHHHHHHHHH");
                    control = true;
                }
                else if (control)
                {
                    if (e.KeyCode == Keys.W)
                    {
                        Application.Exit();
                    }
                    else if (e.KeyCode == Keys.E)
                    {
                        url.Focus();
                    }
                    else if (e.KeyCode == Keys.R)
                    {
                        webbrowser.Reload();
                    }
                }
            };
        }
        private void KeyUpEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                control = false;
            }
        }

        private void InitializeCefSharp()
        {
            var settings = new CefSettings();
            Cef.Initialize(settings);

            webbrowser.Load("https://google.com");

            //webbrowser.BringToFront();
        }

        private void OnResize()
        {
            if (this.ClientSize.Width <= Screen.PrimaryScreen.WorkingArea.Width/2)
            {
                this.Text = "Radius";
            }
            else
            {
                this.Text = "Diameter";
            }
            webbrowser.Height = (this.ClientSize.Height-25);
            webbrowser.Width = this.ClientSize.Width;

            webbrowser.Anchor = AnchorStyles.Bottom;

            webbrowser.Location = new Point(
                this.ClientSize.Width - webbrowser.Width,//align horizontally
                this.ClientSize.Height - webbrowser.Height
            );
        }

        private void Navigate()
        {
            if (url.Text.Length > 0)
            {
                if (url.Text.ToLower().Trim() == "google")
                {
                    MessageBox.Show("Search engine switched to Google.", "Search Engine", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    baseUrl = "https://google.com/search?q=";
                    WriteToFile("baseurl", baseUrl);
                }
                else if (url.Text.ToLower().Trim() == "bing")
                {
                    MessageBox.Show("Search engine switched to Bing.", "Search Engine", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    baseUrl = "https://bing.com/search?q=";
                    WriteToFile("baseurl", baseUrl);
                }
                else if (url.Text.ToLower().Trim() == "perplexity" || url.Text.ToLower().Trim() == "perplexity ai" || url.Text.ToLower().Trim() == "perplexity.ai")
                {
                    MessageBox.Show("Search engine switched to Perplexity.", "Search Engine", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    baseUrl = "https://www.perplexity.ai/?q=";
                    WriteToFile("baseurl", baseUrl);
                }
                else if (url.Text.StartsWith("http:") || url.Text.StartsWith("https:") || url.Text.StartsWith("www."))
                {
                    webbrowser.Load(url.Text);
                }
                else if (url.Text.Split('.')[url.Text.Split('.').Length - 1].Length > 1)
                {
                    webbrowser.Load(url.Text);
                }
                else
                {
                    string toLoad = baseUrl + url.Text;
                    webbrowser.Load(toLoad);
                }
            }
        }
        private string ReadFromFile(string filename)
        {
            try
            {
                string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                string gameFolderPath = Path.Combine(localAppDataPath, appDataFolder);

                string cookiesFilePath = Path.Combine(gameFolderPath, filename);

                if (File.Exists(cookiesFilePath))
                {
                    string textFromFile = File.ReadAllText(cookiesFilePath);
                    return textFromFile;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        private void WriteToFile(string filePath, string toWrite)
        {
            try
            {
                string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                string gameFolderPath = Path.Combine(localAppDataPath, appDataFolder);

                if (!Directory.Exists(gameFolderPath))
                {
                    Directory.CreateDirectory(gameFolderPath);
                }

                string cookiesFilePath = Path.Combine(gameFolderPath, filePath);

                string textToWrite = toWrite;
                File.WriteAllText(cookiesFilePath, textToWrite);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
