using Newtonsoft.Json;
using RestSharp;
using SUDLife_Akshay.Model.Request;
using SUDLife_Akshay.Model.Response;
using SUDLife_CallThirdPartyAPI;

namespace SUDLife_Akshay.ServiceLayer
{
    public class ClsAkshay
    {
        private readonly ClsCommonOperations _CommonOperations;
        private readonly ThirdPartyAPI _thirdPartyAPI;
        public readonly ClsAkshayPlainResponse _AkshayResponse;
        private readonly ILogger<ClsAkshay> _logger;

        public ClsAkshay(ClsCommonOperations CommonOperations, ThirdPartyAPI thirdPartyAPI, ClsAkshayPlainResponse PremiumResponse, ILogger<ClsAkshay> logger)
        {
            _CommonOperations = CommonOperations;
            _AkshayResponse = PremiumResponse;
            this._thirdPartyAPI = thirdPartyAPI;
            this._logger = logger;
        }
        public async Task<dynamic> AkshayDetails(ClsAkshayPlainRequest ObjAkshay)
        {
            try
            {
                ClsAkshayPlainResponse objPremiumResponse = new ClsAkshayPlainResponse();
                _CommonOperations.ReadDynamicCollection();



                int ApplicantAge = Convert.ToInt32(_CommonOperations.CalculateAge(Convert.ToDateTime(_CommonOperations.DateFormating(DateToBeFormatted: ObjAkshay.ApplicantDetails.ApplicantDateOfBirth))));


                if (ApplicantAge < 25)
                {
                    objPremiumResponse.Status = "Failure";
                    objPremiumResponse.Message = "Minimum entry age for Star Union Dai-ichi Akshay Plan is 25 years. Kindly revise the age";
                    return objPremiumResponse;

                }

                if (ApplicantAge > 50)
                {
                    objPremiumResponse.Status = "Failure";
                    objPremiumResponse.Message = "Maximum entry age for Star Union Dai-ichi Akshay Plan is 50 years. Kindly revise the age";
                    return objPremiumResponse;


                }





                List<ClsSDEBaseKeyValuePair> objlstKeyPair = new List<ClsSDEBaseKeyValuePair>

            {
                new ClsSDEBaseKeyValuePair { key = "@LI_FNAME", value = ObjAkshay.ApplicantDetails.ApplicantFName },
                new ClsSDEBaseKeyValuePair { key = "@LI_MNAME", value = string.Empty  },
                new ClsSDEBaseKeyValuePair { key = "@LI_LNAME", value =ObjAkshay.ApplicantDetails.ApplicantLName },

                new ClsSDEBaseKeyValuePair { key = "@LI_ENTRY_AGE", value =  Convert.ToString(ApplicantAge) },
                new ClsSDEBaseKeyValuePair { key = "@LI_DOB", value = _CommonOperations.DateFormating(DateToBeFormatted: ObjAkshay.ApplicantDetails.ApplicantDateOfBirth)},
                new ClsSDEBaseKeyValuePair { key = "@LI_GENDER", value =_CommonOperations.Gender(ObjAkshay.ApplicantDetails.ApplicantGender.ToLower()) },


                new ClsSDEBaseKeyValuePair { key = "@LI_STATE", value = "19" },
                new ClsSDEBaseKeyValuePair { key = "@LI_CITY", value = "65" },
                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_FNAME", value =ObjAkshay.ProposerDetails.ProposerFName },


                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_MNAME", value = string.Empty },
                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_LNAME", value = ObjAkshay.ProposerDetails.ProposerLName },
                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_AGE", value = Convert.ToString(Convert.ToInt32(_CommonOperations.CalculateAge(Convert.ToDateTime(_CommonOperations.DateFormating(DateToBeFormatted: ObjAkshay.ProposerDetails.ProposerDateOfBirth))))) },


                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_DOB", value = _CommonOperations.DateFormating(DateToBeFormatted: ObjAkshay.ProposerDetails.ProposerDateOfBirth) },
                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_GENDER", value = _CommonOperations.Gender(ObjAkshay.ProposerDetails.ProposerGender.ToLower()) },
                new ClsSDEBaseKeyValuePair { key = "@AGE_PROOF", value ="-1" },


                new ClsSDEBaseKeyValuePair { key = "@SameProposer", value = ObjAkshay.ProposerDetails.IsProposersameasApplicant},
                new ClsSDEBaseKeyValuePair { key = "@INPUT_MODE", value = _CommonOperations.GetDynamicValue("InputMode",ObjAkshay.PremiumPaymentModes.ToLower())},
                new ClsSDEBaseKeyValuePair { key = "@PR_ID", value = "1025"},

                new ClsSDEBaseKeyValuePair { key = "@PR_SA", value = Convert.ToString(ObjAkshay.SumAssured) },


                new ClsSDEBaseKeyValuePair { key = "@PR_PT", value = _CommonOperations.ConvertToYears(Convert.ToString(ObjAkshay.PolicyTerm)) },
                new ClsSDEBaseKeyValuePair { key = "@PR_PPT", value = _CommonOperations.ConvertToYears(Convert.ToString(ObjAkshay.PremiumPaymentTerm)) },
                new ClsSDEBaseKeyValuePair { key = "@PR_ANNPREM", value = string.Empty},


                new ClsSDEBaseKeyValuePair { key = "@PR_MI", value = "0"},
                new ClsSDEBaseKeyValuePair { key = "@PR_SAMF", value = "0"},


                new ClsSDEBaseKeyValuePair { key = "@PR_ModalPrem", value = string.Empty},
                new ClsSDEBaseKeyValuePair { key = "@kfc", value = "0"},

                new ClsSDEBaseKeyValuePair { key = "@NSAP_FLAG", value = _CommonOperations.StandardAgeProof(ObjAkshay.StandardAgeProof.ToLower()) },

                new ClsSDEBaseKeyValuePair { key = "@TargetSTOFund", value = "0"},

                new ClsSDEBaseKeyValuePair { key = "@PR_OPTION_1", value = _CommonOperations.GetDynamicValue("DistributionChannel",ObjAkshay.DistributionChannel.ToLower())},
                new ClsSDEBaseKeyValuePair { key = "@OPTION_VALUE_1", value = ""},

                new ClsSDEBaseKeyValuePair { key = "@PR_OPTION_2", value = _CommonOperations.GetDynamicValue("StaffDiscount",ObjAkshay.StaffPolicy.ToLower())},
                new ClsSDEBaseKeyValuePair { key = "@OPTION_VALUE_2", value = ""},

        };



                string RiderName1 = Convert.ToString(ObjAkshay.COVIDRiderSA);

                if (ObjAkshay.COVIDRiderOpted.ToLower() == "no" || ObjAkshay.COVIDRiderOpted == "")
                {
                    RiderName1 = "";
                }

                Rider rider3 = new Rider();
                rider3.RiderId = 2003;
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
                    riders = new List<Rider>(riderList)
                };


                string url = "https://siapi.sudlife.in/nsureservices.svc/generatebiapi";

                string jsonBody = System.Text.Json.JsonSerializer.Serialize(root);
                var APIRequest = jsonBody.Replace("null", "[]");
                _logger.LogInformation("Calling SDE API");
                dynamic ResponseFromNsureservice = _thirdPartyAPI.ClientAPI(url, Method.Post, jsonBody);
                if (!string.IsNullOrEmpty(ResponseFromNsureservice))
                {
                    var jsonString2 = JsonConvert.DeserializeObject<ClsSDEBasePremiumResponse>(ResponseFromNsureservice);
                    if (jsonString2.Status == "Fail" || jsonString2.BIJson == null)
                    {
                        objPremiumResponse.Status = jsonString2.Status;
                        objPremiumResponse.Message = jsonString2.Message;
                        objPremiumResponse.TransactionId = jsonString2.TransactionId;

                        if (jsonString2.InputValidationStatus != null)
                        {
                            if (jsonString2.InputValidationStatus[0].IpKwMessage.Count > 0)
                            {
                                objPremiumResponse.Message += Environment.NewLine + jsonString2.InputValidationStatus[0].IpKwMessage[0];
                            }
                            else
                            {
                                objPremiumResponse.Message += Environment.NewLine + jsonString2.InputValidationStatus[0].GeneralError;
                            }
                        }

                    }
                    else
                    {
                        var InputValidationStatus = jsonString2.InputValidationStatus;
                        objPremiumResponse.ModalPremium = InputValidationStatus[0].ModalPremium;
                        objPremiumResponse.Tax = InputValidationStatus[0].Tax;
                        objPremiumResponse.ModalPremiumwithTax = (objPremiumResponse.ModalPremium + objPremiumResponse.Tax);
                        objPremiumResponse.AnnualPremium = InputValidationStatus[0].AnnualPremium;

                        objPremiumResponse.Message = jsonString2.Message;
                        objPremiumResponse.Status = jsonString2.Status;
                        objPremiumResponse.TransactionId = jsonString2.TransactionId;



                        objPremiumResponse.TotalPremium = Math.Round((objPremiumResponse.ModalPremium));

                        objPremiumResponse.TotalTAX = Math.Round((objPremiumResponse.Tax));

                        objPremiumResponse.TotalPremiumwithTax = Math.Round((objPremiumResponse.ModalPremiumwithTax));

                        objPremiumResponse.TotalAnnualPremium = Math.Round((objPremiumResponse.AnnualPremium));

                        if (RiderName1 != "")
                        {
                            objPremiumResponse.COVIDPremium = InputValidationStatus[1].ModalPremium;

                            objPremiumResponse.COVIDTax = InputValidationStatus[1].Tax;

                            objPremiumResponse.CovidwithTax = (objPremiumResponse.COVIDPremium + objPremiumResponse.COVIDTax);

                            objPremiumResponse.TotalPremium = (objPremiumResponse.ModalPremium + objPremiumResponse.COVIDPremium);

                            objPremiumResponse.TotalTAX = (objPremiumResponse.Tax + objPremiumResponse.COVIDTax);

                            objPremiumResponse.TotalPremiumwithTax = (objPremiumResponse.ModalPremiumwithTax + objPremiumResponse.CovidwithTax);


                            objPremiumResponse.CovidAnnualPremium = InputValidationStatus[1].AnnualPremium;

                            objPremiumResponse.TotalAnnualPremium = (objPremiumResponse.AnnualPremium + objPremiumResponse.CovidAnnualPremium);

                        }
                    }
                }
                else
                {
                    objPremiumResponse.Status = "Failure";
                    objPremiumResponse.Message = "Error occured while consuming SDE API.Please contact SUD Admin";
                }

                return objPremiumResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
    }
}
