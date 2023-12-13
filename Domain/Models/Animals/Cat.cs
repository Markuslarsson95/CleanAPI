namespace Domain.Models.Animals
{
    public class Cat : Animal
    {
        public bool LikesToPlay { get; set; }
        public string Breed { get; set; }
        public int Weight { get; set; }
    }
}
