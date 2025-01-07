# chilipublish-challenge

An algorithm to order dominoes in a chain with the starting number that is the same as the last and using all the pieces.
A undirected Graph is constructed with 6 nodes representing the possible domino numbers and the edges represent the actual pieces.
First we check if the graph is an Eulerian Cycle than we find the path with Fleury's algorithm. 
To check the reachable nodes a Depth First Search is used iteratively to avoid taking an edge that is a bridge. That is achieved by counting nodes connected from the current node with or without the edge in trial.
