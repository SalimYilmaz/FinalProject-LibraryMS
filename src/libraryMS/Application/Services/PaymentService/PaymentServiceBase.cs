using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PaymentService;
public abstract class PaymentServiceBase
{
    public abstract Task<bool> Pay(string cardHolderName,string cardHolderSurname, string creditCartNo, string cvc, Guid memberId,string ipAddress,DateTime expiryDate);
}
