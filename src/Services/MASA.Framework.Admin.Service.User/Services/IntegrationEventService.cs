using Dapr;
using Masa.Framework.Admin.Contracts.Authentication;

namespace Masa.Framework.Admin.Service.User.Services
{
    public class IntegrationEventService : ServiceBase
    {
        private const string DAPR_PUBSUB_NAME = "pubsub";

        public IntegrationEventService(IServiceCollection services) : base(services)
        {
            App.MapPost(Routing.GroupPermissionNotify, AddGroupPermissionAsync);
        }

        [Topic(DAPR_PUBSUB_NAME, nameof(AddGroupPermissionIntegraionEvent))]
        public async Task AddGroupPermissionAsync(
        AddGroupPermissionIntegraionEvent integrationEvent,
        [FromServices] IEventBus eventBus)
        {
            await eventBus.PublishAsync(new CreateGroupPermissionCommand(integrationEvent.GroupId, integrationEvent.PermissionId));
        }
    }
}
