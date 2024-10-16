namespace SandBox.Logic.Rules
{
	/// <summary>
	/// Represents a rule for conducting thermal energy between tiles.
	/// </summary>
	public class ThermalRule : TileRule
	{
		private const int THERMAL_STEP = 10;

		/// <summary>
		/// Applies the thermal rule to the specified tile and its neighboring tiles.
		/// </summary>
		/// <param name="tileManager">The tile manager.</param>
		/// <param name="x">The x-coordinate of the tile.</param>
		/// <param name="y">The y-coordinate of the tile.</param>
		/// <param name="tile">The tile to apply the rule to.</param>
		public override void Apply(TileManager tileManager, int x, int y, Tile tile)
		{
			ConductTemperature(tile, x, y + 1, tileManager);
			ConductTemperature(tile, x, y - 1, tileManager);
			ConductTemperature(tile, x + 1, y, tileManager);
			ConductTemperature(tile, x - 1, y, tileManager);
		}

		private void ConductTemperature(Tile from, int tx, int ty, TileManager tileManager)
		{
			if (tx > 0 && ty > 0 && tx < tileManager.TilesX && ty < tileManager.TilesY)
			{
				float thermalConducitivityMult = 1;
				Tile target = tileManager.GetTile(tx, ty);

				if (target.Temperature >= from.Temperature)
					return;

				if (target.Type == 0)
					thermalConducitivityMult = 0.001f;

				int add = (int)((1f / THERMAL_STEP) * thermalConducitivityMult * from.Temperature);
				from.Temperature -= add;
				tileManager.AddTileTemperature(tx, ty, add);
			}
		}
	}
}
