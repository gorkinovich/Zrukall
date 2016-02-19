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
using System;

namespace Engine {
    /// <summary>
    /// This interface represents an input device.
    /// </summary>
    public interface IInputDevice {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region string CurrentText
        /// <summary>
        /// Gets the current text used by the input.
        /// </summary>
        string CurrentText { get; }
        #endregion

        #region Action<string> OnNextLine
        /// <summary>
        /// Gets or sets the on next line event handler.
        /// </summary>
        Action<string> OnNextLine { get; set; }
        #endregion

        #region bool EnableTextInput
        /// <summary>
        /// Gets or sets the enable flag used by the input.
        /// </summary>
        bool EnableTextInput { get; set; }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region void ResetInput ()
        /// <summary>
        /// Resets the input data.
        /// </summary>
        void ResetInput ();
        #endregion
    }
}
