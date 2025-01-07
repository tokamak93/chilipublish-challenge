namespace chilipublish_challenge;

public class Graph
{
    private readonly List<int>[] _adjacencyList;
    private readonly List<int> _sameNumberDominoes = [];

    public Graph(List<Domino>? dominoes)
    {
        _adjacencyList = new List<int>[6];
        for (var i = 0; i < 6; i++)
            _adjacencyList[i] = [];

        if (dominoes == null)
            return;

        foreach (var domino in dominoes)
        {
            if (domino.LeftSide == domino.RightSide)
                _sameNumberDominoes.Add(domino.LeftSide);
            else
            {
                _adjacencyList[domino.LeftSide].Add(domino.RightSide);
                _adjacencyList[domino.RightSide].Add(domino.LeftSide);
            }
        }
    }

    private bool CheckValidity()
    {
        // check eulerian cycle
        var odd = _adjacencyList.Where(adjacency => adjacency.Count != 0)
            .Count(adjacency => adjacency.Count % 2 != 0);

        if (odd > 0)
            return false;

        // check if all nodes are connected
        var availableNodes = _adjacencyList.Count(adjacency => adjacency.Count != 0);
        var currentNode = _adjacencyList.ToList().FindIndex(x => x.Count > 0);
        return TraverseAndCountNodes(currentNode, new bool[6]) == availableNodes;
    }

    // Get the next valid edges in order to pass each edge once
    private bool ValidNextEdge(int node, int nextNode)
    {
        if (_adjacencyList[node].Count == 1)
            return true;

        var countReachableNodes = TraverseAndCountNodes(node, new bool[6]);
        _adjacencyList[node].Remove(nextNode);
        _adjacencyList[nextNode].Remove(node);
        var countReachableNodesWithoutThisEdge = TraverseAndCountNodes(node, new bool[6]);
        _adjacencyList[node].Add(nextNode);
        _adjacencyList[nextNode].Add(node);

        return countReachableNodes <= countReachableNodesWithoutThisEdge;
    }

    // Traverse graph with DFS and count reachable nodes
    private int TraverseAndCountNodes(int node, bool[] visited)
    {
        visited[node] = true;

        // Recur for all nodes adjacent to this node
        return _adjacencyList[node].Where(i => !visited[i])
            .Aggregate(1, (current, i) => current + TraverseAndCountNodes(i, visited));
    }

    // Fleury's algorithm
    // Bad performance but enables multiple edges
    public List<Domino>? GetPath()
    {
        if (_adjacencyList.All(x => x.Count == 0))
            return null;
        if (!CheckValidity())
            return null;

        var path = new List<Domino>();
        var currentNode = Array.FindIndex(_adjacencyList, x => x.Count > 0);

        while (_adjacencyList.Any(x => x.Count > 0))
        {
            // Handles possible dominoes with the same number because they are not
            // possible to represent with graph
            foreach (var sameNumberDomino in _sameNumberDominoes.Where(x => x == currentNode).ToList())
            {
                path.Add(new Domino(sameNumberDomino, sameNumberDomino));
                _sameNumberDominoes.Remove(sameNumberDomino);
            }

            var nextNode = -1;
            // Copy edges because the resulting list will be modified during iteration
            var edges = new List<int>(_adjacencyList[currentNode]);
            foreach (var possibleNextNode in edges)
            {
                if (!ValidNextEdge(currentNode, possibleNextNode)) continue;
                nextNode = possibleNextNode;
                break;
            }

            path.Add(new Domino(currentNode, nextNode));

            // Remove edge because they are to be used once
            _adjacencyList[currentNode].Remove(nextNode);
            _adjacencyList[nextNode].Remove(currentNode);
            currentNode = nextNode;
        }

        return path;
    }
}