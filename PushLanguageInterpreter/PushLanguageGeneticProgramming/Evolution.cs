using LanguageExt;

namespace PushLanguageGeneticProgramming;

public class Evolution
{
    private readonly int _maxGenerations;

    public Evolution(int maxGenerations)
    {
        _maxGenerations = maxGenerations;
    }

    public void Run()
    {
        for (var i = 0; i < _maxGenerations; i++)
        {
            Console.WriteLine($"Generation {i}");
            var population = CreatePopulation();
        }
    }

    private static IReadOnlyCollection<Individual> CreatePopulation() => List.empty<Individual>();
}
