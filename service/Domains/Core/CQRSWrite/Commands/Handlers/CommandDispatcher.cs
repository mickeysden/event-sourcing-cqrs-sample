using System;
using System.Linq;
using System.Reflection;

namespace EventSourcingCQRS.Domains.Core.CQRSWrite.Commands.Handlers
{
    public class CommandDispatcher
    {
        public void Dispatch<TCommand>(TCommand command) where TCommand : class
        {
            //derive a type based on the ICommand interface and the generic method argument
            Type handler = typeof(ICommandHandler<>);
            Type handlerType = handler.MakeGenericType(command.GetType());

            //Find any concrete classes that implements the interface ICommandHandler<TCommand>
            Type[] concreteTypes = Assembly.GetExecutingAssembly().GetTypes()
                                    .Where(t => t.IsClass && t.GetInterfaces().Contains(handlerType))
                                    .ToArray();

            // Invoke “Handle” on the concrete handler class
            if (!concreteTypes.Any()) return;

            foreach (Type type in concreteTypes)
            {
                var concreteHandler = Activator.CreateInstance(type) as ICommandHandler<TCommand>;
                concreteHandler?.Handle(command);
            }
        }
    }
}