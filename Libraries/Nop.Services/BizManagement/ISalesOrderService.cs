using Nop.Core.Domain.BizManagement;
using System.Collections.Generic;

namespace Nop.Services.BizManagement
{
    public interface ISalesOrderService
    {
        IList<SalesOrderResult> Search(
            SalesOrderSearchDTO searchCondition,
            int pageIndex, int pageSize,
            out int totalRecord,
            out decimal sumSubTotal,
            out decimal sumDiscount,
            out decimal sumShippingFee,
            out decimal sumAeroShippingFee,
            out decimal sumAmount,
            out decimal sumCostAmount,
            out int sumQty);

        IList<SalesOrderDetailLineResult> GetSalesOrderDetail(int orderId);

        void SalesOrderApprove(
            int orderId,
            int debitAcctId,
            int? objId,
            int? feeAcctId,
            decimal? feeAmount,
            string desc,
            string user);

        void SalesOrderUnPost(
            int orderId,
            string user);

        POS_SalesOrder GetSalesOrderById(int orderId);

        void UpdateSalesOrder(UpdatedSalesOrderDTO order);
    }
}
