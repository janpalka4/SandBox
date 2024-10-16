namespace SandBox.Logic
{
	/// <summary>
	/// Represents a base class for tile affectors.
	/// </summary>
	public abstract class TileAffector
	{
		/// <summary>
		/// Applies the tile effect to the specified tile at the given coordinates.
		/// </summary>
		/// <param name="tileManager">The tile manager.</param>
		/// <param name="x">The x-coordinate of the tile.</param>
		/// <param name="y">The y-coordinate of the tile.</param>
		public abstract void Apply(TileManager tileManager, int x, int y);
	}
}
