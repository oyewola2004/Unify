using Model.Enums;
using UNIFY.Model.Base;
using UNIFY.Model.Entities;

namespace Model.Entities
{
    public class MarketPlace : AuditableEntity
    {
        public string Image {get; set; }
        public string CompanyName {get; set; }
        public string CompanyAddress {get; set; }
        public string CompanyWebsite {get; set; }
        public string ServiceRendering {get; set;}
        public ServiceType ServiceType {get; set; }
        public string MemberId {get; set; }
        public Member Member {get; set; }
    }
}