using System.Collections;
public class Score{
	private string name;
	private int numShips;
	private int numBerth;
	private int numTide;
	private int score;

	public string GetName (){
		return this.name;
	}
	public void SetName (string name){
		this.name = name;
	} 
	public int GetNumShips (){
		return this.numShips;
	}
	public void SetNumShips (int numShips){
		this.numShips = numShips;
	}
	public int GetNumBeth (){
		return this.numBerth;
	}
	public void SetNumBerth (int numBerth){
		this.numBerth = numBerth;
	}
	public int GetNumTide (){
		return this.numTide;
	}
	public void SetNumTide (int numTide){
		this.numTide = numTide;
	}
	public int GetScore (){
		return this.score;
	}
	public void SetScore (int score){
		this.score = score;
	}
}