using UnityEngine;
using System.Collections;

public class ButtonCheckMovement : MonoBehaviour 
{
    public bool isBack = false;     // 记录是否可以闭合
    public bool isRotate = false;    // 记录是否可以打开

    private int OpenSpeed = 200;    // 打开速度
    private int CloseSpeed = 350;   // 闭合速度

	void Update () 
    {
        MouseDown();

        if (isRotate)
        {
            OpenPictureToRotate();
        }
        if (isBack)
        {
            ClosePicture();
        }
	}

	void LateUpdate()
	{
		if (isBack)
		{
			ClosePicture();
		}
	}
    /// <summary>
    /// 鼠标点击方块
    /// </summary>
    void MouseDown()
    {
        if (Input.GetMouseButtonDown(0)) // 或用if (Input.GetButtonDown ("Fire1")) 
        {

            RaycastHit hit;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(r, out hit)) // 判断是否在视野范围内鼠标按下
            {
                if (hit.collider.gameObject == gameObject)
                {
					audio.Play();
                    if (isRotate == false)
                    {
                        isRotate = true;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 旋转方块：翻开
    /// </summary>
    void OpenPictureToRotate()
    {
        if (transform.eulerAngles.y < 175)
        {
            transform.Rotate(0, OpenSpeed * Time.deltaTime, 0);
        }
        else if (transform.eulerAngles.y < 180 || transform.eulerAngles.y > -180)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            if (GameControler.checkObjOne == null)
            {
                GameControler.checkObjOne = gameObject;
            }
            else if (GameControler.checkObjTwo == null && GameControler.checkObjOne != gameObject)
            {
                GameControler.checkObjTwo = gameObject;
            }
        }
    }

    /// <summary>
    /// 旋转方块：关上
    /// </summary>
    void ClosePicture()
    {
		if (transform.eulerAngles.y < 355 && transform.eulerAngles.y > 10)
        {
            transform.Rotate(0, CloseSpeed * Time.deltaTime, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            isBack = false;
        }
    }
}
