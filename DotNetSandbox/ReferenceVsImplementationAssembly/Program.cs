using System.Configuration;

namespace ReferenceVsImplementationAssembly
{
    static class Program
    {
        private static void Main() => throw new ConfigurationErrorsException("Cannot parse configuration setting");
    }
}
