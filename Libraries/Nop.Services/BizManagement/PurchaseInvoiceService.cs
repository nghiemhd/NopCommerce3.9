using Nop.Core.Data;
using Nop.Core.Domain.BizManagement;
using Nop.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Nop.Services.BizManagement
{
    public class PurchaseInvoiceService : IPurchaseInvoiceService
    {
        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;

        public PurchaseInvoiceService(IDbContext dbContext, IDataProvider dataProvider)
        {
            this._dbContext = dbContext;
            this._dataProvider = dataProvider;
        }

        public IList<PurchaseInvoiceResult> Search(
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
            out int sumQty)
        {
            var pfromDate = _dataProvider.GetParameter();
            pfromDate.ParameterName = "fromDate";
            if (fromDate == null)
                pfromDate.Value = DBNull.Value;
            else
                pfromDate.Value = fromDate;
            pfromDate.DbType = DbType.DateTime;

            var ptoDate = _dataProvider.GetParameter();
            ptoDate.ParameterName = "toDate";
            if (toDate == null)
                ptoDate.Value = DBNull.Value;
            else
                ptoDate.Value = toDate;
            ptoDate.DbType = DbType.DateTime;

            var pstoreId = _dataProvider.GetParameter();
            pstoreId.ParameterName = "storeId";
            if (storeId == null)
                pstoreId.Value = DBNull.Value;
            else
                pstoreId.Value = storeId.Value;
            pstoreId.DbType = DbType.Int32;

            var pstatus = _dataProvider.GetParameter();
            pstatus.ParameterName = "status";
            if (status == null)
                pstatus.Value = DBNull.Value;
            else
                pstatus.Value = status.Value;
            pstatus.DbType = DbType.Int32;

            var pinvoiceNo = _dataProvider.GetParameter();
            pinvoiceNo.ParameterName = "invoiceNo";
            if (invoiceNo == null)
                pinvoiceNo.Value = DBNull.Value;
            else
                pinvoiceNo.Value = invoiceNo;
            pinvoiceNo.DbType = DbType.String;

            var prefNo = _dataProvider.GetParameter();
            prefNo.ParameterName = "refNo";
            if (refNo == null)
                prefNo.Value = DBNull.Value;
            else
                prefNo.Value = refNo;
            prefNo.DbType = DbType.String;

            var pproductCode = _dataProvider.GetParameter();
            pproductCode.ParameterName = "productCode";
            if (productCode == null)
                pproductCode.Value = DBNull.Value;
            else
                pproductCode.Value = productCode;
            pproductCode.DbType = DbType.String;

            var pbarcode = _dataProvider.GetParameter();
            pbarcode.ParameterName = "barcode";
            if (barcode == null)
                pbarcode.Value = DBNull.Value;
            else
                pbarcode.Value = barcode;
            pbarcode.DbType = DbType.String;

            var ppageIndex = _dataProvider.GetParameter();
            ppageIndex.ParameterName = "pageIndex";
            ppageIndex.Value = pageIndex;
            ppageIndex.DbType = DbType.Int32;

            var ppageSize = _dataProvider.GetParameter();
            ppageSize.ParameterName = "pageSize";
            ppageSize.Value = pageSize;
            ppageSize.DbType = DbType.Int32;

            var pTotalRecords = _dataProvider.GetParameter();
            pTotalRecords.ParameterName = "count";
            pTotalRecords.Direction = ParameterDirection.Output;
            pTotalRecords.DbType = DbType.Int32;

            var psumAmount = _dataProvider.GetParameter();
            psumAmount.ParameterName = "sumAmount";
            psumAmount.Direction = ParameterDirection.Output;
            psumAmount.DbType = DbType.Decimal;

            var psumQty = _dataProvider.GetParameter();
            psumQty.ParameterName = "sumQty";
            psumQty.Direction = ParameterDirection.Output;
            psumQty.DbType = DbType.Int32;

            var rs = _dbContext.SqlQuery<PurchaseInvoiceResult>(
                @"exec usp_PurchaseInvoiceSearch @fromDate, @toDate, @storeId, @status, @invoiceNo, @refNo, @productCode, @barcode, @pageIndex, @pageSize, @count output, @sumAmount output, @sumQty output",
                pfromDate, ptoDate, pstoreId, pstatus, pinvoiceNo, prefNo, pproductCode, pbarcode, ppageIndex, ppageSize, pTotalRecords, psumAmount,psumQty).ToList();

            totalRecord =(int)pTotalRecords.Value;
            sumAmount = (decimal)psumAmount.Value;
            sumQty = (int)psumQty.Value;

            return rs;
        }

        public IList<PurchaseInvoiceDetailLineResult> GetPurchaseInvoiceDetail(
            int invoiceId,
            int pageIndex, int pageSize,
            out int totalRecord)
        {

            var pinvoiceId = _dataProvider.GetParameter();
            pinvoiceId.ParameterName = "invoiceId";
            pinvoiceId.Value = invoiceId;
            pinvoiceId.DbType = DbType.Int32;

            var ppageIndex = _dataProvider.GetParameter();
            ppageIndex.ParameterName = "pageIndex";
            ppageIndex.Value = pageIndex;
            ppageIndex.DbType = DbType.Int32;

            var ppageSize = _dataProvider.GetParameter();
            ppageSize.ParameterName = "pageSize";
            ppageSize.Value = pageSize;
            ppageSize.DbType = DbType.Int32;

            var pTotalRecords = _dataProvider.GetParameter();
            pTotalRecords.ParameterName = "count";
            pTotalRecords.Direction = ParameterDirection.Output;
            pTotalRecords.DbType = DbType.Int32;

            var rs = _dbContext.SqlQuery<PurchaseInvoiceDetailLineResult>(
                @"exec usp_GetPurchaseInvoiceDetailById @invoiceId, @pageIndex, @pageSize, @count output", 
                pinvoiceId, ppageIndex, ppageSize, pTotalRecords).ToList();

            totalRecord = (int)pTotalRecords.Value;

            return rs;
        }


        public void PurchaseInvoiceApprove(
            int invoiceId,
            int creditAcctId,
            int? objId,
            string desc,
            string user)
        {
            var pinvoiceId = _dataProvider.GetParameter();
            pinvoiceId.ParameterName = "invoiceId";
            pinvoiceId.Value = invoiceId;
            pinvoiceId.DbType = DbType.Int32;

            var pcreditAcctId = _dataProvider.GetParameter();
            pcreditAcctId.ParameterName = "creditAcctId";
            pcreditAcctId.Value = creditAcctId;
            pcreditAcctId.DbType = DbType.Int32;

            var pobjId = _dataProvider.GetParameter();
            pobjId.ParameterName = "objId";
            pobjId.Value = (object)objId ?? DBNull.Value;
            pobjId.DbType = DbType.Int32;

            var pdesc = _dataProvider.GetParameter();
            pdesc.ParameterName = "desc";
            pdesc.Value = (object)desc ?? DBNull.Value;
            pdesc.DbType = DbType.String;

            var puser = _dataProvider.GetParameter();
            puser.ParameterName = "user";
            puser.Value = (object)user ?? DBNull.Value;
            puser.DbType = DbType.String;

            _dbContext.ExecuteSqlCommand(
                @"exec usp_PurchaseInvoice_Approve @invoiceId, @creditAcctId, @objId, @desc, @user", 
                false, 10000, pinvoiceId, pcreditAcctId, pobjId, pdesc, puser);
        }

        public void PurchaseInvoiceUnPost(
            int invoiceId,
            string user
            )
        {
            var pinvoiceId = _dataProvider.GetParameter();
            pinvoiceId.ParameterName = "invoiceId";
            pinvoiceId.Value = invoiceId;
            pinvoiceId.DbType = DbType.Int32;

            var puser = _dataProvider.GetParameter();
            puser.ParameterName = "user";
            puser.Value = (object)user ?? DBNull.Value;
            puser.DbType = DbType.String;

            _dbContext.ExecuteSqlCommand(
                @"exec usp_PurchaseInvoice_UnPost @invoiceId, @user", 
                false, 10000, pinvoiceId, puser);
        }
    }
}
