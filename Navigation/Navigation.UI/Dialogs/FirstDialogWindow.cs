using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Navigation.App.Dialogs;
using Navigation.App.Dialogs.Elements;
using Navigation.App.Extensions;
using Navigation.UI.Extensions;
using Navigation.UI.Windows;
using DialogResult = System.Windows.Forms.DialogResult;

namespace Navigation.UI.Dialogs
{
    public class FirstDialogWindow : BaseWindow, IDialogWindow
    {
        public FirstDialogWindow(params DialogElement[] elements)
        {
            Elements = elements;

            var table = new TableLayoutPanel()
                .WithProperty("Dock", DockStyle.Fill)
                .WithColumnStyles(new ColumnStyle(SizeType.Percent, 100))
                .WithRowStyles(new RowStyle(SizeType.Percent, 100))
                .WithRowStyles(new RowStyle(SizeType.Absolute, 30));

            var inputTable = new TableLayoutPanel() {Dock = DockStyle.Fill}
                .WithColumnStyles(new ColumnStyle(SizeType.Percent, 30))
                .WithColumnStyles(new ColumnStyle(SizeType.Percent, 70));

            Elements.Select((name, i) => new {Item = name, Index = i}).ToList().ForEach(it =>
            {
                inputTable.Controls.Add(new Label()
                            .WithProperty("TextAlign", ContentAlignment.MiddleRight)
                            .WithProperty("ForeColor", Color.Black)
                            .WithText(it.Item.Name), 0, it.Index);

                switch (it.Item.Type)
                {
                    case DialogTypes.Input:
                        var textBox = new TextBox().TuneControl();

                        textBox.TextChanged += (s, e) => Elements[it.Index].Value = textBox.Text;

                        inputTable.WithRowStyles(new RowStyle(SizeType.Absolute, 20));
                        
                        inputTable.Controls.Add(textBox, 1, it.Index);

                        break;
                    case DialogTypes.Text:
                        inputTable.Controls.Add(new Label()
                            .WithProperty("TextAlign", ContentAlignment.MiddleRight)
                            .WithProperty("ForeColor", Color.Black)
                            .WithText(it.Item.Value), 1, it.Index);

                        break;
                }
            });
            table.Controls.Add(inputTable, 0, 0);

            table.Controls.Add(
                new Panel()
                    .WithProperty("Dock", DockStyle.Bottom)
                    .WithControls(
                        new Button().TuneControl().WithText("Сохранить").WithOnClick((s, e) =>
                        {
                            DialogResult = DialogResult.OK;
                            Close();
                        })),
                0,
                1);

            MainPanel.Controls.Add(table);
        }

        public DialogElement[] Elements { get; }

        public ResultOfDialog OpenDialog() => ShowDialog().ToResultOfDialog();
    }
}
