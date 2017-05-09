namespace Nop.Core.Domain.BizManagement
{
    public class CF_Obj : BaseEntity
    {
        public string ObjCode { get; set; }
        public string ObjName { get; set; }

        public string ObjFull
        {
            get
            {
                return ObjCode + " - " + ObjName;
            }
        }
    }
}
