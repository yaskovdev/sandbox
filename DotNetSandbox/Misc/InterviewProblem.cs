namespace Misc;

public class InterviewProblem
{
    public int Solve(IEnumerable<int> elements) => elements.Aggregate(0, (current, element) => current ^ element);
}
