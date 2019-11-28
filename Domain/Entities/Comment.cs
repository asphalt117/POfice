using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int CustId { get; set; }
        [DisplayName("Отзыв")]
        public string Note { get; set; }
        [DisplayName("Ответ")]
        public string Response { get; set; }

    }
}
