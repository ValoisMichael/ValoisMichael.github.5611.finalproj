using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Reference to the Grid object
    public Grid grid;

    public float speed; // Speed of player
    private char target = 'n';
    public bool playing; // Flag for if playing
    public int threat = 100; // Weight of tile where player is

    // Tiles to handle target and end tile
    private Tile Target;
    private Tile end;

    // Spawns player at end tile
    void Start() {
        end = grid.tiles[grid.size - 1][(grid.size - 1) / 2];
        transform.position = new Vector3(end.X, end.Y);
        playing = true; 
    }
    
    // Controller Function for player
    void Update() {
        if (playing) {
            float playerX = Mathf.Round(transform.position.x * 10f) / 10f;
            float playerY = Mathf.Round(transform.position.y * 10f) / 10f;

            // Find the next target tile to move to
            void SetTarget(char direction, int x, int y) {
                Target = grid.SafelyRetrieve((int)Mathf.Round(playerX) + x, (int)Mathf.Round(playerY) + y);
                if (Target.Passable) {
                    target = direction;
                    Target.MovementCost = threat;
                    grid.tiles[(int)Mathf.Round(playerX)][(int)Mathf.Round(playerY)].MovementCost = 1;
                }
            }

            // If squarely on a tile, wait for a new target
            if (target == 'n') {
                if (Input.GetKey("w")) SetTarget('w', 0, 1);
                if (Input.GetKey("s")) SetTarget('s', 0, -1);
                if (Input.GetKey("a")) SetTarget('a', -1, 0);
                if (Input.GetKey("d")) SetTarget('d', 1, 0);
            }

            // If in between tiles, keep moving
            if (target != 'n') {
                float moveSpeed = speed * Time.deltaTime;
                if (target == 'w') transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y + moveSpeed);
                if (target == 's') transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y - moveSpeed);
                if (target == 'a') transform.position = new Vector3(transform.position.x - moveSpeed, Mathf.Round(transform.position.y));
                if (target == 'd') transform.position = new Vector3(transform.position.x + moveSpeed, Mathf.Round(transform.position.y));
            }

            // Are you on a tile? Reset target if so
            if (Mathf.Approximately(playerX % 1, 0) && Mathf.Approximately(playerY % 1, 0)) {
                target = 'n';
                grid.tiles[(int)Mathf.Round(playerX)][(int)Mathf.Round(playerY)].MovementCost = threat;
            }
        }
    }
}