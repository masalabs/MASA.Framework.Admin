namespace MASA.Framework.Admin.Infrastructure
{
    public interface IEventBus
    {
        void Publish(object model);
    }
}
