using UnityEngine;
using System.Collections;
using cn.bmob.api;
using cn.bmob.tools;
using cn.bmob.io;
using System.Collections.Generic;
using System.Text;
using System;


public class DataControler : MonoBehaviour
{
    public UILabel lab_user1;
    public UILabel lab_user2;
    public UILabel lab_user3;
    public UIButton btn_Submit;
    public UIInput inputName;
    public UILabel lab_score;
    public UILabel lab_netState;

    private string myObjectid;
    private BmobUnity Bmob;
    private List<MyGameTable> dataList = new List<MyGameTable>();
    private StringBuilder NetState = new StringBuilder();
    private bool canUpload = false;

    void Awake()
    {
        //当网络不可用时
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            NetState = new StringBuilder("404");
            canUpload = false;
        }
        //当用户使用WiFi时
        if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            NetState = new StringBuilder("Wifi");
            canUpload = true;
        }
        //当用户使用移动网络时
        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            NetState = new StringBuilder("Mobile NetWork");
            canUpload = true;
        }
    }

    // Use this for initialization  
    void Start()
    {
        lab_netState.text = "";
        BmobDebug.Register(print);
        Bmob = gameObject.GetComponent<BmobUnity>();
        getAllInfo();
        UIEventListener.Get(btn_Submit.gameObject).onClick += OnSubmitClick;
    }

    void OnGUI()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    InsertData("wuzhang", 100);
        //}
#if false
        #region
        if (GUILayout.Button("Insert"))  
		{  
			InsertData();  
		}  
		if(GUILayout.Button("GetData"))
		{
			getRecoard();
		}
		if(GUILayout.Button("Update"))
		{
			updateData();
		}
		if(GUILayout.Button("AllData"))
		{
			getAllInfo();

		}
		if(GUILayout.Button("获取吴长的战役数"))
		{
			getPlayerCount();
		}
		if(GUILayout.Button("按列名查询"))
		{
			queryByColum();
		}
		if(GUILayout.Button("多列查询"))
		{
			queryByMutiplyColumn();
		}
		if(GUILayout.Button("上传文件"))
		{
			UploadFile();

		}
		if(GUILayout.Button("下载文件"))
		{
			DownLoad();
		}
        if (GUILayout.Button("当前用户"))
        {
            gotCurrentUser();
        }
        if (GUILayout.Button("登录"))
        {
            Login();
        }
        if (GUILayout.Button("重置密码"))
        {
            ResetPassword();
        }
        //if (GUILayout.Button("更新用户信息"))
        //{
        //    updateuser();
        //}
        if (GUILayout.Button("查询用户信息"))
        {
            findAllUser();
        }
        if (GUILayout.Button("注册用户"))
        {
            Signup();
        }
        #endregion
#endif
        lab_score.text = (MenueController.getScore + GameControler.rewardScore).ToString();
        lab_netState.text = NetState.ToString();
        if (dataList.Count == 0)
            return;
        lab_user1.text = dataList[0].playerName + " " + dataList[0].score;
        lab_user2.text = dataList[1].playerName + " " + dataList[1].score;
        lab_user3.text = dataList[2].playerName + " " + dataList[2].score;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("first");
        }
    }

    void OnSubmitClick(GameObject go)
    {
        if (string.IsNullOrEmpty(inputName.value))
            Debug.Log("请输入您的大名");
        StartCoroutine(CheckData());
    }

    /// <summary>
    /// 检查用户是否已存在
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckData()
    {
        int score = MenueController.getScore + GameControler.rewardScore;
        string objectID = "";
        Debug.Log("用户名:" + inputName.value.Trim());
        for (int i = 0; i < dataList.Count; i++)
        {
            if (inputName.value == dataList[i].playerName)
            {
                objectID = dataList[i].objectId;
                break;
            }
            else
                objectID = "";
        }
        if (string.IsNullOrEmpty(objectID))
        {
            Debug.Log("新增加数据");
            InsertData(inputName.value.ToString(), score);
        }
        else
        {
            Debug.Log("更新数据：" + objectID);
            updateData(objectID, score);
        }
        #region

        /*
        int score = MenueController.getScore + GameControler.rewardScore;
        if (true || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork
            || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            Debug.Log("Wifi");
            //string objectid = AlreadyExists(inputName.value.Trim());
            string objectID = "";
            Debug.Log("用户名:" + inputName.value.Trim());
            //对返回结果进行处理
            List<MyGameTable> list = new List<MyGameTable>();
            BmobQuery query = new BmobQuery();
            Bmob.Find<MyGameTable>(MyGameTable.TABLENAME, query, (resp, exception) =>
            {
                if (exception != null)
                {
                    print("查询失败, 失败原因为： " + exception.Message);
                    return;
                }
                list.AddRange(resp.results);
                foreach (var item in list)
                {
                    if (item.playerName.CompareTo(inputName.value.Trim()) == 0)
                    {
                        Debug.Log(item.playerName);
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    Debug.Log(list[i].playerName + "  " + list[i].objectId);
                    if (list[i].playerName == inputName.value.Trim())
                    {
                        objectID = list[i].objectId;
                        Debug.Log("返回:" + objectID);
                        break;
                    }
                }
            });

            Debug.Log(objectID);
            yield return new WaitForEndOfFrame();
            if (string.IsNullOrEmpty(objectID))
            {
                Debug.Log("新增加数据");
                InsertData(inputName.value.ToString(), score);
            }
            else
            {
                Debug.Log("更新数据："+ objectID);
                updateData(objectID, score);
            }
        }
        else
        {
            btn_Submit.gameObject.SetActive(false);
        }
        */
        #endregion
        NetState = new StringBuilder("3s 后自动返回主菜单!");
        yield return new WaitForSeconds(3f);
        Application.LoadLevel("first");

    }
    #region
    /// <summary>
    /// 插入数据
    /// </summary>
    void InsertData(string name, int score)
    {

        MyGameTable mg = new MyGameTable();
        mg.playerName = name;
        mg.score = score;

        Bmob.Create(MyGameTable.TABLENAME, mg, (resp, exception) =>
        {
            if (exception != null)
            {
                Debug.Log("保存失败，原因： " + exception.Message);
            }
            else
            {
                Debug.Log("保存成功" + resp.createdAt);
            }
        });
        getAllInfo();
    }

    private string AlreadyExists(string userName)
    {
        string objectID = "";
        Debug.Log("用户名:"+userName);
        //对返回结果进行处理
        List<MyGameTable> list = new List<MyGameTable>();
        BmobQuery query = new BmobQuery();
        Bmob.Find<MyGameTable>(MyGameTable.TABLENAME, query, (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败, 失败原因为： " + exception.Message);
                return;
            }
            list.AddRange(resp.results);
            foreach (var item in list)
            {
                if (item.playerName.CompareTo(userName) == 0)
                {
                    Debug.Log(item.playerName);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                Debug.Log(list[i].playerName+"  "+list[i].objectId);
                if (list[i].playerName == userName)
                {
                    objectID = list[i].objectId;
                    Debug.Log("返回:"+ objectID);
                    break;
                }
            }
        });
        return objectID;
    }

    /// <summary>
    /// 获取数据
    /// </summary>
    void getRecoard()
    {
        MyGameTable mg = new MyGameTable();
        Bmob.Get<MyGameTable>(MyGameTable.TABLENAME, "2TLe999G", (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败, 失败原因为： " + exception.Message);
                return;
            }

            MyGameTable game = resp;
            print(game.playerName + "," + game.score + "," + game.objectId);
            print("获取的对象为： " + game.ToString());
        });
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    void updateData(string objectID, int score)
    {
        MyGameTable game = new MyGameTable();
        game.score = score;
        Bmob.Update(MyGameTable.TABLENAME, objectID, game, (resp, exception) =>
        {
            if (exception != null)
            {
                print("保存失败, 失败原因为： " + exception.Message);
                return;
            }
            print("更新数据成功, @" + resp.updatedAt);
        });
    }
    /// <summary>
    /// 删除数据
    /// </summary>
    void deleteData()
    {
        Bmob.Delete(MyGameTable.TABLENAME, "4d05c4cd58", (resp, exception) =>
        {
            if (exception != null)
            {
                print("删除失败, 失败原因为： " + exception.Message);
                return;
            }
            else
            {
                print("删除成功, @" + resp.msg);
            }
        });
    }
    #endregion
    void getAllInfo()
    {
        #region 方法一
        /***************
        * 1.数据源排序
        ****************/
        //BmobQuery query = new BmobQuery();
        //Bmob.Find<MyGameTable>(MyGameTable.TABLENAME, query, (resp, exception) =>
        //{
        //    if (exception != null)
        //    {
        //        print("查询失败, 失败原因为： " + exception.Message);
        //        return;
        //    }
        //    //对返回结果进行处理
        //    List<MyGameTable> list = resp.results;
        //    List<int> score = new List<int>();
        //    for(int i=0;i<list.Count;i++)
        //    {
        //        print(list[i].playerName.ToString()+"\t"+list[i].score.ToString()+"\t"+list[i].updatedAt.ToString());
        //        score.Add(int.Parse(list[i].score.ToString()));
        //    }
        //    score.Sort();
        //    score.Reverse();
        //    foreach (var item in score)
        //    {
        //        Debug.Log("得分:"+item);
        //    }
        //});
        #endregion
        /***************
         * 2.数据库排序
         ****************/
        dataList.Clear();
        BmobQuery query = new BmobQuery();
        query.OrderByDescending("Score");
        Bmob.Find<MyGameTable>(MyGameTable.TABLENAME, query, (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败, 失败原因为： " + exception.Message);
                return;
            }
            dataList = resp.results;
            //对返回结果进行处理
            foreach (var game in resp.results)
            {
                print("获取的对象为： " + game.playerName + " " + game.score.ToString());
            }
        });
    }
    #region
    /// <summary>
    /// 获取个人的游戏次数
    /// </summary>
    public void getPlayerCount()
    {
        BmobQuery query = new BmobQuery();
        query.WhereEqualTo("playerName", "wuzhang");
        query.Count();
        Bmob.Find<MyGameTable>(MyGameTable.TABLENAME, query, (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败, 失败原因为： " + exception.Message);
                return;
            }

            List<MyGameTable> list = resp.results;
            BmobInt count = resp.count;
            print("参加战役数： " + count.Get());
            foreach (var game in list)
            {
                print("获取的对象为： " + game.playerName.ToString() + "," + game.score.ToString());
            }
        });
    }

    /// <summary>
    /// 按列名查询得分（单列查询）
    /// </summary>
    /// <param name="column">Column.</param>
    public void queryByColum()
    {
        BmobQuery query = new BmobQuery();
        query.Select("score");
        Bmob.Find<MyGameTable>(MyGameTable.TABLENAME, query, (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败, 失败原因为： " + exception.Message);
                return;
            }

            List<MyGameTable> list = resp.results;
            foreach (var game in list)
            {
                print("查询到的数据:" + game.score.ToString());
            }
        });
    }
    /// <summary>
    /// 多列查询
    /// </summary>
    public void queryByMutiplyColumn()
    {
        BmobQuery query = new BmobQuery();
        query.Select("playerName", "score");
        Bmob.Find<MyGameTable>(MyGameTable.TABLENAME, query, (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败, 失败原因为： " + exception.Message);
                return;
            }

            List<MyGameTable> list = resp.results;
            foreach (var game in list)
            {
                print("查询到的数据:" + game.playerName.ToString() + "," + game.score.ToString());
            }
        });
    }
    public void UploadFile()
    {
        #region
        //直接上传到Bmob服务器
        //Bmob.FileUpload(Application.dataPath + "/Picture/My.png", (resp, exception) =>
        // {

        //     if (exception != null)
        //     {
        //         print("上传失败, 失败原因为： " + exception.Message);
        //         return;
        //     }
        //     print("上传成功，返回数据： " + resp.ToString());
        // });
        #endregion
        var file = Application.dataPath + "/Picture/My.png";
        var ffuture = Bmob.FileUploadTaskAsync(file);

        GameUser user = new GameUser();
        user.username = "zhangsan";
        user.password = "123456";
        user.email = "123456@qq.com";
        user.file = ffuture.Result;
    }
    /// <summary>
    /// 下载txt文本
    /// </summary>
    public void DownLoad()
    {
        print("Downloading...");
        Bmob.Get<MyGameTable>(MyGameTable.TABLENAME, "o9VlFFFH", (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败, 失败原因为： " + exception.Message);
                return;
            }

            MyGameTable game = resp;
            print(game.playerName + "," + game.score + "," + game.File.ToString());
            print("获取的对象为： " + game.File.getPath());
            StartCoroutine("LoadFile", game.File.getPath());
        });
    }
    /// <summary>
    /// 异步加载网络文件
    /// </summary>
    /// <returns>The file.</returns>
    /// <param name="url">URL.</param>
    IEnumerator LoadFile(string url)
    {
        print(url);
        WWW www = new WWW(url);
        yield return www; 
        // while (!www.isDone) {}
        //quard.transform.renderer.material.mainTexture =www.texture; 
        //string result = www.text;
        //print("下载文本信息:"+result);

    }

    void Signup()
    {
        GameUser user = new GameUser();
        user.username = "zhangsan";
        user.password = "123456";
        user.email = "123456@qq.com";
        user.life = 0;
        user.attack = 0;

        Bmob.Signup<GameUser>(user, (resp, exception) =>
        {
            if (exception != null)
            {
                print("注册失败, 失败原因为： " + exception.Message);
                return;
            }

            print("注册成功");
        });
    }

    void Login()
    {
        Bmob.Login<GameUser>("bmob", "123456", (resp, exception) =>
        {
            if (exception != null)
            {
                print("登录失败, 失败原因为： " + exception.Message);
                return;
            }

            print("登录成功, @" + resp.username + "(" + resp.life + ")$[" + resp.sessionToken + "]");

            print("登录成功, 当前用户对象Session： " + BmobUser.CurrentUser.sessionToken);
        });
    }

    void gotCurrentUser()
    {
        print("登录后用户： " + (BmobUser.CurrentUser));
    }

    void updateuser()
    {
        Bmob.Login<GameUser>("bmob", "123456", (resp, ex) =>
        {
            print(resp.sessionToken);
            GameUser u = new GameUser();
            u.attack = 1000;
            u.life = 1000;
            Bmob.UpdateUser(resp.objectId, u, resp.sessionToken, (updateResp, updateException) =>
            {
                if (updateException != null)
                {
                    print("保存失败, 失败原因为： " + updateException.Message);
                    return;
                }

                print("保存成功, @" + updateResp.updatedAt);
            });
        });
    }

    void findAllUser()
    {

        BmobQuery query = new BmobQuery();
        //query.WhereEqualTo ("playerName", "123");
        query.Count();
        Bmob.Find<GameUser>(GameUser.TABLE, query, (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败, 失败原因为： " + exception.Message);
                return;
            }

            List<GameUser> list = resp.results;
            BmobInt count = resp.count;
            print("满足条件的对象个数为： " + count.Get());
            foreach (var game in list)
            {
                print("获取的对象为： " + (game));
            }
        });
    }

    void ResetPassword()
    {
        Bmob.Reset("930116897@qq.com", (resp, exception) =>
        {
            if (exception != null)
            {
                print("重置密码请求失败, 失败原因为： " + exception.Message);
                return;
            }

            print("重置密码请求发送成功！");
        });
    }

    void FileUpload()
    {
        Bmob.FileUpload("E:\\winsegit\\bmob\\bmob-csharp\\bmob-demo-csharp\\examples\\bmob-unity-demo\\README.md", (resp, exception) =>
        {
            if (exception != null)
            {
                print("上传请求失败, 失败原因为： " + exception.Message);
                return;
            }

            print("上传请求发送成功！" + (resp));
        });
    }

    void FindUser()
    {
        BmobQuery query = new BmobQuery();
        query.WhereEqualTo("username", "bmob");
        Bmob.Find<GameUser>(BmobUser.TABLE, query, (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败, 失败原因为： " + exception.Message);
                return;
            }

            List<GameUser> list = resp.results;
            foreach (var user in list)
            {
                print("获取的对象为： " + (user));
            }
        });
    }

    /// <summary>
    /// 调用服务器函数
    /// </summary>
    void endpoint()
    {
        Bmob.Endpoint<Hashtable>("test", (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败, 失败原因为： " + exception.Message);
                return;
            }
            print("返回对象为： " + resp);
        });
    }

    protected void WWWFormRequest()
    {
        WWWForm form = new WWWForm();
        form.AddBinaryData("tab.text", new byte[] { 0, 1, 2 });

        if (form != null && form.headers.Count > 0)
        {
            var headers = new Hashtable(); // add content-type
            IDictionaryEnumerator formHeadersIterator = form.headers.GetEnumerator();
            while (formHeadersIterator.MoveNext())
                headers.Add((String)formHeadersIterator.Key, formHeadersIterator.Value);
        }
    }
    #endregion

}
