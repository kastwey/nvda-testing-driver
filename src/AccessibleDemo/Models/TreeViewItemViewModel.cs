using System.Collections.Generic;

namespace AccessibleDemo.Models
{

	public class TreeViewItemViewModel
	{

		public string Value { get; set; }

		public string Text { get; set; }

		public List<TreeViewItemViewModel> Children { get; set; }
	}
}