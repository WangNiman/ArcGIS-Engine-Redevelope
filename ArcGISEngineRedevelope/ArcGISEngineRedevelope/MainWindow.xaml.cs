using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;

using MyMapView;

namespace ArcGISEngineRedevelope
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        AxMapControl mapControl = null;
        AxTOCControl tocControl = null;
        AxToolbarControl toolbarControl = null;

        public MainWindow()
        {
            InitializeComponent();
            LoadEsriControl();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (mapControl == null || tocControl == null || toolbarControl == null)
            {
                ShowMessage("控件初始化失败", "Error");
                return;
            }
            tocControl.SetBuddyControl(mapControl);
            toolbarControl.SetBuddyControl(mapControl);

            toolbarControl.AddItem("esriControls.ControlsOpenDocCommand", -1, -1, false, -1, esriCommandStyles.esriCommandStyleMenuBar);
            toolbarControl.AddItem(new ControlsSaveAsDocCommandClass());
            toolbarControl.AddItem(new ControlsAddDataCommandClass());
            toolbarControl.AddItem("esriControls.ControlsMapNavigationToolbar");
            toolbarControl.AddItem("esriControls.ControlsMapIdentifyTool");
        }

        public void LoadEsriControl()
        {
            //Init toolbar
            toolbarControl = new AxToolbarControl();
            header.Child = toolbarControl;

            //Init map
            mapControl = new MyMapControl();
            {
                //mapControl.AutoMouseWheel = false;
            }
            body.Child = mapControl;

            //Init toc
            tocControl = new AxTOCControl();
            toc.Child = tocControl;
        }

        public bool LoadMxdMap(string fileName)
        {
            if (mapControl.CheckMxFile(fileName))
            {
                mapControl.LoadMxFile(fileName);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ShowMessage(string messageBoxText, string caption)
        {
            System.Windows.MessageBox.Show(messageBoxText, caption);
        }
    }
}
