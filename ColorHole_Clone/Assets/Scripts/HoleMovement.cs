using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HoleMovement : MonoBehaviour {
    /*
     * I need to store hole vertices indexes(int) in a list to move them later in this script
     * I will do that by calculating distance between each vertex and HoleCenter and compare it with the radius
     * if distanc<Radius then this vertex belongs to the hole
     */

    [Header("Hole Mesh")]
    [SerializeField]
    private MeshFilter meshFilter;

    [SerializeField]
    private MeshCollider meshCollider;

    [Header("Hole vertices radius")]
    [SerializeField]
    private Vector2 moveLimists;

    [SerializeField]
    private float radius;

    [SerializeField]
    private Transform holeCenter;

    [SerializeField]
    private Transform rotatingCircle;

    [Space]
    [SerializeField]
    private float moveSpeed;

    private Mesh mesh;
    private List<int> holeVertices;
    private List<Vector3> offsets;
    private int holeVerticesCount;

    private float x, y;
    private Vector3 touch, targetPosition;

    void Start() {
        RotateCircleAnim();
        Gameplay.isGameOver = false;
        Gameplay.isMoving = false;

        holeVertices = new List<int>();
        offsets = new List<Vector3>();
        mesh = meshFilter.mesh;

        //Find Hole Vertices on the mesh
        FindHoleVertices();
    }

    void RotateCircleAnim() {
        rotatingCircle.DORotate(new Vector3(90f, 0f, -90f), .2f).SetEase(Ease.Linear).From(new Vector3(90f, 0f, 0f)).SetLoops(-1, LoopType.Incremental);
    }

    void Update() {
#if UNITY_EDITOR
        //Mouse move
        Gameplay.isMoving = Input.GetMouseButton(0);
        if (!Gameplay.isGameOver && Gameplay.isMoving) {
            //Move hole center
            MoveHole();
            //Update hole Vertices
            UpdateHoleVerticesPosition();
        }
#else
//Mobile touch move
        Gameplay.isMoving = Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Moved;
        if (!Gameplay.isGameOver && Gameplay.isMoving)
        {
            //Move hole center
            MoveHole();
            //Update hole Vertices
            UpdateHoleVerticesPosition();
        }
#endif
    }

    void MoveHole() {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        touch = Vector3.Lerp(holeCenter.position, holeCenter.position + new Vector3(x, 0f, y), moveSpeed * Time.deltaTime);

        targetPosition = new Vector3(Mathf.Clamp(touch.x, moveLimists.x, -moveLimists.x), touch.y, Mathf.Clamp(touch.z, moveLimists.y, -moveLimists.y));

        holeCenter.position = targetPosition;
    }

    void UpdateHoleVerticesPosition() {
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < holeVerticesCount; i++) {
            vertices[holeVertices[i]] = holeCenter.position + offsets[i];
        }

        //update mesh
        mesh.vertices = vertices;
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    void FindHoleVertices() {
        for (int i = 0; i < mesh.vertices.Length; i++) {
            float distance = Vector3.Distance(holeCenter.position, mesh.vertices[i]);
            if (distance < radius) {
                holeVertices.Add(i);
                offsets.Add(mesh.vertices[i] - holeCenter.position);
            }
        }

        holeVerticesCount = holeVertices.Count;
    }

    //void OnDrawGizmos() {
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(holeCenter.position, radius);
    //}
}