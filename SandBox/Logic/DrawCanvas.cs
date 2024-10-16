using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace SandBox.Logic
{
	/// <summary>
	/// Represents a canvas for drawing tiles with temperature colors.
	/// </summary>
	public class DrawCanvas : FrameworkElement
	{
		/// <summary>
		/// The temperature step used for generating the temperature palette.
		/// </summary>
		public const int TEMPERATURE_STEP = 100;

		private VisualCollection _visuals;
		private DispatcherTimer _timer;
		private TileManager _tileManager;
		private int _tileSize;
		private bool _mouseDown;
		private List<Color> _temperaturePallette;

		/// <summary>
		/// Initializes a new instance of the <see cref="DrawCanvas"/> class.
		/// </summary>
		/// <param name="tileManager">The tile manager.</param>
		/// <param name="tileSize">The size of each tile.</param>
		public DrawCanvas(TileManager tileManager, int tileSize)
		{
			_visuals = new VisualCollection(this);
			_timer = new DispatcherTimer();
			_timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);
			_timer.Tick += Update;

			_tileSize = tileSize;
			_tileManager = tileManager;
			_temperaturePallette = GenerateTemperatuePallette();

			_visuals.Add(Draw());

			_timer.Start();
		}

		/// <summary>
		/// Generates the temperature palette.
		/// </summary>
		/// <returns>The list of colors representing the temperature palette.</returns>
		private List<Color> GenerateTemperatuePallette()
		{
			List<Color> ret = new List<Color>();
			for (int t = 1000; t < 40000; t += TEMPERATURE_STEP)
			{
				ret.Add(Utility.CalculateColorFromKelvin(t));
			}

			return ret;
		}

		/// <summary>
		/// Gets the temperature color based on the given temperature value.
		/// </summary>
		/// <param name="temperature">The temperature value.</param>
		/// <returns>The <see cref="SolidColorBrush"/> representing the temperature color.</returns>
		private SolidColorBrush GetTemperatureColor(int temperature)
		{
			Color colorBase = Colors.Yellow;

			float division = temperature / (float)TEMPERATURE_STEP;
			int index = (int)division;
			float alpha = 1 - division % 1;

			if (index > _temperaturePallette.Count - 1)
			{
				index = _temperaturePallette.Count - 1;
			}

			return new SolidColorBrush(Utility.LerpColor(colorBase, _temperaturePallette[index], alpha));
		}

		/// <summary>
		/// Draws the canvas.
		/// </summary>
		/// <returns>The <see cref="DrawingVisual"/> representing the canvas.</returns>
		private DrawingVisual Draw()
		{
			DrawingVisual drawingVisual = new DrawingVisual();
			using (DrawingContext context = drawingVisual.RenderOpen())
			{
				context.DrawRectangle(Brushes.Black, null, new Rect(0, 0, _tileManager.TilesX * _tileSize, _tileManager.TilesY * _tileSize));

				for (int x = 0; x < _tileManager.TilesX; x++)
				{
					for (int y = 0; y < _tileManager.TilesY; y++)
					{
						int flipped_y = _tileManager.TilesY - y - 1;
						Tile tile = _tileManager.GetTile(x, flipped_y);
						if (tile.Type == 1)
						{
							SolidColorBrush temperatureBrush = GetTemperatureColor(tile.Temperature);
							context.DrawRectangle(temperatureBrush, null, new Rect(x * _tileSize, y * _tileSize, _tileSize, _tileSize));
						}
					}
				}
			}

			return drawingVisual;
		}

		/// <summary>
		/// Updates the canvas.
		/// </summary>
		/// <param name="sender">The sender object.</param>
		/// <param name="e">The event arguments.</param>
		private void Update(object? sender, EventArgs e)
		{
			_tileManager.Update();

			DrawingVisual visual = Draw();

			_visuals.Clear();
			_visuals.Add(visual);
		}

		/// <summary>
		/// Gets the position of the tile based on the given position.
		/// </summary>
		/// <param name="position">The position.</param>
		/// <returns>The position of the tile.</returns>
		private Point GetTilePosition(Point position)
		{
			return new Point((int)position.X / _tileSize, _tileManager.TilesY - (int)position.Y / _tileSize - 1);
		}

		/// <inheritdoc/>
		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			_mouseDown = true;
			Point tile = GetTilePosition(e.GetPosition(this));
			_tileManager.HandleInput((int)tile.X, (int)tile.Y);
			base.OnMouseDown(e);
		}

		/// <inheritdoc/>
		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			_mouseDown = false;
			base.OnMouseUp(e);
		}

		/// <inheritdoc/>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (_mouseDown)
			{
				Point tile = GetTilePosition(e.GetPosition(this));
				_tileManager.HandleInput((int)tile.X, (int)tile.Y);
			}
			base.OnMouseMove(e);
		}

		/// <inheritdoc/>
		protected override void OnMouseLeave(MouseEventArgs e)
		{
			_mouseDown = false;
			base.OnMouseLeave(e);
		}

		/// <inheritdoc/>
		protected override int VisualChildrenCount => _visuals.Count;

		/// <inheritdoc/>
		protected override Visual GetVisualChild(int index)
		{
			return _visuals[index];
		}
	}
}
