using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

namespace VisualGradientTesting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    class RGB
    {
        byte r;
        byte g;
        byte b;

        public RGB(byte red, byte green, byte blue)
        {
            r = red;
            g = green;
            b = blue;
        }
        public void Set(byte newR, byte newG, byte newB)
        {
            r = newR;
            g = newG;
            b = newB;
        }
        static string DecToHexa(int n)
        {

            // char array to store
            // hexadecimal number
            char[] hexaDeciNum = new char[2];

            // Counter for hexadecimal
            // number array
            int i = 0;
            while (n != 0)
            {

                // Temporary variable to
                // store remainder
                int temp = 0;

                // Storing remainder in
                // temp variable.
                temp = n % 16;

                // Check if temp < 10
                if (temp < 10)
                {
                    hexaDeciNum[i] = (char)(temp + 0x30);
                    i++;
                }
                else
                {
                    hexaDeciNum[i] = (char)(temp + 0x41 - 10);
                    i++;
                }
                n = n / 16;
            }
            string hexCode = "";

            if (i == 2)
            {
                hexCode += hexaDeciNum[1];
                hexCode += hexaDeciNum[0];
            }
            else if (i == 1)
            {
                hexCode = "0";
                hexCode += hexaDeciNum[0];
            }
            else if (i == 0)
                hexCode = "00";

            // Return the equivalent
            // hexadecimal color code
            return hexCode;
        }

        // Function to convert the
        // RGB code to Hex color code
        public static string ToHex(RGB rgb)
        {
            if ((rgb.r >= 0 && rgb.r <= 255) &&
                (rgb.g >= 0 && rgb.g <= 255) &&
                (rgb.b >= 0 && rgb.b <= 255))
            {
                string hexCode = "#";
                hexCode += DecToHexa(rgb.r);
                hexCode += DecToHexa(rgb.g);
                hexCode += DecToHexa(rgb.b);

                return hexCode;
            }

            // The hex color code doesn't exist
            else
                return "-1";
        }
        public static string[] ArrayToHex(RGB[] rgbs)
        { 
            string[] hexs = new string[rgbs.Length];
            for (int i = 0; i < rgbs.Length; i++)
            {
                hexs[i] = ToHex(rgbs[i]);
            }
            return hexs;
        }
        public static RGB[] ArraysToRGB(byte[] r, byte[] g, byte[] b)
        {
            RGB[] output = new RGB[r.Length];
            
            for (int i = 0; i < r.Length; i++)
            {
                output[i] = new RGB(Convert.ToByte(r[i]), Convert.ToByte(g[i]), Convert.ToByte(b[i]));
            }
            return output;
        }
        public int GetLuminance()
        {
            double luminance = (double)(r + g + b) / 3 / 255 * 100;
            return Convert.ToInt32(Math.Round(luminance));
        }
        public int GetMax()
        {
            return Math.Max(Math.Max(r, g), b);
        }
        public int GetMin()
        {
            return Math.Min(Math.Min(r, g), b);
        }
        public int GetHue()
        {
            double min = GetMin();
            double max = GetMax();

            if (min == max)
            {
                return 0;
            }

            double hue = 0;
            if (max == r)
            {
                hue = (g - b) / (max - min);
            }
            else if (max == g)
            {
                hue = 2 + (b - r) / (max - min);
            }
            else
            {
                hue = 4 + (r - g) / (max - min);
            }

            hue = hue * 60;
            if (hue < 0)
            {
                hue = hue + 360;
            }
            return Convert.ToInt32(Math.Round(hue));
        }
        public double GetSaturation()
        {
            double min = GetMin();
            double max = GetMax();
            double saturation;
            if (max == 0)
            {
                saturation = 0;
            }
            else
            {
                saturation = (max - min) / max;
            }
            return saturation;
        }
        public HSV ToHSV()
        {
            return new HSV(GetHue(), GetSaturation(), (double)GetMax() / 255);
        }
        public static byte[] GetRedsFromArray(RGB[] rGBs)
        {
            byte[] reds = new byte[rGBs.Length];
            for (int i = 0; i < rGBs.Length; i++)
            {
                reds[i] = rGBs[i].r;
            }
            return reds;
        }
        public static byte[] GetGreensFromArray(RGB[] rGBs)
        {
            byte[] greens = new byte[rGBs.Length];
            for (int i = 0; i < rGBs.Length; i++)
            {
                greens[i] = rGBs[i].g;
            }
            return greens;
        }
        public static byte[] GetBluesFromArray(RGB[] rGBs)
        {
            byte[] blues = new byte[rGBs.Length];
            for (int i = 0; i < rGBs.Length; i++)
            {
                blues[i] = rGBs[i].b;
            }
            return blues;
        }
        public HSL ToHSL()
        {
            return new HSL(GetHue(), GetSaturation(), (double)(GetMax() + GetMin()) / 2 / 255);
        }
    }
    class HSV
    {
        int h;
        double s;
        double v;
        public HSV(int hue, double saturation, double value)
        {
            h = hue;
            s = saturation;
            v = value;
        }
        public int GetHue()
        {
            return h;
        }
        public double GetSaturation()
        {
            return s;
        }
        public double GetValue()
        {
            return v;
        }
        public void SetHue(int input)
        {
            h = input;
        }
        public static HSV[] ArraysToHSV(int[] hues, double[] saturations, double[] values)
        {
            HSV[] output = new HSV[hues.Length];
            {
                for (int i = 0; i < hues.Length; i++)
                {
                    output[i] = new(hues[i], saturations[i], values[i]);
                }
            }
            return output;
        }
        public RGB ToRGB()
        {
            /*
            int hi = Convert.ToInt32(Math.Floor((double)h / 60)) % 6;
            double f = h / 60 - Math.Floor((double)h / 60);

            v = v * 255;
            int c = Convert.ToInt32(v);
            int p = Convert.ToInt32(v * (1 - s));
            int q = Convert.ToInt32(v * (1 - f * s));
            int t = Convert.ToInt32(v * (1 - (1 - f) * s));

            if (hi == 0)
                return new RGB(Convert.ToByte(c), Convert.ToByte(t), Convert.ToByte(p));
            else if (hi == 1)
                return new RGB(Convert.ToByte(q), Convert.ToByte(c), Convert.ToByte(p));
            else if (hi == 2)
                return new RGB(Convert.ToByte(p), Convert.ToByte(c), Convert.ToByte(t));
            else if (hi == 3)
                return new RGB(Convert.ToByte(p), Convert.ToByte(q), Convert.ToByte(c));
            else if (hi == 4)
                return new RGB(Convert.ToByte(t), Convert.ToByte(p), Convert.ToByte(c));
            else
                return new RGB(Convert.ToByte(c), Convert.ToByte(p), Convert.ToByte(q));
            */
            double c = v * s;
            double x = c * (1 - Math.Abs(((double)h / 60) % 2 - 1));
            double m = v - c;

            double tempR = 0;
            double tempG = 0;
            double tempB = 0;

            if (h < 60 && h >= 0)
            {
                tempR = c;
                tempG = x;
                tempB = 0;
            }
            else if (h < 120 && h >= 60)
            {
                tempR = x;
                tempG = c;
                tempB = 0;
            }
            else if (h < 180 && h >= 120)
            {
                tempR = 0;
                tempG = c;
                tempB = x;
            }
            else if (h < 240 && h >= 180)
            {
                tempR = 0;
                tempG = x;
                tempB = c;
            }
            else if (h < 300 && h >= 240)
            {
                tempR = x;
                tempG = 0;
                tempB = c;
            }
            else if (h < 360 && h >= 300)
            {
                tempR = c;
                tempG = 0;
                tempB = x;
            }

            return new(Convert.ToByte((tempR + m) * 255), Convert.ToByte((tempG + m) * 255), Convert.ToByte((tempB + m) * 255));
        }
        public static RGB[] ArrayToRGB(HSV[] hSVs)
        {
            RGB[] output = new RGB[hSVs.Length];
            for (int i = 0; i < hSVs.Length; i++)
            {
                output[i] = hSVs[i].ToRGB();
            }
            return output;
        }
    }

    class HSL
    {
        int h;
        double s;
        double l;

        public HSL(int hue, double saturation, double lightness)
        {
            h = hue;
            s = saturation;
            l = lightness;
        }

        public RGB ToRGB()
        {
            double c = (1 - Math.Abs(2 * l - 1)) * s;
            double x = c * (1 - Math.Abs(((double)h / 60) % 2 - 1));
            double m = l - c / 2;

            double tempR = 0;
            double tempG = 0;
            double tempB = 0;

            if (h < 60 && h >= 0)
            {
                tempR = c;
                tempG = x;
                tempB = 0;
            }
            else if (h < 120 && h >= 60)
            {
                tempR = x;
                tempG = c;
                tempB = 0;
            }
            else if (h < 180 && h >= 120)
            {
                tempR = 0;
                tempG = c;
                tempB = x;
            }
            else if (h < 240 && h >= 180)
            {
                tempR = 0;
                tempG = x;
                tempB = c;
            }
            else if (h < 300 && h >= 240)
            {
                tempR = x;
                tempG = 0;
                tempB = c;
            }
            else if (h < 360 && h >= 300)
            {
                tempR = c;
                tempG = 0;
                tempB = x;
            }

            return new(Convert.ToByte((tempR + m) * 255), Convert.ToByte((tempG + m) * 255), Convert.ToByte((tempB + m) * 255));
        }
        public int GetHue()
        {
            return h;
        }
        public double GetSaturation()
        {
            return s;
        }
        public double GetLightness()
        {
            return l;
        }
        public void SetHue(int input)
        {
            h = input;
        }
        public static RGB[] ArrayToRGB(HSL[] hSLs)
        {
            RGB[] output = new RGB[hSLs.Length];
            for (int i = 0; i < hSLs.Length; i++)
            {
                output[i] = hSLs[i].ToRGB();
            }
            return output;
        }
        public static HSL[] ArraysToHSL(int[] hues, double[] saturations, double[] lightnesses)
        {
            HSL[] output = new HSL[hues.Length];
            {
                for (int i = 0; i < hues.Length; i++)
                {
                    output[i] = new(hues[i], saturations[i], lightnesses[i]);
                }
            }
            return output;
        }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_okay_Click(object sender, RoutedEventArgs e)
        {
            byte startR = Convert.ToByte(txt_startR.Text);
            byte startG = Convert.ToByte(txt_startG.Text);
            byte startB = Convert.ToByte(txt_startB.Text);

            byte endR = Convert.ToByte(txt_endR.Text);
            byte endG = Convert.ToByte(txt_endG.Text);
            byte endB = Convert.ToByte(txt_endB.Text);

            int length = Convert.ToInt32(txt_length.Text);
            
            byte[] red = new byte[length];
            byte[] green = new byte[length];
            byte[] blue = new byte[length];

            RGB[] gradient = new RGB[length];

            if (rb_RGB.IsChecked == true)
            {
                byte[] reds = ArrayToByte(LinearInterpolate(startR, endR, length));
                byte[] greens = ArrayToByte(LinearInterpolate(startG, endG, length));
                byte[] blues = ArrayToByte(LinearInterpolate(startB, endB, length));

                gradient = RGB.ArraysToRGB(reds, greens, blues);
            }
            else if (rb_HSV.IsChecked == true)
            {
                HSV startHSV = new RGB(startR, startG, startB).ToHSV();
                HSV endHSV = new RGB(endR, endG, endB).ToHSV();

                
                if (startHSV.GetHue() - endHSV.GetHue() > 180)
                {
                     endHSV.SetHue(endHSV.GetHue() + 360);
                }
                else if (startHSV.GetHue() - endHSV.GetHue() < -180)
                {
                     startHSV.SetHue(startHSV.GetHue() + 360);
                }


                int[] hues = LinearInterpolate(startHSV.GetHue(), endHSV.GetHue(), length);
                double[] saturations = LinearInterpolate(startHSV.GetSaturation(), endHSV.GetSaturation(), length);
                double[] values = LinearInterpolate(startHSV.GetValue(), endHSV.GetValue(), length);

                for (int i = 0; i < hues.Length; i++)
                {
                    if (hues[i] >= 360)
                    {
                        hues[i] -= 360;
                    }
                }

                HSV[] hSVs = HSV.ArraysToHSV(hues, saturations, values);
                gradient = HSV.ArrayToRGB(hSVs);

            }
            else if (rb_HSL.IsChecked == true)
            {
                HSL startHSL = new RGB(startR, startG, startB).ToHSL();
                HSL endHSL = new RGB(endR, endG, endB).ToHSL();


                if (startHSL.GetHue() - endHSL.GetHue() > 180)
                {
                    endHSL.SetHue(endHSL.GetHue() + 360);
                }
                else if (startHSL.GetHue() - endHSL.GetHue() < -180)
                {
                    startHSL.SetHue(startHSL.GetHue() + 360);
                }


                int[] hues = LinearInterpolate(startHSL.GetHue(), endHSL.GetHue(), length);
                double[] saturations = LinearInterpolate(startHSL.GetSaturation(), endHSL.GetSaturation(), length);
                double[] lightnesses = LinearInterpolate(startHSL.GetLightness(), endHSL.GetLightness(), length);

                for (int i = 0; i < hues.Length; i++)
                {
                    if (hues[i] >= 360)
                    {
                        hues[i] -= 360;
                    }
                }

                HSL[] hSLs = HSL.ArraysToHSL(hues, saturations, lightnesses);
                gradient = HSL.ArrayToRGB(hSLs);
            }

            red = RGB.GetRedsFromArray(gradient);
            green = RGB.GetGreensFromArray(gradient);
            blue = RGB.GetBluesFromArray(gradient);

            stack_Gradient.Children.RemoveRange(0, stack_Gradient.Children.Count);

            for (int i = 0; i < gradient.Length; i++)
            {
                Label dynamicLabel = new();
                dynamicLabel.Name = "lbl_color" + i;
                dynamicLabel.Width = 150;
                dynamicLabel.Height = 25;
                dynamicLabel.Content = RGB.ToHex(gradient[i]);
                dynamicLabel.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(red[i], green[i], blue[i])); // after luminance reaches 5/6, it turns white, after luminance reaches 1/6, it turns black
                
                if (gradient[i].GetHue() < 240 && gradient[i].GetHue() > 60)
                {
                    dynamicLabel.Foreground = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    dynamicLabel.Foreground = new SolidColorBrush(Colors.White);
                }

                if (gradient[i].GetLuminance() > 0.5)
                {
                    dynamicLabel.Foreground = new SolidColorBrush(Colors.Black);
                }
                stack_Gradient.Children.Add(dynamicLabel);
            }


                //RGB start = new RGB(Convert.ToByte(txt_startR), Convert.ToByte(txt_startG), Convert.ToByte(txt_startB));
                //RGB end = new RGB(Convert.ToByte(txt_endR), Convert.ToByte(txt_endG), Convert.ToByte(txt_endB));
        }
        static double[] LinearInterpolate(double start, double end, int length)
        {
            int steps = length - 1;
            double slope = (double)(end - start) / steps;
            double exactValue = start;
            double[] gradient = new double[length];

            for (int i = 0; i < steps; i++)
            {
                gradient[i] = exactValue;
                exactValue += slope;
            }
            gradient[steps] = exactValue;
            return gradient;
        }
        static int[] LinearInterpolate(int start, int end, int length)
        {
            int steps = length - 1;
            double slope = (double)(end - start) / steps;
            double exactValue = start;
            int[] gradient = new int[length];

            for (int i = 0; i < steps; i++)
            {
                gradient[i] = Convert.ToInt32(Math.Round(exactValue));
                exactValue += slope;
            }
            gradient[steps] = Convert.ToInt32(Math.Round(exactValue));
            return gradient;
        }
        public static string StringConstructor(string[] input)
        {
            string output = "";     
            
            for (int i = 0; i < input.Length; i++)
            {
                output = output +  input[i] + "\n";
            }
            return output;
        }
        static byte[] ArrayToByte(int[] input)
        {
            byte[] output = new byte[input.Length];
            for (int i = 0;i < output.Length;i++)
            {
                output[i] = Convert.ToByte(input[i]);
            }
            return output;
        }
    }
}
