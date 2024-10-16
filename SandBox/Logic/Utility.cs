using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SandBox.Logic
{
	public static class Utility
	{
		/// <summary>
		/// Calculates the color based on the given Kelvin temperature.
		/// </summary>
		/// <param name="kelvin">The Kelvin temperature.</param>
		/// <returns>The calculated color.</returns>
		public static Color CalculateColorFromKelvin(int kelvin)
		{
			// Omezíme teplotu na rozsah 1000K až 40000K
			kelvin = Math.Clamp(kelvin, 1000, 40000);

			// Převod teploty z Kelvinů na miredy (1e6 / Kelvin)
			double temp = kelvin / 100.0;

			// Počáteční hodnoty RGB
			double red, green, blue;

			// Výpočet červené složky
			if (temp <= 66)
			{
				red = 255;
			}
			else
			{
				red = temp - 60;
				red = 329.698727446 * Math.Pow(red, -0.1332047592);
				red = Math.Clamp(red, 0, 255);
			}

			// Výpočet zelené složky
			if (temp <= 66)
			{
				green = temp;
				green = 99.4708025861 * Math.Log(green) - 161.1195681661;
				green = Math.Clamp(green, 0, 255);
			}
			else
			{
				green = temp - 60;
				green = 288.1221695283 * Math.Pow(green, -0.0755148492);
				green = Math.Clamp(green, 0, 255);
			}

			// Výpočet modré složky
			if (temp >= 66)
			{
				blue = 255;
			}
			else if (temp <= 19)
			{
				blue = 0;
			}
			else
			{
				blue = temp - 10;
				blue = 138.5177312231 * Math.Log(blue) - 305.0447927307;
				blue = Math.Clamp(blue, 0, 255);
			}

			// Vytvoření výsledné barvy
			return Color.FromRgb((byte)red, (byte)green, (byte)blue);
		}

		/// <summary>
		/// Linearly interpolates between two colors based on the given factor.
		/// </summary>
		/// <param name="color1">The first color.</param>
		/// <param name="color2">The second color.</param>
		/// <param name="t">The interpolation factor.</param>
		/// <returns>The interpolated color.</returns>
		public static Color LerpColor(Color color1, Color color2, float t)
		{
			t = Math.Clamp(t, 0f, 1f);

			byte r = (byte)Math.Round(color1.R + (color2.R - color1.R) * t);
			byte g = (byte)Math.Round(color1.G + (color2.G - color1.G) * t);
			byte b = (byte)Math.Round(color1.B + (color2.B - color1.B) * t);
			byte a = (byte)Math.Round(color1.A + (color2.A - color1.A) * t);

			return Color.FromArgb(a, r, g, b);
		}
	}
}
