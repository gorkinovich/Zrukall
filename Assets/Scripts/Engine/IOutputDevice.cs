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
using Console;
using UnityEngine;

namespace Engine {
    /// <summary>
    /// This interface represents an output device.
    /// </summary>
    public interface IOutputDevice {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region int CursorColumn
        /// <summary>
        /// Gets or sets the column of the cursor used by the console.
        /// </summary>
        int CursorColumn { get; set; }
        #endregion

        #region int CursorRow
        /// <summary>
        /// Gets or sets the row of the cursor used by the console.
        /// </summary>
        int CursorRow { get; set; }
        #endregion

        #region Color ForegroundColor
        /// <summary>
        /// Gets or sets the foreground color used by the console.
        /// </summary>
        Color ForegroundColor { get; set; }
        #endregion

        #region Color BackgroundColor
        /// <summary>
        /// Gets or sets the background color used by the console.
        /// </summary>
        Color BackgroundColor { get; set; }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region void Clear ()
        /// <summary>
        /// Clears the console.
        /// </summary>
        void Clear ();
        #endregion

        #region void Write (char)
        /// <summary>
        /// Writes a character in the console.
        /// </summary>
        /// <param name="item">The character to write.</param>
        void Write (char item);
        #endregion

        #region void Write (string)
        /// <summary>
        /// Writes a message in the console.
        /// </summary>
        /// <param name="message">The message to write.</param>
        void Write (string message);
        #endregion

        #region void WriteLine (string)
        /// <summary>
        /// Writes a line in the console.
        /// </summary>
        /// <param name="message">The message to write.</param>
        void WriteLine (string message);
        #endregion

        #region void WriteLine ()
        /// <summary>
        /// Writes a line in the console.
        /// </summary>
        void WriteLine ();
        #endregion

        #region void Write (ConsoleString)
        /// <summary>
        /// Writes a message in the console.
        /// </summary>
        /// <param name="message">The message to write.</param>
        void Write (ConsoleString message);
        #endregion

        #region void WriteLine (ConsoleString)
        /// <summary>
        /// Writes a line in the console.
        /// </summary>
        /// <param name="message">The message to write.</param>
        void WriteLine (ConsoleString message);
        #endregion

        #region void GotoXY (int, int)
        /// <summary>
        /// Sets the cursor location in the console.
        /// </summary>
        /// <param name="column">The column of the cursor.</param>
        /// <param name="row">The row of the cursor.</param>
        void GotoXY (int column, int row);
        #endregion
    }
}
