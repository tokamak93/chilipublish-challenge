using System.Text.Json;
using chilipublish_challenge;


List<Domino>? dominoes =
[
    new(1, 2),
    new(1, 2),
    new(2, 3),
    new(2, 3),
    new(3, 3),
    new(3, 3),
    new(1, 1)
];


var result = DominoSolver.Solve(dominoes);

if (result.Ordered)
{
    Console.WriteLine($"Dominos chain solved:");

    foreach (var domino in result.Dominoes)
    {
        Console.WriteLine(domino.ToString());
    }
}
else
{
    Console.WriteLine($"Dominos chain not solved");
}