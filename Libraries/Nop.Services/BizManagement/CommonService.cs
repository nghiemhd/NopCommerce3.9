using Nop.Core.Data;
using Nop.Core.Domain.BizManagement;
using Nop.Data;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Services.BizManagement
{
    public class CommonService : ICommonService
    {
        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;

        public CommonService(IDbContext dbContext, IDataProvider dataProvider)
        {
            this._dbContext = dbContext;
            this._dataProvider = dataProvider;
        }

        public IList<CF_Store> GetAllStore()
        {
            var rs = _dbContext.SqlQuery<CF_Store>(@"exec [usp_GetAllStores]").ToList();
            return rs;
        }

        public IList<CF_Carrier> GetAllCarrier()
        {
            var rs = _dbContext.SqlQuery<CF_Carrier>(@"exec [usp_GetAllCarriers]").ToList();
            return rs;
        }


        public IList<CF_SalesSource> GetAllSaleSource()
        {
            var rs = _dbContext.SqlQuery<CF_SalesSource>(@"exec [usp_GetAllSalesSources]").ToList();
            return rs;
        }

        public IList<CF_SalesReturnReason> GetAllSaleReturnReason()
        {
            var rs = _dbContext.SqlQuery<CF_SalesReturnReason>(@"exec [usp_GetAllSalesReturnReason]").ToList();
            return rs;
        }
    }
}
