using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Navigation.App.Dialogs;
using Navigation.App.Dialogs.Elements;
using Navigation.App.Extensions;
using Navigation.UI.Extensions;
using DialogResult = System.Windows.Forms.DialogResult;

namespace Navigation.UI.Views
{
    public class DialogWindow : BaseWindow, IDialogWindow
    {
        public DialogWindow(Size size, params DialogElement[] elements)
        {
            Width = size.Width;
            Height = size.Height;

            Elements = elements;

            var table = new TableLayoutPanel()
                .WithProperty("Dock", DockStyle.Fill)
                .WithColumnStyles(new ColumnStyle(SizeType.Percent, 100))
                .WithRowStyles(new RowStyle(SizeType.Percent, 100))
                .WithRowStyles(new RowStyle(SizeType.Absolute, 30));

            var inputTable = new TableLayoutPanel() {Dock = DockStyle.Fill}
                .WithColumnStyles(new ColumnStyle(SizeType.Percent, 30))
                .WithColumnStyles(new ColumnStyle(SizeType.Percent, 70));

            Elements.Select((it, i) => new {Item = it, Index = i})
                .ToList()
                .ForEach(it =>
                {
                    inputTable.Controls.Add(new Label()
                        .WithProperty("TextAlign", ContentAlignment.MiddleRight)
                        .WithProperty("ForeColor", Color.Black)
                        .WithProperty("Text", it.Item.Name),
                        0, it.Index);

                    switch (it.Item.ElementType)
                    {
                        case DialogElementTypes.Input:
                            inputTable.WithRowStyles(new RowStyle(SizeType.Absolute, 20));

                            inputTable.Controls.Add(new TextBox()
                                .TuneControl()
                                .WithEventHandler("TextChanged", (s, e) => Elements[it.Index].Value = ((TextBox) s).Text),
                                1, it.Index);

                            break;
                        case DialogElementTypes.Text:
                            inputTable.Controls.Add(new Label()
                                .WithProperty("TextAlign", ContentAlignment.MiddleRight)
                                .WithProperty("ForeColor", Color.Black)
                                .WithProperty("Text", it.Item.Value),
                                1, it.Index);

                            break;
                    }
                });
            table.Controls.Add(inputTable, 0, 0);

            table.Controls.Add(
                new Panel()
                    .WithProperty("Dock", DockStyle.Bottom)
                    .WithItems("Controls",
                        new Button()
                            .TuneControl()
                            .WithProperty("Text", "Сохранить")
                            .WithEventHandler("Click", (s, e) =>
                            {
                                DialogResult = DialogResult.OK;
                                Close();
                            })),
                0, 1);

            MainPanel.Controls.Add(table);
        }

        public DialogElement[] Elements { get; }

        public ResultOfDialog OpenDialog() => ShowDialog().ToResultOfDialog();
    }
}
