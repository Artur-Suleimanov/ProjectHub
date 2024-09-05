using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using System.Windows.Controls;

namespace PHDesktopUI.Views
{
    /// <summary>
    /// Логика взаимодействия для TaskView.xaml
    /// </summary>
    public partial class TaskView : UserControl
    {
        public TaskView()
        {
            InitializeComponent();
            //cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            //cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }

        //private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        //{
        //    object temp = RtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
        //    btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
        //    temp = RtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
        //    btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
        //    temp = RtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
        //    btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

        //    temp = RtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
        //    cmbFontFamily.SelectedItem = temp;
        //    temp = RtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
        //    cmbFontSize.Text = temp.ToString();
        //}

        //private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    OpenFileDialog dlg = new OpenFileDialog();
        //    dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
        //    if (dlg.ShowDialog() == true)
        //    {
        //        FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
        //        TextRange range = new TextRange(RtbEditor.Document.ContentStart, RtbEditor.Document.ContentEnd);
        //        range.Load(fileStream, DataFormats.Rtf);
        //    }
        //}

        //private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    SaveFileDialog dlg = new SaveFileDialog();
        //    dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
        //    if (dlg.ShowDialog() == true)
        //    {
        //        FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
        //        TextRange range = new TextRange(RtbEditor.Document.ContentStart, RtbEditor.Document.ContentEnd);
        //        range.Save(fileStream, DataFormats.Rtf);
        //    }
        //}

        //private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (cmbFontFamily.SelectedItem != null)
        //        RtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
        //}

        //private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    RtbEditor.Document.PageWidth = MainGrid.ColumnDefinitions[1].ActualWidth;
        //    RtbEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.Text);
        //}

        //private void RichTextBox1_Pasting(object sender, DataObjectPastingEventArgs e)
        //{
        //    if (e.FormatToApply == "Bitmap")
        //    {
        //        e.CancelCommand();
        //    }
        //}
    }
}
