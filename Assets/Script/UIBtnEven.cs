using UnityEngine;
using System.Collections;

public class UIBtnEven : MonoBehaviour
{
    public UILabel playingTimeLab;      	//用于显示游戏时间
    public UILabel ScoreLab;            	//显示游戏得分
    public static bool click = false;   	//button是否被点击
    public GameObject[] checkGroup;     	// 放数字方格的预制件
    public GameObject[] PinYin;				// 拼音预制件
    public GameObject[] PicPrbs;        	//button3的预制物体
    private Vector3[,] checkPosition = new Vector3[10, 2];  // 方格新建位置
    private GameObject[] randCheckGroup = new GameObject[20];   // 存放乱序的预制件
    private float timeLevelLoad;   	    //记录游戏开始的时间
    private GameObject BtnPanel;            //游戏菜单界面
    public static int getScore = 0; 	    //得分
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
    public GameObject help;   //帮组面板

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
        int a = 0;
        for (int i = 0; i < insterNumber; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                randCheckGroup[a] = checkGroup[i];
                a++;
            }
        }

        int b = 0;
        GameObject t;
        for (int i = 0; i < insterNumber * 2; i++)
        {
            b = Random.Range(0, insterNumber * 2);
            if (b != i)
            {
                t = randCheckGroup[i];
                randCheckGroup[i] = randCheckGroup[b];
                randCheckGroup[b] = t;
            }
        }
    }

    /// <summary>
    /// 创建方格对象
    /// </summary>
    /// <param name="inseerNumber">要创建的方块的个数(根据游戏难度不同)</param>
    void InstantiateCheck(int inseerNumber)
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
    public void BtnOneClickEven()
    {
        getScore = 0;
        RandCheck(6);
        CheckPosition();
        InstantiateCheck(6);
        GameControler.playDestoryNumber = 6;
        timeLevelLoad = Time.timeSinceLevelLoad; //此刻开始时间  
    }

    /// <summary>
    ///  拼音篇
    /// </summary>
    public void BtnTwoClickEven()
    {
        getScore = 0;
        int rowNumber = 6;
        int a = 0;
        for (int i = 0; i < rowNumber; i++)
        {
            int index = Random.Range(0, PinYin.Length);
            for (int j = 0; j < 2; j++)
            {
                randCheckGroup[a] = PinYin[index];
                a++;
            }
        }

        int b = 0;
        GameObject t;
        for (int i = 0; i < rowNumber * 2; i++)
        {
            b = Random.Range(0, rowNumber * 2);
            if (b != i)
            {
                t = randCheckGroup[i];
                randCheckGroup[i] = randCheckGroup[b];
                randCheckGroup[b] = t;
            }
        }
        //        CheckPosition();
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

        //checkPosition[6, 0] = new Vector3(-24, 32, 0);
        //checkPosition[6, 1] = new Vector3(-8, 32, 0);
        //checkPosition[7, 0] = new Vector3(8, 32, 0);
        //checkPosition[7, 1] = new Vector3(24, 32, 0);

        //		checkPosition[8, 0] = new Vector3(-24, -32, 0);
        //		checkPosition[8, 1] = new Vector3(-8, -32, 0);
        //		checkPosition[9, 0] = new Vector3(8, -32, 0);
        //		checkPosition[9, 1] = new Vector3(24, -32, 0);
        //		
        //        InstantiateCheck(10);
        if (GameControler.playDestoryNumber == 0)
        {
            int a1 = 0;
            for (int i = 0; i < rowNumber; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Vector3 p = checkPosition[i, j];
                    GameObject o = randCheckGroup[a1];
                    Quaternion q = Quaternion.identity;
                    GameObject newObject = GameObject.Instantiate(o, new Vector3(p.x, p.y - 5, p.z), q) as GameObject;
                    newObject.transform.parent = this.transform;
                    a1++;
                }
            }
        }
        GameControler.playDestoryNumber = rowNumber;
        timeLevelLoad = Time.timeSinceLevelLoad; //此刻开始时间
    }

   
    /// <summary>
    ///  图画篇
    /// </summary>
    public void BtnThreeClickEven()
    {
        click = true;
        getScore = 0;
        int rowNumber = 6;
        //        RandCheck(10);
        int a = 0;
        for (int i = 0; i < rowNumber; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                randCheckGroup[a] = PicPrbs[i];
                a++;
            }
        }

        int b = 0;
        GameObject t;
        for (int i = 0; i < rowNumber * 2; i++)
        {
            b = Random.Range(0, rowNumber * 2);
            if (b != i)
            {
                t = randCheckGroup[i];
                randCheckGroup[i] = randCheckGroup[b];
                randCheckGroup[b] = t;
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

        //        InstantiateCheck(10);
        if (GameControler.playDestoryNumber == 0)
        {
            int a1 = 0;
            for (int i = 0; i < rowNumber; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Vector3 p = checkPosition[i, j];
                    GameObject o = randCheckGroup[a1];
                    Quaternion q = Quaternion.identity;
                    GameObject newObject = GameObject.Instantiate(o, p, q) as GameObject;
                    newObject.transform.parent = this.transform;
                    a1++;
                }
            }
        }
        GameControler.playDestoryNumber = rowNumber;
        timeLevelLoad = Time.timeSinceLevelLoad; //此刻开始时间
    }

    /// <summary>
    ///  学习篇
    /// </summary>
    public void BtnFourClickEven()
    {
        getScore = 0;
        StartCoroutine("LoagMovie");
    }

    public GameObject helpPanel;
    public GameObject inPos;
    public GameObject outPos;
    bool isIn = false;

    /// <summary>
    /// 帮助按钮点击
    /// </summary>
    public void BtnFiveClickEven()
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
    public void BtnSixClickEven()
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
