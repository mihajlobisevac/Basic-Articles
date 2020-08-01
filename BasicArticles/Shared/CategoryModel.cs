using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BasicArticles.Shared
{
    public class CategoryModel
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
