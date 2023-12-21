namespace Domain.Models.Animals
{
    public class Dog : Animal
    {
        public string Bark()
        {
            return "This animal barks";
        }
        public string Breed { get; set; }
        public int Weight { get; set; }
    }
}
