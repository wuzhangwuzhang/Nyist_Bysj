using UnityEngine;
using System.Collections;
using cn.bmob.io;


public class GameUser : BmobUser
{
	
	public BmobInt life { get; set; }
	public BmobInt attack { get; set; }
    public BmobFile file { get; set; }
	public override void write(BmobOutput output, bool all)
	{
		base.write(output, all);
		
		output.Put("life", this.life);
		output.Put("attack", this.attack);
        output.Put("file", this.file);
	}
	
	public override void readFields(BmobInput input)
	{
		base.readFields(input);
		
		this.life = input.getInt("life");
		this.attack = input.getInt("attack");
        this.file = input.getFile("head");
	}
}

