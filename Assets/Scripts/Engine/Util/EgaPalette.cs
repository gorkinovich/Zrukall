//******************************************************************************************
// Copyright (c) 2021 Gorka Suárez García
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
//******************************************************************************************
using UnityEngine;

/*
 * Default EGA 16-color palette
 * (set up to match the standard CGA colors)
 * Color                                   	rgbRGB 	Decimal
 * 0  – Black                    (#000000) 	000000 	0
 * 1  – Blue                     (#0000AA) 	000001 	1
 * 2  – Green                    (#00AA00) 	000010 	2
 * 3  – Cyan                     (#00AAAA) 	000011 	3
 * 4  – Red                      (#AA0000) 	000100 	4
 * 5  – Magenta                  (#AA00AA) 	000101 	5
 * 6  – Brown                    (#AA5500) 	010100 	20
 * 7  – White / Light gray       (#AAAAAA) 	000111 	7
 * 8  – Dark gray / Bright black (#555555) 	111000 	56
 * 9  – Bright blue              (#5555FF) 	111001 	57
 * 10 – Bright green             (#55FF55) 	111010 	58
 * 11 – Bright cyan              (#55FFFF) 	111011 	59
 * 12 – Bright red               (#FF5555) 	111100 	60
 * 13 – Bright magenta           (#FF55FF) 	111101 	61
 * 14 – Bright yellow            (#FFFF55) 	111110 	62
 * 15 – Bright white             (#FFFFFF) 	111111 	63
 */

namespace Engine.Util {
    /// <summary>
    /// This class represents a collection of colors.
    /// </summary>
    public static class EgaPalette {
        //********************************************************************************
        // Colors
        //********************************************************************************

        public static readonly Color Black = new Color(0x00 / 255.0f, 0x00 / 255.0f, 0x00 / 255.0f);
        public static readonly Color Blue = new Color(0x00 / 255.0f, 0x00 / 255.0f, 0xAA / 255.0f);
        public static readonly Color Green = new Color(0x00 / 255.0f, 0xAA / 255.0f, 0x00 / 255.0f);
        public static readonly Color Cyan = new Color(0x00 / 255.0f, 0xAA / 255.0f, 0xAA / 255.0f);
        public static readonly Color Red = new Color(0xAA / 255.0f, 0x00 / 255.0f, 0x00 / 255.0f);
        public static readonly Color Magenta = new Color(0xAA / 255.0f, 0x00 / 255.0f, 0xAA / 255.0f);
        public static readonly Color Brown = new Color(0xAA / 255.0f, 0x55 / 255.0f, 0x00 / 255.0f);
        public static readonly Color LightGray = new Color(0xAA / 255.0f, 0xAA / 255.0f, 0xAA / 255.0f);
        public static readonly Color DarkGray = new Color(0x55 / 255.0f, 0x55 / 255.0f, 0x55 / 255.0f);
        public static readonly Color BrightBlue = new Color(0x55 / 255.0f, 0x55 / 255.0f, 0xFF / 255.0f);
        public static readonly Color BrightGreen = new Color(0x55 / 255.0f, 0xFF / 255.0f, 0x55 / 255.0f);
        public static readonly Color BrightCyan = new Color(0x55 / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);
        public static readonly Color BrightRed = new Color(0xFF / 255.0f, 0x55 / 255.0f, 0x55 / 255.0f);
        public static readonly Color BrightMagenta = new Color(0xFF / 255.0f, 0x55 / 255.0f, 0xFF / 255.0f);
        public static readonly Color BrightYellow = new Color(0xFF / 255.0f, 0xFF / 255.0f, 0x55 / 255.0f);
        public static readonly Color White = new Color(0xFF / 255.0f, 0xFF / 255.0f, 0xFF / 255.0f);

        //********************************************************************************
        // Indexes
        //********************************************************************************

        public const int BlackIndex = 0;
        public const int BlueIndex = 1;
        public const int GreenIndex = 2;
        public const int CyanIndex = 3;
        public const int RedIndex = 4;
        public const int MagentaIndex = 5;
        public const int BrownIndex = 6;
        public const int LightGrayIndex = 7;
        public const int DarkGrayIndex = 8;
        public const int BrightBlueIndex = 9;
        public const int BrightGreenIndex = 10;
        public const int BrightCyanIndex = 11;
        public const int BrightRedIndex = 12;
        public const int BrightMagentaIndex = 13;
        public const int BrightYellowIndex = 14;
        public const int WhiteIndex = 15;

        //********************************************************************************
        // Array
        //********************************************************************************

        public const int NumberOfColors = 16;

        public static readonly Color[] Colors = new Color[NumberOfColors] {
            Black, Blue, Green, Cyan,
            Red, Magenta, Brown, LightGray,
            DarkGray, BrightBlue, BrightGreen, BrightCyan,
            BrightRed, BrightMagenta, BrightYellow, White
        };

        //********************************************************************************
        // Methods
        //********************************************************************************

        public static Color GetColorByIndex(int index) {
            return GetColorByIndex(index, WhiteIndex);
        }

        public static Color GetColorByIndex (int index, int defaultIndex) {
            if (0 <= index && index < NumberOfColors) {
                return Colors[index];
            } else {
                return Colors[defaultIndex];
            }
        }
    }
}
