using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos;
    public GameObject cam;
    public float ParallaxEffect;
    private void Start()
    {
        startPos = transform.position.x;
    }
    private void Update()
    {
        float distance = cam.transform.position.x * ParallaxEffect;

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

    }
}