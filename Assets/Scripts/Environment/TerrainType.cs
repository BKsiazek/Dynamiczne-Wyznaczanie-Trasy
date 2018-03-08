public class TerrainType {

	public float cost;

	public static TerrainType grass = new TerrainType (1f);
	public static TerrainType mountainLow = new TerrainType (6f);
	public static TerrainType mountainMedium = new TerrainType (8f);
	public static TerrainType mountainHigh = new TerrainType (10f);
	public static TerrainType waterShallow = new TerrainType (7f);
	public static TerrainType waterDeep = new TerrainType (13f);

	public TerrainType(float cost){
		this.cost = cost;
	}
}
