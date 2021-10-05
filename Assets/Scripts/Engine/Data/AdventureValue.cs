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
using Engine.Script;
using System;
using System.Text;

namespace Engine.Data {
    //|==================================================================================|
    //|                                 IAdventureValue                                  |
    //|==================================================================================|

    #region interface IAdventureValue
    /// <summary>
    /// This interface represents a value inside the game adventure.
    /// </summary>
    public interface IAdventureValue {
        //********************************************************************************
        // Methods
        //********************************************************************************

        #region int GetInteger ()
        /// <summary>
        /// Gets the value as an integer.
        /// </summary>
        /// <returns>The value as an integer.</returns>
        int GetInteger ();
        #endregion

        #region void SetInteger (int)
        /// <summary>
        /// Sets the value from an integer.
        /// </summary>
        /// <param name="value">The value to set.</param>
        void SetInteger (int value);
        #endregion

        #region float GetFloat ()
        /// <summary>
        /// Gets the value as a float.
        /// </summary>
        /// <returns>The value as a float.</returns>
        float GetFloat ();
        #endregion

        #region void SetFloat (float)
        /// <summary>
        /// Sets the value from a float.
        /// </summary>
        /// <param name="value">The value to set.</param>
        void SetFloat (float value);
        #endregion

        #region string GetString ()
        /// <summary>
        /// Gets the value as a string.
        /// </summary>
        /// <returns>The value as a string.</returns>
        string GetString ();
        #endregion

        #region void SetString (string)
        /// <summary>
        /// Sets the value from a string.
        /// </summary>
        /// <param name="value">The value to set.</param>
        void SetString (string value);
        #endregion

        #region int GetBoolean ()
        /// <summary>
        /// Gets the value as a boolean.
        /// </summary>
        /// <returns>The value as a boolean.</returns>
        bool GetBoolean ();
        #endregion

        #region void SetBoolean (int)
        /// <summary>
        /// Sets the value from an boolean.
        /// </summary>
        /// <param name="value">The value to set.</param>
        void SetBoolean (bool value);
        #endregion
    }
    #endregion

    //|==================================================================================|
    //|                                  AdventureValue                                  |
    //|==================================================================================|

    #region class AdventureValue
    /// <summary>
    /// This static class represents a factory of value objects inside the game adventure.
    /// </summary>
    public static class AdventureValue {
        //********************************************************************************
        // Methods
        //********************************************************************************

        #region IAdventureValue Create (string)
        /// <summary>
        /// Creates a new value object inside the game adventure.
        /// </summary>
        /// <param name="value">The value to set.</param>
        /// <returns>The new value object or null.</returns>
        public static IAdventureValue Create (string value) {
            if (ScriptSymbol.IsIntegerNumber(value)) {
                return new AdventureIntegerValue(value);
            } else if (ScriptSymbol.IsFloatNumber(value)) {
                return new AdventureFloatValue(value);
            } else if (ScriptSymbol.IsBoolean(value)) {
                return new AdventureBooleanValue(value);
            } else {
                return new AdventureStringValue(value);
            }
        }
        #endregion

        #region string Normalize (string)
        /// <summary>
        /// Normalizes a string literal from a script file.
        /// </summary>
        /// <param name="value">The raw string literal.</param>
        /// <returns>A string value.</returns>
        public static string Normalize (string value) {
            StringBuilder buffer = new StringBuilder(value);
            for (int i = 0; i < buffer.Length; i++) {
                if (buffer[i] == '\\') {
                    buffer.Remove(i, 1);
                    var next = buffer[i];
                    if (next == 'n') {
                        buffer[i] = '\n';
                    } else if (next == 't') {
                        buffer[i] = '\t';
                    } else if (next == 'r') {
                        buffer[i] = '\r';
                    } else if (next == 'b') {
                        buffer[i] = '\b';
                    } else if (next == '"') {
                        buffer[i] = '"';
                    } else if (next == '\'') {
                        buffer[i] = '\'';
                    } else if (next == '\\') {
                        buffer[i] = '\\';
                    } else if (next == '0') {
                        buffer[i] = '\0';
                    } else {
                        throw new Exception("Invalid raw string value: " + value);
                    }
                }
            }
            buffer.Remove(0, 1);
            buffer.Remove(buffer.Length - 1, 1);
            return buffer.ToString();
        }
        #endregion
    }
    #endregion

    //|==================================================================================|
    //|                              AdventureIntegerValue                               |
    //|==================================================================================|

    #region class AdventureIntegerValue
    /// <summary>
    /// This class represents an integer value inside the game adventure.
    /// </summary>
    public class AdventureIntegerValue : IAdventureValue {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region int _data
        /// <summary>
        /// The integer value.
        /// </summary>
        private int _data;
        #endregion

        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureIntegerValue ()
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        public AdventureIntegerValue () {
            _data = 0;
        }
        #endregion

        #region AdventureIntegerValue (int)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public AdventureIntegerValue (int value) {
            SetInteger(value);
        }
        #endregion

        #region AdventureIntegerValue (float)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public AdventureIntegerValue (float value) {
            SetFloat(value);
        }
        #endregion

        #region AdventureIntegerValue (int)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public AdventureIntegerValue (string value) {
            SetString(value);
        }
        #endregion

        #region AdventureIntegerValue (bool)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public AdventureIntegerValue (bool value) {
            SetBoolean(value);
        }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region int GetInteger ()
        /// <summary>
        /// Gets the value as an integer.
        /// </summary>
        /// <returns>The value as an integer.</returns>
        public int GetInteger () {
            return _data;
        }
        #endregion

        #region void SetInteger (int)
        /// <summary>
        /// Sets the value from an integer.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetInteger (int value) {
            _data = value;
        }
        #endregion

        #region float GetFloat ()
        /// <summary>
        /// Gets the value as a float.
        /// </summary>
        /// <returns>The value as a float.</returns>
        public float GetFloat () {
            return _data;
        }
        #endregion

        #region void SetFloat (float)
        /// <summary>
        /// Sets the value from a float.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetFloat (float value) {
            _data = (int) value;
        }
        #endregion

        #region string GetString ()
        /// <summary>
        /// Gets the value as a string.
        /// </summary>
        /// <returns>The value as a string.</returns>
        public string GetString () {
            return _data.ToString();
        }
        #endregion

        #region void SetString (string)
        /// <summary>
        /// Sets the value from a string.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetString (string value) {
            if (!int.TryParse(value, out _data)) {
                _data = 0;
            }
        }
        #endregion

        #region bool GetBoolean ()
        /// <summary>
        /// Gets the value as a boolean.
        /// </summary>
        /// <returns>The value as a boolean.</returns>
        public bool GetBoolean () {
            return _data != 0;
        }
        #endregion

        #region void SetBoolean (int)
        /// <summary>
        /// Sets the value from an boolean.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetBoolean (bool value) {
            _data = value ? 1 : 0;
        }
        #endregion
    }
    #endregion

    //|==================================================================================|
    //|                               AdventureFloatValue                                |
    //|==================================================================================|

    #region class AdventureFloatValue
    /// <summary>
    /// This class represents a float value inside the game adventure.
    /// </summary>
    public class AdventureFloatValue : IAdventureValue {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region float _data
        /// <summary>
        /// The float value.
        /// </summary>
        private float _data;
        #endregion

        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureFloatValue ()
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        public AdventureFloatValue () {
            _data = 0.0f;
        }
        #endregion

        #region AdventureFloatValue (int)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public AdventureFloatValue (int value) {
            SetInteger(value);
        }
        #endregion

        #region AdventureFloatValue (float)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public AdventureFloatValue (float value) {
            SetFloat(value);
        }
        #endregion

        #region AdventureFloatValue (int)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public AdventureFloatValue (string value) {
            SetString(value);
        }
        #endregion

        #region AdventureFloatValue (bool)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public AdventureFloatValue (bool value) {
            SetBoolean(value);
        }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region int GetInteger ()
        /// <summary>
        /// Gets the value as an integer.
        /// </summary>
        /// <returns>The value as an integer.</returns>
        public int GetInteger () {
            return (int) _data;
        }
        #endregion

        #region void SetInteger (int)
        /// <summary>
        /// Sets the value from an integer.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetInteger (int value) {
            _data = value;
        }
        #endregion

        #region float GetFloat ()
        /// <summary>
        /// Gets the value as a float.
        /// </summary>
        /// <returns>The value as a float.</returns>
        public float GetFloat () {
            return _data;
        }
        #endregion

        #region void SetFloat (float)
        /// <summary>
        /// Sets the value from a float.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetFloat (float value) {
            _data = value;
        }
        #endregion

        #region string GetString ()
        /// <summary>
        /// Gets the value as a string.
        /// </summary>
        /// <returns>The value as a string.</returns>
        public string GetString () {
            return _data.ToString();
        }
        #endregion

        #region void SetString (string)
        /// <summary>
        /// Sets the value from a string.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetString (string value) {
            if (!float.TryParse(value, out _data)) {
                _data = 0.0f;
            }
        }
        #endregion

        #region int GetBoolean ()
        /// <summary>
        /// Gets the value as a boolean.
        /// </summary>
        /// <returns>The value as a boolean.</returns>
        public bool GetBoolean () {
            return _data != 0.0f;
        }
        #endregion

        #region void SetBoolean (int)
        /// <summary>
        /// Sets the value from an boolean.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetBoolean (bool value) {
            _data = value ? 1.0f : 0.0f;
        }
        #endregion
    }
    #endregion

    //|==================================================================================|
    //|                               AdventureStringValue                               |
    //|==================================================================================|

    #region class AdventureStringValue
    /// <summary>
    /// This class represents a string value inside the game adventure.
    /// </summary>
    public class AdventureStringValue : IAdventureValue {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region string _data
        /// <summary>
        /// The string value.
        /// </summary>
        private string _data;
        #endregion

        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureStringValue ()
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        public AdventureStringValue () {
            _data = "";
        }
        #endregion

        #region AdventureStringValue (int)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public AdventureStringValue (int value) {
            SetInteger(value);
        }
        #endregion

        #region AdventureStringValue (float)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public AdventureStringValue (float value) {
            SetFloat(value);
        }
        #endregion

        #region AdventureStringValue (int)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public AdventureStringValue (string value) {
            SetString(value);
        }
        #endregion

        #region AdventureStringValue (bool)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public AdventureStringValue (bool value) {
            SetBoolean(value);
        }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region int GetInteger ()
        /// <summary>
        /// Gets the value as an integer.
        /// </summary>
        /// <returns>The value as an integer.</returns>
        public int GetInteger () {
            int result;
            if (!int.TryParse(_data, out result)) {
                return 0;
            }
            return result;
        }
        #endregion

        #region void SetInteger (int)
        /// <summary>
        /// Sets the value from an integer.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetInteger (int value) {
            _data = value.ToString();
        }
        #endregion

        #region float GetFloat ()
        /// <summary>
        /// Gets the value as a float.
        /// </summary>
        /// <returns>The value as a float.</returns>
        public float GetFloat () {
            float result;
            if (!float.TryParse(_data, out result)) {
                return 0.0f;
            }
            return result;
        }
        #endregion

        #region void SetFloat (float)
        /// <summary>
        /// Sets the value from a float.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetFloat (float value) {
            _data = value.ToString();
        }
        #endregion

        #region string GetString ()
        /// <summary>
        /// Gets the value as a string.
        /// </summary>
        /// <returns>The value as a string.</returns>
        public string GetString () {
            return _data;
        }
        #endregion

        #region void SetString (string)
        /// <summary>
        /// Sets the value from a string.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetString (string value) {
            _data = value;
        }
        #endregion

        #region int GetBoolean ()
        /// <summary>
        /// Gets the value as a boolean.
        /// </summary>
        /// <returns>The value as a boolean.</returns>
        public bool GetBoolean () {
            return _data.ToLower() != ScriptKeywords.FALSE;
        }
        #endregion

        #region void SetBoolean (int)
        /// <summary>
        /// Sets the value from an boolean.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetBoolean (bool value) {
            _data = value.ToString();
        }
        #endregion
    }
    #endregion

    //|==================================================================================|
    //|                              AdventureBooleanValue                               |
    //|==================================================================================|

    #region class AdventureBooleanValue
    /// <summary>
    /// This class represents a boolean value inside the game adventure.
    /// </summary>
    public class AdventureBooleanValue : IAdventureValue {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region bool _data
        /// <summary>
        /// The boolean value.
        /// </summary>
        private bool _data;
        #endregion

        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureBooleanValue ()
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        public AdventureBooleanValue () {
            _data = true;
        }
        #endregion

        #region AdventureBooleanValue (int)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public AdventureBooleanValue (int value) {
            SetInteger(value);
        }
        #endregion

        #region AdventureBooleanValue (float)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public AdventureBooleanValue (float value) {
            SetFloat(value);
        }
        #endregion

        #region AdventureBooleanValue (int)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public AdventureBooleanValue (string value) {
            SetString(value);
        }
        #endregion

        #region AdventureBooleanValue (bool)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public AdventureBooleanValue (bool value) {
            SetBoolean(value);
        }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region int GetInteger ()
        /// <summary>
        /// Gets the value as an integer.
        /// </summary>
        /// <returns>The value as an integer.</returns>
        public int GetInteger () {
            return _data ? 1 : 0;
        }
        #endregion

        #region void SetInteger (int)
        /// <summary>
        /// Sets the value from an integer.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetInteger (int value) {
            _data = (value != 0);
        }
        #endregion

        #region float GetFloat ()
        /// <summary>
        /// Gets the value as a float.
        /// </summary>
        /// <returns>The value as a float.</returns>
        public float GetFloat () {
            return _data ? 1.0f : 0.0f;
        }
        #endregion

        #region void SetFloat (float)
        /// <summary>
        /// Sets the value from a float.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetFloat (float value) {
            _data = (value != 0.0f);
        }
        #endregion

        #region string GetString ()
        /// <summary>
        /// Gets the value as a string.
        /// </summary>
        /// <returns>The value as a string.</returns>
        public string GetString () {
            return _data.ToString();
        }
        #endregion

        #region void SetString (string)
        /// <summary>
        /// Sets the value from a string.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetString (string value) {
            _data = (value.ToLower() != ScriptKeywords.FALSE);
        }
        #endregion

        #region int GetBoolean ()
        /// <summary>
        /// Gets the value as a boolean.
        /// </summary>
        /// <returns>The value as a boolean.</returns>
        public bool GetBoolean () {
            return _data;
        }
        #endregion

        #region void SetBoolean (int)
        /// <summary>
        /// Sets the value from an boolean.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetBoolean (bool value) {
            _data = value;
        }
        #endregion
    }
    #endregion
}
