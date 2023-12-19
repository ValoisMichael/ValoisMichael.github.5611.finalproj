using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : MonoBehaviour {
    // Reference to the Grid, Player, and Runner object
    public Grid grid;
    public Player player;
    public Runner runner;

    public int moveCost;  // Movement cost for the Boots
    private bool spawned = false; // Spawn flag
    Tile SpawnTile; // Spawn location

    // ------------------------------------------------- FUNCTIONS ------------------------------------------------- //
    
    void Update () {
        // Check for collisions only if the Boots have been spawned
        if (spawned) {
            CollisionPlayer ();
            CollisionRunner ();
        }
    }

    // Method to spawn the Boots
    public void Spawn () {
        // Find a valid spot for spawning
        ValidSpot();

        // Set the position of the Boots to the chosen spot and flag spawned
        transform.position = new Vector3(SpawnTile.X, SpawnTile.Y);
        spawned = true;
    }

    // Method to find a valid spot for spawning
    private void ValidSpot () {
        int GridOffset = (grid.size - 1);
        bool search = true;

        // Continue searching until a valid spot is found
        while(search) {
            // Choose a random tile within the grid bounds
            SpawnTile = grid.tiles[Random.Range(0 + GridOffset, grid.size - GridOffset)][Random.Range(0, grid.size - 1)];

            // Check if the chosen tile is passable
            if (SpawnTile.Passable) {
                search = false;
            }
        }
        SpawnTile.MovementCost = moveCost;
    }

    // Method to handle collisions with the Player
    private void CollisionPlayer() {
        // Define a collision area around the Boots
        float collisionAreaX = 0.5f;
        float collisionAreaY = 0.5f;

        // Check if the position of the Player is within the collision area of the Boots
        float distanceX = Mathf.Abs(transform.position.x - player.transform.position.x);
        float distanceY = Mathf.Abs(transform.position.y - player.transform.position.y);

        // If a collision is detected
        if (distanceX <= collisionAreaX / 2 && distanceY <= collisionAreaY / 2) {
            transform.position = new Vector3(62, -6); // Remove Boots from play
            player.speed += 2; // Increase the speed of the player
            SpawnTile.MovementCost = 1; // Set the movement cost for the tile
        }
    }

    // Method to handle collisions with the Runner (same as above)
    private void CollisionRunner() {
        float collisionAreaX = 0.5f;
        float collisionAreaY = 0.5f;

        float distanceX = Mathf.Abs(transform.position.x - runner.transform.position.x);
        float distanceY = Mathf.Abs(transform.position.y - runner.transform.position.y);

        if (distanceX <= collisionAreaX / 2 && distanceY <= collisionAreaY / 2) {
            transform.position = new Vector3(62, -6);
            runner.speed += 2;
            SpawnTile.MovementCost = 1;
        }
    }
}
