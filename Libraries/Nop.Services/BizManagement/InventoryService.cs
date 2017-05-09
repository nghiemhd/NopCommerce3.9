using Nop.Core.Data;
using Nop.Core.Domain.BizManagement;
using Nop.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Nop.Services.BizManagement
{
    public class InventoryService : IInventoryService
    {
        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;

        public InventoryService(IDbContext dbContext, IDataProvider dataProvider)
        {
            this._dbContext = dbContext;
            this._dataProvider = dataProvider;
        }

        public void IVMTranITComplete(int transferId, int typeId, string user)
        {
            var ptransferId = _dataProvider.GetParameter();
            ptransferId.ParameterName = "transferId";
            ptransferId.Value = ((object)transferId) ?? DBNull.Value;
            ptransferId.DbType = DbType.Int32;

            var ptypeId = _dataProvider.GetParameter();
            ptypeId.ParameterName = "typeId";
            ptypeId.Value = ((object)typeId) ?? DBNull.Value;
            ptypeId.DbType = DbType.Int32;

            var puser = _dataProvider.GetParameter();
            puser.ParameterName = "user";
            puser.Value = ((object)user) ?? DBNull.Value;
            puser.DbType = DbType.String;

            _dbContext.ExecuteSqlCommand(
                @"exec usp_IVMTranByIT_Complete @transferId,@typeId,@user", 
                false, 10000, ptransferId, ptypeId, puser);
        }

        public IList<WarehouseCountResult> WarehouseCount(
            int? storeId,
            DateTime toEndDate,
            IList<int> categoryIds,
            int manufacturerId,
            bool orderByAge,
            int? filterByQty,
            string productCode,
            int pageIndex,
            int pageSize,
            out int totalRecord,
            out int SumQty,
            out decimal SumCostAmount)
        {
            if (categoryIds != null && categoryIds.Contains(0))
                categoryIds.Remove(0);

            string commaSeparatedCategoryIds = "";
            if (categoryIds != null)
            {
                for (int i = 0; i < categoryIds.Count; i++)
                {
                    commaSeparatedCategoryIds += categoryIds[i].ToString();
                    if (i != categoryIds.Count - 1)
                    {
                        commaSeparatedCategoryIds += ",";
                    }
                }
            }

            var pstoreId = _dataProvider.GetParameter();
            pstoreId.ParameterName = "storeId";
            if (storeId == null)
                pstoreId.Value = DBNull.Value;
            else
                pstoreId.Value = storeId.Value;
            pstoreId.DbType = DbType.Int32;


            var ptoEndOfBeforeDate = _dataProvider.GetParameter();
            ptoEndOfBeforeDate.ParameterName = "toEndOfBeforeDate";
            ptoEndOfBeforeDate.Value = toEndDate.Date.AddDays(1);
            ptoEndOfBeforeDate.DbType = DbType.DateTime;

            var pCategoryIds = _dataProvider.GetParameter();
            pCategoryIds.ParameterName = "categoryIds";
            pCategoryIds.Value = commaSeparatedCategoryIds != null ? (object)commaSeparatedCategoryIds : DBNull.Value;
            pCategoryIds.DbType = DbType.String;

            var pManufacturerId = _dataProvider.GetParameter();
            pManufacturerId.ParameterName = "ManufacturerId";
            pManufacturerId.Value = manufacturerId;
            pManufacturerId.DbType = DbType.Int32;

            var pproductCode = _dataProvider.GetParameter();
            pproductCode.ParameterName = "productCode";
            if (productCode == null)
                pproductCode.Value = DBNull.Value;
            else
                pproductCode.Value = productCode;
            pproductCode.DbType = DbType.String;

            var pfilterByQty = _dataProvider.GetParameter();
            pfilterByQty.ParameterName = "filterByQty";
            if (filterByQty == null)
                pfilterByQty.Value = DBNull.Value;
            else
                pfilterByQty.Value = filterByQty.Value;
            pfilterByQty.DbType = DbType.Int32;

            var porderByAge = _dataProvider.GetParameter();
            porderByAge.ParameterName = "orderByAge";
            porderByAge.Value = orderByAge;
            porderByAge.DbType = DbType.Boolean;

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

            var psumQty = _dataProvider.GetParameter();
            psumQty.ParameterName = "sumQty";
            psumQty.Direction = ParameterDirection.Output;
            psumQty.DbType = DbType.Int32;

            var psumCostAmount = _dataProvider.GetParameter();
            psumCostAmount.ParameterName = "sumCostAmount ";
            psumCostAmount.Direction = ParameterDirection.Output;
            psumCostAmount.DbType = DbType.Decimal;

            var rs = _dbContext.SqlQuery<WarehouseCountResult>(
                @"exec usp_WarehouseCount  @storeId, @manufacturerId, @categoryIds, @orderByAge, @productCode, @filterByQty,@toEndOfBeforeDate, @pageIndex, @pageSize, @count output, @sumQty output, @sumCostAmount output",
                pstoreId,pManufacturerId,pCategoryIds,porderByAge, pproductCode, pfilterByQty,ptoEndOfBeforeDate,ppageIndex, ppageSize, pTotalRecords, psumQty, psumCostAmount).ToList();
            totalRecord = (int)pTotalRecords.Value;
            SumQty = (int)psumQty.Value;
            SumCostAmount = (decimal)psumCostAmount.Value;
            return rs;
        }

        public IList<WarehouseCountLineResult> GetWarehouseCountDetail(int productId, int? storeId, DateTime toEndDate)
        {
            var pproductId = _dataProvider.GetParameter();
            pproductId.ParameterName = "productId";
            pproductId.Value = productId;
            pproductId.DbType = DbType.Int32;

            var pstoreId = _dataProvider.GetParameter();
            pstoreId.ParameterName = "storeId";
            if (storeId == null)
                pstoreId.Value = DBNull.Value;
            else
                pstoreId.Value = storeId.Value;
            pstoreId.DbType = DbType.Int32;


            var ptoEndOfBeforeDate = _dataProvider.GetParameter();
            ptoEndOfBeforeDate.ParameterName = "toEndOfBeforeDate";
            ptoEndOfBeforeDate.Value = toEndDate.Date.AddDays(1);
            ptoEndOfBeforeDate.DbType = DbType.DateTime;

            var rs = _dbContext.SqlQuery<WarehouseCountLineResult>(
                @"exec usp_WarehouseCountDetail @storeId,@productId,@toEndOfBeforeDate", 
                pstoreId, pproductId, ptoEndOfBeforeDate).ToList();

            return rs;
        }

        public IList<InventoryAdjResult> InventoryAdjSearch(
            DateTime? fromDate,
            DateTime? toDate,
            int? storeId,
            int? status,
            string adjNo,
            string productCode,
            string barcode,
            int pageIndex,
            int pageSize,
            out int totalRecord)
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

            var padjNo = _dataProvider.GetParameter();
            padjNo.ParameterName = "adjNo";
            if (adjNo == null)
                padjNo.Value = DBNull.Value;
            else
                padjNo.Value = adjNo;
            padjNo.DbType = DbType.String;

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

            var rs = _dbContext.SqlQuery<InventoryAdjResult>(
                @"exec usp_InventoryAdjSearch @fromDate, @toDate, @storeId, @status, @adjNo, @productCode, @barcode, @pageIndex, @pageSize, @count output",
                pfromDate, ptoDate, pstoreId, pstatus, padjNo, pproductCode, pbarcode, ppageIndex, ppageSize, pTotalRecords).ToList();
            totalRecord = (int)pTotalRecords.Value;

            return rs;
        }

        public IList<InventoryAdjLineResult> GetInventoryAdjDetail(int adjId)
        {
            var padjId = _dataProvider.GetParameter();
            padjId.ParameterName = "adjId";
            padjId.Value = adjId;
            padjId.DbType = DbType.Int32;

            var rs = _dbContext.SqlQuery<InventoryAdjLineResult>(@"exec usp_GetInventoryAdjDetailById @adjId", padjId).ToList();

            return rs;
        }

        public IList<InventoryTransferLineResult> GetInventoryTransferDetail(int transferId)
        {
            var ptransferId = _dataProvider.GetParameter();
            ptransferId.ParameterName = "transferId";
            ptransferId.Value = transferId;
            ptransferId.DbType = DbType.Int32;

            var rs = _dbContext.SqlQuery<InventoryTransferLineResult>(@"exec usp_GetInvTransferDetailByTransferId @transferId", ptransferId).ToList();

            return rs;
        }

        public InventoryAdjResult GetInventoryAdj(int? adjId)
        {
            var padjId = _dataProvider.GetParameter();
            padjId.ParameterName = "adjId";
            if (adjId == null)
                padjId.Value = DBNull.Value;
            else
                padjId.Value = adjId;
            padjId.DbType = DbType.Int32;

            var rs = _dbContext.SqlQuery<InventoryAdjResult>(@"exec usp_GetInventoryAdj @adjId", padjId).First();

            return rs;
        }

        public IList<IVM_Barcode> BarcodeAjax(string code)
        {
            var pcode = _dataProvider.GetParameter();
            pcode.ParameterName = "code";
            if (code == null)
                pcode.Value = DBNull.Value;
            else
                pcode.Value = code;
            pcode.DbType = DbType.String;

            var rs = _dbContext.SqlQuery<IVM_Barcode>(@"exec usp_BarcodeAjax @code", pcode).ToList();

            return rs;
        }

        public IVM_Barcode GetBarcodeById(int id)
        {
            var pid = _dataProvider.GetParameter();
            pid.ParameterName = "id";
            pid.Value = id;
            pid.DbType = DbType.Int32;

            var rs = _dbContext.SqlQuery<IVM_Barcode>(@"exec usp_GetBarcodeById @id", pid).First();

            return rs;
        }

        public int EditInventoryAdj(InventoryAdjResult obj)
        {
            //Insert Master

            var pAdjNo = _dataProvider.GetParameter();
            pAdjNo.ParameterName = "AdjNo";
            pAdjNo.Value = obj.AdjNo;
            pAdjNo.DbType = DbType.String;

            var pAdjDate = _dataProvider.GetParameter();
            pAdjDate.ParameterName = "AdjDate";
            pAdjDate.Value = obj.AdjDate;
            pAdjDate.DbType = DbType.DateTime;

            var pCreatedDate = _dataProvider.GetParameter();
            pCreatedDate.ParameterName = "CreatedDate";
            pCreatedDate.Value = (object)obj.CreatedDate ?? DBNull.Value;
            pCreatedDate.DbType = DbType.DateTime;

            var pUpdatedDate = _dataProvider.GetParameter();
            pUpdatedDate.ParameterName = "UpdatedDate";
            pUpdatedDate.Value = (object)obj.UpdatedDate ?? DBNull.Value;
            pUpdatedDate.DbType = DbType.DateTime;

            var pUpdatedUser = _dataProvider.GetParameter();
            pUpdatedUser.ParameterName = "UpdatedUser";
            pUpdatedUser.Value = (object)obj.UpdatedUser ?? DBNull.Value;
            pUpdatedUser.DbType = DbType.String;

            var pCreatedUser = _dataProvider.GetParameter();
            pCreatedUser.ParameterName = "CreatedUser";
            pCreatedUser.Value = (object)obj.UpdatedUser ?? DBNull.Value;
            pCreatedUser.DbType = DbType.String;

            var pDesc = _dataProvider.GetParameter();
            pDesc.ParameterName = "Desc";
            pDesc.Value = (object)obj.Desc ?? DBNull.Value;
            pDesc.DbType = DbType.String;

            var pDebitAcctId = _dataProvider.GetParameter();
            pDebitAcctId.ParameterName = "DebitAcctId";
            pDebitAcctId.Value = obj.DebitAcctId;
            pDebitAcctId.DbType = DbType.Int32;

            var pStoreId = _dataProvider.GetParameter();
            pStoreId.ParameterName = "StoreId";
            pStoreId.Value = obj.StoreId;
            pStoreId.DbType = DbType.Int32;

            var pObjId = _dataProvider.GetParameter();
            pObjId.ParameterName = "ObjId";
            pObjId.Value = (object)obj.ObjId??DBNull.Value;
            pObjId.DbType = DbType.Int32;

            var pQty = _dataProvider.GetParameter();
            pQty.ParameterName = "Qty";
            pQty.Value = obj.Qty;
            pQty.DbType = DbType.Decimal;

            var pCostAmount = _dataProvider.GetParameter();
            pCostAmount.ParameterName = "CostAmount";
            pCostAmount.Value = obj.CostAmount;
            pCostAmount.DbType = DbType.Decimal;

            using (TransactionScope tran = new TransactionScope())
            {

                var rs = _dbContext.SqlQuery<int>(
                    @"exec usp_InventoryAdjEdit @StoreId, @AdjNo, @AdjDate, @Desc, @CreatedDate, @UpdatedDate, @CreatedUser, @UpdatedUser, @DebitAcctId, @ObjId, @CostAmount, @Qty",
                    pStoreId, pAdjNo, pAdjDate, pDesc, pCreatedDate, pUpdatedDate, pCreatedUser, pUpdatedUser, pDebitAcctId, pObjId, pCostAmount, pQty).First();

                //Insert Detail
                foreach (var item in obj.Items)
                {
                    item.AdjId = rs;
                    insertInventoryAdjDetail(item);
                }

                var puser = _dataProvider.GetParameter();
                puser.ParameterName = "user";
                puser.Value = (object)obj.UpdatedUser ?? DBNull.Value;
                puser.DbType = DbType.String;

                var pAdjId = _dataProvider.GetParameter();
                pAdjId.ParameterName = "adjId";
                pAdjId.Value = rs;
                pAdjId.DbType = DbType.Int32;

                _dbContext.ExecuteSqlCommand(
                    @"exec usp_InventoryAdj_Approve @adjId, @user",
                    false, 10000, pAdjId, puser);

                tran.Complete();

                return rs;
            }
        }

        private int insertInventoryAdjDetail(InventoryAdjLineResult line)
        {
            var pAdjId = _dataProvider.GetParameter();
            pAdjId.ParameterName = "AdjId";
            pAdjId.Value = line.AdjId;
            pAdjId.DbType = DbType.Int32;

            var pSeq = _dataProvider.GetParameter();
            pSeq.ParameterName = "Seq";
            pSeq.Value = line.Seq;
            pSeq.DbType = DbType.Int32;

            var pBarcodeId = _dataProvider.GetParameter();
            pBarcodeId.ParameterName = "BarcodeId";
            pBarcodeId.Value = line.BarcodeId;
            pBarcodeId.DbType = DbType.Int32;

            var pQty = _dataProvider.GetParameter();
            pQty.ParameterName = "Qty";
            pQty.Value = line.Qty;
            pQty.DbType = DbType.Int32;

            var pCostAmount = _dataProvider.GetParameter();
            pCostAmount.ParameterName = "CostAmount";
            pCostAmount.Value = line.CostAmount;
            pCostAmount.DbType = DbType.Decimal;

            return _dbContext.SqlQuery<int>(
                @"exec usp_InventoryAdjDetailInsert @AdjId,@Seq,@BarcodeId,@Qty,@CostAmount",
                pAdjId, pSeq, pBarcodeId, pQty, pCostAmount).First();
        }

        public IList<InventoryTransactionResult> GetInventoryTransaction(
            DateTime? fromDate,
            DateTime? toDate,
            string tranNo,
            string productCode,
            int? storeId,
            string barcode,
            int? typeId,
            int pageIndex,
            int pageSize,
            out int totalRecord,
            out int sumQty)
        {
            var pfromDate = _dataProvider.GetParameter();
            pfromDate.ParameterName = "fromDate";
            pfromDate.Value = ((object)fromDate)??DBNull.Value;
            pfromDate.DbType = DbType.DateTime;

            var ptoDate = _dataProvider.GetParameter();
            ptoDate.ParameterName = "toDate";
            ptoDate.Value = ((object)toDate) ?? DBNull.Value;
            ptoDate.DbType = DbType.DateTime;

            var ptranNo = _dataProvider.GetParameter();
            ptranNo.ParameterName = "tranNo";
            ptranNo.Value = ((object)tranNo) ?? DBNull.Value;
            ptranNo.DbType = DbType.String;

            var pproductCode = _dataProvider.GetParameter();
            pproductCode.ParameterName = "productCode";
            pproductCode.Value = ((object)productCode) ?? DBNull.Value;
            pproductCode.DbType = DbType.String;

            var pstoreId = _dataProvider.GetParameter();
            pstoreId.ParameterName = "storeId";
            pstoreId.Value = ((object)storeId) ?? DBNull.Value;
            pstoreId.DbType = DbType.Int32;

            var pbarcode = _dataProvider.GetParameter();
            pbarcode.ParameterName = "barcode";
            pbarcode.Value = ((object)barcode) ?? DBNull.Value;
            pbarcode.DbType = DbType.String;

            var ptypeId = _dataProvider.GetParameter();
            ptypeId.ParameterName = "typeId";
            ptypeId.Value = ((object)typeId) ?? DBNull.Value;
            ptypeId.DbType = DbType.Int32;
            
            
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

            var psumQty = _dataProvider.GetParameter();
            psumQty.ParameterName = "sumQty";
            psumQty.Direction = ParameterDirection.Output;
            psumQty.DbType = DbType.Int32;

            var rs = _dbContext.SqlQuery<InventoryTransactionResult>(
                @"exec [usp_InvTransactionDetails] @fromDate, @toDate, @tranNo, @productCode, @storeId, @barcode, @typeId, @pageIndex, @pageSize, @count output, @sumQty output",
                pfromDate, ptoDate, ptranNo, pproductCode, pstoreId, pbarcode,ptypeId, ppageIndex, ppageSize, pTotalRecords, psumQty).ToList();

            totalRecord = (int)pTotalRecords.Value;
            sumQty = (int)psumQty.Value;

            return rs;
        }

        public IList<InventoryTransferResult> GetInventoryTransfer(
            DateTime? fromDate,
            DateTime? toDate,
            string tranNo,
            string productCode,
            int? fromStoreId,
            int? toStoreId,
            string barcode,
            int? status,
            int pageIndex,
            int pageSize,
            out int totalRecord,
            out int sumQty)
        {
            var pfromDate = _dataProvider.GetParameter();
            pfromDate.ParameterName = "fromDate";
            pfromDate.Value = ((object)fromDate) ?? DBNull.Value;
            pfromDate.DbType = DbType.DateTime;

            var ptoDate = _dataProvider.GetParameter();
            ptoDate.ParameterName = "toDate";
            ptoDate.Value = ((object)toDate) ?? DBNull.Value;
            ptoDate.DbType = DbType.DateTime;

            var ptranNo = _dataProvider.GetParameter();
            ptranNo.ParameterName = "tranNo";
            ptranNo.Value = ((object)tranNo) ?? DBNull.Value;
            ptranNo.DbType = DbType.String;

            var pproductCode = _dataProvider.GetParameter();
            pproductCode.ParameterName = "productCode";
            pproductCode.Value = ((object)productCode) ?? DBNull.Value;
            pproductCode.DbType = DbType.String;

            var pfromStoreId = _dataProvider.GetParameter();
            pfromStoreId.ParameterName = "fromStoreId";
            pfromStoreId.Value = ((object)fromStoreId) ?? DBNull.Value;
            pfromStoreId.DbType = DbType.Int32;

            var ptoStoreId = _dataProvider.GetParameter();
            ptoStoreId.ParameterName = "toStoreId";
            ptoStoreId.Value = ((object)toStoreId) ?? DBNull.Value;
            ptoStoreId.DbType = DbType.Int32;

            var pbarcode = _dataProvider.GetParameter();
            pbarcode.ParameterName = "barcode";
            pbarcode.Value = ((object)barcode) ?? DBNull.Value;
            pbarcode.DbType = DbType.String;

            var pstatus = _dataProvider.GetParameter();
            pstatus.ParameterName = "status";
            pstatus.Value = ((object)status) ?? DBNull.Value;
            pstatus.DbType = DbType.Int32;

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

            var psumQty = _dataProvider.GetParameter();
            psumQty.ParameterName = "sumQty";
            psumQty.Direction = ParameterDirection.Output;
            psumQty.DbType = DbType.Int32;

            var rs = _dbContext.SqlQuery<InventoryTransferResult>(
                @"exec [usp_InvTransferSearch] @fromDate, @toDate, @tranNo, @productCode, @fromStoreId, @toStoreId, @barcode, @status, @pageIndex, @pageSize, @count output, @sumQty output",
                pfromDate, ptoDate, ptranNo, pproductCode, pfromStoreId, ptoStoreId, pbarcode, pstatus, ppageIndex, ppageSize, pTotalRecords, psumQty).ToList();

            totalRecord = (int)pTotalRecords.Value;
            sumQty = (int)psumQty.Value;

            return rs;
        }

    }
}
