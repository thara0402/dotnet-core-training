using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Person
    {
        public int Id { get; set; }

        [DisplayName("コード")]
        [Required(ErrorMessage = "{0}は必須です。")]
        [StringLength(2, ErrorMessage = "{0}は{1}桁以内で入力してください。")]
        public string Code { get; set; }

        [DisplayName("名前")]
        [StringLength(20, ErrorMessage = "{0}は{1}桁以内で入力してください。")]
        public string Name { get; set; }
    }
}
