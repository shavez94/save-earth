using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class camerborder : MonoBehaviour
{
    public Camera camera;
    public GameObject player;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    public float speed;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
        GenerateCollidersAcrossScreen();
    }

    // Update is called once per frame
    void LateUpdate()
    {
      //  Quaternion rotation = Quaternion.Euler(0,0,player.transform.rotation.z* speed);
        //transform.rotation = rotation;

    }

    // Start is called before the first frame update

    public void cameraoff()
    {
        FindObjectOfType<astroidmanager>().cameraanim_off();
    }
    // Update is called once per frame
    void GenerateCollidersAcrossScreen()
    {
        Vector2 lDCorner = camera.ViewportToWorldPoint(new Vector3(0, 0f, camera.nearClipPlane));
        Vector2 rUCorner = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.nearClipPlane));
        Vector2[] colliderpoints;

        EdgeCollider2D upperEdge = new GameObject("upperEdge").AddComponent<EdgeCollider2D>();
        colliderpoints = upperEdge.points;
        colliderpoints[0] = new Vector2(lDCorner.x, rUCorner.y);
        colliderpoints[1] = new Vector2(rUCorner.x, rUCorner.y);
        upperEdge.points = colliderpoints;

      //  EdgeCollider2D lowerEdge = new GameObject("lowerEdge").AddComponent<EdgeCollider2D>();
       // colliderpoints = lowerEdge.points;
       // colliderpoints[0] = new Vector2(lDCorner.x, lDCorner.y);
       // colliderpoints[1] = new Vector2(rUCorner.x, lDCorner.y);
       // lowerEdge.points = colliderpoints;

        EdgeCollider2D leftEdge = new GameObject("leftEdge").AddComponent<EdgeCollider2D>();
        colliderpoints = leftEdge.points;
        colliderpoints[0] = new Vector2(lDCorner.x, lDCorner.y);
        colliderpoints[1] = new Vector2(lDCorner.x, rUCorner.y);
        leftEdge.points = colliderpoints;

        EdgeCollider2D rightEdge = new GameObject("rightEdge").AddComponent<EdgeCollider2D>();

        colliderpoints = rightEdge.points;
        colliderpoints[0] = new Vector2(rUCorner.x, rUCorner.y);
        colliderpoints[1] = new Vector2(rUCorner.x, lDCorner.y);
        rightEdge.points = colliderpoints;
    }
}
