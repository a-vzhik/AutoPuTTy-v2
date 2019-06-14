using AutoPuTTY.Core;
using AutoPuTTY.Core.Parameters;
using AutoPuTTY.Desktop.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace AutoPuTTY.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow : Window
    {
        public MainWindow()
        {
            Title = string.Format("AutoPuTTY (версия {0})", Assembly.GetExecutingAssembly().GetName().Version);
            DataContext = new MainWindowViewModel();

            InitializeComponent();

            Model.SelectedObjectChanged += Model_SelectedObjectChanged;
            Closing += MainWindow_Closing;
            TreeView.ItemContainerGenerator.ItemsChanged += ItemContainerGenerator_ItemsChanged;
            TreeView.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
        }

        private void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
            SyncSelectedItem();
        }

        private void ItemContainerGenerator_ItemsChanged(object sender, System.Windows.Controls.Primitives.ItemsChangedEventArgs e)
        {
            SyncSelectedItem();
        }

        public static TreeViewItem FindTviFromObjectRecursive(ItemsControl ic, object o)
        {
            if (ic == null)
                return null;

            //Search for the object model in first level children (recursively)
            TreeViewItem tvi = ic.ItemContainerGenerator.ContainerFromItem(o) as TreeViewItem;
            if (tvi != null)
                return tvi;
            //Loop through user object models
            foreach (object i in ic.Items)
            {
                //Get the TreeViewItem associated with the iterated object model
                TreeViewItem tvi2 = ic.ItemContainerGenerator.ContainerFromItem(i) as TreeViewItem;
                tvi = FindTviFromObjectRecursive(tvi2, o);
                if (tvi != null) return tvi;
            }
            return null;
        }

        private void SyncSelectedItem()
        {
            if (TreeView.SelectedValue != Model.SelectedObject)
            {
                var tvi = FindTviFromObjectRecursive(TreeView, Model.SelectedObject);
                if (tvi != null)
                {
                    tvi.IsSelected = true;
                    tvi.BringIntoView();
                }
            }
        }

        private void Model_SelectedObjectChanged(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Render,
                new Action(() =>
                {
                    SyncSelectedItem();
                }));
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Model.Close();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Model.SelectedObject = e.NewValue;
        }
        
        public MainWindowViewModel Model { get { return DataContext as MainWindowViewModel; } }
    }
}
