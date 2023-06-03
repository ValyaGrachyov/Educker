using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    //курс обозревают ЮЗЕРЫ
    public class CourseReview
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public int Raiting { get; set; }

        [ForeignKey(nameof(CourseId))] public int? CourseId { get; }
        public virtual Course Course { get; set; }

        [ForeignKey(nameof(UserInfoId))] public int? UserInfoId { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}