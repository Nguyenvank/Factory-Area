using System;
using System.Windows.Forms;

namespace Inventory_Data
{
	public class DeleteColumn : DataGridViewButtonColumn
	{
		public DeleteColumn()
		{
			this.CellTemplate = new DeleteCell();
			base.Width = 20;
		}
	}
}
