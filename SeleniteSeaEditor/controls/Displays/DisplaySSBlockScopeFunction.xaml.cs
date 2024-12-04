using SeleniteSeaCore;
using SeleniteSeaCore.codeblocks;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Formats.Asn1.AsnWriter;

namespace SeleniteSeaEditor.controls.Displays
{
    /// <summary>
    /// Logika interakcji dla klasy DisplaySSBlockScopeFunction.xaml
    /// </summary>
    public partial class DisplaySSBlockScopeFunction : DisplayBlock
    {
        private readonly SSBlockScopeFunction ownedScope;
        public override Color Color
        {
            get => _color;
            set
            {
                _color = value;
                PrimaryBorder.Color = value;
                ActionTitle.Foreground = new SolidColorBrush(value);
                Background = new SolidColorBrush(value)
                {
                    Opacity = 0.02
                };
                foreach (var i in ScopeActionsContainer.Children)
                    if (i is ScopeAddButton a)
                        a.Color = value;
            }
        }
        public DisplaySSBlockScopeFunction(SSBlockScopeFunction scope)
        {
            ownedScope = scope;
            InitializeComponent();

            ActionTitle.Content = "Function " + ownedScope.Name;
            var additionbutton = new ScopeAddButton((o, e) => { ClickAddButton(0); });
            ScopeActionsContainer.Children.Add(additionbutton);
            AdjustRequestedVarsVisual();
            ActionParams.MouseUp += (o, e) => RunEditor();
        }
        public void AddAction(DisplayBlock action, int index)
        {
            ScopeActionsContainer.Children.Insert(index + 1, action);
            var additionbutton = new ScopeAddButton((o, e) => { ClickAddButton(index + 2); });
            ScopeActionsContainer.Children.Insert(index + 2, additionbutton);
        }
        //This method is added as an onClick for all Add buttons
        public void ClickAddButton(int targetindex)
        {
            //New selection dialog
            NewBlockWindow dialog = new();
            if (dialog.ShowDialog() == true)
            {
                var type = dialog.Result;
                if (type is not null)
                {
                    //Instantiate and add the display if it isn't null
                    var block = EditorCore.InstantiateTryEditAndGetDisplay(ownedScope, type);
                    if (block is not null)
                        AddAction(block, targetindex);
                }
            }
        }
        public void RunEditor()
        {
            Debug.Log(StatusCode.Info,EditorCore.TryOpenEditor(ownedScope, out _).ToString(),null);

            AdjustRequestedVarsVisual();
        }
        public void AdjustRequestedVarsVisual()
        {
            ActionParams.Content = $"({string.Join(", ",ownedScope.RequestedVariables.Keys)})";
        }
    }
}
