using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Magnet : MonoBehaviour {
    #region Singleton class: Magnet

    public static Magnet Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    #endregion

    [SerializeField]
    private float magnetForce;

    private List<Rigidbody> affectedrigidbodies = new List<Rigidbody>();
    private Transform magnet;

    void Start() {
        magnet = transform;
        affectedrigidbodies.Clear();
    }

    void FixedUpdate() {
        if (!Gameplay.isGameOver && Gameplay.isMoving) {
            foreach (Rigidbody rb in affectedrigidbodies) {
                rb.AddForce((magnet.position - rb.position) * magnetForce * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        string tag = other.tag;
        if (!Gameplay.isGameOver && tag.Equals("Collectible") || tag.Equals("Obstacle")) {
            AddToMagnetField(other.attachedRigidbody);
        }
    }

    void OnTriggerExit(Collider other) {
        string tag = other.tag;
        if (!Gameplay.isGameOver && tag.Equals("Collectible") || tag.Equals("Obstacle")) {
            RemoveFromMagnetField(other.attachedRigidbody);
        }
    }

    public void AddToMagnetField(Rigidbody rb) {
        affectedrigidbodies.Add(rb);
    }

    public void RemoveFromMagnetField(Rigidbody rb) {
        affectedrigidbodies.Remove(rb);
    }
}