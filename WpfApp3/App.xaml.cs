using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace WpfApp3
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        static App()
        {

            
            var theme = new Theme("NewTheme2");
            theme.AssemblyName = "DevExpress.Xpf.Themes.NewTheme2.v20.2";
            Theme.RegisterTheme(theme);
            ApplicationThemeHelper.ApplicationThemeName = "NewTheme2";
            




        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {

            var splashScreen = new SplahWindow();
            splashScreen.Topmost = true;
            this.MainWindow = splashScreen;
            splashScreen.Show();


            Window wnd = null;
            if (e.Args.Length > 2) {
                if (e.Args[1].Equals("minus"))
                {
                    switch (e.Args[2])
                    {
                        // SBOM
                        case "5000":
                            wnd = new WindowSBOM(e.Args[3]);
                            break;

                        // SUPPLY
                        case "6000":
                            wnd = new WindowSupply(e.Args[3]);
                            break;
                    }
                    if (wnd != null)
                    {
                        wnd.Left = -3000; // 초기에 안보이게
                        wnd.WindowStyle = WindowStyle.None;
                        wnd.Tag = e.Args[1];
                        wnd.Title = "Window" + e.Args[2] ;
                        wnd.ShowInTaskbar = false;
                    }
                }
            } 
            else if (wnd == null)
            {
                wnd = new WindowSBOM("MP20210415");
                wnd.WindowState = WindowState.Maximized;
                wnd.WindowStyle = WindowStyle.SingleBorderWindow;
            }

            wnd.Show();
            splashScreen.Close();

        }
    }
}
