using UnityEngine;
using System.Collections;

/// <summary>
/// 对比点击的两个方块是否一样
/// </summary>
public class GameControler : MonoBehaviour
{
    public static GameObject checkObjOne;       // 引用到鼠标第一个点击对象
    public static GameObject checkObjTwo;       // 引用到鼠标第二个点击对象
    public static int playDestoryNumber = 0;    // 记录消除了多少个
    public GameObject gaoxiao;

    public UIPlayAnimation overGameShowUI;
    public GameObject fxMaker;
    public GameObject BtnPanel;
    public AudioClip right;
    public AudioClip cut;
    public static bool firstPlayOver = false;
    public GameObject btn_music;
    public GameObject sparks;
    private AudioSource source;
    private AudioClip clip;

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
            go.GetComponent<UISprite>().spriteName = "02";
        }
        else
        {
            source.Play();
            go.GetComponent<UISprite>().spriteName = "01";
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
                UIBtnEven.getScore += 10;
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

                    if (UIBtnEven.TimeCount <= 20)
                    {
                        //搞笑图片
                        //GameObject go = Instantiate(gaoxiao) as GameObject;
                        //Destroy(go, 1.5f);
                        Debug.Log("恭喜挑战成功，是否存储挑战成绩?");
                        Handheld.Vibrate();
                        StartCoroutine(loadBmobLevel());
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
                if (UIBtnEven.getScore > 0)
                {
                    UIBtnEven.getScore -= 5;
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
            UIBtnEven.getScore = 0;
            UIBtnEven.isStarting = false;
            UIBtnEven.ScoreStr = "";
            UIBtnEven.TimeCount = 0;
            UIBtnEven.TimeStr = "";
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
