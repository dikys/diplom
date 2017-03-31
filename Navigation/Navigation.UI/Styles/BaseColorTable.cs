using System.Drawing;
using System.Windows.Forms;

namespace Navigation.UI.Styles
{
    public class BaseColorTable : ProfessionalColorTable
    {
        #region Button Style
        // Когда нажали
        public override Color ButtonCheckedHighlightBorder => Color.FromArgb(24, 49, 82);
        public override Color ButtonPressedHighlight => Color.FromArgb(24, 49, 82);

        // При наведении
        public override Color ButtonSelectedHighlight => Color.FromArgb(24, 49, 82);
        public override Color ButtonSelectedBorder => Color.FromArgb(24, 49, 82);
        #endregion

        #region Item Style
        // Цвет полоски над всеми элементами
        public override Color MenuBorder => Color.FromArgb(24, 49, 82);

        // Цвет квадратика перед элементом в выпадающем списке
        public override Color ImageMarginGradientBegin => Color.FromArgb(24, 49, 82);
        public override Color ImageMarginGradientEnd => Color.FromArgb(24, 49, 82);
        public override Color ImageMarginGradientMiddle => Color.FromArgb(24, 49, 82);

        // Цвет элементов в выпадающем списке
        public override Color ToolStripDropDownBackground => Color.FromArgb(24, 49, 82);
        #endregion

        /////////////////////////
        /*public override Color ButtonCheckedGradientBegin => Color.Black;
        public override Color ButtonCheckedGradientEnd => Color.Black;
        public override Color ButtonCheckedGradientMiddle => Color.Black;
        public override Color ButtonPressedBorder => Color.Black;
        public override Color ButtonPressedGradientBegin => Color.Black;
        public override Color ButtonPressedGradientEnd => Color.Black;
        public override Color ButtonPressedGradientMiddle => Color.Black;
        public override Color ButtonPressedHighlightBorder => Color.Black;
        /*public override Color ButtonSelectedGradientBegin => Color.Black;
        public override Color ButtonSelectedGradientEnd => Color.Black;
        public override Color ButtonSelectedGradientMiddle => Color.Black;
        public override Color ButtonSelectedHighlightBorder => Color.Black;
        public override Color CheckBackground => Color.Black;
        public override Color CheckPressedBackground => Color.Black;
        public override Color CheckSelectedBackground => Color.Black;
        public override Color GripDark => Color.Black;
        public override Color GripLight => Color.Black;
        public override Color ImageMarginRevealedGradientBegin => Color.Black;
        public override Color ImageMarginRevealedGradientEnd => Color.Black;
        public override Color ImageMarginRevealedGradientMiddle => Color.Black;
        public override Color MenuItemBorder => Color.Black; 
        public override Color MenuItemPressedGradientBegin => Color.Black;
        public override Color MenuItemPressedGradientEnd => Color.Black;
        public override Color MenuItemPressedGradientMiddle => Color.Black;
        public override Color MenuItemSelected => Color.Black;
        public override Color MenuItemSelectedGradientBegin => Color.Black;
        public override Color MenuItemSelectedGradientEnd => Color.Black;
        public override Color MenuStripGradientBegin => Color.Black;
        public override Color MenuStripGradientEnd => Color.Black;
        public override Color OverflowButtonGradientBegin => Color.Black;
        public override Color OverflowButtonGradientEnd => Color.Black;
        public override Color OverflowButtonGradientMiddle => Color.Black;
        public override Color RaftingContainerGradientBegin => Color.Black;
        public override Color RaftingContainerGradientEnd => Color.Black;
        public override Color SeparatorDark => Color.Black;
        public override Color SeparatorLight => Color.Black;
        public override Color StatusStripGradientBegin => Color.Black;
        public override Color StatusStripGradientEnd => Color.Black;
        public override Color ToolStripBorder => Color.Black;
        public override Color ToolStripContentPanelGradientBegin => Color.Black;
        public override Color ToolStripContentPanelGradientEnd => Color.Black;
        public override Color ToolStripGradientBegin => Color.Black;
        public override Color ToolStripGradientEnd => Color.Black;
        public override Color ToolStripGradientMiddle => Color.Black;
        public override Color ToolStripPanelGradientBegin => Color.Black;
        public override Color ToolStripPanelGradientEnd => Color.Black;*/
    }
}
