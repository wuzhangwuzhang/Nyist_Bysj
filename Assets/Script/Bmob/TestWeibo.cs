using UnityEngine;
using System.Collections;
using cn.bmob.io;
using cn.bmob.tools;
using cn.bmob.api;

namespace AssemblyCSharp
{
	public class TestWeibo : MonoBehaviour 
	{
		private BmobUnity Bmob;  
		class GameUser
		{
		}

		public const string TABLENAME = "weibo";  
		// Use this for initialization
		void Start () 
		{
			BmobDebug.Register(print);  
			Bmob = gameObject.GetComponent<BmobUnity>();  
			Bmob.FileUpload("C:/testBmob.txt", (resp, exception) =>
			                     {
				if (exception != null)
				{
					print("上传失败, 失败原因为： " + exception.Message);
					return;
				}
				print("上传成功，返回数据： " + resp.ToString());
			});
			//			BmobDebug.Register(print);  
//			
//
//			//获取当前登录用户信息
//			GameUser user = BmobUser.CurrentUser();
//			var comment = new Comment();
//			// 设定评论内容
//			comment.comment = "发布的评论信息";
//			// 设定评论人
//			comment.user = new BmobPointer<BmobUser>(user);
//			// 设定评论对应的微博
//			Weibo weibo = new Weibo();
//			weibo.objectId = "ZGwboItm";
//			comment.weibo = new BmobPointer<Weibo>(weibo);;    
//			
//			Bmob.Create(TABLENAME, comment, (resp, exception) =>
//			                 {
//				if (exception != null)
//				{
//					print("添加失败, 失败原因为： " + exception.Message);
//					return;
//				}
//				
//				print("添加成功, @" + resp.createAt);
//			});
		}
		
		// Update is called once per frame
		void Update () 
		{
		
		}
	}
}
