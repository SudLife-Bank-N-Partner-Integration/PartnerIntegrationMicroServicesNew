using SUDLife_Abhay.Models.Request;
using SUDLife_Abhay.Models.Response;
using SUDLife_CallThirdPartyAPI;
using Newtonsoft.Json;
using RestSharp;

namespace SUDLife_Abhay.ServiceLayer
{
    public class ClsAbhay
    {
        private readonly ClsCommonOperations _CommonOperations;
        private readonly ThirdPartyAPI _thirdPartyAPI;
        public readonly ClsAbhayPlainResponse _AbhayResponse;
        private readonly ILogger<ClsAbhay> _logger;

        public ClsAbhay(ClsCommonOperations CommonOperations, ThirdPartyAPI thirdPartyAPI, ClsAbhayPlainResponse PremiumResponse, ILogger<ClsAbhay> logger)
        {
            _CommonOperations = CommonOperations;
            _AbhayResponse = PremiumResponse;
            this._thirdPartyAPI = thirdPartyAPI;
            this._logger = logger;
        }

        public async Task<dynamic> AbhayDetails(ClsAbhayPlainRequest ObjAbhay)
        {
            try
            {
                _logger.LogInformation("Creating list request for SDE");
                _CommonOperations.ReadDynamicCollection();

                List<ClsSDEBaseKeyValuePair> objlstKeyPair = new List<ClsSDEBaseKeyValuePair>
            {
                new ClsSDEBaseKeyValuePair { key = "@LI_FNAME", value = ObjAbhay.ApplicantDetails.ApplicantFName },
                new ClsSDEBaseKeyValuePair { key = "@LI_MNAME", value = string.Empty  },
                new ClsSDEBaseKeyValuePair { key = "@LI_LNAME", value =ObjAbhay.ApplicantDetails.ApplicantLName },

                new ClsSDEBaseKeyValuePair { key = "@LI_ENTRY_AGE", value =Convert.ToString(_CommonOperations.CalculateAge(Convert.ToDateTime(_CommonOperations.DateFormating((ObjAbhay.ApplicantDetails.ApplicantDateOfBirth)))))  },
                new ClsSDEBaseKeyValuePair { key = "@LI_DOB", value = _CommonOperations.DateFormating((ObjAbhay.ApplicantDetails.ApplicantDateOfBirth))},
                new ClsSDEBaseKeyValuePair { key = "@LI_GENDER", value =_CommonOperations.Gender(ObjAbhay.ApplicantDetails.ApplicantGender.ToLower()) },



                new ClsSDEBaseKeyValuePair { key = "@LI_STATE", value = "9" },
                new ClsSDEBaseKeyValuePair { key = "@LI_CITY", value = "142" },
                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_FNAME", value =ObjAbhay.ProposerDetails.ProposerFName },


                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_MNAME", value = string.Empty },
                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_LNAME", value = ObjAbhay.ProposerDetails.ProposerLName },
                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_AGE", value = Convert.ToString(_CommonOperations.CalculateAge(Convert.ToDateTime(_CommonOperations.DateFormating(DateToBeFormatted: ObjAbhay.ProposerDetails.ProposerDateOfBirth.ToLower())))) },

                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_DOB", value = _CommonOperations.DateFormating(DateToBeFormatted: ObjAbhay.ProposerDetails.ProposerDateOfBirth.ToLower()) },
                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_GENDER", value = _CommonOperations.Gender(ObjAbhay.ProposerDetails.ProposerGender.ToLower()) },
                new ClsSDEBaseKeyValuePair { key = "@AGE_PROOF", value ="-1" },


                new ClsSDEBaseKeyValuePair { key = "@SameProposer", value = ObjAbhay.ProposerDetails.IsProposersameasApplicant},
                new ClsSDEBaseKeyValuePair { key = "@INPUT_MODE", value = _CommonOperations.GetDynamicValue("InputMode",ObjAbhay.PremiumPaymentModes.ToLower()) },

                new ClsSDEBaseKeyValuePair { key = "@PR_ID", value = "1009"},
                  new ClsSDEBaseKeyValuePair { key = "@PR_PT", value = _CommonOperations.ConvertToYears(Convert.ToString(ObjAbhay.PolicyTerm)) },
               new ClsSDEBaseKeyValuePair { key = "@PR_PPT", value = _CommonOperations.ConvertToYears(Convert.ToString(ObjAbhay.PremiumPaymentTerm))},

                new ClsSDEBaseKeyValuePair { key = "@PR_ANNPREM", value = string.Empty},


                new ClsSDEBaseKeyValuePair { key = "@PR_MI", value = "0"},
                new ClsSDEBaseKeyValuePair { key = "@PR_SA", value = Convert.ToString(ObjAbhay.SumAssured) },

                new ClsSDEBaseKeyValuePair { key = "@PR_SAMF", value = "0"},


                new ClsSDEBaseKeyValuePair { key = "@PR_ModalPrem", value = string.Empty},
                new ClsSDEBaseKeyValuePair { key = "@NSAP_FLAG", value = _CommonOperations.StandardAgeProof(ObjAbhay.StandardAgeProof.ToLower())},

                new ClsSDEBaseKeyValuePair { key = "@kfc", value = "0"},

                new ClsSDEBaseKeyValuePair { key = "@LI_SMOKE", value =  _CommonOperations.GetDynamicValue("Smoker",ObjAbhay.Smoker.ToLower())},

                new ClsSDEBaseKeyValuePair { key = "@TargetSTOFund", value = "0"},

                new ClsSDEBaseKeyValuePair { key = "@PR_OPTION_1", value = _CommonOperations.GetDynamicValue("BenefitOption",ObjAbhay.BenefitOption.ToLower())},
                new ClsSDEBaseKeyValuePair { key = "@OPTION_VALUE_1", value = ""},

                new ClsSDEBaseKeyValuePair { key = "@PR_OPTION_2", value = _CommonOperations.GetDynamicValue("PayoutOptionAbhay",ObjAbhay.PayoutOption.ToLower())},
                new ClsSDEBaseKeyValuePair { key = "@OPTION_VALUE_2", value = ""},


                new ClsSDEBaseKeyValuePair { key = "@PR_OPTION_3", value = _CommonOperations.GetDynamicValue("DistributionChannelAbhay",ObjAbhay.DistributionChannel.ToLower())},
                new ClsSDEBaseKeyValuePair { key = "@OPTION_VALUE_3", value = ""},

                new ClsSDEBaseKeyValuePair { key = "@PR_OPTION_4", value = _CommonOperations.GetDynamicValue("StaffPolicyAbhay",ObjAbhay.StaffPolicy.ToLower())},
                new ClsSDEBaseKeyValuePair { key = "@OPTION_VALUE_4", value = ""},


                new ClsSDEBaseKeyValuePair { key = "@PR_OPTION_5", value = _CommonOperations.GetDynamicValue("SubDistributionChannelAbhayPremium",ObjAbhay.SubDistributionChannel.ToLower())},
                new ClsSDEBaseKeyValuePair { key = "@OPTION_VALUE_5", value = ""},

        };

                string RiderName1 = ObjAbhay.AATPDRiderSA;

                if (ObjAbhay.AATPDRiderOpted.ToLower() == "no" || ObjAbhay.AATPDRiderOpted == "")
                {
                    RiderName1 = "";
                }

                Rider rider3 = new Rider();
                rider3.RiderId = 2001;
                List<FormInput> f3 = new List<FormInput>();
                f3.Add(new FormInput() { key = "@RD_SA", value = RiderName1 });
                rider3.formInputs = f3;
                List<Rider> riderList = new List<Rider>();

                if (RiderName1 == "")
                {
                    riderList = new List<Rider>();
                }

                if (RiderName1 != "")
                {
                    riderList = new List<Rider>();
                    riderList.Add(rider3);
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
                    var jsonString2 = _JsonConvert.DeSerializeObject<ClsSDEBasePremiumResponse>(ResponseFromNsureservice);
                    if (jsonString2.Status == "Fail" || jsonString2.BIJson == null)
                    {
                        _AbhayResponse.Status = jsonString2.Status;
                        _AbhayResponse.Message = jsonString2.Message;
                        _AbhayResponse.TransactionId = jsonString2.TransactionId;

                        if (jsonString2.InputValidationStatus != null)
                        {
                            if (jsonString2.InputValidationStatus[0].IpKwMessage.Count > 0)
                            {
                                _AbhayResponse.Message += Environment.NewLine + jsonString2.InputValidationStatus[0].IpKwMessage[0];
                            }
                            else
                            {
                                _AbhayResponse.Message += Environment.NewLine + jsonString2.InputValidationStatus[0].GeneralError;
                            }
                        }
                    }
                    else
                    {
                        var InputValidationStatus = jsonString2.InputValidationStatus;
                        _AbhayResponse.ModalPremium = InputValidationStatus[0].ModalPremium;
                        _AbhayResponse.Tax = InputValidationStatus[0].Tax;
                        _AbhayResponse.ModalPremiumwithTax = (_AbhayResponse.ModalPremium + _AbhayResponse.Tax);
                        _AbhayResponse.AnnualPremium = InputValidationStatus[0].AnnualPremium;

                        _AbhayResponse.Message = jsonString2.Message;
                        _AbhayResponse.Status = jsonString2.Status;
                        _AbhayResponse.TransactionId = jsonString2.TransactionId;



                        _AbhayResponse.TotalPremium = Math.Round((_AbhayResponse.ModalPremium));

                        _AbhayResponse.TotalTAX = Math.Round((_AbhayResponse.Tax));

                        _AbhayResponse.TotalPremiumwithTax = Math.Round((_AbhayResponse.ModalPremiumwithTax));

                        _AbhayResponse.TotalAnnualPremium = Math.Round((_AbhayResponse.AnnualPremium));

                    }
                }
                else
                {

                    _AbhayResponse.Message = "Error occured while consuming SDE API.Please contact SUD Admin";
                }

                return _AbhayResponse;

            }
            catch (Exception ex) 
            {
                _logger.LogError("An error occured in creating request for SDE or calling SDE API");

                throw;
            }
            } 
    }
}
