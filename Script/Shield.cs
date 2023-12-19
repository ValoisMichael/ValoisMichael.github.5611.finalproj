using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    // Reference to the Grid, Player, and Runner object
    public Grid grid;
    public Player player;
    public Runner runner;

    private bool spawned = false; // Spawn flag
    Tile SpawnTile; // Spawn location

    // ------------------------------------------------- FUNCTIONS ------------------------------------------------- //

    void Update () {
        // Check for collisions only if the Shield has been spawned
        if (spawned) {
            Collision ();
        }
    }

    // Method to spawn the Shield
    public void Spawn () {
        // Find a valid spot for spawning
        ValidSpot();

        // Set the position of the Shield to the chosen spot and flag spawned
        transform.position = new Vector3(SpawnTile.X, SpawnTile.Y);
        spawned = true;
    }

    // Method to find a valid spot for spawning
    private void ValidSpot () {
        int GridOffset = (grid.size-1)/5;
        int GridOffsetLeft = (grid.size-1)/2;
        bool search = true;

        // Continue searching until a valid spot is found
        while(search) {
            // Choose a random tile within the grid bounds
            SpawnTile = grid.tiles[Random.Range(0 + GridOffsetLeft, grid.size - GridOffset)][Random.Range(0, grid.size-1)];

            // Check if the chosen tile is passable
            if (SpawnTile.Passable) {
                search = false;
            }
        }
    }

    // Method to handle collisions with the Player
    private void Collision() {
        // Define a collision area around the Shield
        float collisionAreaX = 0.5f;
        float collisionAreaY = 0.5f;

        // Check if the position of the Player is within the collision area of the Shield
        float distanceX = Mathf.Abs(transform.position.x - player.transform.position.x);
        float distanceY = Mathf.Abs(transform.position.y - player.transform.position.y);

        // If a collision is detected
        if (distanceX <= collisionAreaX / 2 && distanceY <= collisionAreaY / 2) {
            Debug.Log("Shield Collected");
            transform.position = new Vector3(62, -6); // Remove Shield from play
            runner.ShieldAquired(); // Remove sword from Runner
            player.threat = 100; // Reset Minotaur weight
        }
    }
}

