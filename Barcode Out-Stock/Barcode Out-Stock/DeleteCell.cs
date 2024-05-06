using System;
using System.Drawing;
using System.Windows.Forms;

namespace Inventory_Data
{
	public class DeleteCell : DataGridViewButtonCell
	{
		private Image del = Image.FromFile(Application.StartupPath + "\\delete01.png");

		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
			graphics.DrawImage(this.del, cellBounds);
		}
	}
}
