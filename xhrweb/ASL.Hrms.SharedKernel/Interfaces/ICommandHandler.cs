using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Interfaces
{
    public interface IBaseCommandHandler
    {
    }

    public interface ICommandHandler<in TCommand> : IBaseCommandHandler where TCommand : ICommand
    {
        Task Handle(TCommand command); 
    }
}
