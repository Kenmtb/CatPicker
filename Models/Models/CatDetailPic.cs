using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
	[Table("catDetailPics")]
	public class CatDetailPic
	{
		public int Id { get; set; }
		public int? catId { get; set; }
		public string catPicUrl { get; set; }
		public string catPicDescription { get; set; }
	}
}
