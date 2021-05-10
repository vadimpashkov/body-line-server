using Domain.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;
using Schedule.Presentation.Output;

namespace Schedule.Presentation.Presenters
{
    public class ObjectPresenter: IPresenter<ObjectOutput>
    {
        public IActionResult Present(ObjectOutput output)
        {
            return JsonActionResult.Ok(output.Object);
        }
    }
}