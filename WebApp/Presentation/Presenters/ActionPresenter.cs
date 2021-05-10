using Domain.Abstractions.Outputs;
using Microsoft.AspNetCore.Mvc;
using Schedule.Presentation.Output;

namespace Schedule.Presentation.Presenters
{
    public class ActionPresenter: IPresenter<ActionOutput>
    {
        public IActionResult Present(ActionOutput output)
        {
            return JsonActionResult.Ok(output);
        }
    }
}