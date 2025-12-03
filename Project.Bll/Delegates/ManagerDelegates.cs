using Project.Bll.Results;

namespace Project.Bll.Delegates
{
    public delegate string ErrorHandlerDelegate(Exception ex, string operationName);

    public delegate Result PreOperationCheckDelegate<T>(T entity);

    public delegate void LogDelegate(string message, string logLevel = "Info");
}