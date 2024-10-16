namespace SandBox.Logic.Affectors
{
	/// <summary>
	/// Represents an affector that draws sand on a tile.
	/// </summary>
	public class DrawSandAffector : TileAffector
	{
		/// <summary>
		/// Gets or sets the selected temperature for the sand.
		/// </summary>
		public int SelectedTemperature = 390;

		/// <summary>
		/// Applies the sand affector to the specified tile.
		/// </summary>
		/// <param name="tileManager">The tile manager.</param>
		/// <param name="x">The x-coordinate of the tile.</param>
		/// <param name="y">The y-coordinate of the tile.</param>
		public override void Apply(TileManager tileManager, int x, int y)
		{
			Tile tile = Tile.SandGrain();
			tile.Temperature = SelectedTemperature;

			tileManager.SetTile(x, y, tile);
		}
	}
}
