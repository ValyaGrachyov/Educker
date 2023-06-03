using System.ComponentModel.DataAnnotations;

namespace Eduker.Areas.Admin.ViewModels.EventsVM
{
    public class AddEventsVM
    {
        [Required(ErrorMessage = "Неверное название события")]
        [MaxLength(256)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Неверное описание события")]
        [MaxLength(256)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Неверное время начала события")]
        [DataType(DataType.Date, ErrorMessage = "Введите дату")]
        [DisplayFormat(DataFormatString = "dd.MM.yyyy", ApplyFormatInEditMode = true)]
        public DateTime? TimeStart { get; set; }
        [Required(ErrorMessage = "Неверное время конца события")]
        [DataType(DataType.Date, ErrorMessage = "Введите дату")]
        [DisplayFormat(DataFormatString = "dd.MM.yyyy", ApplyFormatInEditMode = true)]
        public DateTime? TimeEnd { get; set; }

        [Required(ErrorMessage = "Неверное имя преподавателя")]
        [MaxLength(256)]
        public string EducatorName { get; set; }
        [Required(ErrorMessage = "Неверный адресс проведения события")]
        [MaxLength(256)]
        public string Address { get; set; }
        [Required(ErrorMessage = "Неверно выбран язык события")]
        [MaxLength(256)]
        public string Language { get; set; }
        [Required(ErrorMessage = "Неверно указана цена события")]
        public int Price { get; set; }
        public string ImgPath { get; set; }
    }
}
