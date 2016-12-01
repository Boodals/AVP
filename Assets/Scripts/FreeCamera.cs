using UnityEngine;
using System.Collections;

public class FreeCamera : MonoBehaviour
{

	public float lookSpeed = 2f;

	private Vector2 yawPitch;

	void Awake()
	{
		yawPitch = new Vector2(transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.x);
	}

	void Update()
	{
		//Yaw
		yawPitch.x = Mathf.Repeat(yawPitch.x + Input.GetAxis("Mouse X") * lookSpeed, 360f);

		//Pitch
		yawPitch.y = Mathf.Clamp(yawPitch.y - Input.GetAxis("Mouse Y") * lookSpeed, -90f, 90f);
	}

	void LateUpdate()
	{
		transform.rotation = Quaternion.Euler(yawPitch.y, yawPitch.x, 0f);
	}

}
