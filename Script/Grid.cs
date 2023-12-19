using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Grid : MonoBehaviour {   
    public Tile tilePrefab; // Grab Tile prefab for spawning
    public Tile[][] tiles; // Initialize Tile matrix
    public Sprite[] sprites; // Grab array of sprites
    public int size; // Size of the maze (size x size)

    // ------------------------------------------------- FUNCTIONS ------------------------------------------------- //

    // Spawn the tiles into the tile matrix
    public void CreateTiles() {
        tiles = new Tile[size][];
        for (int i = 0; i < size; i++) {
            tiles[i] = new Tile[size];
            for (int j = 0; j < size; j++) {
                tiles[i][j] = Instantiate(tilePrefab, new Vector3(i, j), Quaternion.identity);
                tiles[i][j].X = i;
                tiles[i][j].Y = j;
                tiles[i][j].Passable = true;
                tiles[i][j].MovementCost = 1;
                tiles[i][j].gameObject.name = i + ", " + j;
            }
        }
    }

    // Create the walls and textures for the maze
    public void CreateWalls() {
        // Generate a maze
        MazeGenerator mazey = new MazeGenerator();
        bool[][] maze = mazey.GenerateMaze(size);

        // Load the maze onto the tile matrix
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                if(!maze[i][j]) {
                    tiles[i][j].Passable = false;
                    tiles[i][j].LoadSprite(sprites[1]);
                }
            }
        }

        // Remove size/1.2 amount of random walls from the maze
        int n = 0;
        while (n < (size/1.2)) {
            int x = Random.Range(1, size - 1);
            int y = Random.Range(2, size - 2);
            if (tiles[x][y].Passable == true) {
                continue;
            }
            if (!WallRemove(GetWallNeighbors(tiles[x][y]))) {
                continue;
            }
            tiles[x][y].Passable = true;
            tiles[x][y].LoadSprite(sprites[0]);
            n++;
        }

        // Apply correct textures to the entire maze
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                tiles[i][j].LoadSprite(GetWallSprite(GetWallNeighbors(tiles[i][j])));
                if (tiles[i][j].Passable == true) tiles[i][j].LoadSprite(sprites[0]);
            }
        }
    }

    // Method for safe retrieval of tiles
    public Tile SafelyRetrieve(int x, int y) {
        if (x <= tiles.Length-1 && x >= 0) {
            if (y <= tiles.Length-1 && y >= 0) {
                return tiles[x][y];
            }
        }
        return null;
    }

    // Method for find which neighbors are walls, used for textures and wall removal
    public int[] GetWallNeighbors(Tile tile) {
        int[] neighbors = new int[] { 1, 1, 1, 1 };

        Tile top = this.SafelyRetrieve(tile.X, tile.Y + 1);
        if (top != null) {
            if (top.Passable == true) {
                neighbors[0] = 0;
            }
        } else {
            neighbors[0] = 2;
        }
        Tile left = this.SafelyRetrieve(tile.X - 1, tile.Y);
        if (left != null) {
            if (left.Passable == true) {
                neighbors[1] = 0;
            }
        } else {
            neighbors[1] = 2;
        }
        Tile bottom = this.SafelyRetrieve(tile.X, tile.Y - 1);
        if (bottom != null) {
            if (bottom.Passable == true) {
                neighbors[2] = 0;
            }
        } else {
            neighbors[2] = 2;
        }
        Tile right = this.SafelyRetrieve(tile.X + 1, tile.Y);
        if (right != null) {
            if (right.Passable == true) {
                neighbors[3] = 0;
            }
        } else {
            neighbors[3] = 2;
        }
        return neighbors;
    }

    // Method for wall removal post-maze generation
    public bool WallRemove(int[] ints) {
        if (ints.SequenceEqual(new int[] { 1, 0, 1, 0 }) || ints.SequenceEqual(new int[] { 0, 1, 0, 1 })) {
            return true;
        }
        return false;
    }

    // Method for assigning correct textures to wall type
    public Sprite GetWallSprite(int[] ints) {
        if (ints.SequenceEqual(new int[] { 0, 0, 0, 0 })) {
            return sprites[2];
        }
        if (ints.SequenceEqual(new int[] { 1, 0, 0, 0 })) {
            return sprites[3];
        }
        if (ints.SequenceEqual(new int[] { 0, 1, 0, 0 })) {
            return sprites[4];
        }
        if (ints.SequenceEqual(new int[] { 0, 0, 1, 0 })) {
            return sprites[5];
        }
        if (ints.SequenceEqual(new int[] { 0, 0, 0, 1 })) {
            return sprites[6];
        }
        if (ints.SequenceEqual(new int[] { 1, 0, 1, 0 })) {
            return sprites[7];
        }
        if (ints.SequenceEqual(new int[] { 0, 1, 0, 1 })) {
            return sprites[8];
        }
        if (ints.SequenceEqual(new int[] { 1, 1, 0, 0 })) {
            return sprites[9];
        }
        if (ints.SequenceEqual(new int[] { 0, 1, 1, 0 })) {
            return sprites[10];
        }
        if (ints.SequenceEqual(new int[] { 0, 0, 1, 1 })) {
            return sprites[11];
        }
        if (ints.SequenceEqual(new int[] { 1, 0, 0, 1 })) {
            return sprites[12];
        }
        if (ints.SequenceEqual(new int[] { 1, 1, 0, 1 })) {
            return sprites[13];
        }
        if (ints.SequenceEqual(new int[] { 1, 1, 1, 0 })) {
            return sprites[14];
        }
        if (ints.SequenceEqual(new int[] { 0, 1, 1, 1 })) {
            return sprites[15];
        }
        if (ints.SequenceEqual(new int[] { 1, 0, 1, 1 })) {
            return sprites[16];
        }
        if (ints.SequenceEqual(new int[] { 2, 1, 0, 1 })) {
            return sprites[17];
        }
        if (ints.SequenceEqual(new int[] { 1, 2, 1, 0 })) {
            return sprites[18];
        }
        if (ints.SequenceEqual(new int[] { 1, 0, 1, 2 })) {
            return sprites[19];
        }
        if (ints.SequenceEqual(new int[] { 0, 1, 2, 1 })) {
            return sprites[20];
        }
        return sprites[1];
    }

    // ------------------------------------------------------------------------- Weighted A* ------------------------------------------------------------------------- //

    // A* pathfinding algorithm to find a path from 'start' to 'end' on a grid of tiles
    public LinkedList<Tile> FindPath(Tile start, Tile end) {
        // Initialize open and closed sets for pathfinding
        HashSet<Tile> openSet = new HashSet<Tile>();
        HashSet<Tile> closedSet = new HashSet<Tile>();

        // Add the starting tile to the open set
        openSet.Add(start);

        // Continue searching until there are tiles in the open set
        while (openSet.Count > 0) {
            // Get the tile with the lowest F score from the open set
            Tile currentTile = GetLowestFScoreTile(openSet);

            // If the current tile is the destination, reconstruct and return the path
            if (currentTile == end) {
                return ReconstructPath(start, end);
            }

            // Move the current tile from open to closed set
            openSet.Remove(currentTile);
            closedSet.Add(currentTile);

            // Explore neighbors of the current tile
            foreach (Tile neighbor in GetNeighbors(currentTile)) {
                // Skip impassable or already closed tiles
                if (!neighbor.Passable || closedSet.Contains(neighbor)) continue;

                // Calculate tentative G score for the neighbor
                int tentativeGScore = currentTile.G + neighbor.MovementCost;

                // Update neighbor's properties if it's a better path
                if (!openSet.Contains(neighbor) || tentativeGScore < neighbor.G) {
                    neighbor.Parent = currentTile;
                    neighbor.G = tentativeGScore;
                    neighbor.H = neighbor.ManhattanDistance(end);

                    // Add the neighbor to the open set if not already present
                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        // No path found
        return null;
    }

    // Helper function to get the tile with the lowest F score from a set of tiles
    private Tile GetLowestFScoreTile(HashSet<Tile> tileSet) {
        Tile lowestFScoreTile = null;
        foreach (Tile tile in tileSet) {
            if (lowestFScoreTile == null || tile.F < lowestFScoreTile.F) {
                lowestFScoreTile = tile;
            }
        }
        return lowestFScoreTile;
    }

    // Helper function to get neighboring tiles of a given tile
    private List<Tile> GetNeighbors(Tile tile) {
        List<Tile> neighbors = new List<Tile>();

        // Assuming 4-way movement (no diagonals)
        if (tile.X > 0) neighbors.Add(tiles[tile.X - 1][tile.Y]);
        if (tile.X < size - 1) neighbors.Add(tiles[tile.X + 1][tile.Y]);
        if (tile.Y > 0) neighbors.Add(tiles[tile.X][tile.Y - 1]);
        if (tile.Y < size - 1) neighbors.Add(tiles[tile.X][tile.Y + 1]);

        return neighbors;
    }

    // Helper function to reconstruct the path from end to start
    private LinkedList<Tile> ReconstructPath(Tile start, Tile end) {
        LinkedList<Tile> path = new LinkedList<Tile>();
        Tile currentTile = end;

        // Backtrack from end to start to construct the path
        while (currentTile != start) {
            path.AddFirst(currentTile);
            currentTile = currentTile.Parent;
        }

        return path;
    }
}

