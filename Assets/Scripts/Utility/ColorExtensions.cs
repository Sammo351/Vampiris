using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public static class ColorExtensions
{
    static float preferredSaturation = 0.5f;
    static float preferredBrightness = 0.9f;
    public static Color RandomColor(bool randomizeAlpha = false)
    {
        float r = UnityEngine.Random.Range(0, 1f);
        float g = UnityEngine.Random.Range(0, 1f);
        float b = UnityEngine.Random.Range(0, 1f);
        float a = randomizeAlpha ? UnityEngine.Random.Range(0, 1f) : 1;
        return new Color(r, g, b, a);
    }
    public static Color Pastel(this Color col)
    {
        //high brightness & low saturation
        float[] hsv = col.HSV();
        Color newCol = Color.HSVToRGB(hsv[0], preferredSaturation, preferredBrightness);
        return newCol;
    }
    public static Color ShiftHue(this Color color, float amount)
    {
        float[] hsv = color.HSV();
        float newH = Mathf.Repeat(hsv[0] + amount, 1f);
        return Color.HSVToRGB(newH, hsv[1], hsv[2]);
    }
    public static float[] HSV(this Color col)
    {
        float h, s, v;
        Color.RGBToHSV(col, out h, out s, out v);
        return new float[] { h, s, v };
    }
    public static Color IncreaseBrightness(this Color color, int steps = 1)
    {
        float[] hsv = color.HSV();
        float newV = Mathf.Clamp01(hsv[2] + (0.1f * steps));
        Color newCol = Color.HSVToRGB(hsv[0], hsv[1], newV);
        return newCol;
    }
    public static Color DecreaseBrightness(this Color color, int steps = 1)
    {
        float[] hsv = color.HSV();
        float newV = Mathf.Clamp01(hsv[2] - (0.1f * steps));
        Color newCol = Color.HSVToRGB(hsv[0], hsv[1], newV);
        return newCol;
    }
    public static Color SetBrightness(this Color color, float value)
    {
        float[] hsv = color.HSV();

        float newV = Mathf.Clamp01(value);
        Color newCol = Color.HSVToRGB(hsv[0], hsv[1], newV);
        return newCol;
    }
    public static Color IncreaseSaturation(this Color color, int steps = 1)
    {
        float[] hsv = color.HSV();
        float newS = Mathf.Clamp01(hsv[1] + (0.1f * steps));
        Color newCol = Color.HSVToRGB(hsv[0], newS, hsv[2]);
        return newCol;
    }
    public static Color DecreaseSaturation(this Color color, int steps = 1)
    {
        float[] hsv = color.HSV();
        float newS = Mathf.Clamp01(hsv[1] - (0.1f * steps));
        Color newCol = Color.HSVToRGB(hsv[0], newS, hsv[2]);
        return newCol;
    }

    public static Color RandomBrightness(this Color color)
    {
        float[] hsv = color.HSV();

        float vChange = Random.Range(-0.5f, 0.5f);
        float newV = Mathf.Clamp(hsv[2] + vChange, 0.2f, 0.9f);
        Color newCol = Color.HSVToRGB(hsv[0], hsv[1], newV);
        return newCol;
    }
    public static Color RandomSaturation(this Color color)
    {
        float[] hsv = color.HSV();

        float sChange = Random.Range(-0.5f, 0.5f);
        float newS = Mathf.Clamp(hsv[1] + sChange, 0.2f, 0.9f);
        Color newCol = Color.HSVToRGB(hsv[0], newS, hsv[2]);
        return newCol;
    }
    public static Color SetSaturation(this Color color, float value)
    {
        float[] hsv = color.HSV();

        float newS = Mathf.Clamp01(value);
        Color newCol = Color.HSVToRGB(hsv[0], newS, hsv[2]);
        return newCol;
    }
    public static Color Complementary(this Color color)
    {
        return color.ShiftHue(0.5f);
        // float h, s, v;
        // Color.RGBToHSV(color, out h, out s, out v);
        // h = Mathf.Repeat(h + 0.5f, 1);
        // return Color.HSVToRGB(h, s, v);
    }
    public static List<Color> Split(this Color col, int num = 2)
    {
        List<Color> cols = new List<Color>();

        float fraction = 1 / (float)num;
        float[] hsv = col.HSV();
        float h = hsv[0];
        for (int i = 0; i < num; i++)
        {
            float newH = Mathf.Repeat(i * fraction + h, 1);
            //Color newCol = Color.HSVToRGB(newH, hsv[1], hsv[2]);
            Color newCol = col.ShiftHue(i * fraction);
            cols.Add(newCol);
        }
        return cols;
    }
    public static List<Color> Triadic(this Color col)
    {
        return col.Split(3);
    }
    public static List<Color> Tetradic(this Color col)
    {
        return col.Split(4);
    }
    public static List<Color> Analogous(this Color col, int num = 3)
    {
        int randomIndex = Random.Range(0, 2) * 2 - 1; //-1 or 1
        bool removeOne = num % 2 == 0;
        List<Color> cols = new List<Color>();
        int half = num / 2;
        int resolution = Mathf.Max(num * 3, 12);
        float fraction = 1 / (float)resolution;
        float[] hsv = col.HSV();
        float h = hsv[0];
        int start = -half + (randomIndex == -1 && removeOne ? 1 : 0);
        int end = half - (randomIndex == 1 && removeOne ? 1 : 0);
        for (int i = start; i <= end; i++)
        {
            float incr = i * fraction;
            float newH = Mathf.Repeat(incr + h, 1);
            Color newCol = Color.HSVToRGB(newH, hsv[1], hsv[2]);
            cols.Add(newCol);
        }
        return cols;
    }
    public static List<Color> Gradient(this Color col, Color end, int num = 5)
    {
        List<Color> cols = new List<Color>();

        float[] hsv = col.HSV();
        float h = hsv[0];
        float s = hsv[1];
        float incr = s / (float)num;
        for (int i = 0; i < num; i++)
        {

            float percentage = i / (float)num;
            Color newCol = Color.Lerp(col, end, percentage);
            cols.Add(newCol);
        }
        return cols;
    }
    public static List<Color> GradientMinimal(this Color col)
    {
        List<Color> cols = new List<Color>();
        Color end = col.Analogous().Last();
        int num = 3;
        float[] hsv = col.HSV();
        float h = hsv[0];
        float s = hsv[1];
        float incr = s / (float)num;
        for (int i = 0; i < num; i++)
        {

            float percentage = i / (float)num;
            Color newCol = Color.Lerp(col, end, percentage);
            cols.Add(newCol);
        }
        return cols;
    }
    public static List<Color> Shades(this Color col, int num = 5)
    {
        List<Color> cols = new List<Color>();
        int half = Mathf.CeilToInt(num * 0.5f);
        cols.AddRange(col.Gradient(Color.white, half));
        cols.Reverse();
        cols.AddRange(col.Gradient(Color.black, half).GetRange(1, half - 1));

        /*  int halfWay = num / 2;
         float[] hsv = col.HSV();
         float h = hsv[0];
         float s = hsv[1];
         float incr = s / (float)num;
         for (int i = 0; i < num; i++)
         {
             float t = i / (float)halfWay;

             float newS = Mathf.Lerp(0.01f, preferredSaturation, t);
             float newV = Mathf.Lerp(preferredBrightness, 0, Mathf.Clamp(t, 0f, preferredBrightness));
             Color newCol = Color.HSVToRGB(h, newS, newV);
             cols.Add(newCol);
         } */
        return cols;
    }
}
