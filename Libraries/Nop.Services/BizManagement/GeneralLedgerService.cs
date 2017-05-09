using Nop.Core.Data;
using Nop.Core.Domain.BizManagement;
using Nop.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;

namespace Nop.Services.BizManagement
{
    public class GeneralLedgerService : IGeneralLedgerService
    {
        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;

        public GeneralLedgerService(IDbContext dbContext, IDataProvider dataProvider)
        {
            this._dbContext = dbContext;
            this._dataProvider = dataProvider;
        }

        public IList<GeneralLedgerResult> Search(
            DateTime? fromDate,
            DateTime? toDate,
            int? typeId,
            string docNo,
            string acctCode,
            string objCode,
            string objName,
            int pageIndex, int pageSize,
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

            var pdocNo = _dataProvider.GetParameter();
            pdocNo.ParameterName = "docNo";
            if (docNo == null)
                pdocNo.Value = DBNull.Value;
            else
                pdocNo.Value = docNo;
            pdocNo.DbType = DbType.String;

            var ptypeId = _dataProvider.GetParameter();
            ptypeId.ParameterName = "typeId";
            if (typeId == null)
                ptypeId.Value = DBNull.Value;
            else
                ptypeId.Value = typeId;
            ptypeId.DbType = DbType.Int32;

            var pacctCode = _dataProvider.GetParameter();
            pacctCode.ParameterName = "acctCode";
            if (acctCode == null)
                pacctCode.Value = DBNull.Value;
            else
                pacctCode.Value = acctCode;
            pacctCode.DbType = DbType.String;

            var pobjCode = _dataProvider.GetParameter();
            pobjCode.ParameterName = "objCode";
            if (objCode == null)
                pobjCode.Value = DBNull.Value;
            else
                pobjCode.Value = objCode;
            pobjCode.DbType = DbType.String;

            var pobjName = _dataProvider.GetParameter();
            pobjName.ParameterName = "objName";
            if (objName == null)
                pobjName.Value = DBNull.Value;
            else
                pobjName.Value = objName;
            pobjName.DbType = DbType.String;

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

            var rs = _dbContext.SqlQuery<GeneralLedgerResult>(@"exec usp_GeneralLedgerSearch @fromDate, @toDate, @typeId, @docNo, @acctCode, @objCode, @objName, @pageIndex, @pageSize, @count output",
                pfromDate, ptoDate, ptypeId, pdocNo, pacctCode,
                pobjCode, pobjName, ppageIndex, ppageSize, pTotalRecords).ToList();
            totalRecord = (int)pTotalRecords.Value;
            return rs;
        }

        public IList<GeneralLedgerLineResult> GetGeneralLedgerDetail(int tranId)
        {
            var ptranId = _dataProvider.GetParameter();
            ptranId.ParameterName = "docId";
            ptranId.Value = tranId;
            ptranId.DbType = DbType.Int32;

            var rs = _dbContext.SqlQuery<GeneralLedgerLineResult>(@"exec usp_GetGeneralLedgerDetailById @docId", ptranId).ToList();

            return rs;
        }

        public GeneralLedgerResult GetGeneralLedger(int? docId)
        {
            var ptranId = _dataProvider.GetParameter();
            ptranId.ParameterName = "docId";
            if (docId == null)
                ptranId.Value = DBNull.Value;
            else
                ptranId.Value = docId;
            ptranId.DbType = DbType.Int32;

            var rs = _dbContext.SqlQuery<GeneralLedgerResult>(@"exec usp_GetGeneralLedger @docId", ptranId).First();

            return rs;
        }

        public IList<CF_Acct> AcctAjax(string acc)
        {
            var pacc = _dataProvider.GetParameter();
            pacc.ParameterName = "acc";
            if (acc == null)
                pacc.Value = DBNull.Value;
            else
                pacc.Value = acc;
            pacc.DbType = DbType.String;

            var rs = _dbContext.SqlQuery<CF_Acct>(@"exec usp_AcctAjax @acc", pacc).ToList();

            return rs;
        }

        public IList<CF_Obj> ObjAjax(string text)
        {
            var pacc = _dataProvider.GetParameter();
            pacc.ParameterName = "text";
            if (text == null)
                pacc.Value = DBNull.Value;
            else
                pacc.Value = text;
            pacc.DbType = DbType.String;

            var rs = _dbContext.SqlQuery<CF_Obj>(@"exec usp_ObjAjax @text", pacc).ToList();

            return rs;
        }

        public CF_Acct getAcctById(int id)
        {
            var pId = _dataProvider.GetParameter();
            pId.ParameterName = "id";
            pId.Value = id;
            pId.DbType = DbType.Int32;

            var rs = _dbContext.SqlQuery<CF_Acct>(@"exec usp_GetAcctById @id", pId).First();

            return rs;
        }
        public CF_Obj getObjById(int id)
        {
            var pId = _dataProvider.GetParameter();
            pId.ParameterName = "id";
            pId.Value = id;
            pId.DbType = DbType.Int32;

            var rs = _dbContext.SqlQuery<CF_Obj>(@"exec usp_GetObjById @id", pId).First();

            return rs;
        }

        public int EditGeneralLedger(GeneralLedgerResult obj)
        {
            //Insert Master
            var pId = _dataProvider.GetParameter();
            pId.ParameterName = "Id";
            pId.Value = obj.Id;
            pId.DbType = DbType.Int32;

            var pDocDate = _dataProvider.GetParameter();
            pDocDate.ParameterName = "DocDate";
            pDocDate.Value = obj.DocDate;
            pDocDate.DbType = DbType.DateTime;

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
            pDesc.Value = (object)obj.Desc??DBNull.Value;
            pDesc.DbType = DbType.String;

            var pDocNo = _dataProvider.GetParameter();
            pDocNo.ParameterName = "DocNo";
            pDocNo.Value = obj.DocNo;
            pDocNo.DbType = DbType.String;

            var ptypeId = _dataProvider.GetParameter();
            ptypeId.ParameterName = "TypeId";
            ptypeId.Value = obj.TypeId;
            ptypeId.DbType = DbType.Int32;

            using (TransactionScope tran = new TransactionScope())
            {

                var rs = _dbContext.SqlQuery<int>(@"exec usp_GeneralLedgerEdit @Id,@DocNo, @DocDate, @TypeId, @Desc, @CreatedDate, @UpdatedDate, @CreatedUser, @UpdatedUser", 
                    pId,pDocNo, pDocDate, ptypeId, pDesc, pCreatedDate,pUpdatedDate,pCreatedUser,pUpdatedUser).First();

                //Insert Detail
                foreach (var item in obj.Items)
                {
                    item.TransId = rs;
                    insertGeneralLedgerDetail(item);
                }

                tran.Complete();

                return rs;
            }
        }

        private int insertGeneralLedgerDetail(GeneralLedgerLineResult line)
        {
            var pTransId = _dataProvider.GetParameter();
            pTransId.ParameterName = "TransId";
            pTransId.Value = line.TransId;
            pTransId.DbType = DbType.Int32;

            var pLineId = _dataProvider.GetParameter();
            pLineId.ParameterName = "LineId";
            pLineId.Value = line.LineId;
            pLineId.DbType = DbType.Int32;

            var pAcctId = _dataProvider.GetParameter();
            pAcctId.ParameterName = "AcctId";
            pAcctId.Value = line.AcctId;
            pAcctId.DbType = DbType.Int32;

            var pDebit = _dataProvider.GetParameter();
            pDebit.ParameterName = "Debit";
            pDebit.Value = line.Debit;
            pDebit.DbType = DbType.Decimal;

            var pCredit = _dataProvider.GetParameter();
            pCredit.ParameterName = "Credit";
            pCredit.Value = line.Credit;
            pCredit.DbType = DbType.Decimal;

            var pRefObjId = _dataProvider.GetParameter();
            pRefObjId.ParameterName = "RefObjId";
            pRefObjId.Value = (object)line.RefObjId??DBNull.Value;
            pRefObjId.DbType = DbType.Int32;

            var pDesc = _dataProvider.GetParameter();
            pDesc.ParameterName = "Desc";
            pDesc.Value = (object)line.Desc ?? DBNull.Value;
            pDesc.DbType = DbType.String;

            var pCreatedDate = _dataProvider.GetParameter();
            pCreatedDate.ParameterName = "CreatedDate";
            pCreatedDate.Value = (object)line.CreatedDate ?? DBNull.Value;
            pCreatedDate.DbType = DbType.DateTime;

            var pUpdatedDate = _dataProvider.GetParameter();
            pUpdatedDate.ParameterName = "UpdatedDate";
            pUpdatedDate.Value = (object)line.UpdatedDate ?? DBNull.Value;
            pUpdatedDate.DbType = DbType.DateTime;

            var pUpdatedUser = _dataProvider.GetParameter();
            pUpdatedUser.ParameterName = "UpdatedUser";
            pUpdatedUser.Value = (object)line.UpdatedUser ?? DBNull.Value;
            pUpdatedUser.DbType = DbType.String;

            var pCreatedUser = _dataProvider.GetParameter();
            pCreatedUser.ParameterName = "CreatedUser";
            pCreatedUser.Value = (object)line.UpdatedUser ?? DBNull.Value;
            pCreatedUser.DbType = DbType.String;

            return _dbContext.SqlQuery<int>(@"exec usp_GeneralLedgerDetailInsert @TransId,@LineId,@AcctId,@Debit,@Credit,@RefObjId,@Desc,@CreatedDate, @UpdatedDate, @CreatedUser, @UpdatedUser",
                pTransId, pLineId, pAcctId, pDebit, pCredit, pRefObjId, pDesc, pCreatedDate, pUpdatedDate, pCreatedUser, pUpdatedUser).First();
        }

        public IList<AccountBalanceResult> GetAccountBalance(DateTime fromDate, DateTime toDate, int? objId)
        {
            var pfromDate = _dataProvider.GetParameter();
            pfromDate.ParameterName = "fromDate";
            pfromDate.Value = fromDate;
            pfromDate.DbType = DbType.DateTime;

            var ptoDate = _dataProvider.GetParameter();
            ptoDate.ParameterName = "toDate";
            ptoDate.Value = toDate;
            ptoDate.DbType = DbType.DateTime;

            var pobjId = _dataProvider.GetParameter();
            pobjId.ParameterName = "objId";
            pobjId.Value = (object)objId??DBNull.Value;
            pobjId.DbType = DbType.Int32;

            var rs = _dbContext.SqlQuery<AccountBalanceResult>(@"exec usp_AccountBalance @fromDate,@toDate,@objId", pfromDate, ptoDate, pobjId).ToList();

            return rs;
        }

        public IList<GeneralLedgerLineResult> GetAcctTransactionDetail(
            DateTime fromDate, 
            DateTime toDate, 
            int acctId, 
            int? objId, 
            int pageIndex, 
            int pageSize,
            out int totalRecord)
        {
            var pfromDate = _dataProvider.GetParameter();
            pfromDate.ParameterName = "fromDate";
            pfromDate.Value = fromDate;
            pfromDate.DbType = DbType.DateTime;

            var ptoDate = _dataProvider.GetParameter();
            ptoDate.ParameterName = "toDate";
            ptoDate.Value = toDate;
            ptoDate.DbType = DbType.DateTime;

            var pacctId = _dataProvider.GetParameter();
            pacctId.ParameterName = "acctId";
            pacctId.Value = acctId;
            pacctId.DbType = DbType.Int32;

            var pobjId = _dataProvider.GetParameter();
            pobjId.ParameterName = "objId";
            pobjId.Value = (object)objId ?? DBNull.Value;
            pobjId.DbType = DbType.Int32;

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


            var rs = _dbContext.SqlQuery<GeneralLedgerLineResult>(@"exec usp_GetAcctTransactionDetail @fromDate, @toDate, @acctId, @objId, @pageIndex, @pageSize, @count output",
                pfromDate, ptoDate, pacctId, pobjId, ppageIndex, ppageSize, pTotalRecords).ToList();
            totalRecord = (int)pTotalRecords.Value;
            return rs;
        }
    }
}
