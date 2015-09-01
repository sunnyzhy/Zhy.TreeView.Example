using System.Windows;
using System.Windows.Controls;

namespace Zhy.TreeView.Example
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void chx_Click(object sender, RoutedEventArgs e)
        {
            CheckBox chx = sender as CheckBox;
            TreeViewItem item = chx.Parent as TreeViewItem;
            if (chx.IsChecked.Value)
            {
                CheckedChildren(item);
                CheckedParent(item);
            }
            else
            {
                UnCheckedChildren(item);
                UnCheckedParent(item);
            }
        }

        #region CheckedChildren
        /// <summary>
        /// 设置当前节点下的所有子节点处于选中状态
        /// </summary>
        /// <param name="parent"></param>
        private void CheckedChildren(TreeViewItem parent)
        {
            foreach (TreeViewItem child in parent.Items)
            {
                (child.Header as CheckBox).IsChecked = true;
                CheckedChildren(child);
            }
        }
        #endregion

        #region UnCheckedChildren
        /// <summary>
        /// 设置当前节点下的所有子节点处于非选中状态
        /// </summary>
        /// <param name="parent"></param>
        private void UnCheckedChildren(TreeViewItem parent)
        {
            foreach (TreeViewItem child in parent.Items)
            {
                (child.Header as CheckBox).IsChecked = false;
                UnCheckedChildren(child);
            }
        }
        #endregion

        #region CheckedParent
        /// <summary>
        /// 判断与当前节点处于同一级的所有节点是否都为选中状态，如果是，那么设置当前节点的父节点为选中状态
        /// </summary>
        /// <param name="item"></param>
        private void CheckedParent(TreeViewItem item)
        {
            if (item.Parent is System.Windows.Controls.TreeView)
            {
                return;
            }
            TreeViewItem parent = item.Parent as TreeViewItem;
            int childrenCount = 0;
            GetChildrenCount(parent, ref childrenCount);
            int checkedChildrenCount = 0;
            GetCheckedChildren(parent, ref checkedChildrenCount);

            if (childrenCount == checkedChildrenCount)
            {
                (parent.Header as CheckBox).IsChecked = true;
            }

            CheckedParent(item.Parent as TreeViewItem);
        }

        /// <summary>
        /// 获取所有子节点的个数
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="count"></param>
        private void GetChildrenCount(TreeViewItem parent, ref int count)
        {
            foreach (TreeViewItem child in parent.Items)
            {
                count++;
                GetChildrenCount(child, ref count);
            }
        }

        /// <summary>
        /// 获取所有处于选中状态的子节点个数
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="count"></param>
        private void GetCheckedChildren(TreeViewItem parent, ref int count)
        {
            foreach (TreeViewItem child in parent.Items)
            {
                if ((child.Header as CheckBox).IsChecked.Value)
                {
                    count++;
                }
                GetCheckedChildren(child, ref count);
            }
        }
        #endregion

        #region UnCheckedParent
        /// <summary>
        /// 判断与当前节点处于同一级的所有节点中是否含有未选中状态的节点，如果有，那么设置当前节点的父节点为非选中状态
        /// </summary>
        /// <param name="item"></param>
        private void UnCheckedParent(TreeViewItem item)
        {
            if (item.Parent is System.Windows.Controls.TreeView)
            {
                return;
            }
            TreeViewItem parent = item.Parent as TreeViewItem;

            int uncheckedChildrenCount = 0;
            GetUnCheckedChildren(parent, ref uncheckedChildrenCount);

            if (uncheckedChildrenCount > 0)
            {
                (parent.Header as CheckBox).IsChecked = false;
            }

            UnCheckedParent(item.Parent as TreeViewItem);
        }

        /// <summary>
        /// 获取所有处于未选中状态的子节点个数
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="count"></param>
        private void GetUnCheckedChildren(TreeViewItem parent, ref int count)
        {
            foreach (TreeViewItem child in parent.Items)
            {
                if (!(child.Header as CheckBox).IsChecked.Value)
                {
                    count++;
                }
                GetUnCheckedChildren(child, ref count);
            }
        }
        #endregion
    }
}
