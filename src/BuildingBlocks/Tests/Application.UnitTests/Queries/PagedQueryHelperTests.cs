using CompanyName.MyMeetings.BuildingBlocks.Application.Queries;
using NUnit.Framework;

namespace CompanyName.MyMeetings.BuildingBlocks.Application.UnitTests.Queries
{
    [TestFixture]
    public class PagedQueryHelperTests
    {
        [TestCase(1, 5, 0, 5)]
        [TestCase(3, 10, 20, 10)]
        [TestCase(null, 20, 0, 20)]
        [TestCase(5, null, 0, int.MaxValue)]
        [TestCase(null, null, 0, int.MaxValue)]
        public void PagedQueryHelper_GetPageData_Test(int? page, int? perPage, int offset, int next)
        {
            IPagedQuery query = new TestQuery(page, perPage);
            var pageData = PagedQueryHelper.GetPageData(query);

            Assert.That(pageData, Is.EqualTo(new PageData(offset, next)));
        }

        private class TestQuery : IPagedQuery
        {
            public TestQuery(int? page, int? perPage)
            {
                Page = page;
                PerPage = perPage;
            }

            public int? Page { get; }

            public int? PerPage { get; }
        }
    }
}