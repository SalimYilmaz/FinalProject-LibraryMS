using Application.Services.Members;
using Application.Services.PaymentService;
using AutoMapper.Execution;
using Domain.Entities;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NArchitecture.Core.Mailing;
using Nest;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Adapters.Payment;
public class IyziCoPaymentAdapter : PaymentServiceBase
{
    private readonly PaymentInformation _paymentInformation;
    private readonly IMemberService _memberService;
    public IyziCoPaymentAdapter(IConfiguration configuration, IMemberService memberService)
    {
        _paymentInformation = configuration.GetSection("PaySettings").Get<PaymentInformation>()!;
        _memberService = memberService;
    }
    public  CreatePaymentRequest setCreatePaymentRequest(Guid memberId,string price)
    {
        CreatePaymentRequest request = new CreatePaymentRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = _paymentInformation.ConversationId;
        request.Price = price;
        request.PaidPrice = price;
        request.Currency = Currency.TRY.ToString();
        request.Installment = 1;
        request.BasketId = "B" + new Random().Next(10000, 99999);
        request.PaymentChannel = PaymentChannel.WEB.ToString();
        request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
        return request;
    }
    public void setBuyerForPay(CreatePaymentRequest request,string cardHolderName,string cardHolderSurname,Domain.Entities.Member member,string ipAddress)
    {
        Buyer buyer = new Buyer();
        buyer.Id = "BY" + new Random().Next(111,999).ToString();
        buyer.Name = cardHolderName;
        buyer.Surname = cardHolderSurname;
        buyer.GsmNumber = "+90" + member.PhoneNumber;
        buyer.Email = member.User.Email;
        buyer.IdentityNumber = member.NationalIdentity;
        buyer.LastLoginDate = DateTime.Now.ToString().Replace(".", "-");
        buyer.RegistrationDate = DateTime.Now.ToString().Replace(".","-");
        buyer.RegistrationAddress = member.Address;
        buyer.Ip = ipAddress;
        buyer.City = "Istanbul";
        buyer.Country = "Turkey";
        buyer.ZipCode = "34732";
        request.Buyer = buyer;
    }
    public  void setShippingAddress(CreatePaymentRequest request, Domain.Entities.Member member)
    {
        Address shippingAddress = new Address();
        shippingAddress.ContactName = member.Name;
        shippingAddress.City = "Istanbul";
        shippingAddress.Country = "Turkey";
        shippingAddress.Description = member.Address;
        shippingAddress.ZipCode = "34742";
        request.ShippingAddress = shippingAddress;
    }
    public void setBillingAddress(CreatePaymentRequest request,Domain.Entities.Member member)
    {
        Address billingAddress = new Address();
        billingAddress.ContactName = member.Name;
        billingAddress.City = "Istanbul";
        billingAddress.Country = "Turkey";
        billingAddress.Description = member.Address;
        billingAddress.ZipCode = "34742";
        request.BillingAddress = billingAddress;
    }
    public PaymentCard setPaymentCard(string cardHolderName,string creditCartNo,DateTime expiryDate,string cvc)
    {

        PaymentCard paymentCard = new PaymentCard();
        paymentCard.CardHolderName = cardHolderName;
        paymentCard.CardNumber = creditCartNo;
        paymentCard.ExpireMonth = expiryDate.Month.ToString();
        paymentCard.ExpireYear = expiryDate.Year.ToString();
        paymentCard.Cvc = cvc;
        paymentCard.RegisterCard = 0;
        return paymentCard;
    }
    public List<BasketItem> setBasketItems(string price)
    {
        List<BasketItem> basketItems = new List<BasketItem>();
        BasketItem firstBasketItem = new BasketItem();
        firstBasketItem.Id = "BI" + new Random().Next(100,999).ToString();
        firstBasketItem.Name = "NNItem";
        firstBasketItem.Category1 = "PHYSICAL Materials";
        firstBasketItem.Category2 = "Accessories";
        firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
        firstBasketItem.Price = price.ToString();
        basketItems.Add(firstBasketItem);
        return basketItems;
    }
    public override async Task<bool> Pay(string cardHolderName, string cardHolderSurname, string creditCartNo, string cvc, Guid memberId,string ipAddress,DateTime expiryDate)
    {
        Options options = new Options();
        options.ApiKey = _paymentInformation.ApiKey;
        options.SecretKey = _paymentInformation.ApiSecret;
        options.BaseUrl = _paymentInformation.BaseUrl;

        Domain.Entities.Member member = await _memberService.GetAsync(m => m.Id.Equals(memberId), include : m => m.Include(m => m.User));
        string price = Convert.ToInt32(member.TotalDebt).ToString();
        CreatePaymentRequest request = setCreatePaymentRequest(memberId,price);
        setBuyerForPay(request,cardHolderName,cardHolderSurname,member,ipAddress);
        setShippingAddress(request,member);
        setBillingAddress(request,member);

        PaymentCard paymentCard = setPaymentCard(cardHolderName,creditCartNo,expiryDate,cvc);

        List<BasketItem> basketItems = setBasketItems(price);

        request.BasketItems = basketItems;

        request.PaymentCard = paymentCard;

        Iyzipay.Model.Payment payment = Iyzipay.Model.Payment.Create(request, options);
        if(payment.Status == Iyzipay.Model.Status.SUCCESS.ToString())
        {
            member.TotalDebt = 0;
            await _memberService.UpdateAsync(member);
        }

        return payment.Status == Iyzipay.Model.Status.SUCCESS.ToString();
    }
}