using System;

namespace ASL.Hrms.SharedKernel.Interfaces
{
    public interface ICommandBase { }
    
    public interface ICommand : ICommandBase
    {
        DateTime CommandPublished { get; set; }
        string UserId { get; set; }
    }
}
