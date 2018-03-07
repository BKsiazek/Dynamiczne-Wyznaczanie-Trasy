public class TerrainType {

	byte cost;

	public static TerrainType grass = new TerrainType (3);
	public static TerrainType waterShallow = new TerrainType (6);
	public static TerrainType waterDeep = new TerrainType (13);
	public static TerrainType mountainLow = new TerrainType (9);
	public static TerrainType mountainMedium = new TerrainType (12);
	public static TerrainType mountainHigh = new TerrainType (15);
	//public static TerrainType singleRock = new TerrainType (20);


	public TerrainType(byte cost)
	{
		this.cost = cost;
	}

	public byte getCost()
	{
		return this.cost;
	}
}
