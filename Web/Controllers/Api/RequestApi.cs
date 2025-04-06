using Microsoft.AspNetCore.Mvc;
using Web.Common;

namespace Web.Controllers.Api
{
    public class RequestApi : BaseController
    {
        public async Task<IActionResult> GetHistory(int start, int length, string number, string from_date, string to_date)
        {
            //var command = new FindArchiveCourtLetterHistoryQuery(start, length, number, lang, team);
            //var result = await Mediator.Send(command);
            return Ok("asdas");
        }
    }
}
