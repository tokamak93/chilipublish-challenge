using System.Collections;
using System.Collections.Generic;
using chilipublish_challenge;
using Xunit;

namespace TestProject1;

public class UnitTest1
{
    [Theory]
    [ClassData(typeof(ValidTestDataGenerator))]
    public void DominoSolver_should_return_ordered_chain(List<Domino>? dominoes, List<Domino> expectedDominoes)
    {
        var result = DominoSolver.Solve(dominoes);

        Assert.True(result.Ordered);
        Assert.Equal(result.Dominoes, expectedDominoes);
    }

    [Theory]
    [ClassData(typeof(NotValidTestDataGenerator))]
    public void DominoSolver_should_return_error(List<Domino>? dominoes)
    {
        var result = DominoSolver.Solve(dominoes);

        Assert.False(result.Ordered);
    }
}

public class ValidTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data =
    [
        new object[]
        {
            new List<Domino>
            {
                new(2, 1),
                new(2, 3),
                new(1, 3)
            },
            new List<Domino>
            {
                new(1, 2),
                new(2, 3),
                new(3, 1)
            }
        },
        new object[]
        {
            new List<Domino>
            {
                new(1, 2),
                new(2, 3),
                new(2, 3),
                new(2, 3),
                new(2, 3),
                new(2, 1),
                new(4, 1),
                new(4, 1)
            },
            new List<Domino>
            {
                new(1, 2),
                new(2, 3),
                new(3, 2),
                new(2, 3),
                new(3, 2),
                new(2, 1),
                new(1, 4),
                new(4, 1)
            }
        },
        new object[]
        {
            new List<Domino>
            {
                new(1, 2),
                new(1, 2),
                new(2, 3),
                new(2, 3),
                new(3, 3),
                new(3, 3),
                new(1, 1)
            },
            new List<Domino>
            {
                new(1, 1),
                new(1, 2),
                new(2, 3),
                new(3, 3),
                new(3, 3),
                new(3, 2),
                new(2, 1),
            }
        }
    ];

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class NotValidTestDataGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data =
    [
        new object[]
        {
            null!
        },
        new object[]
        {
            new List<Domino>()
        },
        new object[]
        {
            new List<Domino>
            {
                new(1, 2)
            }
        },
        new object[]
        {
            new List<Domino>
            {
                new(1, 2),
                new(4, 1),
                new(2, 3)
            }
        }
    ];

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}