using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniControlScript : MonoBehaviour {
	public string inputFront = "up";
	public string inputBack = "down";
	public string inputLeft = "left";
	public string inputRight = "right";
	public string inputJump = "space";
	public float vAcceleration = .5f;
	public float hAcceleration = .5f;
	public float walkStepDistance = 2;
	public float turnStepAngle = 45;

	private Animator myAnimator;
	private float vSpeed;
	private float hSpeed;

	void Start () {
		myAnimator = GetComponent<Animator>();
		vSpeed = 0;
		hSpeed = 0;
	}

	void Update () {
		if(Input.GetKey(inputFront)) {
			vSpeed = Mathf.Clamp(vSpeed + vAcceleration * Time.deltaTime, 0, 1);
			transform.Translate(0,0,walkStepDistance*vSpeed*Time.deltaTime);
		}/*else if(Input.GetKey(inputBack)) {
			vSpeed = Mathf.Clamp(vSpeed - vAcceleration * Time.deltaTime, -1, 1);
			transform.Translate(0,0,-walkStepDistance*vSpeed*Time.deltaTime);
		}*/ else {
			vSpeed = Mathf.Clamp(vSpeed - 2*vAcceleration * Time.deltaTime, 0, 1);
		}
		
		if(Input.GetKey(inputLeft)) {
			hSpeed = Mathf.Clamp(hSpeed - hAcceleration * Time.deltaTime, -1f, 1f);
			transform.Rotate(Vector3.up, hSpeed * turnStepAngle * Time.deltaTime);
		} else if(Input.GetKey(inputRight)) {
			hSpeed = Mathf.Clamp(hSpeed + hAcceleration * Time.deltaTime, -1f, 1f);
			transform.Rotate(Vector3.up, hSpeed * turnStepAngle * Time.deltaTime);
		} else if(hSpeed > 0) {
			hSpeed = Mathf.Clamp(hSpeed - 2*hAcceleration * Time.deltaTime, -1f, 1f);
		} else if(hSpeed < 0) {
			hSpeed = Mathf.Clamp(hSpeed + 2*hAcceleration * Time.deltaTime,-1f, 1f);
		} 

		//myAnimator.SetBool("isJump", Input.GetKey(KeyCode.Space));
		if(Input.GetKey(inputJump))
			myAnimator.SetTrigger("jump");

		myAnimator.SetFloat("vSpeed", vSpeed);
		myAnimator.SetFloat("hSpeed", hSpeed);
	}/*
	public float speed;
	public float rotateHead;
	public float rotationSpeed;
	public float minCamRotation;
	public float maxCamRotation;

	void Update()
	{
		rotateHead = Input.mousePosition.x/Screen.width - .5f;
		transform.Rotate(Vector3.up, rotateHead * rotationSpeed);

		Transform cam = gameObject.transform.Find("Main Camera");
		float camRotate = Mathf.Clamp(Input.mousePosition.y/Screen.height - .5f, minCamRotation, maxCamRotation);
		Debug.Log(camRotate);
        cam.rotation = Quaternion.identity;
		cam.RotateAround(transform.position, transform.right, camRotate);
        //cam.Rotate(Vector3.left, camRotate*10);

        if (Input.GetMouseButton(0))
			gameObject.GetComponent<CharacterController>().Move(transform.TransformDirection(Vector3.forward * speed * Time.deltaTime));

	}*/
}
