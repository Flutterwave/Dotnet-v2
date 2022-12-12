using System;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rave;
using Rave.Models;
using Rave.Models.Account;
using Rave.Models.VirtualCard;
using Rave.Models.Card;
using Rave.Models.Charge;
using Rave.Models.MobileMoney;
using Rave.Models.Subaccount;
using Rave.Models.Tokens;
using Rave.Models.Ebills;
using Rave.Models.Subscriptions;
using Rave.Models.VirtualAccount;
using Rave.Models.PayPal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RaveTests
{
    [TestClass]
    public class UnitTest1
    {

        string txRef = Environment.GetEnvironmentVariable("txRef");
        string successfulFwRef = Environment.GetEnvironmentVariable("successfulFwRef");
        string unCapturedFwRef = Environment.GetEnvironmentVariable("unCapturedFwRef");
        string tranxRef = Environment.GetEnvironmentVariable("tranxRef");
        string PbKey = Environment.GetEnvironmentVariable("PbKey");
        string ScKey = Environment.GetEnvironmentVariable("ScKey");


        //Pay with PayPal charge test
        [TestMethod]
        public void PayPalTest()
        {
            var raveConfig = new RaveConfig(PbKey, ScKey, false);
            var PayPalCharge = new ChargePayPal(raveConfig);

            var sampleAddress = new Address("333 Fremont Street, San Francisco, CA", "San Francisco", "California", "94105", "US");

            var samplePayload = new PayPalParams(raveConfig.PbfPubKey, raveConfig.SecretKey, "Flutterwave", "Developers", "developers@flutterwavego.com", 120, "USD", "US", "paypal", "PayPal-09100", "https://flutterwave.com/ng/", sampleAddress);

            var res = PayPalCharge.Charge(samplePayload).Result;

            try
            {
                Assert.IsNotNull(res.Data);
                Console.WriteLine(res.Data);
                Assert.AreEqual("success", res.Status);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        //Preauth Card charge test
        //[TestMethod]
        //public void preauthTest()
        //{

        //    var raveConfig = new RaveConfig(PbKey, ScKey, false);
        //    var preauthCard = new PreAuth(raveConfig);

        //    var card = new Card("5377283645077450", "09", "21", "789");

        //    var preauthResponse = preauthCard.Preauthorize(new PreAuthParams(raveConfig.PbfPubKey, raveConfig.SecretKey, "Olufumi", "Obafumiso", "olufemi@gmail.com", 120, "USD", card) { TxRef = txRef }).Result;


        //    try
        //    {
        //        Assert.IsNotNull(preauthResponse.Data);
        //        Assert.AreEqual(preauthResponse.Status, "success");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }


        //}

        ////Noauth Card charge test
        //[TestMethod]
        //public void avschargetest()
        //{
        //    var raveConfig = new RaveConfig(PbKey, ScKey, false);
        //    var cardCharge = new ChargeCard(raveConfig);
        //    var card = new Card("5377283645077450", "09", "21", "789");

        //    var payload = new CardParams(PbKey, ScKey, "Anonymous", "Tester", "user@example.com", 200, "USD", card, "07205", "Hillside", "470 Mundet PI", "NJ", "US");

        //    var res = cardCharge.Charge(payload).Result;

        //    try
        //    {
        //        Assert.IsNotNull(res.Data);
        //        Console.WriteLine(res.Data);
        //        Assert.AreEqual("success", res.Status);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }


        //}


        ////Mobile money charge test
        //[TestMethod]
        //public void mobileMoneyTest()
        //{

        //    var raveConfig = new RaveConfig(PbKey, ScKey, false);
        //    var mobilemoney = new ChargeMobileMoney(raveConfig);

        //    var Payload = new MobileMoneyParams(PbKey, ScKey, "Anonymous", "customer", "user@example.com", 1055, "GHS", "054709929220", "MTN", "GH", "mobilemoneygh", tranxRef);
        //    var cha = mobilemoney.Charge(Payload).Result;

        //    try
        //    {
        //        Assert.IsNotNull(cha.Data);
        //        Console.WriteLine(cha.Data);
        //        Assert.AreEqual("success", cha.Status);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //}

        ////Account charge test
        //[TestMethod]
        //public void accountTest()
        //{
        //    var raveConfig = new RaveConfig(PbKey, ScKey, false);
        //    var account = new ChargeAccount(raveConfig);

        //    var Payload = new AccountParams(PbKey, ScKey, "customer", "customer", "user@example.com", "0690000031", 1000, "044", "NGN", "MC-0292920");
        //    var chargeResponse = account.Charge(Payload).Result;

        //    if (chargeResponse.Data.Status == "success-pending-validation")
        //    {

        //        Payload.Otp = "12345";
        //        chargeResponse = account.Charge(Payload).Result;
        //    }
        //    // ValidateAccountCharge(chargeResponse.Data.FlwRef);

        //    Assert.IsNotNull(chargeResponse.Data);
        //    Assert.AreEqual("success", chargeResponse.Status);
        //}


        ////Subaccount creation test
        //[TestMethod]
        //public void CreateSubAccountTest()
        //{
        //    var raveConfig = new RaveConfig(PbKey, ScKey, false);
        //    var subacc = new CreateSubAccount(raveConfig);

        //    var payload = new SubAccountParams(ScKey, "0690000031", "0690000031", "TEST BUSINESS", "user@example.com", "0900000000", "0900000000");
        //    var chargeResponse = subacc.Charge(payload).Result;

        //    // Assert.IsNotNull(chargeResponse.Data);
        //    Assert.AreEqual("error", chargeResponse.Status);
        //}

        ////Tokenized Card charge test suite
        //[TestMethod]
        //public void TokenTest()
        //{

        //    var raveConfig = new RaveConfig(PbKey, ScKey, false);
        //    var tokenCard = new Tokenize(raveConfig);

        //    var tokenparam = new TokensParams(raveConfig.SecretKey, "Olufumi", "Obafumiso", "olufemi@gmail.com", tranxRef, 100, "NGN", "NG")
        //    {
        //        Token = "flw-t1nf-139d69763063262928b77bc1f4fba199-m03k",
        //        Narration = "Test",
        //    };
        //    var tokenResponse = tokenCard.Charge(tokenparam).Result;


        //    try
        //    {
        //        Assert.IsNotNull(tokenResponse.Data);
        //        Assert.AreEqual(tokenResponse.Status, "success");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }


        //}


        //[TestMethod]
        //public void VirtualStaticAccountTest()
        //{
        //    var virtuala = new VirtualAccount();
        //    var virtualaccountparams = new VirtualAccountParams( "TEST-C-ACCOUNT", ScKey, "d@gmail.com", "TRF-SHDJß");

        //    var chargeResponse = virtuala.CreateStaticVirtualAccount(virtualaccountparams);

        //    System.Console.WriteLine("chargeResponse:" + chargeResponse.ToString());

        //    JObject json = JObject.Parse(chargeResponse);
        //    // Assert.IsNotNull(chargeResponse.Data);
        //    Assert.AreEqual("success", (string)json.SelectToken("status"));
        //}

        //[TestMethod]
        //public void VirtualTransactionAccountTest()
        //{
        //    var virtuala = new VirtualAccount();
        //    var virtualaccountparams = new VirtualAccountParams("TEST-C-ACCOUNT", ScKey, "d@gmail.com", "TRF-SHDJß", "100");

        //    var chargeResponse = virtuala.CreateTransactionVirtualAccount(virtualaccountparams);

        //    System.Console.WriteLine("chargeResponse:" + chargeResponse.ToString());

        //    JObject json = JObject.Parse(chargeResponse);
        //    // Assert.IsNotNull(chargeResponse.Data);
        //    Assert.AreEqual("success", (string)json.SelectToken("status"));
        //}

        //[TestMethod]
        //public void VirtualDurationAccountTest()
        //{
        //    var virtuala = new VirtualAccount();
        //    var virtualaccountparams = new VirtualAccountParams(1, 2, "TEST-C-ACCOUNT", ScKey, "d@gmail.com", "TRF-SHDJß", "100");

        //    var chargeResponse = virtuala.CreateTransactionVirtualAccount(virtualaccountparams);

        //    System.Console.WriteLine("chargeResponse:" + chargeResponse.ToString());

        //    JObject json = JObject.Parse(chargeResponse);
        //    // Assert.IsNotNull(chargeResponse.Data);
        //    Assert.AreEqual("success", (string)json.SelectToken("status"));
        //}

        //[TestMethod]
        //public void CreateEbillsTest()
        //{
        //    var ebillscreate = new CreateOrder();
        //    var ebillscreateparams = new EbillsCreateRequestParams("NGN", 1, "TEST-C", ScKey, "d@gmail.com", 100, "09384747474", "773838837373", "127.9.0.7");

        //    var chargeResponse = ebillscreate.doCreateOrder(ebillscreateparams);

        //    System.Console.WriteLine("chargeResponse:" + chargeResponse.ToString());

        //    JObject json = JObject.Parse(chargeResponse);
        //    // Assert.IsNotNull(chargeResponse.Data);
        //    Assert.AreEqual("success", (string)json.SelectToken("status"));
        //}

        //[TestMethod]
        //public void UpdateEbillsTest()
        //{
        //    var ebillsupdate = new UpdateOrder();
        //    var ebillsupdateparams = new EbillsUpdateRequestParams("NGN", ScKey, 500, "RVEBLS-DD2EB67752B9-36138");

        //    var chargeResponse = ebillsupdate.doUpdateOrder(ebillsupdateparams);

        //    System.Console.WriteLine("chargeResponse:" + chargeResponse.ToString());

        //    JObject json = JObject.Parse(chargeResponse);
        //    // Assert.IsNotNull(chargeResponse.Data);
        //    Assert.AreEqual("success", (string)json.SelectToken("status"));
        //}

        ////Subscription list test
        //[TestMethod]
        //public void ListSubscriptions()
        //{
        //    var listsubs = new ListSubscriptions();

        //    var chargeResponse = listsubs.doListSubscriptions(ScKey);

        //    System.Console.WriteLine("chargeResponse:" + chargeResponse.ToString());

        //    JObject json = JObject.Parse(chargeResponse);
        //    // Assert.IsNotNull(chargeResponse.Data);
        //    Assert.AreEqual("success", (string)json.SelectToken("status"));
        //}

        ////Virtual Card creation test
        //[TestMethod]
        //public void CreateVirtualCard()
        //{
        //    var createvirtualcard = new VirtualCard();

        //    var virtualcardparams = new VirtualCardParams(ScKey, "clopat", "NGN", "100", "lagos, lagos", "lagos", "lagos", "0000", "NG", "www.facebook.com");
        //    var chargeResponse = createvirtualcard.doCreateVirtualCard(virtualcardparams);

        //    System.Console.WriteLine("chargeResponse:" + chargeResponse.ToString());

        //    JObject json = JObject.Parse(chargeResponse);
        //    // Assert.IsNotNull(chargeResponse.Data);
        //    Assert.AreEqual("error", (string)json.SelectToken("status"));
        //}

    }
    }
