namespace SandBox.Logic.Rules
{
	/// <summary>
	/// Represents a rule for sliding tiles in a tile manager.
	/// </summary>
	public class SlideRule : TileRule
	{
		/// <summary>
		/// Applies the slide rule to the specified tile manager.
		/// </summary>
		/// <param name="tileManager">The tile manager.</param>
		/// <param name="x">The x-coordinate of the tile.</param>
		/// <param name="y">The y-coordinate of the tile.</param>
		/// <param name="tile">The tile to apply the rule to.</param>
		public override void Apply(TileManager tileManager, int x, int y, Tile tile)
		{
			if (tile.Type != 0 && y > 0)
			{
				bool shouldSlideLeft = x > 0 && tileManager.IsTileEmpty(x - 1, y) && tileManager.IsTileEmpty(x - 1, y - 1);
				bool shouldSlideRight = x + 1 < tileManager.TilesX && tileManager.IsTileEmpty(x + 1, y) && tileManager.IsTileEmpty(x + 1, y - 1);

				if (shouldSlideLeft && shouldSlideRight)
				{
					int random = Random.Shared.Next(0, 2);
					if (random >= 1)
					{
						shouldSlideLeft = false;
					}
					else
					{
						shouldSlideRight = false;
					}
				}

				if (shouldSlideLeft)
				{
					tileManager.SetTile(x - 1, y - 1, tile);
					tileManager.SetTile(x, y, Tile.Empty());
				}
				else if (shouldSlideRight)
				{
					tileManager.SetTile(x + 1, y - 1, tile);
					tileManager.SetTile(x, y, Tile.Empty());
				}
			}
		}
	}
}
