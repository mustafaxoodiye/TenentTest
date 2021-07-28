namespace WebApplication1.Models
{
    public class Student : MustHaveTenant<long>
    {
        public string Name { get; set; }

        public string Class { get; set; }
    }
}
