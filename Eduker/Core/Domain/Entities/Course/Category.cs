namespace Domain.Entities;

//Главный критерий курса, он ОДИН. Отображается возле названия курса в красивой рамочке!!
public class Category
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
}