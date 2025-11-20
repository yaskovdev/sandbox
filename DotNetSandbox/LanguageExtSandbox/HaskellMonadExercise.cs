using LanguageExt;

namespace LanguageExtSandbox;

public class HaskellMonadExercise
{
    // TODO: System.Linq.Async
    public static void Run()
    {
        // >>= :: a -> IO b
        // Select :: a -> b
        var effect = Prelude.Eff(() =>
        {
            Console.WriteLine("Effect");
            return 0;
        });
        var effect2 = Prelude.Eff(() =>
        {
            Console.WriteLine("Effect2");
            return 0;
        });
        effect.Bind(it => effect2).Run(); // TODO: pass it to effect2
        Console.WriteLine("Done");
        effect
            .Bind(it => ParametrizedEffect(it))
            .Run();

        effect.Select(it => ParametrizedEffect(it));
        // If you convert the LINQ below to a method chain, you'll see SelectMany (aks Bind):
        Eff<int> eff = from it in effect
            from it2 in ParametrizedEffect(it)
            select it2;
        
        Console.WriteLine("Going to run!");
        eff.Run();
    }

    // See https://github.com/louthy/language-ext/wiki/How-to-deal-with-side-effects#ioa.
    private static Eff<int> ParametrizedEffect(int parameter) =>
        Prelude.Eff(() =>
        {
            Console.WriteLine("ParametrizedEffect with parameter: " + parameter);
            return 0;
        });
}