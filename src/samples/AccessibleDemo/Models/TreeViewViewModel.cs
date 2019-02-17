using System;
using System.Collections.Generic;
using System.Text;

namespace AccessibleDemo.Models
{
	public class TreeViewViewModel
	{

		public string SelectedItem { get; set; }

		public IEnumerable<TreeViewItemViewModel> Items { get; set; }

	}
}
