using Nop.Core.Domain.BizManagement;
using System.Collections.Generic;

namespace Nop.Services.BizManagement
{
    public interface ICommonService
    {
        IList<CF_Store> GetAllStore();

        IList<CF_Carrier> GetAllCarrier();

        IList<CF_SalesSource> GetAllSaleSource();

        IList<CF_SalesReturnReason> GetAllSaleReturnReason();
    }
}
