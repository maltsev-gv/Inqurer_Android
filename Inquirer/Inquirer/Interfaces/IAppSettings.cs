namespace Inquirer_Android.Interfaces
{
    public interface IAppSettings
    {
        string AppVersion { get; }
        string AppBuild { get; }
        string AppName { get; }
        string AppBundle { get; }
        string AppPhone { get; }
    }
}