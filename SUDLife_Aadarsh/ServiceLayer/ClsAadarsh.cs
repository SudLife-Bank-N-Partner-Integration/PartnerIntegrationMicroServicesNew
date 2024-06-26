using Newtonsoft.Json;
using RestSharp;
using SUDLife_Aadarsh.Models.Request;
using SUDLife_Aadarsh.Models.Response;
using SUDLife_CallThirdPartyAPI;

namespace SUDLife_Aadarsh.ServiceLayer
{
    public class ClsAadarsh
    {
        private IConfiguration config { get; set; }
        private readonly ThirdPartyAPI _thirdPartyAPI;
        private readonly ClsCommonOperations _CommonOperations;
        ClsSDEBasePremiumRequest _root;
        
        public ClsAadarsh(ClsCommonOperations clsCommonOperations, ClsSDEBasePremiumRequest root, ThirdPartyAPI thirdPartyAPI)
        {
            _CommonOperations = clsCommonOperations;
            this._thirdPartyAPI = thirdPartyAPI;
            _root = root;
        }

        public async Task<ClsAadarshPlainResponse> AadarshDetails(ClsAadarshPlainRequest request)
        {
            ClsAadarshPlainResponse clsAadarshPlainResponse =new ClsAadarshPlainResponse();
            _CommonOperations.ReadDynamicCollection();
            List<ClsSDEBaseKeyValuePair> lstkeyvalue = new List<ClsSDEBaseKeyValuePair>
            {
                new ClsSDEBaseKeyValuePair { key = "@LI_FNAME", value = request.ApplicantDetails.ApplicantFName },
                new ClsSDEBaseKeyValuePair { key = "@LI_MNAME", value = string.Empty  },
                new ClsSDEBaseKeyValuePair { key = "@LI_LNAME", value =request.ApplicantDetails.ApplicantLName },

                new ClsSDEBaseKeyValuePair { key = "@LI_ENTRY_AGE", value =  Convert.ToString( Convert.ToInt32(_CommonOperations.CalculateAge(Convert.ToDateTime(_CommonOperations.DateFormating(DateToBeFormatted: request.ApplicantDetails.ApplicantDateOfBirth)))))  },
                new ClsSDEBaseKeyValuePair { key = "@LI_DOB", value = _CommonOperations.DateFormating(DateToBeFormatted: request.ApplicantDetails.ApplicantDateOfBirth)},
                new ClsSDEBaseKeyValuePair { key = "@LI_GENDER", value =_CommonOperations.GetDynamicValue("Gender",(request.ApplicantDetails.ApplicantGender.ToLower())) },


                new ClsSDEBaseKeyValuePair { key = "@LI_STATE", value = "9" },
                new ClsSDEBaseKeyValuePair { key = "@LI_CITY", value = "142" },
                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_FNAME", value =request.ProposerDetails.ProposerFName },


                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_MNAME", value = string.Empty },
                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_LNAME", value = request.ProposerDetails.ProposerLName },
                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_AGE", value = Convert.ToString(Convert.ToInt32(_CommonOperations.CalculateAge(Convert.ToDateTime(_CommonOperations.DateFormating(DateToBeFormatted: request.ProposerDetails.ProposerDateOfBirth))))) },


                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_DOB", value = _CommonOperations.DateFormating(DateToBeFormatted: request.ProposerDetails.ProposerDateOfBirth) },
                new ClsSDEBaseKeyValuePair { key = "@PROPOSER_GENDER", value = _CommonOperations.GetDynamicValue("Gender",(request.ProposerDetails.ProposerGender.ToLower())) },
                new ClsSDEBaseKeyValuePair { key = "@AGE_PROOF", value ="-1" },


                new ClsSDEBaseKeyValuePair { key = "@SameProposer", value = request.ProposerDetails.IsProposersameasApplicant},
                new ClsSDEBaseKeyValuePair { key = "@INPUT_MODE", value = _CommonOperations.GetDynamicValue("InputMode",request.PremiumPaymentModes.ToLower())},
                new ClsSDEBaseKeyValuePair { key = "@PR_ID", value = "1010"},

                new ClsSDEBaseKeyValuePair { key = "@PR_PT", value = "10" },
                new ClsSDEBaseKeyValuePair { key = "@PR_PPT", value = "5" },
                new ClsSDEBaseKeyValuePair { key = "@PR_ANNPREM", value = string.Empty},


                new ClsSDEBaseKeyValuePair { key = "@PR_MI", value = "0"},
                new ClsSDEBaseKeyValuePair { key = "@PR_SA", value = _CommonOperations.GetDynamicValue("SumAssured",Convert.ToString(request.SumAssured)) },

                new ClsSDEBaseKeyValuePair { key = "@PR_SAMF", value = "0"},


                new ClsSDEBaseKeyValuePair { key = "@PR_ModalPrem", value = string.Empty},
                new ClsSDEBaseKeyValuePair { key = "@NSAP_FLAG", value =  _CommonOperations.GetDynamicValue("StandardAgeProof",(request.StandardAgeProof.ToLower())) },

                new ClsSDEBaseKeyValuePair { key = "@kfc", value = "0"},

                new ClsSDEBaseKeyValuePair { key = "@TargetSTOFund", value = "0"},

                new ClsSDEBaseKeyValuePair { key = "@PR_OPTION_1", value = _CommonOperations.GetDynamicValue("DistributionChannelAdarsh",request.DistributionChannel.ToLower())},
                new ClsSDEBaseKeyValuePair { key = "@OPTION_VALUE_1", value = ""},

                new ClsSDEBaseKeyValuePair { key = "@PR_OPTION_2", value = _CommonOperations.GetDynamicValue("StaffPolicyPOSAdarsh",request.StaffPolicy.ToLower())},
                new ClsSDEBaseKeyValuePair { key = "@OPTION_VALUE_2", value = ""},

        };




            string APIKey = _CommonOperations.APIKey();

            //SDEBaseRequest root = new SDEBaseRequest
            _root = new ClsSDEBasePremiumRequest
            {
                APIKey = APIKey,
                formInputs = new List<ClsSDEBaseKeyValuePair>(lstkeyvalue),
                inputPartialWithdrawal = new List<InputPW>(),
                inputOptions = new List<InputOptions>(),
                funds = new List<FundInput>(),
                riders = new List<Rider>()
            };
            ClsAadarshPlainResponse _AadarshResponse = new ClsAadarshPlainResponse();

            string url = "https://siapi.sudlife.in/nsureservices.svc/generatebiapi"; // Need to provide condition here.

            string jsonBody = System.Text.Json.JsonSerializer.Serialize(_root);
            var APIRequest = jsonBody.Replace("null", "[]");

            dynamic ResponseFromNsureservice = _thirdPartyAPI.ClientAPI(url, Method.Post, jsonBody);

            if (!string.IsNullOrEmpty(ResponseFromNsureservice))
            {
                var jsonString2 = JsonConvert.DeserializeObject<ClsSDEBasePremiumResponse>(ResponseFromNsureservice);
                if (jsonString2.Status == "Fail" || jsonString2.BIJson == null)
                {
                    _AadarshResponse.Status = jsonString2.Status;
                    _AadarshResponse.Message = jsonString2.Message;
                    _AadarshResponse.TransactionId = jsonString2.TransactionId;

                    if (jsonString2.InputValidationStatus != null)
                    {
                        if (jsonString2.InputValidationStatus[0].IpKwMessage.Count > 0)
                        {
                            _AadarshResponse.Message += Environment.NewLine + jsonString2.InputValidationStatus[0].IpKwMessage[0];
                        }
                        else
                        {
                            _AadarshResponse.Message += Environment.NewLine + jsonString2.InputValidationStatus[0].GeneralError;
                        }
                    }
                }
                else
                {
                    var InputValidationStatus = jsonString2.InputValidationStatus;
                    _AadarshResponse.ModalPremium = Math.Round(InputValidationStatus[0].ModalPremium);
                    _AadarshResponse.Tax = Math.Round(InputValidationStatus[0].Tax);
                    _AadarshResponse.ModalPremiumwithTax = Math.Round((_AadarshResponse.ModalPremium + _AadarshResponse.Tax));

                    _AadarshResponse.Message = jsonString2.Message;
                    _AadarshResponse.Status = jsonString2.Status;
                    _AadarshResponse.TransactionId = jsonString2.TransactionId;
                }
            }
            else
            {
                _AadarshResponse.Status = "Failure - Service Calling";
                _AadarshResponse.Message = "Error occured while consuming SDE API.Please contact SUD Admin";
            }

            return clsAadarshPlainResponse;

        }
    }
}
