using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orbitra.Entities.Abstract;


namespace Orbitra.Entities
{
    public class UserOperationClaim : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; } // <-- EKLENDİ
        public int OperationClaimId { get; set; }
        public virtual OperationClaim OperationClaim { get; set; } // <-- EKLENDİ

        // Navigation Properties (İlişkileri belirtmek için - Opsiyonel ama önerilir)
        // public User User { get; set; }
        // public OperationClaim OperationClaim { get; set; }
    }
}
