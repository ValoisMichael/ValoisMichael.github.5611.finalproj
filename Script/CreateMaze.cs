using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator {

    // Generates a maze using a depth-first search algorithm
    public bool[][] GenerateMaze(int size) {
        // Initialize the maze grid
        bool[][] maze = new bool[size][];
        for (int i = 0; i < size; i++) {
            maze[i] = new bool[size];
            for (int j = 0; j < size; j++) {
                maze[i][j] = false; // Initialize all cells as walls
            }
        }

        // Start generating the maze from the entrance
        GenerateMazeCell(maze, size, 1, size / 2);

        // Mark entrance and exit
        maze[0][size / 2] = true;   // Entrance
        maze[size - 1][size / 2] = true;  // Exit

        return maze;
    }

    // Recursively generates a maze using depth-first search
    private void GenerateMazeCell(bool[][] maze, int size, int x, int y) {
        maze[x][y] = true; // Mark the current cell as open

        // Define the order in which neighboring cells will be visited
        int[] directions = { 0, 1, 2, 3 }; // 0: Up, 1: Right, 2: Down, 3: Left
        ShuffleArray(directions);

        foreach (int direction in directions) {
            int newX = x + DX[direction] * 2; // Move two steps to make a wall between cells
            int newY = y + DY[direction] * 2; // Move two steps to make a wall between cells

            // Check if the new position is valid
            if (newX > 0 && newX < size - 1 && newY > 0 && newY < size - 1 && !maze[newX][newY]) {
                maze[newX - DX[direction]][newY - DY[direction]] = true; // Connect the current cell to the neighbor

                // Recursively visit the neighbor
                GenerateMazeCell(maze, size, newX, newY);
            }
        }
    }

    // Helper function to shuffle the directions array
    private void ShuffleArray(int[] array) {
        int n = array.Length;
        for (int i = n - 1; i > 0; i--) {
            int j = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    // Offsets for each direction (Up, Right, Down, Left)
    private static readonly int[] DX = { 0, 1, 0, -1 };
    private static readonly int[] DY = { -1, 0, 1, 0 };
}
