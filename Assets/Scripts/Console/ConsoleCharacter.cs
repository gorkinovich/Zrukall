//******************************************************************************************
// Copyright (c) 2016 Gorka Suárez García
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
    /// This structure represents a character inside the console buffer.
    /// </summary>
    public struct ConsoleCharacter {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region char Value
        /// <summary>
        /// The main value of the buffer element data.
        /// </summary>
        private char _value;

        /// <summary>
        /// Gets or sets the main value of the buffer element data.
        /// </summary>
        public char Value {
            get { return _value; }
            set { _value = value; IsChanged = true; }
        }
        #endregion

        #region Color ForegroundColor
        /// <summary>
        /// The foreground color of the buffer element data.
        /// </summary>
        private Color _foregroundColor;

        /// <summary>
        /// Gets or sets the foreground color of the buffer element data.
        /// </summary>
        public Color ForegroundColor {
            get { return _foregroundColor; }
            set { _foregroundColor = value; IsChanged = true; }
        }
        #endregion

        #region Color BackgroundColor
        /// <summary>
        /// The background color of the buffer element data.
        /// </summary>
        private Color _backgroundColor;

        /// <summary>
        /// Gets or sets the background color of the buffer element data.
        /// </summary>
        public Color BackgroundColor {
            get { return _backgroundColor; }
            set { _backgroundColor = value; IsChanged = true; }
        }
        #endregion

        #region bool IsChanged
        /// <summary>
        /// Gets or sets if the data has been changed or not.
        /// </summary>
        public bool IsChanged { get; set; }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region void Set (char, Color, Color)
        /// <summary>
        /// Sets all the inner data of the buffer element data.
        /// </summary>
        /// <param name="value">The main value.</param>
        /// <param name="foreground">The foreground color</param>
        /// <param name="background">The background color.</param>
        public void Set (char value, Color foreground, Color background) {
            _value = value;
            _foregroundColor = foreground;
            _backgroundColor = background;
            IsChanged = true;
        }
        #endregion

        #region void Set (ref ConsoleCharacter)
        /// <summary>
        /// Sets all the inner data of the buffer element data.
        /// </summary>
        /// <param name="source">The source data.</param>
        public void Set (ref ConsoleCharacter source) {
            _value = source._value;
            _foregroundColor = source._foregroundColor;
            _backgroundColor = source._backgroundColor;
            IsChanged = true;
        }
        #endregion

        //********************************************************************************
        // Methods (Static)
        //********************************************************************************

        #region ConsoleCharacter Create ()
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <returns>A new object.</returns>
        public static ConsoleCharacter Create () {
            return new ConsoleCharacter {
                _value = '\0',
                _foregroundColor = Color.white,
                _backgroundColor = Color.black,
                IsChanged = true
            };
        }
        #endregion
    }
}
