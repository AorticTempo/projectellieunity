using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GfG
{

    private class pair
    {
        public int first, second;

        public pair(int first, int second)
        {
            this.first = first;
            this.second = second;
        }
    }
    private static readonly int ROW = 6;
    private static readonly int COL = 15;

    // Direction vectors
    private static int[] dRow = { -1, 0, 1, 0 };
    private static int[] dCol = { 0, 1, 0, -1 };

    // Function to check if a cell
    // is be visited or not
    private static bool isValid(bool[,] vis,
                        int row, int col)
    {

        // If cell lies out of bounds
        if (row < 0 || col < 0 ||
            row >= ROW || col >= COL)
            return false;

        // If cell is already visited
        if (vis[row, col])
            return false;

        // Otherwise
        return true;
    }

    // Function to perform the BFS traversal
    public static Tuple<int, int> BFS(object[,] grid, bool[,] vis, object element,
                    int row = 0, int col = 0)
    {
        // Stores indices of the matrix cells
        Queue<pair> q = new Queue<pair>();
        // Mark the starting cell as visited
        // and push it into the queue
        q.Enqueue(new pair(row, col));
        vis[row, col] = true;

        // Iterate while the queue
        // is not empty
        while (q.Count != 0)
        {
            pair cell = q.Peek();
            int x = cell.first;
            int y = cell.second;
            
            q.Dequeue();

            // Go to the adjacent cells
            for (int i = 0; i < 4; i++)
            {
                int adjx = x + dRow[i];
                int adjy = y + dCol[i];
                if (isValid(vis, adjx, adjy))
                {
                    if (Equals(grid[x,y], element))
                    {
                        Tuple<int, int> tuple = new Tuple<int, int>(x,y);
                        return tuple;
                    }
                    q.Enqueue(new pair(adjx, adjy));
                    vis[adjx, adjy] = true;
                }
            }
        }
        Debug.Log("failure");
        return null;
    }
}

/// https://www.geeksforgeeks.org/breadth-first-search-or-bfs-for-a-graph/
/// This code is contributed by 29AjayKumar