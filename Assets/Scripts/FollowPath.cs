using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour
{
	public Path path;

	public bool lockRotation = false;

	public float speed = 3f;


	public float distOnPath = 0f;

	void OnValidate()
	{
		UpdateObject();

		distOnPath = Mathf.Clamp(distOnPath, 0f, path.length);
	}

	void LateUpdate()
	{
		UpdateObject();

		distOnPath = Mathf.Clamp(distOnPath + speed * Time.deltaTime, 0f, path.length);
	}

	public void UpdateObject()
	{
		transform.position = path.GetPos(distOnPath);

		if(lockRotation)
		{
			transform.rotation = Quaternion.LookRotation(path.GetNormal(distOnPath));
		}
	}
}
