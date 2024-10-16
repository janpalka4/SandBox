using SandBox.Logic;
using SandBox.Logic.Affectors;
using SandBox.Logic.Rules;
using System.Windows;
using System.Windows.Controls;

namespace SandBox.Controls
{
	/// <summary>
	/// Interaction logic for DrawCanvasControl.xaml
	/// </summary>
	public partial class DrawCanvasControl : UserControl
	{
		public TileManager TileManager { get; protected set; }
		public DrawCanvas DrawCanvas { get; protected set; }
		public DrawSandAffector DrawSandAffector { get; protected set; }

		public DrawCanvasControl()
		{
			InitializeComponent();

			TileManager = new TileManager(40, 40);
			DrawSandAffector = new DrawSandAffector();

			TileManager.Rules.Add(new GravityRule());
			TileManager.Rules.Add(new SlideRule());
			TileManager.Rules.Add(new ThermalRule());

			TileManager.CurrentAffector = DrawSandAffector;

			DrawCanvas = new DrawCanvas(TileManager, 16);
			DrawCanvas.Width = 640;
			DrawCanvas.Height = 640;

			panel.Children.Add (DrawCanvas);
		}

		private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if(DrawSandAffector != null)
				DrawSandAffector.SelectedTemperature = (int)e.NewValue;
		}
	}
}
