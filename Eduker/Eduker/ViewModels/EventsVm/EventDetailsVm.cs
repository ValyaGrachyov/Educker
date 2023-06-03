namespace Eduker.ViewModels.EventsVm
{
    public class EventDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string EducatorName { get; set; }
        public string Address { get; set; }
        public int Price { get; set; }
        public string ImgPath { get; set; }
    }
}