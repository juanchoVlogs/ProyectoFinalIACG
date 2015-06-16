using System;
using System.Collections;

class Node
{
	double probability = 1.0;
	ArrayList children;
	int IALife = 100;
	int PLife = 100;
	int hit = 0;
	string type = "max";
	double benefit = 0;
	
	public Node(double probability, ArrayList children, int IALife, int PLife, int hit, string type)
	{
		this.probability = probability;
		this.children = children;
		this.IALife = IALife;
		this.PLife = PLife;
		this.hit = hit;
		this.type = type;
	}
	
	public double getProbability()
	{
		return this.probability;
	}
	public void setProbability(double probability)
	{
		this.probability = probability;
	}
	
	public ArrayList getChildren()
	{
		return this.children;
	}
	public void setChildren(ArrayList children)
	{
		this.children = children;
	}
	
	public int getIALife()
	{
		return this.IALife;
	}
	public void setIALife(int IALife)
	{
		this.IALife = IALife;
	}
	
	public int getPLife()
	{
		return this.PLife;
	}
	public void setPLife(int PLife)
	{
		this.PLife = PLife;
	}
	
	public int getHit()
	{
		return this.hit;
	}
	public void setHit(int hit)
	{
		this.hit = hit;
	}
	
	public string getType()
	{
		return this.type;
	}
	public void setType(string type)
	{
		this.type = type;
	}
	
	public double getBenefit(){
		return benefit;
	}
	public void setBenefit(double benefit){
		this.benefit = benefit;
	}
}
