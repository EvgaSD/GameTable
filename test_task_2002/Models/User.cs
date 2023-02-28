namespace test_task_2002.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Status { get; set; }

       public string NickName { get; set; }      

       public List<Game> Games { get; set; } = new(); 
    }
}
