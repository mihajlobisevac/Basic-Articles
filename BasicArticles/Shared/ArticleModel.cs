using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BasicArticles.Shared
{
    public class ArticleModel
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(150)]
        public string Title { get; set; }
        [MaxLength(300)]
        public string IntroText { get; set; }
        [MaxLength(3000)]
        public string BodyText { get; set; }
        [MaxLength(100)]
        public string ImagePath { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Category { get; set; }
        public string User { get; set; }
    }
}
