using UnityEngine;
using System.Collections;

/// <summary>
/// 对比点击的两个方块是否一样
/// </summary>
public class GameControler : MonoBehaviour
{
    /// <summary>
    /// 引用到鼠标第一个点击对象
    /// </summary>
    public static GameObject checkObjOne;
    /// <summary>
    /// 引用到鼠标第二个点击对象
    /// </summary>
    public static GameObject checkObjTwo;
    /// <summary>
    /// 记录消除对数
    /// </summary>      
    public static int playDestoryNumber = 0;     
    
    /// <summary>
    /// 结束动画
    /// </summary>
    public UIPlayAnimation overGameShowUI;
    /// <summary>
    /// 单对消除特效
    /// </summary>
    public GameObject fxMaker;
    public GameObject BtnPanel;
    public AudioClip right;
    public AudioClip cut;
    public static bool firstPlayOver = false;
    public GameObject btn_music;
    /// <summary>
    /// 挑战成功特效
    /// </summary>
    public GameObject sparks;
    private AudioSource source;
    private AudioClip clip;
    public static int rewardScore = 0;

    void Awake()
    {
        source = this.GetComponent<AudioSource>();
        clip = source.clip;
    }

    void Start()
    {
        firstPlayOver = false;
        UIEventListener.Get(btn_music.gameObject).onClick += OnBtnMusicClick;
    }

    public void OnBtnMusicClick(GameObject go)
    {
        if (source.isPlaying)
        {
            source.Stop();
            go.GetComponent<UISprite>().spriteName = "off";
        }
        else
        {
            source.Play();
            go.GetComponent<UISprite>().spriteName = "on";
        }
    }
    void Update()
    {
        if (checkObjOne != null && checkObjTwo != null)
        {
            // tag一样将消失
            if (checkObjOne.tag == checkObjTwo.tag)
            {
                audio.volume = 0.5f;
                audio.PlayOneShot(cut);
                MenueController.getScore += 10;
                Destroy(Instantiate(fxMaker, checkObjOne.transform.position, checkObjOne.transform.rotation), 2f);
                Destroy(Instantiate(fxMaker, checkObjTwo.transform.position, checkObjTwo.transform.rotation), 2f);
                Destroy(checkObjOne);
                Destroy(checkObjTwo);
                checkObjOne = null;
                checkObjTwo = null;
                playDestoryNumber--;
                //预制物体全部消除
                if (playDestoryNumber == 0)
                {
                    firstPlayOver = true;
                    audio.volume = 0.1f;
                    audio.PlayOneShot(right);
                    audio.volume = 0.1f;

                    for (int i = -1; i < 2; i++)
                    {
                        GameObject go =Instantiate(sparks)as GameObject;
                        go.transform.localPosition += new Vector3(6 * i, 0, 0);
                        Debug.Log(go.transform.localPosition);
                        Destroy(go, 3f);
                    }

                    if (MenueController.TimeCount <= 20)
                    {
                        Debug.Log("恭喜挑战成功，是否存储挑战成绩?");
                        Handheld.Vibrate();
                        StartCoroutine(loadBmobLevel());
                        //奖励得分
                        rewardScore = Mathf.CeilToInt((20f - MenueController.TimeCount) * 20f);
                    }
                    else
                    {
                        StartCoroutine(ShowMenue());
                    }
                }
            }
            else
            {
                checkObjOne.GetComponent<ButtonCheckMovement>().isBack = true;
                checkObjOne.GetComponent<ButtonCheckMovement>().isRotate = false;
                checkObjTwo.GetComponent<ButtonCheckMovement>().isBack = true;
                checkObjTwo.GetComponent<ButtonCheckMovement>().isRotate = false;
                if (MenueController.getScore > 0)
                {
                    MenueController.getScore -= 5;
                }
                checkObjOne = null;
                checkObjTwo = null;
                firstPlayOver = false;
            }
        }
        //加载数字或图片后的推出
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.LoadLevel("first");
            MenueController.getScore = 0;
            MenueController.isStarting = false;
            MenueController.ScoreStr = "";
            MenueController.TimeCount = 0;
            MenueController.TimeStr = "";
            playDestoryNumber = 0;
        }
    }

    IEnumerator loadBmobLevel()
    {
        yield return new WaitForSeconds(2f);
        Application.LoadLevel("Bmob");
    }

    public IEnumerator ShowMenue()
    {
        yield return new WaitForSeconds(3f);
        BtnPanel.transform.localPosition = new Vector3(0, 70, 0);
    }
}
