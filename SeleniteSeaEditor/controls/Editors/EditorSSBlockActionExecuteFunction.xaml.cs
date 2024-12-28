using SeleniteSeaCore.codeblocks;
using SeleniteSeaCore.codeblocks.actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SeleniteSeaEditor.controls.Editors
{
    /// <summary>
    /// Logika interakcji dla klasy EditorSSBlockActionExecuteFunction.xaml
    /// </summary>
    public partial class EditorSSBlockActionExecuteFunction : Window
    {
        SSBlock? previousSelection = null;
        private readonly SSBlockActionExecuteFunction ToEdit;
        public EditorSSBlockActionExecuteFunction(SSBlockActionExecuteFunction toEdit)
        {
            InitializeComponent();
            ToEdit = toEdit;
            foreach (var i in EditorCore.LoadedFunctions)
                FunctionName.Items.Add(i.Key);
            if(ToEdit.Function != "")
                FunctionName.SelectedItem = EditorCore.LoadedFunctions.Keys.FirstOrDefault(k=>k?.Title == ToEdit.Function,null);
            VarReturn.Text = ToEdit.TargetVariable.Data;
        }

        private void FunctionName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ArgBox.Children.Clear();
            if (FunctionName.SelectedItem is not SSBlockScopeFunction func)
                return;
            if (previousSelection == func)
                return;
            foreach(var i in func.RequestedVariables)
            {
                string val = (ToEdit.PassedValues.ContainsKey(i.Key) && ToEdit.Function == func.Title) ? ToEdit.PassedValues[i.Key].Data : i.Value.defaultValue;
                ArgBox.Children.Add(new SSValueEditor(i.Key, val));
            }
            previousSelection = func;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ToEdit.PassedValues.Clear();
            foreach(var i in ArgBox.Children)
            {
                if (i is not SSValueEditor ed)
                    continue;
                ToEdit.PassedValues.Add(ed.Title, new(ed.Value));
            }
            ToEdit.TargetVariable = new(VarReturn.Text);
            ToEdit.Function = FunctionName.SelectedItem.ToString()??"";
            DialogResult = true;
        }
    }
}
