using SandBox.Logic.Affectors;

namespace SandBox.Logic
{
	/// <summary>
	/// Manages the tiles in the sandbox.
	/// </summary>
	public class TileManager
	{
		/// <summary>
		/// Gets the number of tiles in the X direction.
		/// </summary>
		public int TilesX { get; protected set; }

		/// <summary>
		/// Gets the number of tiles in the Y direction.
		/// </summary>
		public int TilesY { get; protected set; }

		/// <summary>
		/// Gets or sets the current tile affector.
		/// </summary>
		public TileAffector CurrentAffector { get; set; } = new DrawSandAffector();

		/// <summary>
		/// Gets or sets the list of tile rules.
		/// </summary>
		public List<TileRule> Rules { get; set; } = new List<TileRule>();

		private Tile[][] _tiles;

		/// <summary>
		/// Initializes a new instance of the <see cref="TileManager"/> class.
		/// </summary>
		/// <param name="tilesX">The number of tiles in the X direction.</param>
		/// <param name="tilesY">The number of tiles in the Y direction.</param>
		public TileManager(int tilesX, int tilesY)
		{
			TilesX = tilesX;
			TilesY = tilesY;

			_tiles = new Tile[tilesX][];
			for (int i = 0; i < tilesX; i++)
			{
				_tiles[i] = new Tile[tilesY];
				for (int j = 0; j < tilesY; j++)
				{
					_tiles[i][j] = Tile.Empty();
				}
			}
		}

		/// <summary>
		/// Updates the state of the tiles.
		/// </summary>
		public void Update()
		{
			for (int x = 0; x < TilesX; x++)
			{
				for (int y = 0; y < TilesY; y++)
				{
					foreach (TileRule rule in Rules)
					{
						rule.Apply(this, x, y, GetTile(x, y));
					}
				}
			}
		}

		/// <summary>
		/// Handles the input at the specified tile coordinates.
		/// </summary>
		/// <param name="x">The X coordinate of the tile.</param>
		/// <param name="y">The Y coordinate of the tile.</param>
		public void HandleInput(int x, int y)
		{
			CurrentAffector.Apply(this, x, y);
		}

		/// <summary>
		/// Gets the tile at the specified coordinates.
		/// </summary>
		/// <param name="x">The X coordinate of the tile.</param>
		/// <param name="y">The Y coordinate of the tile.</param>
		/// <returns>The tile at the specified coordinates.</returns>
		public Tile GetTile(int x, int y)
		{
			return _tiles[x][y];
		}

		/// <summary>
		/// Checks if the tile at the specified coordinates is empty.
		/// </summary>
		/// <param name="x">The X coordinate of the tile.</param>
		/// <param name="y">The Y coordinate of the tile.</param>
		/// <returns><c>true</c> if the tile is empty; otherwise, <c>false</c>.</returns>
		public bool IsTileEmpty(int x, int y)
		{
			return GetTile(x, y).Type == 0;
		}

		/// <summary>
		/// Sets the tile at the specified coordinates.
		/// </summary>
		/// <param name="x">The X coordinate of the tile.</param>
		/// <param name="y">The Y coordinate of the tile.</param>
		/// <param name="tile">The tile to set.</param>
		public void SetTile(int x, int y, Tile tile)
		{
			_tiles[x][y] = tile;
		}

		/// <summary>
		/// Sets the type of the tile at the specified coordinates.
		/// </summary>
		/// <param name="x">The X coordinate of the tile.</param>
		/// <param name="y">The Y coordinate of the tile.</param>
		/// <param name="type">The type to set.</param>
		public void SetTileType(int x, int y, short type)
		{
			_tiles[x][y].Type = type;
		}

		/// <summary>
		/// Sets the temperature of the tile at the specified coordinates.
		/// </summary>
		/// <param name="x">The X coordinate of the tile.</param>
		/// <param name="y">The Y coordinate of the tile.</param>
		/// <param name="temperature">The temperature to set.</param>
		public void SetTileTemperature(int x, int y, int temperature)
		{
			_tiles[x][y].Temperature = temperature;
		}

		/// <summary>
		/// Adds the specified temperature to the tile at the specified coordinates.
		/// </summary>
		/// <param name="x">The X coordinate of the tile.</param>
		/// <param name="y">The Y coordinate of the tile.</param>
		/// <param name="temperature">The temperature to add.</param>
		public void AddTileTemperature(int x, int y, int temperature)
		{
			_tiles[x][y].Temperature += temperature;
		}
	}

	/// <summary>
	/// Represents a tile in the sandbox.
	/// </summary>
	public struct Tile
	{
		/// <summary>
		/// Gets or sets the type of the tile.
		/// </summary>
		public short Type { get; set; }

		/// <summary>
		/// Gets or sets the temperature of the tile.
		/// </summary>
		public int Temperature { get; set; }

		/// <summary>
		/// Creates an empty tile.
		/// </summary>
		/// <returns>The empty tile.</returns>
		public static Tile Empty()
		{
			return new Tile { Type = 0, Temperature = 390 };
		}

		/// <summary>
		/// Creates a sand grain tile.
		/// </summary>
		/// <returns>The sand grain tile.</returns>
		public static Tile SandGrain()
		{
			return new Tile { Type = 1, Temperature = 390 };
		}
	}
}
