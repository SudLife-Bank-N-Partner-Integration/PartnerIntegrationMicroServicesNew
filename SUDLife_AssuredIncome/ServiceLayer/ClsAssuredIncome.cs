using Newtonsoft.Json;
using RestSharp;
using SUDLife_AssuredIncome.Model.Request;
using SUDLife_AssuredIncome.Model.Response;
using SUDLife_CallThirdPartyAPI;

namespace SUDLife_AssuredIncome.ServiceLayer
{
    public class ClsAssuredIncome
    {
        private readonly ClsCommonOperations _CommonOperations;
        private readonly ThirdPartyAPI _thirdPartyAPI;
        public readonly ClsAssuredIncomePlainResponse _AssuredIncomeResponse;
        private readonly ILogger<ClsAssuredIncome> _logger;

        public ClsAssuredIncome(ClsCommonOperations CommonOperations, ThirdPartyAPI thirdPartyAPI, ClsAssuredIncomePlainResponse PremiumResponse, ILogger<ClsAssuredIncome> logger)
        {
            _CommonOperations = CommonOperations;
            _AssuredIncomeResponse = PremiumResponse;
            this._thirdPartyAPI = thirdPartyAPI;
            this._logger = logger;
        }
        public async Task<dynamic> AssuredIncomeDetails(ClsAssuredIncomePlainRequest ObjPremiumRequest)
        {
            try
            {
                _CommonOperations.ReadDynamicCollection();
                ClsAssuredIncomePlainResponse objPremiumResponse = new ClsAssuredIncomePlainResponse();


                string AnnualPayout = ObjPremiumRequest.AnnualPayout;


                if (Convert.ToInt32(AnnualPayout) < 24000)
                {
                    objPremiumResponse.Status = "Failure";
                    objPremiumResponse.Message = "Minimum value of Annual Payout must be 24000.00. Kindly revise the value";

                    return objPremiumResponse;

                }

                if (Convert.ToInt32(AnnualPayout) > 5000000)
                {

                    objPremiumResponse.Status = "Failure";
                    objPremiumResponse.Message = "Maximum value of Annual Payout must be 5000000.00. Kindly revise the value";

                    return objPremiumResponse;

                }
               

                List<ClsSDEBaseKeyValuePair> lstkeyvalue = new List<ClsSDEBaseKeyValuePair>
                {
                    new ClsSDEBaseKeyValuePair { key = "@LI_FNAME", value = ObjPremiumRequest.ApplicantDetails.ApplicantFName },
                    new ClsSDEBaseKeyValuePair { key = "@LI_MNAME", value = string.Empty  },
                    new ClsSDEBaseKeyValuePair { key = "@LI_LNAME", value =ObjPremiumRequest.ApplicantDetails.ApplicantLName },

                    new ClsSDEBaseKeyValuePair { key = "@LI_ENTRY_AGE", value = Convert.ToString(Convert.ToInt32(_CommonOperations.CalculateAge(Convert.ToDateTime(_CommonOperations.DateFormating(DateToBeFormatted: ObjPremiumRequest.ApplicantDetails.ApplicantDateOfBirth))))) },
                    new ClsSDEBaseKeyValuePair { key = "@LI_DOB", value = _CommonOperations.DateFormating(DateToBeFormatted: ObjPremiumRequest.ApplicantDetails.ApplicantDateOfBirth)},
                    new ClsSDEBaseKeyValuePair { key = "@LI_GENDER", value =_CommonOperations.Gender(ObjPremiumRequest.ApplicantDetails.ApplicantGender.ToLower()) },


                    new ClsSDEBaseKeyValuePair { key = "@LI_STATE", value = "19" },
                    new ClsSDEBaseKeyValuePair { key = "@LI_CITY", value = "65" },
                    new ClsSDEBaseKeyValuePair { key = "@PROPOSER_FNAME", value =ObjPremiumRequest.ProposerDetails.ProposerFName },


                    new ClsSDEBaseKeyValuePair { key = "@PROPOSER_MNAME", value = string.Empty },
                    new ClsSDEBaseKeyValuePair { key = "@PROPOSER_LNAME", value = ObjPremiumRequest.ProposerDetails.ProposerLName },
                    new ClsSDEBaseKeyValuePair { key = "@PROPOSER_AGE", value = Convert.ToString(Convert.ToInt32(_CommonOperations.CalculateAge(Convert.ToDateTime(_CommonOperations.DateFormating(DateToBeFormatted: ObjPremiumRequest.ProposerDetails.ProposerDateOfBirth))))) },


                    new ClsSDEBaseKeyValuePair { key = "@PROPOSER_DOB", value = _CommonOperations.DateFormating(DateToBeFormatted: ObjPremiumRequest.ProposerDetails.ProposerDateOfBirth) },
                    new ClsSDEBaseKeyValuePair { key = "@PROPOSER_GENDER", value = _CommonOperations.Gender(ObjPremiumRequest.ProposerDetails.ProposerGender.ToLower()) },
                    new ClsSDEBaseKeyValuePair { key = "@AGE_PROOF", value ="-1" },


                    new ClsSDEBaseKeyValuePair { key = "@SameProposer", value = ObjPremiumRequest.ProposerDetails.IsProposersameasApplicant },
                    new ClsSDEBaseKeyValuePair { key = "@INPUT_MODE", value = _CommonOperations.GetDynamicValue("InputMode",ObjPremiumRequest.PremiumPaymentModes.ToLower()) },
                    new ClsSDEBaseKeyValuePair { key = "@PR_ID", value = "1001"},




                    new ClsSDEBaseKeyValuePair { key = "@PR_PT", value = "0" },//PolicyTerm
                    new ClsSDEBaseKeyValuePair { key = "@PR_PPT", value = _CommonOperations.ConvertToYears(Convert.ToString(ObjPremiumRequest.PremiumPaymentTerm)) },
                    new ClsSDEBaseKeyValuePair { key = "@PR_ANNPREM", value = ""},


                    new ClsSDEBaseKeyValuePair { key = "@PR_MI", value = "0"},
                    new ClsSDEBaseKeyValuePair { key = "@PR_SA", value = ""},
                    new ClsSDEBaseKeyValuePair { key = "@PR_SAMF", value = "0"},




                    new ClsSDEBaseKeyValuePair { key = "@PR_ModalPrem", value = string.Empty},


                    new ClsSDEBaseKeyValuePair { key = "@NSAP_FLAG", value = _CommonOperations.StandardAgeProof(ObjPremiumRequest.StandardAgeProof.ToLower()) },
                    new ClsSDEBaseKeyValuePair { key = "@PR_CHANNEL", value = _CommonOperations.GetDynamicValue("DistributionChannelAssuredIncome",ObjPremiumRequest.DistributionChannel.ToLower())},
                    new ClsSDEBaseKeyValuePair { key = "@kfc", value = "0"},

                    new ClsSDEBaseKeyValuePair { key = "@TargetSTOFund", value = "0"},



                    new ClsSDEBaseKeyValuePair { key = "@PR_OPTION_1", value = _CommonOperations.GetDynamicValue("DefermentPeriod",ObjPremiumRequest.DefermentPeriod)},
                    new ClsSDEBaseKeyValuePair { key = "@OPTION_VALUE_1", value = ""},



                    new ClsSDEBaseKeyValuePair { key = "@PR_OPTION_2", value = _CommonOperations.GetDynamicValue("PayoutOptionAssuredIncome",ObjPremiumRequest.Payoutperiod)},
                    new ClsSDEBaseKeyValuePair { key = "@OPTION_VALUE_2", value = ""},

                    new ClsSDEBaseKeyValuePair { key = "@PR_OPTION_3", value = "10"},
                    new ClsSDEBaseKeyValuePair { key = "@OPTION_VALUE_3", value = AnnualPayout},

                    new ClsSDEBaseKeyValuePair { key = "@PR_OPTION_4", value = _CommonOperations.GetDynamicValue("StaffDiscountAssuredIncome",ObjPremiumRequest.StaffDiscount.ToLower())},
                    new ClsSDEBaseKeyValuePair { key = "@OPTION_VALUE_4", value = ""},

            };


               

                string RiderName1 = Convert.ToString(ObjPremiumRequest.COVIDRiderSA);

                if (ObjPremiumRequest.COVIDRiderOpted.ToLower() == "no" || ObjPremiumRequest.COVIDRiderOpted == "")
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
                    formInputs = new List<ClsSDEBaseKeyValuePair>(lstkeyvalue),
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

                        objPremiumResponse.SumAssured = Convert.ToInt32((InputValidationStatus[0].SA));

                        objPremiumResponse.ModalPremium = Math.Round(InputValidationStatus[0].ModalPremium);

                        objPremiumResponse.Tax = Math.Round(InputValidationStatus[0].Tax);

                        objPremiumResponse.ModalPremiumwithTax = Math.Round((objPremiumResponse.ModalPremium + objPremiumResponse.Tax));

                        objPremiumResponse.AnnualPremium = Math.Round(InputValidationStatus[0].AnnualPremium);

                        objPremiumResponse.Message = jsonString2.Message;

                        objPremiumResponse.Status = jsonString2.Status;

                        objPremiumResponse.TransactionId = jsonString2.TransactionId;

                        if (ObjPremiumRequest.ADTPDRiderOpted == string.Empty || ObjPremiumRequest.ADTPDRiderOpted == "")
                        {
                            ObjPremiumRequest.ADTPDRiderOpted = "no";
                        }

                        if (ObjPremiumRequest.COVIDRiderOpted == string.Empty || ObjPremiumRequest.COVIDRiderOpted == "")
                        {
                            ObjPremiumRequest.COVIDRiderOpted = "no";
                        }

                        if (ObjPremiumRequest.ADTPDRiderOpted.ToLower() == "yes" && ObjPremiumRequest.COVIDRiderOpted.ToLower() == "no")
                        {

                            int PRID = InputValidationStatus[0].ProductId;
                            int RiderID1 = InputValidationStatus[1].ProductId;


                            if (RiderID1 == 2001)
                            {
                                objPremiumResponse.ADTPDPremium = Math.Round(InputValidationStatus[1].ModalPremium);

                                objPremiumResponse.ADTPDTax = Math.Round(InputValidationStatus[1].Tax);

                                objPremiumResponse.ADTPDwithTax = Math.Round((objPremiumResponse.ADTPDPremium + objPremiumResponse.ADTPDTax));

                                objPremiumResponse.ADTPDAnnualPremium = Math.Round(InputValidationStatus[1].AnnualPremium);
                            }

                        }

                        if (ObjPremiumRequest.ADTPDRiderOpted.ToLower() == "yes" && ObjPremiumRequest.COVIDRiderOpted.ToLower() == "yes")
                        {

                            int PRID = InputValidationStatus[0].ProductId;
                            int RiderID1 = InputValidationStatus[1].ProductId;
                            int RiderID2 = InputValidationStatus[2].ProductId;



                            if (RiderID1 == 2001)
                            {
                                objPremiumResponse.ADTPDPremium = Math.Round(InputValidationStatus[1].ModalPremium);

                                objPremiumResponse.ADTPDTax = Math.Round(InputValidationStatus[1].Tax);

                                objPremiumResponse.ADTPDwithTax = Math.Round((objPremiumResponse.ADTPDPremium + objPremiumResponse.ADTPDTax));

                                objPremiumResponse.ADTPDAnnualPremium = Math.Round(InputValidationStatus[1].AnnualPremium);
                            }



                            if (RiderID2 == 2003)
                            {
                                objPremiumResponse.COVIDPremium = Math.Round(InputValidationStatus[2].ModalPremium);

                                objPremiumResponse.COVIDTax = Math.Round(InputValidationStatus[2].Tax);

                                objPremiumResponse.CovidwithTax = Math.Round((objPremiumResponse.COVIDPremium + objPremiumResponse.COVIDTax));

                                objPremiumResponse.COVIDAnnualPremium = Math.Round(InputValidationStatus[2].AnnualPremium);
                            }
                        }


                        if (ObjPremiumRequest.ADTPDRiderOpted.ToLower() == "no" && ObjPremiumRequest.COVIDRiderOpted.ToLower() == "yes")
                        {

                            int PRID = InputValidationStatus[0].ProductId;
                            int RiderID2 = InputValidationStatus[1].ProductId;


                            if (RiderID2 == 2003)
                            {
                                objPremiumResponse.COVIDPremium = Math.Round(InputValidationStatus[1].ModalPremium);

                                objPremiumResponse.COVIDTax = Math.Round(InputValidationStatus[1].Tax);

                                objPremiumResponse.CovidwithTax = Math.Round((objPremiumResponse.COVIDPremium + objPremiumResponse.COVIDTax));

                                objPremiumResponse.COVIDAnnualPremium = Math.Round(InputValidationStatus[1].AnnualPremium);

                            }
                        }

                        objPremiumResponse.TotalPremium = Math.Round((objPremiumResponse.ModalPremium + objPremiumResponse.ADTPDPremium + objPremiumResponse.COVIDPremium));

                        objPremiumResponse.TotalTAX = Math.Round((objPremiumResponse.Tax + objPremiumResponse.ADTPDTax + objPremiumResponse.COVIDTax));

                        objPremiumResponse.TotalPremiumwithTax = Math.Round((objPremiumResponse.ModalPremiumwithTax + objPremiumResponse.ADTPDwithTax + objPremiumResponse.CovidwithTax));

                        objPremiumResponse.TotalAnnualPremium = Math.Round((objPremiumResponse.AnnualPremium + objPremiumResponse.ADTPDAnnualPremium + objPremiumResponse.COVIDAnnualPremium));

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
