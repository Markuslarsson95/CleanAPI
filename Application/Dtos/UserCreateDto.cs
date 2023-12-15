namespace Application.Dtos
{
    public class UserCreateDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<DogDto> Dogs { get; set; }
        public List<CatDto> Cats { get; set; }
        public List<BirdDto> Birds { get; set; }
    }
}
