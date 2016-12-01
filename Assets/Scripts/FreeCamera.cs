using UnityEngine;
using System.Collections;

public class FreeCamera : MonoBehaviour
{

	public float lookSpeed = 2f;

	public float momentumScale = 0.2f;
	public float momentumFalloff = 10f;

	private Vector2 yawPitch;
	private Vector2 yawPitchMomentum;

	void Awake()
	{
		yawPitch = new Vector2(transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.x);
		yawPitchMomentum = new Vector2();
	}

	void Update()
	{
		float yaw = Input.GetAxis("Mouse X") * lookSpeed;
		float pitch = -Input.GetAxis("Mouse Y") * lookSpeed;
		//Yaw
		yawPitch.x = Mathf.Repeat(yawPitch.x + yaw + yawPitchMomentum.x * Time.deltaTime, 360f);

		//Pitch
		yawPitch.y = Mathf.Clamp(yawPitch.y + pitch + yawPitchMomentum.y * Time.deltaTime, -90f, 90f);


		yawPitchMomentum += new Vector2(yaw, pitch) * momentumScale;

		yawPitchMomentum *= momentumFalloff * Time.deltaTime;
    }

	void LateUpdate()
	{
		transform.rotation = Quaternion.Euler(yawPitch.y, yawPitch.x, 0f);
	}

}
