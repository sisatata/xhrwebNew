using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(BaseDomainEvent domainEvent);
    }
}
