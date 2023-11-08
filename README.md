# Exthand.Gateway
Client library to use Exthand:Gateway and connect to +1300 banks worldwide.
This is a .Net Standard 2.0 version.
Version .Net core 6 is also available.

## How to get started.

Company website: https://www.exthand.com
Nuget Paackage of this repo: https://www.nuget.org/packages/Exthand.Gateway

### 1. First create an account.

Go to  https://developer.bankingsdk.com and register yourself and your company.
Create an application, get the application key and secret.
Store the secret in a safe place.
Send us ( support at exthand.com) your application key and company key. We'll provide you a license key.

From there, you'll be able to debug your sandbox calls in real time, to get bank statements or initiate payments.
In the documentation part of the website, you'll find the latest PDF Documentation file. Read it carefully, mainly chapter 6.


### 2. BankingSDK Docker.

To use Exthand:Gateway, to connect to banks, you also have to install a Docker container in your own cloud infrastrucutre.
Docker container might be found here: https://hub.docker.com/r/bankingsdk/bankingsdkdockerapi/tags?page=1&ordering=last_updated

You have to install it first (check PDF Documentation file).
Then, use this nuget to call the Docker and get access to bank APIs.


### 3. The global flow.

Your app using this package will be able to call API of your BankingSDK Docker.
Your BankingSDK Docker instance will call the Exthand:Gateway API and transfer your requests to the banks.
Banks does answer to the Exthand:Gateway which sends back the response to your Docker.
As simple as that!

The day you get your own open banking license, you have to change the configuration file in the BankingSDK Docker.
It will then be able to directly connect to banks without going throught the Exthand:Gateway anymore.


## How to start using the Exthand:Gateway with this nuget package.

### 1. Register a user.

Before being able to get transactions or initiate payement, you have to send to the Exthand:Gateway (E:G) information about the your user (PSU).
For PIS, your internal PSU ID and an email address or cell phone number is sufficient.
For AIS, we require first name, last name, date of birth, email address and version of the Terms and Conditions accepted by the PSU.

#### Call GetTCAsync (AIS only)

Retrieves the latest version of the Terms and Conditions and Privacy Notice.
If doing AIS, you have to show or provide a link to those two files, and collect the consent (click on checkbox). The consent means: Me, as a PSU, I accept Exthand shares my banking data with __your company__.
If doing PIS, forget this, consent is not required.

Once you get the consent of the PSU, you have to register him on E:G.

#### Call CreateUserAsync

See above to know how much data you have to provide to this method.
This will return a [UserRegisterResponse](https://github.com/exthand/Exthand.Gateway/blob/master/Models/UserRegisterResponse.cs) object.
Normal case, action property should be == "OK", then you have to store the userContext property with your PSU data.
You will need userContext for all operations, it's __important__ to store it attached to your user and be able to provide it to E/G.


### 2. Payment Initiation (PIS)

Payment initiation is a four step process:
* You get the bank connector behavior by calling GetBankPaymentAccessOptionsAsync
* You initiate the payment and redirect your PSU to his bank for signing the payment.
* You call the finalize method when you get called back by the bank.
* Later, you call the status method to get a finalized status of your payment (executed, canceled, etc).

#### Call GetBankPaymentAccessOptionsAsync

This call returns information, that help build the payment request object for specific connector. 

#### Call PaymentInitiateAsync

This call needs a [PaymentInitRequest](https://github.com/exthand/Exthand.Gateway/blob/master/Models/PaymentInitRequest.cs) object.

You'll have to create a Flow object on our side. That Flow should have a unique identifier. You'll use the Flow id in all calls (init/finalize) which arelinked together.

You can fill it in like this sample code:
```C# 
PaymentInitRequest paymentInitRequest = new()

            {
                connectorId = 1, // Calls ING in BELGIUM
                userContext = userContext, // You do remember this one ;)
                tppContext = new TppContext() 
                {
                    TppId = _options.TPPName, // Your name.
                    App = _options.AppName,   // Your app name.
                    Flow = flow.Id.ToString() // An unique identifier of the flow in your system.
                },
                paymentInitiationRequest = new PaymentInitiationRequest()
                {
                    amount = payment.Amount,  // Amount to be paid.
                    currency = "EUR",         // Currency
                    recipient = new RecipientInfo()
                    {
                        iban = payment.IBAN.BankAccount,   
                        name = payment.Person.FirstName + " " + payment.Person.LastName
                    },
                    debtor = new DebtorInfo()
                    {
                        currency = "EUR",
                        iban = debtorIbanAccount,
                        name = payment.CounterpartyName,
                        email = payment.ToEmail
                    },
                    remittanceInformationUnstructured = payment.Remittance,  // Remittance information (MAX 140 CHAR)
                    endToEndId = flow.Id.ToString().Replace("-", ""),        // Unique identifier for this transaction (sent to the bank, MAX 35 CHAR)
                    flowId = flow.Id.ToString(),                             // Unique identifier for this transaction.
                    redirectUrl = _options.RedirectURL + redirectURL,        // Your redirect URL
                    psuIp = IP,                                              // The IP Address (IPv4, IPv6) of the PSU
                    requestedExecutionDate = DateTime.UtcNow                 // Requested payment date.
                }
            };
```

Once the call is executed, you get [PaymentInitResponse](https://github.com/exthand/Exthand.Gateway/blob/master/Models/PaymentInitResponse.cs) object. Save the FlowContext included in the result. It will be needed in the next step.
In that response, the [ResultStatus](https://github.com/exthand/Exthand.Gateway/blob/master/Models/ResultStatus.cs) indicates if the payment can be initiated.
Value should be REDIRECT, and redirect url can be found in ```dataString``` property like in the following example:

``` C#

            switch ((ResultStatus)flow.ResponseInitStatus)
            {
                case ResultStatus.UNKNOW:
                    break;
                case ResultStatus.DONE:
                    break;
                case ResultStatus.REDIRECT:
                    return Redirect(flow.ResponseInitDataString);
                case ResultStatus.DECOUPLED:
                    return RedirectToPage("/bank/handlerSCA", new { id = flow.Id });
                case ResultStatus.PASSWORD:
                    return RedirectToPage("/bank/handlerSCA", new { id = flow.Id });
                case ResultStatus.MORE_INFO:
                    return RedirectToPage("/bank/handlerSCA", new { id = flow.Id });
                case ResultStatus.SELECT_OPTION:
                    return RedirectToPage("/bank/handlerSCA", new { id = flow.Id });
                case ResultStatus.ERROR:
                    break;
            }
```


#### Calls to Finalize PaymentFinalizeAsync

This call is executed to finalize the payment process.
You have to execute it when your redirect URL is called back after the PSU signed the payment on the bank's app or website.

You should first call FindFlowIdAsync, give it the QueryString and get back your flowId.
```C#
            string query = Request.QueryString.ToString();
            string flowId = await FindFlowIdAsync(query);
```

Once you have you flowId you can recover previously saved FlowContex and call the PaymentFinalizeAsync method with a [PaymentFinalizeRequest](https://github.com/exthand/Exthand.Gateway/blob/master/Models/PaymentFinalizeRequest.cs).

```C#
            PaymentFinalizeRequest paymentFinalizeRequest = new PaymentFinalizeRequest()
            {
                flow = flow.ResponseInitFlowContext, // Pay attention to the fact we are speaking now about FlowContext and not Flow's ID.
                tppContext = new()
                {
                    TppId = _options.TPPName,
                    App = _options.AppName,
                    Flow = flow.Id.ToString()
                },
                userContext = flow.UserContext,
                dataString = query  // The querystring you sent to FindFlowIdAsync.
            };
```

The [PaymentFinalizeResponse](https://github.com/exthand/Exthand.Gateway/blob/master/Models/PaymentFinalizeResponse.cs) object will be returned.
The ```resultStatus``` property will contain a result code based on [PaymentStatusISO20022](https://github.com/exthand/Exthand.Gateway/blob/master/Models/PaymentStatusISO20022.cs).
You can handle that property in a way like this.

```C#
            switch ((PaymentStatusISO20022)flow.ResponsePaymentStatus)
            {
                case ResultStatus.UNKNOW:
                    payment = await _paymentService.SetPendingAsync(flow.PaymentId.Value);
                    // As we don't know the final status, we set it as pending in our system.
                    return RedirectToAction("PayPending");
                case ResultStatus.DONE:
                    switch ((PaymentStatusISO20022)flow.ResponsePaymentStatus)
                    {
                        case PaymentStatusISO20022.ACCC:
                            // Payment accepted.
                            payment = await _paymentService.SetPaidAsync(flow.PaymentId.Value);
                            return RedirectToAction("ithankyou");
                        case PaymentStatusISO20022.RJCT:
                        case PaymentStatusISO20022.BLCK:
                        case PaymentStatusISO20022.CANC:
                            // Payment has been refused.
                            payment = await _paymentService.SetRejectedAsync(flow.PaymentId.Value);
                            return RedirectToAction("PayRejected");
                        default:
                            // Payment status is unknow. Should be good, best is to call PaymentStatusAsync later. 
                            payment = await _paymentService.SetPendingAsync(flow.PaymentId.Value);
                            return RedirectToAction("PayPending");
                    }
                    break;
                case ResultStatus.REDIRECT:
                    // One more redirection requested by the bank, let's play.
                    return Redirect(flow.ResponseFinalizeDataString);
                case ResultStatus.ERROR:
                    // Handle the error here.
                    break;
            }
```

#### Call PaymentStatusAsync

This call allows you to get the latests status of a payment. Most of the time, status received in the Finalize call are the correct and final ones, butnot always...  ;)
Just call the method with the [PaymentStatusRequest](https://github.com/exthand/Exthand.Gateway/blob/master/Models/PaymentStatusRequest.cs) initialized.


You'll get an answer object [PaymentStatusResponse](https://github.com/exthand/Exthand.Gateway/blob/master/Models/PaymentStatusResponse.cs). Structure is similar to the one you receive when you call the FinalizeAsync method.




