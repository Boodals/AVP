using UnityEngine;
using System.Collections;

public class PathCamera : MonoBehaviour
{

	new public Camera camera;
	public Path path;

	private float progress = 0f;
	public float speed = 3f;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
		camera.transform.position = path.GetPos(progress);

		progress += speed * Time.deltaTime;
	}
}
