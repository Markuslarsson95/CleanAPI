namespace Application.Dtos
{
    public class AnimalUserDto
    {
        public Guid UserId { get; set; }
        public List<Guid> AnimalId { get; set; }
    }
}
