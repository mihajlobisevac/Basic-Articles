using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BasicArticles.Shared
{
    public class CommentModel
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(500)]
        public string BodyText { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long Article { get; set; }
        public string User { get; set; }
    }
}
