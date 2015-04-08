using System.Collections;
public class _Material {
	private string name;
	private float globalStock;

	public void SetName (string name){
		this.name = name;
	}
	public void SetGlobalStock (float globalStock){
		this.globalStock = globalStock;
	}
	public string GetName (){
		return this.name;
	}
	public float GetGlobalStock (){
		return this.globalStock;
	}
}