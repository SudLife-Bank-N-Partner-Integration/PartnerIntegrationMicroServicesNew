using Newtonsoft.Json.Linq;
using System.Globalization;

namespace SUDLife_AssuredIncome.ServiceLayer
{
    public class ClsCommonOperations
    {
        private readonly IConfiguration? _configuration;
        static JObject jsonObject; // Declare JObject to store the JSON data
        public ClsCommonOperations(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public string DateFormating(string DateToBeFormatted)
        {
            try
            {
                string FormattedDate = string.Empty;
                if (string.IsNullOrEmpty(DateToBeFormatted))
                    return null;
                FormattedDate = DateTime.ParseExact(DateToBeFormatted, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                        .ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                return FormattedDate;
            }
            catch (Exception ex)
            {
                // WriteExceptionToLog(ex.Message, "DateFormatter");
                throw;
            }
        }

        
        public string CalculateAge(DateTime dateOfBirth)
        {
            try
            {
                int age = 0;
                age = DateTime.Now.Year - dateOfBirth.Year;
                if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                    age = age - 1;

                return age.ToString();
            }
            catch (Exception ex)
            {
                
                return null;
            }

        }
    
        public string APIKey()
        {
            try
            {
                string Key = string.Empty;
                dynamic _responseJson = string.Empty;

                Key = _configuration.GetSection("URLS:APIKey").Value;
                return Key;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public string ConvertToYears(string PolicyTerm)
        {
            try
            {
                int Loantenureinyears = Convert.ToInt32(PolicyTerm) / 12;
                return PolicyTerm = Convert.ToString(Loantenureinyears);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void ReadDynamicCollection()
        {
            string JsonFilePath = string.Empty;
            // Read JSON file as a string
            JsonFilePath = _configuration.GetSection("URLS:DynamicCollectionPath").Value;
            string jsonText = File.ReadAllText(JsonFilePath);

            // Parse JSON string to JObject
            jsonObject = JObject.Parse(jsonText);
        }

        public string GetDynamicValue(string section, string key)
        {
            string value = "";
            // Navigate to the specific section
            JToken section1 = jsonObject[section];

            if (section1 != null)
            {
                // Get the value of the key within the section                
                value = section1[key]?.ToString();
                if (string.IsNullOrEmpty(value))
                {
                    value = "";
                }
            }
            else
            {
                value = "";
            }
            return value;

        }
    }
}
