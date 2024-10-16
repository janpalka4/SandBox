namespace SandBox.Logic
{
	/// <summary>
	/// Represents a base class for tile rules.
	/// </summary>
	public abstract class TileRule
	{
		/// <summary>
		/// Applies the tile rule to the specified tile at the given position.
		/// </summary>
		/// <param name="tileManager">The tile manager.</param>
		/// <param name="x">The x-coordinate of the tile.</param>
		/// <param name="y">The y-coordinate of the tile.</param>
		/// <param name="tile">The tile to apply the rule to.</param>
		public abstract void Apply(TileManager tileManager, int x, int y, Tile tile);
	}
}
