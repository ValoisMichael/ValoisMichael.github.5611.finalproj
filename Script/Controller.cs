using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    // Reference to the Grid, Sword, and Boots objects
    public Grid grid;
    public Sword sword;
    public Boots boots;

    // ------------------------------------------------- FUNCTIONS ------------------------------------------------- //

    void Start() {
        grid.CreateTiles(); // Create tiles on the grid
        grid.CreateWalls(); // Create walls on the grid
        sword.Spawn(); // Spawn the sword

        // Spawns 3 boots
        for (int i = 0; i < 3; i++) {
            Boots boot = Instantiate(boots, new Vector3(62, 5), Quaternion.identity);
            boot.Spawn();
        }
    }

    void Update() {
        // Quit application on Esc
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
