using DevExpress.Xpf.Core;
using DevExpress.Xpf.Diagram;
using System;
using System.Linq;
using System.Windows;
using WpfApp3.Model;
using System.Windows.Media;
using DevExpress.Xpf.Grid;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Diagram.Themes;

namespace WpfApp3
{
    /// <summary>
    /// WindowSupply.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WindowSupply : BaseWindow
    {
        public WindowSupply(string versionNm)
        {
            SetVersionName(versionNm);

            var theme = new Theme("NewTheme2");
            theme.AssemblyName = "DevExpress.Xpf.Themes.NewTheme2.v20.2";
            try
            {
                Theme.RegisterTheme(theme);
            }
            catch { }
            ApplicationThemeHelper.ApplicationThemeName = "NewTheme2";
            InitializeComponent();
        }
        void GenerateSupplyChartItem(object sender, DiagramGenerateItemEventArgs e)
        {
            SUPPLY s = (SUPPLY)e.DataObject;

            string templateName = "";
            int compareResult = DateTime.Compare(Convert.ToDateTime(s.AvailDate), Convert.ToDateTime(s.DemandDate));

            switch (s.RoutingAction.ToString().Substring(0,1))
            {
                

                case "S":
                    templateName = "STOCK";
                    dts.Stroke = (compareResult > 0) ? Brushes.Red : Brushes.DarkBlue ;
                    break;
                case "M":
                    templateName = "MOVE";
                    dtm.Stroke = (compareResult > 0) ? Brushes.Red : Brushes.Gray ;
                    break;
                case "B":
                    templateName = "BUY";
                    dtb.Stroke = (compareResult > 0) ? Brushes.Red : Brushes.DarkGreen ;
                    break;
                default:
                    templateName = "PROD";
                    dtp.Stroke = (compareResult > 0) ? Brushes.Red : Brushes.Goldenrod ;
                    break;
            }

            e.Item = e.CreateItemFromTemplate(templateName);
        }
        private void OnPanelLoaded(object sender, RoutedEventArgs e)
        {
            var offset = GetPanelBottomRightOffset();
            var location = diagramPanel.GetBounds(diagramPanel).BottomRight;
            location.Offset(-panAndZoomGroup.FloatSize.Width - offset, -panAndZoomGroup.FloatSize.Height - offset);
            panAndZoomGroup.FloatLocation = location;
        }
        double GetPanelBottomRightOffset()
        {
            var key = new DiagramDesignerControlThemeKeysExtension() { ResourceKey = DiagramDesignerControlThemeKeys.PanZoomBottomRightOffset };
            return (double)diagram.FindResource(key);
        }

        private void grid1_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Type type = e.MouseDevice.Target.GetType();

            DevExpress.Xpf.Grid.TableView tableView = (DevExpress.Xpf.Grid.TableView)e.Source;
            spvm.SelectedSupplyNodeGroup = (SUPPLY)tableView.FocusedRowData.Row;
            spvm.SelectedSupplyNode = (SUPPLY)tableView.FocusedRowData.Row;
        }
    }
}
