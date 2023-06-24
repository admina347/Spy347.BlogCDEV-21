using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Spy347.BlogCDEV_21.Web
{
    public class ApiExplorerIgnores : IActionModelConvention
{
    public void Apply(ActionModel action)
    {
        if (action.Controller.ControllerName.Equals("Pwa"))
            action.ApiExplorer.IsVisible = false;
    }
}
}