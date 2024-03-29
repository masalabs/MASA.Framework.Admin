﻿using Masa.Framework.Admin.Contracts.Order.Model;
using Masa.Utils.Caller.HttpClient;

namespace Masa.Framework.Admin.Caller.Callers
{
    public class OrderCaller : HttpClientCallerBase
    {
        protected override string BaseAddress { get; set; } = "http://localhost:6083";

        public OrderCaller(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Name = nameof(OrderCaller);
        }

        public async Task<List<Order>> List()
        {
            return await CallerProvider.GetAsync<List<Order>>($"order/query");
        }
    }
}