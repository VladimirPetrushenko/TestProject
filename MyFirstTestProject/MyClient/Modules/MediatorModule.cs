using MediatR.Pipeline;
using MediatR;
using Autofac;

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
            this.RegisterPipeline(builder);

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

        private void RegisterPipeline(ContainerBuilder builder)
        {
            //// register Mediator pre- and post-processor pipelines to activate custom IRequestPreProcessor<> and IRequestPostProcessor<>
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
