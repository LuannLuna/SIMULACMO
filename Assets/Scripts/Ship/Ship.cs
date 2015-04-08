using System.Collections;
public class Ship{
	private int tideArrival;
	private int[] cargo;
	private int numMaterials;

	public void SetShip (int numMaterials){
		this.numMaterials = numMaterials;
	}
	public int GetShip (){
		return this.numMaterials;
	}
	public void SetTideArrival (int tideArrival){
		this.tideArrival = tideArrival;
	}
	public int GetTideArrival (){
		return this.tideArrival;
	}
	public void SetCargo (int[] cargo){
		this.cargo = cargo;
	}
	public int[] GetCargo (){
		return this.cargo;
	}
}