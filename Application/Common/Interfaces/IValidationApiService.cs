using Application.Common.Services;

namespace Application.Common.Interfaces
{
    public interface IValidationApiService : ISingletonService
    {
        /// <summary>
        /// Returns a tuple containing valid, message, List of Valid Numbers and List of VIP Numbers
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="inputType"></param>
        /// <returns></returns>
        Task<Tuple<bool, string, List<string>, List<string>>> ValidateMultiple(string inputs, string inputType);
        Task<Tuple<bool, string, List<string>, List<string>, List<string>>> ValidateMultiple2(string inputs, string inputType);
    }
}
