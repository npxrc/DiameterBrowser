﻿using System;
using System.Drawing;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                WriteToFile("baseurl", "https://perplexity.ai/?q=");
                Application.Exit();
            }
            InitializeCefSharp();
            this.Resize += (sender, e) => OnResize();
            navigate.Click += (sender, e) => Navigate();
            back.Click += (sender, e) => MakeBrowserBack(sender, e);
            forward.Click += (sender, e) => MakeBrowserForward(sender, e);
            refresh.Click += (sender, e) => MakeBrowserReload(sender, e);
            url.GotFocus += (sender, e) => url.SelectAll();
            url.KeyDown += (sender, e) => {
                if (e.KeyCode == Keys.Enter)
                {
                    Navigate();
                    webbrowser.Focus();
                }
            };
            this.KeyDown += FocusURL;
            webbrowser.KeyboardHandler = new KeyboardHandler(this);
        }
        public void MakeBrowserBack(object sender, EventArgs e)
        {
            webbrowser.Back();
        }
        public void MakeBrowserForward(object sender, EventArgs e)
        {
            webbrowser.Forward();
        }
        public void MakeBrowserReload(object sender, EventArgs e)
        {
            webbrowser.Reload();
        }
        public void FocusURL(object sender, KeyEventArgs e)
        {
            url.Focus();
            url.SelectAll();
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
            if (this.ClientSize.Width <= Screen.PrimaryScreen.WorkingArea.Width / 2)
            {
                if (FormWindowState.Minimized == WindowState) return;
                this.Text = "Radius";
            }
            else
            {
                this.Text = "Diameter";
            }
            webbrowser.Height = (this.ClientSize.Height - 25);
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
                if (url.Text.ToLower().Trim() == "!google")
                {
                    MessageBox.Show("Search engine switched to Google.", "Search Engine", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    baseUrl = "https://google.com/search?q=";
                    WriteToFile("baseurl", baseUrl);
                }
                else if (url.Text.ToLower().Trim() == "!bing")
                {
                    MessageBox.Show("Search engine switched to Bing.", "Search Engine", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    baseUrl = "https://bing.com/search?q=";
                    WriteToFile("baseurl", baseUrl);
                }
                else if (url.Text.ToLower().Trim() == "!perplexity" || url.Text.ToLower().Trim() == "perplexity ai" || url.Text.ToLower().Trim() == "perplexity.ai")
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
    public class KeyboardHandler : IKeyboardHandler
    {
        private bool ctrlPressed = false;
        private bool altPressed = false;
        #pragma warning disable IDE0044 // Add readonly modifier
        private DiameterApp diameterAppInstance;
        public KeyboardHandler(DiameterApp appInstance)
        {
            diameterAppInstance = appInstance;
        }
        public bool OnKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
        {
            if (type == KeyType.RawKeyDown || type == KeyType.KeyUp)
            {
                var key = (Keys)windowsKeyCode;

                // Check if CTRL is pressed
                if ((modifiers & CefEventFlags.ControlDown) != 0)
                {
                    ctrlPressed = true;
                }
                else if ((modifiers & CefEventFlags.AltDown) != 0)
                {
                    altPressed = true;
                }
                else
                {
                    altPressed = false;
                    ctrlPressed = false;
                }

                // Check for CTRL + E
                if (ctrlPressed && key == Keys.E && type == KeyType.RawKeyDown)
                {
                    // Call FocusURL method of DiameterApp instance on the UI thread
                    diameterAppInstance.Invoke(new Action(() =>
                    {
                        diameterAppInstance.FocusURL(null, null); // Passing null for sender and EventArgs as they are not used in FocusURL method
                    }));
                    return true;
                }
                else if (ctrlPressed && key  == Keys.W && type == KeyType.RawKeyDown)
                {
                    Application.Exit();
                    return true;
                }
                else if (ctrlPressed && key == Keys.R && type == KeyType.RawKeyDown)
                {
                    browser.Reload(true);
                    return true;
                }
                else if (ctrlPressed && key == Keys.T &&  type == KeyType.RawKeyDown)
                {
                    MessageBox.Show("dumbass there are no tabs in Diameter bc im too lazy to implement them", "haha idiot", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return true;
                }
                else if (altPressed && key == Keys.Right && type == KeyType.RawKeyDown)
                {
                    browser.Forward();
                    return true;
                }
                else if (altPressed && key == Keys.Left && type == KeyType.RawKeyDown)
                {
                    browser.Back();
                    return true;
                }
                else if (key == Keys.F12)
                {
                    browser.ShowDevTools();
                    return true;
                }
            }
            return false;
        }
        public bool OnPreKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
        {
            return false;
        }
    }
}