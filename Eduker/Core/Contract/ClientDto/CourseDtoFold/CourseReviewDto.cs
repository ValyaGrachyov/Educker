using Contract.ClientDto.UserDto;

namespace Contract.ClientDto.CourseDtoFold;

public class CourseReviewDto
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public string Description { get; set; }
    public int Raiting { get; set; }
    
    public virtual UserInfoDto UserInfoDto { get; set; }
}