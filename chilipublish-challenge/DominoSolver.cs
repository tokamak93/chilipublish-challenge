namespace chilipublish_challenge;

public static class DominoSolver
{
    public static (bool Ordered, List<Domino> Dominoes) Solve(List<Domino>? dominoes)
    {
        var graph = new Graph(dominoes);
        var path = graph.GetPath();
        return path == null
            ? (false, [])
            : (true, path);
    }
}