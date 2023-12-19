using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IComparable {

    // Coordinates of the tile
    public int X;
    public int Y;

    // Cost values for pathfinding (G, H, F)
    public int G;
    public int H;
    private int _f;
    public int F {
        get { return H + G; }
    }

    public int MovementCost; // Cost of movement through this tile
    public bool Passable; // Whether the tile is passable or not
    public Tile Parent; // Parent tile in the pathfinding algorithm
    private SpriteRenderer renderer; // SpriteRenderer component for rendering the tile sprite

    // ------------------------------------------------- FUNCTIONS ------------------------------------------------- //

    // Upon spawning, set movement cost
    void Awake() {
        this.renderer = GetComponent<SpriteRenderer>();
        this.MovementCost = 1;
    }

    // Load a sprite onto the tile
    public void LoadSprite(Sprite sprite) {
        renderer.sprite = sprite;
    }

    // CompareTo method for sorting tiles based on F value
    public int CompareTo(object obj) {
        if (obj is Tile) {
            return this._f - ((Tile)obj)._f;
        } else {
            return 0;
        }            
    }

    // Calculate Manhattan distance between this tile and another tile
    public int ManhattanDistance(Tile other) {
        return Mathf.Abs(this.X - other.X) + Mathf.Abs(this.Y - other.Y);
    }


}