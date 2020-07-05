using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Queries;

namespace CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.GetPriceListItems
{
    public class GetPriceListItemsQueryHandler : IQueryHandler<GetPriceListItemsQuery, List<PriceListItemDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetPriceListItemsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<PriceListItemDto>> Handle(GetPriceListItemsQuery request, CancellationToken cancellationToken)
        {
            return await PriceListProvider.GetPriceListItems(_sqlConnectionFactory.GetOpenConnection());
        }
    }
}