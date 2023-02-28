using Bitcoin.Core.Models; 
using Bitcoin.Core.Models.CoreLightning;
using Bitcoin.Core.Models.CoreLightning.Invoices;
using Bitcoin.Core.Models.CoreLightning.Withdraw;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bitcoin.Core.Interfaces
{
    public interface ICoreLightningClient
    {
        Task<ResponseCLN<GetInfoResponse>> GetInfoAsync();
        Task<ResponseCLN<NewAddrResponse>> NewAddrAsync(NewAddrRequest model);
        Task<ResponseCLN<ListfundsResponse>> ListFundsAsync();
        Task<ResponseCLN<ListpeersResponse>> ListpeersAsync();
        Task<ResponseCLN<InvoiceResponse>> InvoiceAsync(InvoiceRequest model);
        Task<ResponseCLN<ListInvoicesResponse>> ListInvoicesAsync();
        Task<ResponseCLN<ListInvoiceResponse>> ListInvoiceAsync(ListInvoiceRequest model);
        Task<ResponseCLN<GenerateInvoiceQRCodeResponse>> GenerateInvoiceQRCodeAsync(GenerateInvoiceQRCodeRequest model);
        Task<ResponseCLN<ConnectResponse>> ConnectAsync(ConnectRequest model);
        Task<ResponseCLN<DisconnectResponse>> DisconnectAsync(DisconnectRequest model);
        Task<ResponseCLN<FundChannelResponse>> FundChannelAsync(FundChannelRequest model);
        Task<ResponseCLN<ListNodesResponse>> ListNodesAsync();
        Task<ResponseCLN<ListChannelsResponse>> ListChannelsAsync();
        Task<ResponseCLN<PayResponse>> PayAsync(PayRequest model);
        Task<ResponseCLN<DecodeInvoiceResponse>> DecodeInvoiceAsync(DecodeInvoiceRequest model);
        Task<ResponseCLN<DeleteInvoiceResponse>> DelInvoiceAsync(DeleteInvoiceRequest model);
        Task<ResponseCLN<DelexpiredinvoiceResponse>> DelexpiredinvoiceAsync();
        Task<ResponseCLN<DecodePayResponse>> DecodePayAsync(DecodePayRequest model);
        Task<ResponseCLN<PaystatusResponse>> PaystatusAsync();

        //offer for static invoices
        Task<ResponseCLN<OfferResponse>> OfferAsync(OfferRequest model);
        Task<ResponseCLN<FetchInvoiceResponse>> FetchInvoiceAsync(FetchInvoiceRequest model);
        Task<ResponseCLN<DecodeOfferResponse>> DecodeOfferAsync(DecodeOfferRequest model);
        Task<ResponseCLN<GetInvoiceStatusResponse>> GetInvoiceStatusAsync(GetInvoiceStatusRequest model);

        Task<ResponseCLN<ListPaysResponse>> ListPaysAsync();

        Task<ResponseCLN<WithdrawResponse>> WithdrawAsync(WithdrawRequest model);


    }
}
