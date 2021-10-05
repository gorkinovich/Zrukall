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

namespace Console {
    /// <summary>
    /// This class represents a string to be writen inside the console buffer.
    /// </summary>
    public class ConsoleString {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region string TextMessage
        /// <summary>
        /// The text message of the string.
        /// </summary>
        public string TextMessage { get; set; } 
        #endregion

        #region Color ForegroundColor
        /// <summary>
        /// The foreground color of the string.
        /// </summary>
        public Color ForegroundColor { get; set; } 
        #endregion

        #region Color BackgroundColor
        /// <summary>
        /// The background color of the string.
        /// </summary>
        public Color BackgroundColor { get; set; } 
        #endregion

        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region ConsoleString ()
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        public ConsoleString () {
            TextMessage = "";
            ForegroundColor = Color.white;
            BackgroundColor = Color.black;
        }
        #endregion

        #region ConsoleString ()
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        public ConsoleString (string message, Color foreground, Color background) {
            TextMessage = message;
            ForegroundColor = foreground;
            BackgroundColor = background;
        }
        #endregion

    }
}
