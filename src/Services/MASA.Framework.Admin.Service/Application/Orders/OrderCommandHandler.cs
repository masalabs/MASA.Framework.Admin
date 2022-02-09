using MASA.Contrib.Dispatcher.Events.Enums;

namespace MASA.Framework.Admin.Service.Application.Orders
{
    public class OrderCommandHandler
    {
        public OrderCommandHandler()
        {

        }

        [EventHandler(Order = 1)]
        public async Task CreateHandleAsync(OrderCreateCommand command)
        {
            //you work
            await Task.CompletedTask;
        }
    }

    public class OrderStockHandler : CommandHandler<OrderCreateCommand>
    {
        public override Task CancelAsync(OrderCreateCommand comman)
        {
            //cancel todo callback 
            return Task.CompletedTask;
        }

        [EventHandler(FailureLevels = FailureLevels.ThrowAndCancel)]
        public override Task HandleAsync(OrderCreateCommand comman)
        {
            //todo decrease stock
            return Task.CompletedTask;
        }

        [EventHandler(0, FailureLevels.Ignore, IsCancel = true)]
        public Task AddCancelLogs(OrderCreateCommand query)
        {
            //todo increase stock
            return Task.CompletedTask;
        }
    }
}