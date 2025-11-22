using LanguageExt;

namespace LanguageExtSandbox;

public static class HaskellMonadExercise
{
    public static void Run()
    {
        var effect = Prelude.liftEff(() =>
        {
            Console.WriteLine("Effect");
            return 0;
        });
        // If you convert the LINQ below to a method chain, you'll see SelectMany (aks Bind):
        var eff = from param in effect
            from res in ParametrizedEffect(param)
            select res;

        Console.WriteLine("Going to run!");
        eff.Run();
    }

    // See https://github.com/louthy/language-ext/wiki/How-to-deal-with-side-effects#ioa.
    private static Eff<int> ParametrizedEffect(int parameter) =>
        Prelude.liftEff(() =>
        {
            Console.WriteLine("ParametrizedEffect with parameter: " + parameter);
            return 0;
        });
}