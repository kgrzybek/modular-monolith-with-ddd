using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Queries;

namespace CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.GetPriceListItems
{
    internal class GetPriceListItemsQueryHandler : IQueryHandler<GetPriceListItemsQuery, List<PriceListItemDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetPriceListItemsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<PriceListItemDto>> Handle(GetPriceListItemsQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.GetOpenConnection();
            
            return await PriceListProvider.GetPriceListItems(connection);
        }
    }
}