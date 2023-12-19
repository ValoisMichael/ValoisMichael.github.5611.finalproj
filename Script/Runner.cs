using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour {
    // Reference to the Grid, Shield, and Player object
    public Grid grid;
    public Shield shield;
    public Player player;

    bool Running = true; // Flag for if playing
    public float speed; // Speed of runner
    private bool sword; // flag if runner has sword

    // Tiles to handle start, end, and goal tile
    private Tile start;
    private Tile end;
    private Tile goal;

    // Initialize Runner's start position
    void Start() {
        sword = false;
        start = grid.tiles[0][(grid.size-1)/2];
        end = grid.tiles[grid.size-1][(grid.size-1)/2];

        LinkedList<Tile> nums = grid.FindPath(start, end);
        goal = nums.First.Value;
        transform.position = new Vector3(start.X, start.Y);
    }   

    void Update () {
        MovementAI();
        PlayerCollision();
    }

    // Sword and Shield aquiring Methods
    public void SwordAquired() {
        sword = true;
        shield.Spawn();
    }
    public void ShieldAquired() {
        sword = false;
    }

    // AI function, uses target movement based on A* call
    private void MovementAI () {
        if (Running) {
            float runnerX = Mathf.Round(transform.position.x * 10f) / 10f;
            float runnerY = Mathf.Round(transform.position.y * 10f) / 10f;
            
            // Check if the runner is in between tiles
            if (runnerX == goal.X && runnerY == goal.Y) {
                // If at end, the runner wins
                if (runnerX == end.X && runnerY == end.Y) {
                    Debug.Log("Solved");
                    Running = false;
                    return;
                }

                // else call A*
                LinkedList<Tile> path = grid.FindPath(goal, end);
                goal = path.First.Value;
            } else {
                // If in between tiles, keep moving
                float moveSpeed = speed * Time.deltaTime;
                if (runnerX != goal.X) {
                    if (runnerX < goal.X) {
                        transform.position = new Vector3(transform.position.x + moveSpeed, Mathf.Round(transform.position.y)); 
                    } else {
                        transform.position = new Vector3(transform.position.x - moveSpeed, Mathf.Round(transform.position.y)); 
                    }
                } else {
                    if (runnerY < goal.Y) {
                        transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y + moveSpeed);   
                    } else {
                        transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y - moveSpeed); 
                    }
                }
            }
        }
    }

    // Collision detectiong Method for Runner-Player
    private void PlayerCollision() {
        // Define a collision area around the Runner
        float collisionAreaX = 0.9f;
        float collisionAreaY = 0.9f;

        // Check if the position of the Runner is within the collision area of the Player
        float distanceX = Mathf.Abs(transform.position.x - player.transform.position.x);
        float distanceY = Mathf.Abs(transform.position.y - player.transform.position.y);

        if (distanceX <= collisionAreaX / 2 && distanceY <= collisionAreaY / 2) {
            // Collision detected
            if (sword) {
                Debug.Log("Runner Wins Collected");
                player.playing = false;
            } else {
                Debug.Log("Player Wins Collected");
                Running = false;
            }
        }
    }
}
