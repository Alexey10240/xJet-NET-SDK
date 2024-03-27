
// xJet .NET SDK
//
// Author: Alexey1024 (https://t.me/alexey1024)
//
// To work you will need the following libraries:
// https://www.nuget.org/packages/BouncyCastle/1.8.9
// https://www.nuget.org/packages/Newtonsoft.Json
//
// Thanks for the help: Shadow (https://t.me/VipShadow)
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using System.Text;

public class xJet
{
    private HttpClient client = new();
    private string PrivateKey = "";
    private string ApiKey = "";
    string Endpoint = "";

    public xJet(string apiKey, string privateKey, bool mainnet = true)
    {
        PrivateKey = privateKey;
        ApiKey = apiKey;
        Endpoint = mainnet ? "https://xjet.app/api/v1/" : "https://testnet.xjet.app/api/v1/";
    }
    public async Task<string> currencies()
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{Endpoint}system.currencies");
        HttpResponseMessage responseMessage = await client.SendAsync(request);
        return await responseMessage.Content.ReadAsStringAsync();
    }
    public async Task<string> account_me()
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{Endpoint}account.me");
        request.Headers.Add("X-API-Key", ApiKey);
        return await (await client.SendAsync(request)).Content.ReadAsStringAsync();
    }
    public async Task<string> account_balances()
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{Endpoint}account.balances");
        request.Headers.Add("X-API-Key", ApiKey);
        return await (await client.SendAsync(request)).Content.ReadAsStringAsync();
    }
    public async Task<string> account_sumbit_deposit()
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{Endpoint}account.submitDeposit");
        request.Headers.Add("X-API-Key", ApiKey);
        return await (await client.SendAsync(request)).Content.ReadAsStringAsync();
    }
    public async Task<string> account_withdraw(string ton_address, string currency, float amount)
    {
        var pre_data = new Dictionary<string, object?>()
                {
                    {"ton_address", ton_address },
                    {"currency", currency },
                    {"amount", amount },
                };
        var data = xJetUtils.SignMessage(pre_data, PrivateKey);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{Endpoint}account.withdraw");
        request.Headers.Add("X-API-Key", ApiKey);
        request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        return await (await client.SendAsync(request)).Content.ReadAsStringAsync();
    }
    public async Task<string> account_operations(int limit,int offset)
    {
        var pre_data = new Dictionary<string, object?>()
                {
                    {"limit", limit },
                    {"offset", offset },
                };
        var data = xJetUtils.SignMessage(pre_data, PrivateKey);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{Endpoint}account.operations");
        request.Headers.Add("X-API-Key", ApiKey);
        request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        return await (await client.SendAsync(request)).Content.ReadAsStringAsync();
    }
    public async Task<string> cheque_create(string currency,float amount,float? expires,string? description,int activates_count,List<string>? groups_id,string? personal_id,string? password)
    {
        var pre_data = new Dictionary<string, object?>()
                {
                    {"currency", currency.ToLower() },
                    {"amount", amount },
                    {"expires", expires },
                    {"description", description },
                    {"activates_count", activates_count },
                    {"groups_id", groups_id },
                    {"personal_id", personal_id},
                    {"password", password},
                };
        var data = xJetUtils.SignMessage(pre_data, PrivateKey);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{Endpoint}cheque.create");
        request.Headers.Add("X-API-Key", ApiKey);
        request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        return await (await client.SendAsync(request)).Content.ReadAsStringAsync();
    }
    public async Task<string> cheque_status(string cheque_id)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{Endpoint}cheque.status?cheque_id={cheque_id}");
        request.Headers.Add("X-API-Key", ApiKey);
        return await (await client.SendAsync(request)).Content.ReadAsStringAsync();
    }
    public async Task<string> cheque_list()
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{Endpoint}cheque.list");
        request.Headers.Add("X-API-Key", ApiKey);
        return await (await client.SendAsync(request)).Content.ReadAsStringAsync();
    }
    public async Task<string> cheque_cancel(string cheque_id)
    {
        var pre_data = new Dictionary<string, object?>()
                {
                    {"cheque_id", cheque_id },
                };
        var data = xJetUtils.SignMessage(pre_data, PrivateKey);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{Endpoint}cheque.cancel");
        request.Headers.Add("X-API-Key", ApiKey);
        request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        return await (await client.SendAsync(request)).Content.ReadAsStringAsync();
    }
    public async Task<string> invoice_create(string currency, float amount, float? expires, string? description, int? max_payments)
    {
        var pre_data = new Dictionary<string, object?>()
                {
                    {"currency", currency.ToLower() },
                    {"amount", amount },
                    {"expires", expires },
                    {"description", description },
                    {"max_payments", max_payments },
                };
        var data = xJetUtils.SignMessage(pre_data, PrivateKey);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{Endpoint}invoice.create");
        request.Headers.Add("X-API-Key", ApiKey);
        request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        return await (await client.SendAsync(request)).Content.ReadAsStringAsync();
    }
    public async Task<string> invoice_status(string invoice_id)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{Endpoint}invoice.status?invoice_id={invoice_id}");
        request.Headers.Add("X-API-Key", ApiKey);
        return await (await client.SendAsync(request)).Content.ReadAsStringAsync();
    }
    public async Task<string> invoice_list()
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{Endpoint}invoice.list");
        request.Headers.Add("X-API-Key", ApiKey);
        return await (await client.SendAsync(request)).Content.ReadAsStringAsync();
    }
    public async Task<string> nft_list()
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{Endpoint}nft.list");
        request.Headers.Add("X-API-Key", ApiKey);
        return await (await client.SendAsync(request)).Content.ReadAsStringAsync();
    }
    public async Task<string> nft_transfer(string nft_address, string to_address)
    {
        var pre_data = new Dictionary<string, object?>()
                {
                    {"nft_address", nft_address },
                    {"to_address", to_address },
                };
        var data = xJetUtils.SignMessage(pre_data, PrivateKey);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{Endpoint}nft.transfer");
        request.Headers.Add("X-API-Key", ApiKey);
        request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        return await (await client.SendAsync(request)).Content.ReadAsStringAsync();
    }
    public async Task<string> exchanges_pairs()
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{Endpoint}exchanges.pairs");
        request.Headers.Add("X-API-Key", ApiKey);
        return await (await client.SendAsync(request)).Content.ReadAsStringAsync();
    }
    public async Task<string> exchanges_estimate(string[] pair,string type,float amount)
    {
        var pre_data = new Dictionary<string, object?>()
                {
                    {"pair", pair },
                    {"type", type },
                    {"amount", amount },
                };
        var data = xJetUtils.SignMessage(pre_data, PrivateKey);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{Endpoint}exchanges.estimate");
        request.Headers.Add("X-API-Key", ApiKey);
        request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        return await (await client.SendAsync(request)).Content.ReadAsStringAsync();
    }
    public async Task<string> exchanges_create_order(string[] pair, string type, float amount,float min_expected_amount)
    {
        var pre_data = new Dictionary<string, object?>()
                {
                    {"pair", pair },
                    {"type", type },
                    {"amount", amount },
                    {"min_expected_amount", amount },
                };
        var data = xJetUtils.SignMessage(pre_data, PrivateKey);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{Endpoint}exchanges.createOrder");
        request.Headers.Add("X-API-Key", ApiKey);
        request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        return await (await client.SendAsync(request)).Content.ReadAsStringAsync();
    }
    public async Task<string> exchanges_order_status(string id)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{Endpoint}exchanges.orderStatus?id={id}");
        request.Headers.Add("X-API-Key", ApiKey);
        return await (await client.SendAsync(request)).Content.ReadAsStringAsync();
    }
    public static class xJetUtils
{
    public static byte[] HexString2Bytes(string hex)
    {
        int numberChars = hex.Length;
        byte[] bytes = new byte[numberChars / 2];
        for (int i = 0; i < numberChars; i += 2)
        {bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);}
        return bytes;
    }
    public static Dictionary<string, object?> SignMessage(Dictionary<string, object?> message, string privateKey)
    {
        var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        message["query_id"] = (currentTime + 60) << 16;

        var messageJson = JsonConvert.SerializeObject(message);

        var dataToSign = Encoding.UTF8.GetBytes(messageJson);

        var privateKeyBytes = HexString2Bytes(privateKey);

        var signer = new Ed25519Signer();

        var ed25519pkcs8Parameters = new Ed25519PrivateKeyParameters(privateKeyBytes,0);

        signer.Init(true, ed25519pkcs8Parameters);
        signer.BlockUpdate(dataToSign, 0, dataToSign.Length);

        var signature = signer.GenerateSignature();

        var signatureHex = BitConverter.ToString(signature).Replace("-", "").ToLower();

        message["signature"] = signatureHex;

        return message;
    }
}
}

/*
 
 //example code:
 xJet app = new("API_KEY","PRIVATE_API_KEY");
 Console.WriteLine(await app.account_me());

 */
