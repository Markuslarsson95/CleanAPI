namespace Application.Exceptions
{
    public class EntityNotFoundException : BaseCustomException
    {
        public EntityNotFoundException(string name, object id) : base($"Entity {name} with Id of {id} was not found IN THE DATABASE.")
        {

        }
    }
}
