using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Payment : Entity<Guid>
{
    public string CreditCartHolderName { get; set; }
    public string CreditCardNo { get; set; }
    public string Cvc {  get; set; }
    public DateTime ExpiryDate { get; set; }
    public Guid MemberId { get; set; }
    public Member? Member { get; set; }
}