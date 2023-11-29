namespace Application.Abstractions.Messaging
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command, CancellationToken cancellationToken);
    }

    public interface ICommandHandler<in Tcommand, TResponse> where Tcommand : ICommand<TResponse>
    {
        Task<TResponse> Handle(Tcommand command, CancellationToken cancellationToken);
    }
}
