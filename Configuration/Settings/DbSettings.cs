using System.Reflection;

namespace ApiDevBP.Configuration
{
    public class DbSettings
    {
        public string DbName { get; init; }
        public string DbPath => Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), DbName);
    }
}