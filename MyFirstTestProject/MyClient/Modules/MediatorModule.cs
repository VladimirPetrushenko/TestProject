using MediatR.Pipeline;
using MediatR;
using Autofac;
using FluentValidation;

namespace MyClient.Modules
{
    public class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            this.RegisterMessageHandlers(builder);
            RegisterPipeline(builder);

            builder.Register<ServiceFactory>(
                ctx =>
                {
                    var c = ctx.Resolve<IComponentContext>();
                    return t => c.Resolve(t);
                });
        }

        private void RegisterMessageHandlers(ContainerBuilder builder)
        {
            var mediatorOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
                typeof(IValidator<>),
            };

            foreach (var mediatorOpenType in mediatorOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(this.ThisAssembly)
                    .AsClosedTypesOf(mediatorOpenType)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            }
        }

        private static void RegisterPipeline(ContainerBuilder builder)
        {
            builder
                .RegisterGeneric(typeof(RequestPreProcessorBehavior<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerDependency();

            builder
                .RegisterGeneric(typeof(RequestPostProcessorBehavior<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerDependency();
        }
    }
}
