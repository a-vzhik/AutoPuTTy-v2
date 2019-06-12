using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AutoPuTTY.Desktop
{
    public class TreeViewHelper
    {
        private static Dictionary<DependencyObject, TreeViewSelectedItemBehavior> behaviors = new Dictionary<DependencyObject, TreeViewSelectedItemBehavior>();


        public static object GetSelectedItem(DependencyObject obj)
        {
            return (object)obj.GetValue(SelectedItemProperty);
        }

        public static void SetSelectedItem(DependencyObject obj, object value)
        {
            obj.SetValue(SelectedItemProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.RegisterAttached("SelectedItem", typeof(object), typeof(TreeViewHelper), new UIPropertyMetadata(null, SelectedItemChanged));

        private static void SelectedItemChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (!(obj is TreeView))
                return;

            if (!behaviors.ContainsKey(obj))
                behaviors.Add(obj, new TreeViewSelectedItemBehavior(obj as TreeView));

            TreeViewSelectedItemBehavior view = behaviors[obj];
            view.ChangeSelectedItem(e.NewValue);
        }

        private class TreeViewSelectedItemBehavior
        {
            TreeView view;

            object delayedSelectedObject; 

            public TreeViewSelectedItemBehavior(TreeView view)
            {
                this.view = view;
                view.SelectedItemChanged += (sender, e) => SetSelectedItem(view, e.NewValue);
            }

            internal void ChangeSelectedItem(object p)
            {
                TreeViewItem item = (TreeViewItem)FindContainer(p, view);
                if (item != null)
                {
                    item.IsSelected = true;
                }
                else
                {
                    delayedSelectedObject = p;
                }

            }

            private DependencyObject FindContainer(object p, TreeView treeView)
            {
                var generator = view.ItemContainerGenerator;
                var container = generator.ContainerFromItem(p);
                if (container != null)
                {
                    return container;
                }
                else
                {
                    if(generator.Status != GeneratorStatus.ContainersGenerated)
                    {
                        delayedSelectedObject = p;
                        generator.ItemsChanged += Generator_ItemsChanged;
                        generator.StatusChanged += Generator_StatusChanged;
                        return null;
                    }
                }

                foreach (var child in treeView.Items.Cast<object>())
                {
                    container = FindContainer(p, (TreeViewItem)view.ItemContainerGenerator.ContainerFromItem(child));

                    if (container != null)
                    {
                        return container;
                    }
                }

                return null;
            }

            private DependencyObject FindContainer(object p, TreeViewItem tvi)
            {
                var generator = tvi.ItemContainerGenerator;

                var container = generator.ContainerFromItem(p);
                if (container != null)
                {
                    return container;
                }
                else
                {
                    if (generator.Status != GeneratorStatus.ContainersGenerated)
                    {
                        delayedSelectedObject = p;
                        generator.ItemsChanged += Generator_ItemsChanged;
                        generator.StatusChanged += Generator_StatusChanged;
                    }
                }

                return null;
            }

            private void Generator_StatusChanged(object sender, EventArgs e)
            {
                var generator = (ItemContainerGenerator)sender;
                generator.StatusChanged -= Generator_StatusChanged;

                ChangeSelectedItem(delayedSelectedObject);
                // delayedSelectedObject = null;
            }

            private void Generator_ItemsChanged(object sender, ItemsChangedEventArgs e)
            {
                var generator = (ItemContainerGenerator)sender;
                generator.ItemsChanged -= Generator_ItemsChanged;

                ChangeSelectedItem(delayedSelectedObject);
                // delayedSelectedObject = null;
            }
        }
    }
}
