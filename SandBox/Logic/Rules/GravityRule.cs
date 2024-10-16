namespace SandBox.Logic.Rules
{
	/// <summary>
	/// Represents a rule that applies gravity to the tiles in a tile manager.
	/// </summary>
	public class GravityRule : TileRule
	{
		/// <summary>
		/// Applies the gravity rule to the specified tile in the tile manager.
		/// </summary>
		/// <param name="tileManager">The tile manager.</param>
		/// <param name="x">The x-coordinate of the tile.</param>
		/// <param name="y">The y-coordinate of the tile.</param>
		/// <param name="tile">The tile to apply the rule to.</param>
		public override void Apply(TileManager tileManager, int x, int y, Tile tile)
		{
			if (y > 0)
			{
				if (tileManager.IsTileEmpty(x, y - 1))
				{
					tileManager.SetTile(x, y - 1, tile);
					tileManager.SetTile(x, y, Tile.Empty());
				}
			}
		}
	}
}
