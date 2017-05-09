using Nop.Core.Domain.BizManagement;
using System;
using System.Collections.Generic;

namespace Nop.Services.BizManagement
{
    public interface IPurchaseInvoiceService
    {
        IList<PurchaseInvoiceResult> Search(
            DateTime? fromDate,
            DateTime? toDate,
            int? storeId,
            int? status,
            string invoiceNo,
            string refNo,
            string productCode,
            string barcode,
            int pageIndex, int pageSize,
            out int totalRecord,
            out decimal sumAmount,
            out int sumQty);

        IList<PurchaseInvoiceDetailLineResult> GetPurchaseInvoiceDetail(
            int invoiceId,
            int pageIndex, int pageSize,
            out int totalRecord);

        void PurchaseInvoiceApprove(
            int invoiceId,
            int creditAcctId,
            int? objId,
            string desc,
            string user);

        void PurchaseInvoiceUnPost(
            int invoiceId,
            string user);
    }
}
