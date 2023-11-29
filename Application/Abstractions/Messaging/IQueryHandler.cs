namespace Application.Abstractions.Messaging
{
    public interface IQueryHandler<in Tquery, TResponse> where Tquery : IQuery<TResponse>
    {
        Task<TResponse> Handle(Tquery query, CancellationToken cancellationToken);
    }
}
