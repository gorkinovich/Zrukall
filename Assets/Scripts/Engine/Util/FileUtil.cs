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

namespace Engine.Util {
    /// <summary>
    /// This class represents a collection of file utility methods.
    /// </summary>
    public static class FileUtil {
        //********************************************************************************
        // Methods
        //********************************************************************************

        #region string LoadTextFromResources (string)
        /// <summary>
        /// Loads a text file from the resources folder.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <returns>The loaded text if success; otherwise an empty string.</returns>
        public static string LoadTextFromResources (string assetName) {
            var asset = Resources.Load<TextAsset>(assetName);
            return asset != null ? asset.text : "";
        }
        #endregion
    }
}
