using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Refit;

namespace Infrastructure.Services
{
    public class ValidationApiService : IValidationApiService
    {
        private readonly IConfiguration _configuration;

        public ValidationApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Tuple<bool, string, List<string>, List<string>, List<string>>> ValidateMultiple2(string inputs, string inputType)
        {
            try
            {
                var httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(_configuration.GetSection("ValidationApiSettings")["Url"])
                };
                var token = _configuration.GetSection("ValidationApiSettings")["Korek_Key"];
                httpClient.DefaultRequestHeaders.Add("KOREK_KEY", token);
                var validationApi = RestService.For<IValidationApi>(httpClient);
                var response = await validationApi.ValidateMultiple(new ValidationRequest
                {
                    Inputs = inputs,
                    InputType = inputType,
                });

                return new Tuple<bool, string, List<string>, List<string>, List<string>>(
                    response.Valid,
                    response.Message,
                    response.ValidInputs,
                    response.VipNumbers,
                    response.GoldenNumbers);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Tuple<bool, string, List<string>, List<string>>> ValidateMultiple(string inputs, string inputType)
        {
            try
            {
                var httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(_configuration.GetSection("ValidationApiSettings")["Url"])
                };
                var token = _configuration.GetSection("ValidationApiSettings")["Korek_Key"];
                httpClient.DefaultRequestHeaders.Add("KOREK_KEY", token);
                var validationApi = RestService.For<IValidationApi>(httpClient);
                var response = await validationApi.ValidateMultiple(new ValidationRequest
                {
                    Inputs = inputs,
                    InputType = inputType,
                });

                return new Tuple<bool, string, List<string>, List<string>>(
                    response.Valid,
                    response.Message,
                    response.ValidInputs,
                    response.VipNumbers);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }

    public interface IValidationApi
    {
        [Headers("KOREK_KEY: fcb642a621caad01bd408d20e1067c9e458b5fa7035df03c436f1d56b5cbdec9")]
        [Post("/api/validations/multiple")]
        Task<ValidationResponse> ValidateMultiple([Body(BodySerializationMethod.Json)] ValidationRequest request);
    }

    public class ValidationRequest
    {
        public string Inputs { get; set; }
        public string InputType { get; set; }
    }

    public class ValidationResponse
    {
        public List<string> ValidInputs { get; set; } = new List<string>();
        public string Message { get; set; }
        public bool Valid { get; set; }
        public List<string> VipNumbers { get; set; }
        public List<string> GoldenNumbers { get; set; } = new List<string>();
        public List<string> TestNumbers { get; set; }
    }
}
