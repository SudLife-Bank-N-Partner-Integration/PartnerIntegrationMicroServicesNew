using SUDLife_Aashirwaad.Model.Request;
using SUDLife_Aashirwaad.Model.Response;
using SUDLife_CallThirdPartyAPI;
using Newtonsoft.Json;
using RestSharp;
using SUDLife_Aashirwaad.Controllers;

namespace SUDLife_Aashirwaad.ServiceLayer
{
    public class ClsAashirwaad

    {
        private readonly ClsCommonOperations _CommonOperations;
        private readonly ThirdPartyAPI _thirdPartyAPI;
        public readonly ClsAashirwaadPlainResponse _AashirwaadResponse;
        private readonly ILogger<ClsAashirwaad> _logger;

        public ClsAashirwaad(ClsCommonOperations CommonOperations, ThirdPartyAPI thirdPartyAPI,ClsAashirwaadPlainResponse PremiumResponse, ILogger<ClsAashirwaad> logger)
        {
            _CommonOperations = CommonOperations;
            _AashirwaadResponse = PremiumResponse;
            this._thirdPartyAPI = thirdPartyAPI;
            this._logger = logger;
        }
        public async Task<dynamic> AashirwaadDetails(ClsAashirwaadPlainRequest ObjAashirwaad)
        {
            try
            {
                _logger.LogInformation("Creating list request for SDE");
                _CommonOperations.ReadDynamicCollection();
           
                List<ClsSDEBaseKeyValuePair> objlstKeyPair = new List<ClsSDEBaseKeyValuePair>
            {
                new ClsSDEBaseKeyValuePair { key = "@LI_FNAME", value = ObjAashirwaad.ApplicantDetails.ApplicantFName },
                new ClsSDEBaseKeyValuePair { key = "@LI_MNAME", value = string.Empty  },
                new ClsSDEBaseKeyValuePair { key = "@LI_LNAME", value =ObjAashirwaad.ApplicantDetails.ApplicantLName },

                new ClsSDEBaseKeyValuePair { key = "@LI_ENTRY_AGE", value =Convert.ToString(_CommonOperations.CalculateAge(Convert.ToDateTime(_CommonOperations.DateFormating((ObjAashirwaad.ApplicantDetails.ApplicantDateOfBirth)))))  },
                new ClsSDEBaseKeyValuePair { key = "@LI_DOB", value = _CommonOperations.DateFormating((ObjAashirwaad.ApplicantDetails.ApplicantDateOfBirth))},
                new ClsSDEBaseKeyValuePair { key = "@LI_GENDER", value =_CommonOperations.Gender(ObjAashirwaad.ApplicantDetails.ApplicantGender.ToLower()) },



                new ClsSDEBaseKeyValuePair { key = "@LI_STATE", value = "9" },
                new ClsSDEBaseKeyValuePair { key = "@LI_CITY", value = "142" },
                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_FNAME", value =ObjAashirwaad.ProposerDetails.ProposerFName },


                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_MNAME", value = string.Empty },
                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_LNAME", value = ObjAashirwaad.ProposerDetails.ProposerLName },
                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_AGE", value = Convert.ToString(_CommonOperations.CalculateAge(Convert.ToDateTime(_CommonOperations.DateFormating(DateToBeFormatted: ObjAashirwaad.ProposerDetails.ProposerDateOfBirth.ToLower())))) },

                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_DOB", value = _CommonOperations.DateFormating(DateToBeFormatted: ObjAashirwaad.ProposerDetails.ProposerDateOfBirth.ToLower()) },
                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_GENDER", value = _CommonOperations.Gender(ObjAashirwaad.ProposerDetails.ProposerGender.ToLower()) },
                new ClsSDEBaseKeyValuePair { key = "@AGE_PROOF", value ="-1" },


                new ClsSDEBaseKeyValuePair { key = "@SameProposer", value = ObjAashirwaad.ProposerDetails.IsProposersameasApplicant},
                new ClsSDEBaseKeyValuePair { key = "@INPUT_MODE", value = _CommonOperations.GetDynamicValue("InputModeAashirwaad",ObjAashirwaad.PremiumPaymentModes.ToLower()) },

                new ClsSDEBaseKeyValuePair { key = "@PR_ID", value = "1009"},
                  new ClsSDEBaseKeyValuePair { key = "@PR_PT", value = _CommonOperations.ConvertToYears(Convert.ToString(ObjAashirwaad.PolicyTerm)) },
               new ClsSDEBaseKeyValuePair { key = "@PR_PPT", value = _CommonOperations.ConvertToYears(Convert.ToString(ObjAashirwaad.PremiumPaymentTerm))},

                new ClsSDEBaseKeyValuePair { key = "@PR_ANNPREM", value = string.Empty},


                new ClsSDEBaseKeyValuePair { key = "@PR_MI", value = "0"},
                new ClsSDEBaseKeyValuePair { key = "@PR_SA", value = Convert.ToString(ObjAashirwaad.SumAssured) },

                new ClsSDEBaseKeyValuePair { key = "@PR_SAMF", value = "0"},


                new ClsSDEBaseKeyValuePair { key = "@PR_ModalPrem", value = string.Empty},
                new ClsSDEBaseKeyValuePair { key = "@NSAP_FLAG", value = _CommonOperations.StandardAgeProof(ObjAashirwaad.StandardAgeProof.ToLower())},

                new ClsSDEBaseKeyValuePair { key = "@kfc", value = "0"},

                new ClsSDEBaseKeyValuePair { key = "@PR_CHANNEL", value = _CommonOperations.GetDynamicValue("DistributionChannelAashirwaad",ObjAashirwaad.DistributionChannel.ToLower())},

                new ClsSDEBaseKeyValuePair { key = "@TargetSTOFund", value = "0"},

                new ClsSDEBaseKeyValuePair { key = "@PR_OPTION_1", value = _CommonOperations.GetDynamicValue("PayoutOptionAashirwaad",ObjAashirwaad.PayoutOption.ToLower())},
                new ClsSDEBaseKeyValuePair { key = "@OPTION_VALUE_1", value = ""},

                new ClsSDEBaseKeyValuePair { key = "@PR_OPTION_2", value = _CommonOperations.GetDynamicValue("StaffPolicyAashirwaad",ObjAashirwaad.StaffPolicy.ToLower())},
                new ClsSDEBaseKeyValuePair { key = "@OPTION_VALUE_2", value = ""},

        };
                

                string RiderName1 = Convert.ToString(ObjAashirwaad.ADTPDRiderSA);

                string RiderName2 = Convert.ToString(ObjAashirwaad.FIBRIderSA);



                if (ObjAashirwaad.ADTPDRiderOpted.ToLower() == "no" || ObjAashirwaad.ADTPDRiderOpted == "")
                {
                    RiderName1 = "";
                }
                if (ObjAashirwaad.FIBRiderOpted.ToLower() == "no" || ObjAashirwaad.FIBRiderOpted == "")
                {
                    RiderName2 = "";
                }
                Rider rider1 = new Rider();
                rider1.RiderId = 2001;
                List<FormInput> f1 = new List<FormInput>();
                f1.Add(new FormInput() { key = "@RD_SA", value = RiderName1 });
                rider1.formInputs = f1;

                Rider rider2 = new Rider();
                rider2.RiderId = 2002;
                List<FormInput> f2 = new List<FormInput>();
                f2.Add(new FormInput() { key = "@RD_SA", value = RiderName2 });
                rider2.formInputs = f2;

                List<Rider> riderList = new List<Rider>();
                if (RiderName1 == string.Empty)
                {
                    riderList = new List<Rider>();

                    riderList.Add(rider2);


                }


                if (RiderName2 == string.Empty)
                {
                    riderList = new List<Rider>();
                    riderList.Add(rider1);

                }


                if (RiderName1 == string.Empty && RiderName2 != string.Empty)
                {

                    riderList = new List<Rider>();
                    riderList.Add(rider2);
                }

                if (RiderName2 == string.Empty && RiderName1 != string.Empty)
                {

                    riderList = new List<Rider>();

                    riderList.Add(rider1);
                }


                if (RiderName1 == string.Empty & RiderName2 == string.Empty)
                {

                    riderList = new List<Rider>();

                }
                if (RiderName1 != string.Empty && RiderName2 != string.Empty)
                {

                    riderList = new List<Rider>();
                    riderList = new List<Rider>();
                    riderList.Add(rider1);
                    riderList.Add(rider2);
                }
              



                string APIKey = _CommonOperations.APIKey();

                ClsSDEBasePremiumRequest root = new ClsSDEBasePremiumRequest
                {
                    APIKey = APIKey,
                    formInputs = new List<ClsSDEBaseKeyValuePair>(objlstKeyPair),
                    inputPartialWithdrawal = new List<InputPW>(),
                    inputOptions = new List<InputOptions>(),
                    funds = new List<FundInput>(),
                    riders = riderList
                };


                string url = "https://siapi.sudlife.in/nsureservices.svc/generatebiapi";

                string jsonBody = System.Text.Json.JsonSerializer.Serialize(root);
                var APIRequest = jsonBody.Replace("null", "[]");
                _logger.LogInformation("Calling SDE API");
                dynamic ResponseFromNsureservice = _thirdPartyAPI.ClientAPI(url, Method.Post, jsonBody);

                DateTime ResponseTime = DateTime.Now;

                if (!string.IsNullOrEmpty(ResponseFromNsureservice))
                {
                    var jsonString2 = JsonConvert.DeserializeObject<ClsSDEBasePremiumResponse>(ResponseFromNsureservice);
                    if (jsonString2.Status == "Fail" || jsonString2.BIJson == null)
                    {
                        _AashirwaadResponse.Status = jsonString2.Status.ToString();
                        _AashirwaadResponse.Message = jsonString2.Message;
                        _AashirwaadResponse.TransactionId = jsonString2.TransactionId;

                        if (jsonString2.InputValidationStatus != null)
                        {
                            if (jsonString2.InputValidationStatus[0].IpKwMessage.Count > 0)
                            {
                                _AashirwaadResponse.Message += Environment.NewLine + jsonString2.InputValidationStatus[0].IpKwMessage[0];
                            }
                            else
                            {
                                _AashirwaadResponse.Message += Environment.NewLine + jsonString2.InputValidationStatus[0].GeneralError;
                            }
                        }

                       
                    }
                    else
                    {
                        var InputValidationStatus = jsonString2.InputValidationStatus;

                        _AashirwaadResponse.SumAssured = Convert.ToInt32(InputValidationStatus[0].SA);

                        _AashirwaadResponse.ModalPremium = Math.Round(InputValidationStatus[0].ModalPremium);

                        _AashirwaadResponse.Tax = Math.Round(InputValidationStatus[0].Tax);

                        _AashirwaadResponse.ModalPremiumwithTax = Math.Round(_AashirwaadResponse.ModalPremium + _AashirwaadResponse.Tax);

                        _AashirwaadResponse.AnnualPremium = Math.Round(InputValidationStatus[0].AnnualPremium);

                        _AashirwaadResponse.Message = jsonString2.Message;

                        _AashirwaadResponse.Status = jsonString2.Status;


                        _AashirwaadResponse.TransactionId = jsonString2.TransactionId;

                        if (ObjAashirwaad.ADTPDRiderOpted == string.Empty || ObjAashirwaad.ADTPDRiderOpted == "")
                        {
                            ObjAashirwaad.ADTPDRiderOpted = "no";
                        }

                        if (ObjAashirwaad.FIBRiderOpted == string.Empty || ObjAashirwaad.FIBRiderOpted == "")
                        {
                            ObjAashirwaad.FIBRiderOpted = "no";
                        }

                        if (ObjAashirwaad.FIBRiderOpted.ToLower() == "yes" && ObjAashirwaad.ADTPDRiderOpted.ToLower() == "no")
                        {

                            int RiderID1 = InputValidationStatus[1].ProductId;

                            if (RiderID1 == 2002)
                            {
                                _AashirwaadResponse.FIBPremium = Math.Round(InputValidationStatus[1].ModalPremium);

                                _AashirwaadResponse.FIBTax = Math.Round(InputValidationStatus[1].Tax);

                                _AashirwaadResponse.FIBwithTax = Math.Round(_AashirwaadResponse.FIBPremium + _AashirwaadResponse.FIBTax);

                                _AashirwaadResponse.FIBAnnualPremium = Math.Round(InputValidationStatus[1].AnnualPremium);
                            }

                        }

                        if (ObjAashirwaad.ADTPDRiderOpted.ToLower() == "yes" && ObjAashirwaad.FIBRiderOpted.ToLower() == "yes")
                        {

                            int PRID = InputValidationStatus[0].ProductId;
                            int RiderID1 = InputValidationStatus[1].ProductId;
                            int RiderID2 = InputValidationStatus[2].ProductId;


                            if (RiderID2 == 2002)
                            {
                                _AashirwaadResponse.FIBPremium = Math.Round(InputValidationStatus[2].ModalPremium);

                                _AashirwaadResponse.FIBTax = Math.Round(InputValidationStatus[2].Tax);

                                _AashirwaadResponse.FIBwithTax = Math.Round(_AashirwaadResponse.FIBPremium + _AashirwaadResponse.FIBTax);

                                _AashirwaadResponse.FIBAnnualPremium = Math.Round(InputValidationStatus[2].AnnualPremium);
                            }


                            if (RiderID1 == 2001)
                            {
                                _AashirwaadResponse.ADTPDPremium = Math.Round(InputValidationStatus[1].ModalPremium);

                                _AashirwaadResponse.ADTPDTax = Math.Round(InputValidationStatus[1].Tax);

                                _AashirwaadResponse.ADTPDwithTax = Math.Round(_AashirwaadResponse.ADTPDPremium + _AashirwaadResponse.ADTPDTax);

                                _AashirwaadResponse.ADTPDAnnualPremium = Math.Round(InputValidationStatus[1].AnnualPremium);
                            }
                        }


                        if (ObjAashirwaad.ADTPDRiderOpted.ToLower() == "yes" && ObjAashirwaad.FIBRiderOpted.ToLower() == "no")
                        {

                            int PRID = InputValidationStatus[0].ProductId;
                            int RiderID2 = InputValidationStatus[1].ProductId;


                            if (RiderID2 == 2001)
                            {
                                _AashirwaadResponse.ADTPDPremium = Math.Round(InputValidationStatus[1].ModalPremium);

                                _AashirwaadResponse.ADTPDTax = Math.Round(InputValidationStatus[1].Tax);

                                _AashirwaadResponse.ADTPDwithTax = Math.Round(_AashirwaadResponse.ADTPDPremium + _AashirwaadResponse.ADTPDTax);

                                _AashirwaadResponse.ADTPDAnnualPremium = Math.Round(InputValidationStatus[1].AnnualPremium);

                            }
                        }

                        _AashirwaadResponse.TotalPremium = Math.Round(_AashirwaadResponse.ModalPremium + _AashirwaadResponse.FIBPremium + _AashirwaadResponse.ADTPDPremium);

                        _AashirwaadResponse.TotalTAX = Math.Round(_AashirwaadResponse.Tax + _AashirwaadResponse.FIBTax + _AashirwaadResponse.ADTPDTax);

                        _AashirwaadResponse.TotalPremiumwithTax = Math.Round(_AashirwaadResponse.ModalPremiumwithTax + _AashirwaadResponse.FIBwithTax + _AashirwaadResponse.ADTPDwithTax);

                        _AashirwaadResponse.TotalAnnualPremium = Math.Round(_AashirwaadResponse.AnnualPremium + _AashirwaadResponse.FIBAnnualPremium + _AashirwaadResponse.ADTPDAnnualPremium);

                    }
                   
                }
                else
                {

                    _AashirwaadResponse.Message = "Error occured while consuming SDE API.Please contact SUD Admin";
                }

                return _AashirwaadResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured in creating request for SDE or calling SDE API");
               
                throw;
            }
        }
    }
}
