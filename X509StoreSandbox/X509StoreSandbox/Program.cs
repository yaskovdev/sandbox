using System.Security.Cryptography.X509Certificates;

namespace X509StoreSandbox;

public static class Program
{
    public static void Main(string[] args)
    {
        var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
        try
        {
            store.Open(OpenFlags.ReadOnly);
            var name = "";
            var fullName = "";
            // .Find(X509FindType.FindByTimeValid, DateTime.UtcNow.AddDays(30), false)
            var certificates = store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, fullName, false);
            Console.WriteLine(string.Join(", ", certificates));
        }
        finally
        {
            store.Close();
        }
    }
}