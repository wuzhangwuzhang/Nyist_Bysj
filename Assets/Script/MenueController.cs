using UnityEngine;
using System.Collections;

public class MenueController : MonoBehaviour
{
    #region 公有变量
    /// <summary>
    /// 游戏时间显示
    /// </summary>
    public UILabel playingTimeLab;
    /// <summary>
    /// 游戏得分显示
    /// </summary>
    public UILabel ScoreLab;           
    /// <summary>
    /// 对象选中标记
    /// </summary>
    public static bool click = false;   	
    /// <summary>
    /// 待实例化对象预制
    /// </summary>
    public GameObject[] checkGroup;
    /// <summary>
    /// 拼音预制件
    /// </summary>
    public GameObject[] PinYin;				
    /// <summary>
    /// 图片预制
    /// </summary>         
    public GameObject[] PicPrbs;
    /// <summary>
    /// 方格新建位置
    /// </summary>
    private Vector3[,] checkPosition = new Vector3[10, 2];
    /// <summary>
    /// 存放乱序的预制
    /// </summary>
    private GameObject[] randCheckGroup = new GameObject[20];  
    /// <summary>
    /// 游戏计时时间
    /// </summary>
    private float timeLevelLoad;
    /// <summary>
    /// 游戏菜单按钮
    /// </summary>
    private GameObject BtnPanel;
    /// <summary>
    /// 游戏得分
    /// </summary>

    public static int getScore = 0; 	   
    /// <summary>
    /// 是否开始游戏
    /// </summary>
    public static bool isStarting;
    /// <summary>
    /// 游戏时间总和
    /// </summary>
    public static float TimeCount = 0;

    /// <summary>
    /// 显示时间的字符串
    /// </summary>
    public static string TimeStr = string.Empty;
    /// <summary>
    /// 显示得分的字符串
    /// </summary>
    public static string ScoreStr = string.Empty;
    /// <summary>
    /// 帮助面板
    /// </summary>
    public GameObject help;
    public GameObject helpPanel;
    public GameObject inPos;
    public GameObject outPos;
    bool isIn = false;
    #endregion

    void Start()
    {
        TimeStr = null;
        ScoreStr = null;
        getScore = 0;
        isStarting = false;
        BtnPanel = GameObject.Find("UIBtn").gameObject;
    }

    void Update()
    {
        if (GameControler.playDestoryNumber != 0)
        {
            PlayingTime();
            ScoreStr = "Score:" + getScore.ToString();
            ScoreLab.text = ScoreStr;
        }
        if (isIn && Input.GetMouseButtonDown(0))
        {
            isIn = false;
            StartCoroutine("loadSelf");
            iTween.MoveTo(helpPanel, outPos.transform.position, 2f);
        }
    }

    IEnumerator loadSelf()
    {
        yield return new WaitForSeconds(0.7f);
        Application.LoadLevel("first");
    }

    /// <summary>
    ///  方格新建的位置
    /// </summary>
    void CheckPosition()
    {
        checkPosition[0, 0] = new Vector3(-24, 16, 0);
        checkPosition[0, 1] = new Vector3(-8, 16, 0);
        checkPosition[1, 0] = new Vector3(8, 16, 0);
        checkPosition[1, 1] = new Vector3(24, 16, 0);

        checkPosition[2, 0] = new Vector3(-24, 0, 0);
        checkPosition[2, 1] = new Vector3(-8, 0, 0);
        checkPosition[3, 0] = new Vector3(8, 0, 0);
        checkPosition[3, 1] = new Vector3(24, 0, 0);

        checkPosition[4, 0] = new Vector3(-24, -16, 0);
        checkPosition[4, 1] = new Vector3(-8, -16, 0);
        checkPosition[5, 0] = new Vector3(8, -16, 0);
        checkPosition[5, 1] = new Vector3(24, -16, 0);

        checkPosition[6, 0] = new Vector3(-24, 32, 0);
        checkPosition[6, 1] = new Vector3(-8, 32, 0);
        checkPosition[7, 0] = new Vector3(8, 32, 0);
        checkPosition[7, 1] = new Vector3(24, 32, 0);

        checkPosition[8, 0] = new Vector3(-24, -32, 0);
        checkPosition[8, 1] = new Vector3(-8, -32, 0);
        checkPosition[9, 0] = new Vector3(8, -32, 0);
        checkPosition[9, 1] = new Vector3(24, -32, 0);
    }

    /// <summary>
    /// 将方块的顺序打散
    /// </summary>
    /// <param name="insterNumber"> 方块的个数(根据游戏难度不同) </param>
    void RandCheck(int insterNumber)
    {
        int index = 0;
        for (int i = 0; i < insterNumber; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                randCheckGroup[index] = checkGroup[i];
                index++;
            }
        }
        //位置交换随机索引位置下表
        GameObject t;
        int randomIndex = 0;
        for (int i = 0; i < insterNumber * 2; i++)
        {
            randomIndex = Random.Range(0, insterNumber * 2);
            if (randomIndex != i)
            {
                t = randCheckGroup[i];
                randCheckGroup[i] = randCheckGroup[randomIndex];
                randCheckGroup[randomIndex] = t;
            }
        }
    }

    /// <summary>
    /// 创建方格对象
    /// </summary>
    /// <param name="inseerNumber">要创建的方块的个数(根据游戏难度不同)</param>
    void InitPrefab(int inseerNumber)
    {
        if (GameControler.playDestoryNumber == 0)
        {
            int index = 0;
            for (int i = 0; i < inseerNumber; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Vector3 p = checkPosition[i, j];
                    GameObject o = randCheckGroup[index];
                    Quaternion q = Quaternion.identity;
                    GameObject newObject = GameObject.Instantiate(o, p, q) as GameObject;
                    newObject.transform.parent = this.transform;
                    index++;
                }
            }
        }
    }

    /// <summary>
    ///  识数篇
    /// </summary>
    public void BtnOneClickEvent()
    {
        getScore = 0;
        RandCheck(6);
        CheckPosition();
        InitPrefab(6);
        GameControler.playDestoryNumber = 6;
        timeLevelLoad = Time.timeSinceLevelLoad; 
    }

    /// <summary>
    ///  拼音篇
    /// </summary>
    public void BtnTwoClickEvent()
    {
        getScore = 0;
        int rowNumber = 6;
        int tempIndex1 = 0;
        for (int i = 0; i < rowNumber; i++)
        {
            int index = Random.Range(0, PinYin.Length);
            for (int j = 0; j < 2; j++)
            {
                randCheckGroup[tempIndex1] = PinYin[index];
                tempIndex1++;
            }
        }

        int tempIndex2 = 0;
        GameObject swap;
        for (int i = 0; i < rowNumber * 2; i++)
        {
            tempIndex2 = Random.Range(0, rowNumber * 2);
            if (tempIndex2 != i)
            {
                swap = randCheckGroup[i];
                randCheckGroup[i] = randCheckGroup[tempIndex2];
                randCheckGroup[tempIndex2] = swap;
            }
        }
        checkPosition[0, 0] = new Vector3(-24, 16, 0);
        checkPosition[0, 1] = new Vector3(-8, 16, 0);
        checkPosition[1, 0] = new Vector3(8, 16, 0);
        checkPosition[1, 1] = new Vector3(24, 16, 0);

        checkPosition[2, 0] = new Vector3(-24, 0, 0);
        checkPosition[2, 1] = new Vector3(-8, 0, 0);
        checkPosition[3, 0] = new Vector3(8, 0, 0);
        checkPosition[3, 1] = new Vector3(24, 0, 0);

        checkPosition[4, 0] = new Vector3(-24, -16, 0);
        checkPosition[4, 1] = new Vector3(-8, -16, 0);
        checkPosition[5, 0] = new Vector3(8, -16, 0);
        checkPosition[5, 1] = new Vector3(24, -16, 0);

       
        if (GameControler.playDestoryNumber == 0)
        {
            int counter = 0;
            for (int i = 0; i < rowNumber; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Vector3 p = checkPosition[i, j];
                    GameObject o = randCheckGroup[counter];
                    Quaternion q = Quaternion.identity;
                    GameObject newObject = GameObject.Instantiate(o, new Vector3(p.x, p.y - 5, p.z), q) as GameObject;
                    newObject.transform.parent = this.transform;
                    counter++;
                }
            }
        }
        GameControler.playDestoryNumber = rowNumber;
        timeLevelLoad = Time.timeSinceLevelLoad;
    }

   
    /// <summary>
    ///  图画篇
    /// </summary>
    public void BtnThreeClickEvent()
    {
        click = true;
        getScore = 0;
        int rowNumber = 6;
        int tempIndex1 = 0;
        for (int i = 0; i < rowNumber; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                randCheckGroup[tempIndex1] = PicPrbs[i];
                tempIndex1++;
            }
        }

        int tempIndex2 = 0;
        GameObject t;
        for (int i = 0; i < rowNumber * 2; i++)
        {
            tempIndex2 = Random.Range(0, rowNumber * 2);
            if (tempIndex2 != i)
            {
                t = randCheckGroup[i];
                randCheckGroup[i] = randCheckGroup[tempIndex2];
                randCheckGroup[tempIndex2] = t;
            }
        }

        checkPosition[0, 0] = new Vector3(-24, 16, 0);
        checkPosition[0, 1] = new Vector3(-8, 16, 0);
        checkPosition[1, 0] = new Vector3(8, 16, 0);
        checkPosition[1, 1] = new Vector3(24, 16, 0);

        checkPosition[2, 0] = new Vector3(-24, 0, 0);
        checkPosition[2, 1] = new Vector3(-8, 0, 0);
        checkPosition[3, 0] = new Vector3(8, 0, 0);
        checkPosition[3, 1] = new Vector3(24, 0, 0);

        checkPosition[4, 0] = new Vector3(-24, -16, 0);
        checkPosition[4, 1] = new Vector3(-8, -16, 0);
        checkPosition[5, 0] = new Vector3(8, -16, 0);
        checkPosition[5, 1] = new Vector3(24, -16, 0);

        checkPosition[6, 0] = new Vector3(-24, 32, 0);
        checkPosition[6, 1] = new Vector3(-8, 32, 0);
        checkPosition[7, 0] = new Vector3(8, 32, 0);
        checkPosition[7, 1] = new Vector3(24, 32, 0);

        checkPosition[8, 0] = new Vector3(-24, -32, 0);
        checkPosition[8, 1] = new Vector3(-8, -32, 0);
        checkPosition[9, 0] = new Vector3(8, -32, 0);
        checkPosition[9, 1] = new Vector3(24, -32, 0);

        if (GameControler.playDestoryNumber == 0)
        {
            int counter = 0;
            for (int i = 0; i < rowNumber; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Vector3 p = checkPosition[i, j];
                    GameObject o = randCheckGroup[counter];
                    Quaternion q = Quaternion.identity;
                    GameObject newObject = GameObject.Instantiate(o, p, q) as GameObject;
                    newObject.transform.parent = this.transform;
                    counter++;
                }
            }
        }
        GameControler.playDestoryNumber = rowNumber;
        timeLevelLoad = Time.timeSinceLevelLoad; //此刻开始时间
    }

    /// <summary>
    ///  学习篇
    /// </summary>
    public void BtnFourClickEvent()
    {
        getScore = 0;
        StartCoroutine("LoagMovie");
    }

    /// <summary>
    /// 帮助按钮点击
    /// </summary>
    public void BtnFiveClickEvent()
    {
        if (!isIn)
        {
            isIn = true;
            iTween.MoveTo(helpPanel, inPos.transform.position, 2f);
            Debug.Log("Comeing in");
        }
        else
        {
            isIn = false;
            iTween.MoveTo(helpPanel, outPos.transform.position, 2f);
            Debug.Log("Comeing out");
        }
    }

    /// <summary>
    /// 分享
    /// </summary>
    public void BtnSixClickEvent()
    {
        StartCoroutine("loadLevelShare");
    }

    public IEnumerator loadLevelShare()
    {
        yield return new WaitForSeconds(1f);
        Application.LoadLevel("share");
    }

    /// <summary>
    /// 返回menue
    /// </summary>
    /// <returns>The to menue.</returns>
    public IEnumerator backToMenue()
    {
        yield return new WaitForSeconds(3.1f);
        Application.LoadLevel("first");
    }

    /// <summary>
    /// 协程加载关卡
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoagMovie()
    {
        yield return new WaitForSeconds(0.3f);
        Application.LoadLevel("moive");
    }

    /// <summary>
    ///  游戏时间
    /// </summary>
    void PlayingTime()
    {
        float runTime;
        runTime = Time.timeSinceLevelLoad - timeLevelLoad;
        TimeStr = "Time: " + runTime.ToString("0");
        playingTimeLab.text = TimeStr;
        TimeCount = runTime;
    }
    void FixedUpdate()
    {
        if (GameControler.firstPlayOver)
        {
            playingTimeLab.text = "";
            ScoreLab.text = "";
        }
    }

}
