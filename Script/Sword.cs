using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {
    // Reference to the Grid, Player, and Runner object
    public Grid grid;
    public Player player;
    public Runner runner;

    private bool spawned = false; // Spawn flag
    Tile SpawnTile; // Spawn location

    // ------------------------------------------------- FUNCTIONS ------------------------------------------------- //

    void Update () {
        // Check for collisions only if the Boots have been spawned
        if (spawned) {
            Collision ();
        }
    }

    // Method to spawn the Sword
    public void Spawn () {
        // Find a valid spot for spawning
        ValidSpot();

        // Set the position of the Sword to the chosen spot and flag spawned
        transform.position = new Vector3(SpawnTile.X, SpawnTile.Y);
        SpawnTile.MovementCost = -100;
        spawned = true;
    }

    // Method to find a valid spot for spawning
    private void ValidSpot () {
        int GridOffset = (grid.size-1)/5;
        bool search = true;

        // Continue searching until a valid spot is found
        while(search) {
            // Choose a random tile within the grid bounds
            SpawnTile = grid.tiles[Random.Range(0 + GridOffset, grid.size - GridOffset)][Random.Range(0, grid.size-1)];

            // Check if the chosen tile is passable
            if (SpawnTile.Passable) {
                search = false;
            }
        }
    }

    // Method to handle collisions with the Runner
    private void Collision() {
        // Define a collision area around the Sword
        float collisionAreaX = 0.5f;
        float collisionAreaY = 0.5f;

        // Check if the position of the Runner is within the collision area of the Sword
        float distanceX = Mathf.Abs(transform.position.x - runner.transform.position.x);
        float distanceY = Mathf.Abs(transform.position.y - runner.transform.position.y);

        // If a collision is detected
        if (distanceX <= collisionAreaX / 2 && distanceY <= collisionAreaY / 2) {
            Debug.Log("Sword Collected");
            transform.position = new Vector3(62, -6); // Remove Sword from play
            runner.SwordAquired(); // Give Runner the Sword
            player.threat = -10000; // Flip Minotaurs weight
            SpawnTile.MovementCost = 1; // Reset tile's weight
        }
    }
}
