using UnityEngine;  
using System.Collections;  
using cn.bmob.io;  

public class MyGameTable : BmobTable  
{  
	public const string TABLENAME = "SmartBabyTable";  
	
	public string playerName { get; set; }  
	public BmobInt score { get; set; }  
	public BmobFile File { get; set; }
	public string testBmobtxt{get;set;}

	
	public override void readFields(BmobInput input)  
	{  
		base.readFields(input);  
		this.score = input.getInt("Score");  
		this.playerName = input.getString("UserName");
        this.File = input.Get<BmobFile>("HeadIcon");
	}  
	
	public override void write(BmobOutput output, bool all)  
	{  
		base.write(output, all);  
		output.Put("Score", this.score);  
		output.Put("UserName", this.playerName);
        output.Put("HeadIcon", this.File);
	}  
}  