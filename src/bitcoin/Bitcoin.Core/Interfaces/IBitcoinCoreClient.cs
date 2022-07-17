using Bitcoin.Core.Models;
using Bitcoin.Core.Models.BitcoinCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bitcoin.Core.Interfaces
{
    public interface IBitcoinCoreClient
    {
        Task<ResponseBTC<string>> GetNewAddressAsync(GetNewAddressRequest model);
        Task<ResponseBTC<decimal>> GetBalanceAsync();
        Task<ResponseBTC<GetBalancesResponse>> GetBalancesAsync();
        Task<ResponseBTC<GetAddressInfoResponse>> GetAddressInfoAsync(GetAddressInfoRequest model);
        Task<ResponseBTC<List<ListReceivedByAddressResponse>>> ListReceivedByAddressAsync();
        Task<ResponseBTC<decimal>> GetReceivedByAddressAsync(GetReceivedByAddressRequest model);
        Task<Response<GetAddressInfo3rdPartyResponse>> GetAddressBalance3rdPartyAsync(GetAddressInfo3rdPartyRequest model);
        Task<ValidateResponseBTC<ValidateAddressResponse>> ValidateAddressAsync(ValidateAddressRequest model);
        Task<Response<GetRecommendedFeeResponse>> GetRecommendedFeeAsync();
        Task<ResponseBTC<GetTransactionResponse>> GetTransactionAsync(GetTransactionRequest model);
        Task<ResponseBTC<GetTxOutResponse>> GetTxOutAsync(GetTxOutRequest model);
        Task<ResponseBTC<GetTxOutsetInfoResponse>> GetTxOutSetInfoAsync();
        Task<ResponseBTC<List<ListUnspentResponse>>> ListUnspentAsync(ListUnspentRequest model);
        Task<ResponseBTC<GetBlockResponse>> GetBlockAsync(GetBlockRequest model);
        Task<Response<GetTransaction3rdPartyResponse>> GetTransaction3rdPartyAsync(GetTransaction3rdPartyRequest model);
        Task<Response<GetAssetPrizesResponse>> GetAssetPrizesAsync();
        Task<Response<GetAssetPrizeResponse>> GetAssetPrizeAsync(GetAssetPrizeRequest model);
        Task<Response<GetMempoolTransactionResponse>> GetMempoolTransactionAsync(GetMempoolTransactionRequest model);
        Task<ResponseBTC<GetRawTransactionAsJSONResponse>> GetRawTransactionAsJSONAsync(GetRawTransactionAsJSONRequest model);
        Task<ResponseBTC<string>> CreateRawTransactionAsync(CreateRawTransactionRequest model);
        Task<ResponseBTC<DecodeRawTransactionResponse>> DecodeRawTransactionAsync(DecodeRawTransactionRequest model);
        Task<ResponseBTC<SignRawTransactionWithKeysResponse>> SignRawTransactionAsync(SignRawTransactionWithKeysRequest model);
        Task<ResponseBTC<string>> SendRawTransactionAsync(SendRawTransactionRequest model);
        Task<ResponseBTC<string>> CreateSignAndSendRawTransactionAsync(CreateSignAndSendRawTransactionViaTxIdRequest model);
        Task<ResponseBTC<string>> DumpPrivKeyAsync(DumpPrivKeyRequest model);
        Task<ResponseBTC<GetBlockchainInfoResponse>> GetBlockchainInfoAsync();
        Task<ListWalletsResponse> ListWalletsAsync();
        Task<ResponseBTC<LoadWalletResponse>> LoadWalletAsync(LoadWalletRequest model);
        Task<Response<GenerateQRCodeResponse>> GenerateQRCodeAsync(GenerateQRCodeRequest model);
        Task<ResponseBTC<AbandonTransactionResponse>> AbandonTransactionAsync(AbandonTransactionRequest model);
        Task<ResponseBTC<CreateWalletResponse>> CreateWalletAsync(CreateWalletRequest model);
        Task<ResponseBTC<DumpWalletResponse>> DumpWalletAsync(DumpWalletRequest model);
        Task<ResponseBTC<BumpFeeResponse>> BumpFeeAsync(BumpFeeRequest model);
        Task<ResponseBTC<GetWalletInfoResponse>> GetWalletInfoAsync();
        Task<ResponseBTC<UnloadWalletResponse>> UnloadwalletAsync(UnloadWalletRequest model);
        Task<ResponseBTC<ListWalletDirResponse>> ListWalletDirAsync();
        Task<ResponseBTC<string>> GetRawChangeAddressAsync(GetRawChangeAddressRequest model);
        Task<ResponseBTC<BackupWalletResponse>> BackupWalletAsync(BackupWalletRequest model);
        Task<ResponseBTC<WalletlockResponse>> WalletlockAsync(WalletlockRequest model);
        Task<ResponseBTC<WalletPassphraseResponse>> WalletPassphraseAsync(WalletPassphraseRequest model);
        Task<ResponseBTC<string>> SignMessageAsync(SignMessageRequest model);
        Task<ResponseBTC<bool>> VerifySignedMessageAsync(VerifySignedMessageRequest model);
        Task<ResponseBTC<FundRawtransactionResponse>> FundRawtransactionAsync(FundRawtransactionRequest model);
        Task<ResponseBTC<WalletCreateFundedPsbtResponse>> WalletCreateFundedPSBTAsync(WalletCreateFundedPsbtRequest model);
        Task<ResponseBTC<DecodePsbtResponse>> DecodePSBTAsync(DecodePsbtRequest model);
        Task<ResponseBTC<CreateMultiSigResponse>> CreateMultisigAsync(CreateMultisigRequest model);
        Task<ResponseBTC<CreateMultisigAddressResponse>> CreateMultiSigAddressAsync(CreateMultisigAddressRequest model);
        Task<ResponseBTC<string>> CreatePsbtAsync(CreatePsbtRequest model);
        Task<ResponseBTC<WalletProcessPsbtResponse>> WalletProcessPsbtAsync(WalletProcessPsbtRequest model); 
        Task<ResponseBTC<FinalizePsbtResponse>> FinalizePsbtAsync(FinalizePsbtRequest model);
        Task<ResponseBTC<string>> UtxoUpdatePsbtAsync(UtxoUpdatePsbtRequest model);
        Task<ResponseBTC<ConvertToPsbtResponse>> ConvertToPsbtAsync(ConvertToPsbtRequest model);
        Task<ResponseBTC<AddMultisigAddressResponse>> AddMultisigAddressAsync(AddMultisigAddressRequest model);
        Task<ResponseBTC<ImportAddressResponse>> ImportAddressAsync(ImportAddressRequest model);

        
    }
}
