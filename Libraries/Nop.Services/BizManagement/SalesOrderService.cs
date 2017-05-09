using Nop.Core.Data;
using Nop.Core.Domain.BizManagement;
using Nop.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Nop.Services.BizManagement
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;
        private readonly IRepository<POS_SalesOrder> _saleOrderRepository;

        public SalesOrderService(
            IDbContext dbContext, 
            IDataProvider dataProvider, 
            IRepository<POS_SalesOrder> saleOrderRepository)
        {
            this._dbContext = dbContext;
            this._dataProvider = dataProvider;
            this._saleOrderRepository = saleOrderRepository;
        }

        public IList<SalesOrderResult> Search(
            SalesOrderSearchDTO searchCondition,
            int pageIndex, int pageSize, 
            out int totalRecord,
            out decimal sumSubTotal,
            out decimal sumDiscount,
            out decimal sumShippingFee,
            out decimal sumAeroShippingFee,
            out decimal sumAmount,
            out decimal sumCostAmount,
            out int sumQty)
        {
            var pfromDate = _dataProvider.GetParameter();
            pfromDate.ParameterName = "fromDate";
            if (searchCondition.FromDate == null)
                pfromDate.Value = DBNull.Value;
            else
                pfromDate.Value = searchCondition.FromDate;
            pfromDate.DbType = DbType.DateTime;

            var ptoDate = _dataProvider.GetParameter();
            ptoDate.ParameterName = "toDate";
            if (searchCondition.ToDate == null)
                ptoDate.Value = DBNull.Value;
            else
                ptoDate.Value = searchCondition.ToDate;
            ptoDate.DbType = DbType.DateTime;

            var pstoreId = _dataProvider.GetParameter();
            pstoreId.ParameterName = "storeId";
            if (searchCondition.StoreId == null)
                pstoreId.Value = DBNull.Value;
            else
                pstoreId.Value = searchCondition.StoreId.Value;
            pstoreId.DbType = DbType.Int32;

            var pcarrierId = _dataProvider.GetParameter();
            pcarrierId.ParameterName = "carrierId";
            if (searchCondition.CarrierId == null)
                pcarrierId.Value = DBNull.Value;
            else
                pcarrierId.Value = searchCondition.CarrierId.Value;
            pcarrierId.DbType = DbType.Int32;

            var pstatus = _dataProvider.GetParameter();
            pstatus.ParameterName = "status";
            if (searchCondition.PSStatus == null)
                pstatus.Value = DBNull.Value;
            else
                pstatus.Value = searchCondition.PSStatus.Value;
            pstatus.DbType = DbType.Int32;

            var pchanelId = _dataProvider.GetParameter();
            pchanelId.ParameterName = "chanelId";
            if (searchCondition.SaleChanelId == null)
                pchanelId.Value = DBNull.Value;
            else
                pchanelId.Value = searchCondition.SaleChanelId.Value;
            pchanelId.DbType = DbType.Int32;

            var psourceId = _dataProvider.GetParameter();
            psourceId.ParameterName = "sourceId";
            if (searchCondition.SourceId == null)
                psourceId.Value = DBNull.Value;
            else
                psourceId.Value = searchCondition.SourceId.Value;
            psourceId.DbType = DbType.Int32;

            var preturnReasonId = _dataProvider.GetParameter();
            preturnReasonId.ParameterName = "returnReasonId";
            if (searchCondition.ReturnReasonId == null)
                preturnReasonId.Value = DBNull.Value;
            else
                preturnReasonId.Value = searchCondition.ReturnReasonId.Value;
            preturnReasonId.DbType = DbType.Int32;

            var porderNo = _dataProvider.GetParameter();
            porderNo.ParameterName = "orderNo";
            if (searchCondition.OrderNo == null)
                porderNo.Value = DBNull.Value;
            else
                porderNo.Value = searchCondition.OrderNo;
            porderNo.DbType = DbType.String;

            var prefNo = _dataProvider.GetParameter();
            prefNo.ParameterName = "refNo";
            if (searchCondition.RefNo == null)
                prefNo.Value = DBNull.Value;
            else
                prefNo.Value = searchCondition.RefNo;
            prefNo.DbType = DbType.String;

            var pcustomerName = _dataProvider.GetParameter();
            pcustomerName.ParameterName = "customerName";
            if (searchCondition.CustomerName == null)
                pcustomerName.Value = DBNull.Value;
            else
                pcustomerName.Value = searchCondition.CustomerName;
            pcustomerName.DbType = DbType.String;

            var pmobile = _dataProvider.GetParameter();
            pmobile.ParameterName = "mobile";
            if (searchCondition.CustomerPhone == null)
                pmobile.Value = DBNull.Value;
            else
                pmobile.Value = searchCondition.CustomerPhone;
            pmobile.DbType = DbType.String;

            var pproductCode = _dataProvider.GetParameter();
            pproductCode.ParameterName = "productCode";
            if (searchCondition.ProductCode == null)
                pproductCode.Value = DBNull.Value;
            else
                pproductCode.Value = searchCondition.ProductCode;
            pproductCode.DbType = DbType.String;

            var pbarcode = _dataProvider.GetParameter();
            pbarcode.ParameterName = "barcode";
            if (searchCondition.Barcode == null)
                pbarcode.Value = DBNull.Value;
            else
                pbarcode.Value = searchCondition.Barcode;
            pbarcode.DbType = DbType.String;

            var pdesc = _dataProvider.GetParameter();
            pdesc.ParameterName = "desc";
            if (searchCondition.Desc == null)
                pdesc.Value = DBNull.Value;
            else
                pdesc.Value = searchCondition.Desc;
            pdesc.DbType = DbType.String;

            var pshippingCode = _dataProvider.GetParameter();
            pshippingCode.ParameterName = "shippingCode";
            if (searchCondition.ShippingCode == null)
                pshippingCode.Value = DBNull.Value;
            else
                pshippingCode.Value = searchCondition.ShippingCode;
            pshippingCode.DbType = DbType.String;

            var ppaymentType = _dataProvider.GetParameter();
            ppaymentType.ParameterName = "paymentType";
            if (searchCondition.PaymentType == null)
                ppaymentType.Value = DBNull.Value;
            else
                ppaymentType.Value = searchCondition.PaymentType.Value;
            ppaymentType.DbType = DbType.Int32;

            var ppaymentStatus = _dataProvider.GetParameter();
            ppaymentStatus.ParameterName = "paymentStatus";
            if (searchCondition.PaymentStatus == null)
                ppaymentStatus.Value = DBNull.Value;
            else
                ppaymentStatus.Value = searchCondition.PaymentStatus.Value;
            ppaymentStatus.DbType = DbType.Int32;

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

            var psumSubTotal = _dataProvider.GetParameter();
            psumSubTotal.ParameterName = "sumSubTotal";
            psumSubTotal.Direction = ParameterDirection.Output;
            psumSubTotal.DbType = DbType.Decimal;

            var psumDiscount = _dataProvider.GetParameter();
            psumDiscount.ParameterName = "sumDiscount";
            psumDiscount.Direction = ParameterDirection.Output;
            psumDiscount.DbType = DbType.Decimal;

            var psumShippingFee = _dataProvider.GetParameter();
            psumShippingFee.ParameterName = "sumShippingFee";
            psumShippingFee.Direction = ParameterDirection.Output;
            psumShippingFee.DbType = DbType.Decimal;

            var psumAmount = _dataProvider.GetParameter();
            psumAmount.ParameterName = "sumAmount";
            psumAmount.Direction = ParameterDirection.Output;
            psumAmount.DbType = DbType.Decimal;

            var psumAeroShippingFee = _dataProvider.GetParameter();
            psumAeroShippingFee.ParameterName = "sumAeroShippingFee";
            psumAeroShippingFee.Direction = ParameterDirection.Output;
            psumAeroShippingFee.DbType = DbType.Decimal;

            var psumCostAmount = _dataProvider.GetParameter();
            psumCostAmount.ParameterName = "sumCostAmount";
            psumCostAmount.Direction = ParameterDirection.Output;
            psumCostAmount.DbType = DbType.Decimal;

            var psumQty = _dataProvider.GetParameter();
            psumQty.ParameterName = "sumQty";
            psumQty.Direction = ParameterDirection.Output;
            psumQty.DbType = DbType.Int32;

            var sqlCommand = @"exec usp_SalesOrderSearch 
                @fromDate, @toDate, @storeId, @status, @orderNo, @refNo, @customerName, @mobile, @productCode, @barcode, @desc, @carrierId, @chanelId, @sourceId, @returnReasonId, @shippingCode, @paymentType, @paymentStatus, @pageIndex, @pageSize, 
                @count output, @sumSubTotal output, @sumDiscount output, @sumShippingFee output, @sumAeroShippingFee output, @sumAmount output, @sumCostAmount output, @sumQty output";

            var rs = _dbContext.SqlQuery<SalesOrderResult>(
                sqlCommand,
                pfromDate, ptoDate, pstoreId, pstatus, porderNo, prefNo, pcustomerName, pmobile, pproductCode, pbarcode, pdesc, pcarrierId, pchanelId, psourceId, preturnReasonId, pshippingCode, ppaymentType, ppaymentStatus, ppageIndex, ppageSize, 
                pTotalRecords, psumSubTotal, psumDiscount, psumShippingFee, psumAeroShippingFee, psumAmount, psumCostAmount, psumQty).ToList();

            totalRecord =(int)pTotalRecords.Value;
            sumSubTotal = (decimal)psumSubTotal.Value;
            sumDiscount = (decimal)psumDiscount.Value;
            sumShippingFee = (decimal)psumShippingFee.Value;
            sumAeroShippingFee = (decimal)psumAeroShippingFee.Value;
            sumAmount = (decimal)psumAmount.Value;
            sumCostAmount = (decimal)psumCostAmount.Value;
            sumQty = (int)psumQty.Value;

            return rs;
        }

        public IList<SalesOrderDetailLineResult> GetSalesOrderDetail(int orderId)
        {

            var porderId = _dataProvider.GetParameter();
            porderId.ParameterName = "saleOrderId";
            porderId.Value = orderId;
            porderId.DbType = DbType.Int32;

            var rs = _dbContext.SqlQuery<SalesOrderDetailLineResult>(@"exec usp_GetPOSSaleOrderDetailBySaleOrderId @saleOrderId", porderId).ToList();
            
            return rs;
        }

        public void SalesOrderApprove(
            int orderId,
            int debitAcctId,
            int? objId,
            int? feeAcctId,
            decimal? feeAmount,
            string desc,
            string user)
        {
            var porderId = _dataProvider.GetParameter();
            porderId.ParameterName = "orderId";
            porderId.Value = orderId;
            porderId.DbType = DbType.Int32;

            var pdebitAcctId = _dataProvider.GetParameter();
            pdebitAcctId.ParameterName = "debitAcctId";
            pdebitAcctId.Value = debitAcctId;
            pdebitAcctId.DbType = DbType.Int32;

            var pobjId = _dataProvider.GetParameter();
            pobjId.ParameterName = "objId";
            pobjId.Value = (object)objId??DBNull.Value;
            pobjId.DbType = DbType.Int32;

            var pfeeAcctId = _dataProvider.GetParameter();
            pfeeAcctId.ParameterName = "feeAcctId";
            pfeeAcctId.Value = (object)feeAcctId ?? DBNull.Value;
            pfeeAcctId.DbType = DbType.Int32;

            var pfeeAmount = _dataProvider.GetParameter();
            pfeeAmount.ParameterName = "feeAmount";
            pfeeAmount.Value = (object)feeAmount ?? DBNull.Value;
            pfeeAmount.DbType = DbType.Decimal;

            var pdesc = _dataProvider.GetParameter();
            pdesc.ParameterName = "desc";
            pdesc.Value = (object)desc ?? DBNull.Value;
            pdesc.DbType = DbType.String;

            var puser = _dataProvider.GetParameter();
            puser.ParameterName = "user";
            puser.Value = (object)user ?? DBNull.Value;
            puser.DbType = DbType.String;

            _dbContext.ExecuteSqlCommand(
                @"exec usp_SaleOrder_Approve @orderid, @debitAcctId, @objId, @feeAcctId, @feeAmount, @desc, @user",
                false, 10000, porderId, pdebitAcctId, pobjId, pfeeAcctId, pfeeAmount, pdesc, puser);
        }

        public void SalesOrderUnPost(
            int orderId,
            string user)
        {
            var porderId = _dataProvider.GetParameter();
            porderId.ParameterName = "orderId";
            porderId.Value = orderId;
            porderId.DbType = DbType.Int32;

            var puser = _dataProvider.GetParameter();
            puser.ParameterName = "user";
            puser.Value = (object)user ?? DBNull.Value;
            puser.DbType = DbType.String;

            _dbContext.ExecuteSqlCommand(
                @"exec usp_SaleOrder_UnPost @orderid, @user", 
                false, 10000, porderId, puser);
        }

        public POS_SalesOrder GetSalesOrderById(int orderId)
        {
            if (orderId == 0)
                return null;

            return _saleOrderRepository.GetById(orderId);
        }

        public void UpdateSalesOrder(UpdatedSalesOrderDTO order)
        {
            if(order == null)
            {
                throw new ArgumentNullException("order");
            }

            if(order.IsPaid == false)
            {
                order.PaymentDate = null;
            }
            else if(order.PaymentDate == null)
            {
                order.PaymentDate = DateTime.Now.Date;
            }

            var pId = _dataProvider.GetParameter();
            pId.ParameterName = "Id";
            pId.DbType = DbType.Int32;
            pId.Value = order.Id;

            var pIsPaid = _dataProvider.GetParameter();
            pIsPaid.ParameterName = "IsPaid";
            pIsPaid.DbType = DbType.Boolean;
            pIsPaid.Value = order.IsPaid;

            var pPaymentDate = _dataProvider.GetParameter();
            pPaymentDate.ParameterName = "PaymentDate";
            pPaymentDate.DbType = DbType.Date;
            pPaymentDate.Value = (object)order.PaymentDate ?? DBNull.Value;

            _dbContext.ExecuteSqlCommand(
                @"exec usp_UpdateSaleOrder @Id, @IsPaid, @PaymentDate", 
                false, 10000, pId, pIsPaid, pPaymentDate);
        }
    }
}
