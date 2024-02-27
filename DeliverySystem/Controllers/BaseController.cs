using BLL.Singleton;
using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

public abstract class BaseController : Controller
{
    protected ILoggerService _loggerService;

    public BaseController(IServiceProvider serviceProvider)
    {
        _loggerService = serviceProvider.GetRequiredService<ILoggerService>();
    }

    protected async Task<T> TryExecuteAsync<T>(Func<Task<T>> onOk, Func<Task<T>> onError) where T : class
    {
        T result = null;
        try
        {
            result = await onOk();
        }
        catch (DeliverySystemException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            result = await onError();
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "An error occurred, try again";
            _loggerService.LogError(ex);
            result = await onError();
        }
        return result;
    }
}
