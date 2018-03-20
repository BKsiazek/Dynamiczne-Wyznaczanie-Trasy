public class TerrainType {

	public float cost;

	public static TerrainType grass = new TerrainType (1f);
	public static TerrainType mountainLow = new TerrainType (600f);
	public static TerrainType mountainMedium = new TerrainType (80f);
	public static TerrainType mountainHigh = new TerrainType (999f);
	public static TerrainType waterShallow = new TerrainType (70f);
	public static TerrainType waterDeep = new TerrainType (130f);

	public TerrainType(float cost){
		this.cost = cost;
	}
}
