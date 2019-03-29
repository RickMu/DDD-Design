using System;
using MediatR;

namespace Domain.Common.Domain
{
    public class DomainEvent: INotification
    {
        
        private DateTime OccuredOn { get; set; }
    }
}