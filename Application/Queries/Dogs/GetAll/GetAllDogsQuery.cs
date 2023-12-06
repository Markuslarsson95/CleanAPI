using Domain.Models;
using MediatR;

namespace Application.Queries.Dogs.GetAll
{
    public class GetAllDogsQuery : IRequest<IEnumerable<Dog>>
    {

    }
}
