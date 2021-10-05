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
using Engine;
using System;
using UnityEngine;

namespace Console {
    /// <summary>
    /// This class represents the console device behaviour.
    /// </summary>
    public class ConsoleBehaviour : MonoBehaviour, IOutputDevice, IInputDevice {
        //********************************************************************************
        // Constants
        //********************************************************************************

        #region int DEFAULT_FONT_WIDTH
        /// <summary>
        /// The default font width used in the console.
        /// </summary>
        private const int DEFAULT_FONT_WIDTH = 8;
        #endregion

        #region int DEFAULT_FONT_HEIGHT
        /// <summary>
        /// The default font height used in the console.
        /// </summary>
        private const int DEFAULT_FONT_HEIGHT = 12;
        #endregion

        #region int DEFAULT_FONT_WIDTH_SEPARATION
        /// <summary>
        /// The default font width separation used in the console.
        /// </summary>
        private const int DEFAULT_FONT_WIDTH_SEPARATION = 0;
        #endregion

        #region int DEFAULT_FONT_HEIGHT_SEPARATION
        /// <summary>
        /// The default font height separation used in the console.
        /// </summary>
        private const int DEFAULT_FONT_HEIGHT_SEPARATION = 0;
        #endregion

        #region int MAX_CHARS
        /// <summary>
        /// The maximum number of characters used in the console.
        /// </summary>
        private const int MAX_CHARS = 256;
        #endregion

        #region int INDEX_MASK
        /// <summary>
        /// The index mask to transform a character into an index.
        /// </summary>
        private const int INDEX_MASK = 0xFF;
        #endregion

        #region Vector2 SPRITE_PIVOT
        /// <summary>
        /// The pivot point in the sprites used in the console.
        /// </summary>
        private static readonly Vector2 SPRITE_PIVOT = new Vector2(0.5f, 0.5f);
        #endregion

        #region string TAB_STRING
        /// <summary>
        /// The tabulator string used in the console.
        /// </summary>
        private const string TAB_STRING = "    ";
        #endregion

        //********************************************************************************
        // Type
        //********************************************************************************

        #region struct BufferData
        /// <summary>
        /// This structure represents a cell inside the console buffer.
        /// </summary>
        private struct BufferData {
            public ConsoleCharacter character;
            public GameObject foreground;
            public GameObject background;
        }
        #endregion

        //********************************************************************************
        // Properties
        //********************************************************************************

        #region Texture2D _fontTexture
        /// <summary>
        /// The texture of the font used by the console.
        /// </summary>
        [SerializeField]
        private Texture2D _fontTexture = null;
        #endregion

        #region Vector2 _fontSize
        /// <summary>
        /// The size of the font used by the console.
        /// </summary>
        [SerializeField]
        private Vector2 _fontSize = new Vector2(DEFAULT_FONT_WIDTH, DEFAULT_FONT_HEIGHT);
        #endregion

        #region Vector2 _fontSeparation
        /// <summary>
        /// The separation of the font used by the console.
        /// </summary>
        [SerializeField]
        private Vector2 _fontSeparation = new Vector2(DEFAULT_FONT_WIDTH_SEPARATION, DEFAULT_FONT_HEIGHT_SEPARATION);
        #endregion

        #region Sprite[] _fontSprites
        /// <summary>
        /// The sprites of the font used by the console.
        /// </summary>
        private Sprite[] _fontSprites = null;
        #endregion

        #region Texture2D _backgroundTexture
        /// <summary>
        /// The background texture used by the console.
        /// </summary>
        private Texture2D _backgroundTexture = null;
        #endregion

        #region Sprite _backgroundSprite
        /// <summary>
        /// The background sprite used by the console.
        /// </summary>
        private Sprite _backgroundSprite = null;
        #endregion

        #region BufferData[] _bufferData
        /// <summary>
        /// The buffer data used by the console.
        /// </summary>
        private BufferData[] _bufferData = null;
        #endregion

        #region int _columns
        /// <summary>
        /// The columns in the buffer data used by the console.
        /// </summary>
        private int _columns = 0;
        #endregion

        #region int _rows
        /// <summary>
        /// The rows in the buffer data used by the console.
        /// </summary>
        private int _rows = 0;
        #endregion

        #region int _virtualWidth
        /// <summary>
        /// The width used by the console to work with.
        /// </summary>
        private int _virtualWidth = 0;
        #endregion

        #region int _virtualHeight
        /// <summary>
        /// The height used by the console to work with.
        /// </summary>
        private int _virtualHeight = 0;
        #endregion

        //********************************************************************************
        // Properties (Output)
        //********************************************************************************

        #region int CursorColumn
        /// <summary>
        /// The column of the cursor used by the console.
        /// </summary>
        private int _cursorColumn;

        /// <summary>
        /// Gets or sets the column of the cursor used by the console.
        /// </summary>
        public int CursorColumn {
            get { return _cursorColumn; }
            set { _cursorColumn = Mathf.Clamp(value, 0, _columns - 1); }
        }
        #endregion

        #region int CursorRow
        /// <summary>
        /// The row of the cursor used by the console.
        /// </summary>
        private int _cursorRow;

        /// <summary>
        /// Gets or sets the row of the cursor used by the console.
        /// </summary>
        public int CursorRow {
            get { return _cursorRow; }
            set { _cursorRow = Mathf.Clamp(value, 0, _rows - 1); }
        }
        #endregion

        #region Color ForegroundColor
        /// <summary>
        /// Gets or sets the foreground color used by the console.
        /// </summary>
        public Color ForegroundColor { get; set; }
        #endregion

        #region Color BackgroundColor
        /// <summary>
        /// Gets or sets the background color used by the console.
        /// </summary>
        public Color BackgroundColor { get; set; }
        #endregion

        //********************************************************************************
        // Properties (Input)
        //********************************************************************************

        #region string CurrentText
        /// <summary>
        /// The current text used by the input.
        /// </summary>
        private string _currentText = "";

        /// <summary>
        /// Gets the current text used by the input.
        /// </summary>
        public string CurrentText {
            get { return _currentText; }
        }
        #endregion

        #region Action<string> OnNextLine
        /// <summary>
        /// Gets or sets the on next line event handler.
        /// </summary>
        public Action<string> OnNextLine { get; set; }
        #endregion

        #region bool EnableTextInput
        /// <summary>
        /// Gets or sets the enable flag used by the input.
        /// </summary>
        public bool EnableTextInput { get; set; }
        #endregion

        //********************************************************************************
        // Methods (Awake)
        //********************************************************************************

        #region void Awake ()
        /// <summary>
        /// This method is used for initialization.
        /// </summary>
        public void Awake () {
            makeFontSprites();
            makeBackground();
            configureCamera();
            makeScreenBuffer();
            configureOutput();
            configureInput();
        }
        #endregion

        #region void makeFontSprites ()
        /// <summary>
        /// This method creates the sprites of the font used in the console.
        /// </summary>
        private void makeFontSprites () {
            _fontSprites = new Sprite[MAX_CHARS];
            var pos = new Vector2(_fontSeparation.x, (_fontTexture.height - (_fontSize.y + _fontSeparation.y)));
            for (int i = 0; i < MAX_CHARS; i++) {
                var rect = new Rect(pos, _fontSize);
                _fontSprites[i] = Sprite.Create(_fontTexture, rect, SPRITE_PIVOT, 1.0f);
                pos.x += (_fontSize.x + _fontSeparation.x);
                if (pos.x >= _fontTexture.width) {
                    pos.x = _fontSeparation.x;
                    pos.y -= (_fontSize.y + _fontSeparation.y);
                }
            }
        }
        #endregion

        #region void makeBackground ()
        /// <summary>
        /// This method creates the background sprite used in the console.
        /// </summary>
        private void makeBackground () {
            var w = (int) _fontSize.x;
            var h = (int) _fontSize.y;
            var size = w * h;
            var backgroundData = new Color[size];
            for (int i = 0; i < size; i++) {
                backgroundData[i] = Color.white;
            }

            _backgroundTexture = new Texture2D(w, h, TextureFormat.RGBA32, false, false);
            _backgroundTexture.SetPixels(backgroundData);
            _backgroundTexture.Apply();

            var rect = new Rect(Vector2.zero, _fontSize);
            _backgroundSprite = Sprite.Create(_backgroundTexture, rect, SPRITE_PIVOT, 1.0f);
        }
        #endregion

        #region void configureCamera ()
        /// <summary>
        /// This method initializes the camera used in the console.
        /// </summary>
        private void configureCamera () {
            var camera = GetComponent<Camera>();
            if (camera != null) {
                const float DEFAULT_SIZE = 180.0f;
                camera.orthographic = true;
                camera.orthographicSize = DEFAULT_SIZE;
                _virtualHeight = (int) (DEFAULT_SIZE * 2.0f);
                _virtualWidth = (Screen.width * _virtualHeight) / Screen.height;
                _rows = _virtualHeight / (int) _fontSize.y;
                _columns = _virtualWidth / (int) _fontSize.x;
                print("Real screen: " + Screen.width + "x" + Screen.height);
                print("Virtual screen: " + _virtualWidth + "x" + _virtualHeight);
                print("Buffer size: " + _columns + "x" + _rows);
            }
        }
        #endregion

        #region void makeScreenBuffer ()
        /// <summary>
        /// This method creates the screen buffer used in the console.
        /// </summary>
        private void makeScreenBuffer () {
            var startX = -((_columns - 1) * _fontSize.x * 0.5f);
            var startY = ((_rows - 1) * _fontSize.y * 0.5f);
            var fgpos = new Vector3(startX, startY, 0.0f);
            var bgpos = new Vector3(fgpos.x, fgpos.y, -1.0f);
            var delta = new Vector3(_fontSize.x, _fontSize.y, 0.0f);
            var length = _columns * _rows;

            _bufferData = new BufferData[length];
            ConsoleCharacter cc = ConsoleCharacter.Create();
            Func<string, Vector3, string, GameObject> createGameObject =
                (name, position, layerName) => {
                    var go = new GameObject(name, typeof(SpriteRenderer));
                    go.transform.localPosition = position;
                    var sr = go.GetComponent<SpriteRenderer>();
                    sr.sortingLayerName = layerName;
                    return go;
                };
            for (int i = 0, k = 0; i < _rows; i++) {
                for (int j = 0; j < _columns; j++, k++) {
                    _bufferData[k] = new BufferData {
                        character = cc,
                        foreground = createGameObject("fgcell" + k, fgpos, "Foreground"),
                        background = createGameObject("bgcell" + k, bgpos, "Background"),
                    };
                    cellUpdate(ref _bufferData[k]);
                    fgpos.x += delta.x;
                    bgpos.x += delta.x;
                }
                fgpos.x = startX;
                bgpos.x = startX;
                fgpos.y -= delta.y;
                bgpos.y -= delta.y;
            }
        }
        #endregion

        #region void configureOutput ()
        /// <summary>
        /// This method initializes the data used in the output.
        /// </summary>
        private void configureOutput () {
            _cursorRow = 0;
            _cursorColumn = 0;
            ForegroundColor = Color.white;
            BackgroundColor = Color.black;
        }
        #endregion

        #region void configureInput ()
        /// <summary>
        /// This method initializes the data used in the input.
        /// </summary>
        private void configureInput () {
            ResetInput();
        }
        #endregion

        //********************************************************************************
        // Methods (Update)
        //********************************************************************************

        #region void Update ()
        /// <summary>
        /// This method is called once per frame, to update the component.
        /// </summary>
        public void Update () {
            drawUpdate();
            inputUpdate();
        }
        #endregion

        #region void drawUpdate ()
        /// <summary>
        /// This method updates the screen buffer to draw it on the screen.
        /// </summary>
        private void drawUpdate () {
            for (int i = 0; i < _bufferData.Length; i++) {
                drawUpdate(ref _bufferData[i]);
            }
        }
        #endregion

        #region void drawUpdate (ref BufferData)
        /// <summary>
        /// This method updates a cell to draw it on the screen.
        /// </summary>
        /// <param name="victim">The cell to update.</param>
        private void drawUpdate (ref BufferData victim) {
            if (victim.character.IsChanged) {
                var fgsr = victim.foreground.GetComponent<SpriteRenderer>();
                if (fgsr != null) {
                    fgsr.sprite = _fontSprites[victim.character.Value & INDEX_MASK];
                    fgsr.color = victim.character.ForegroundColor;
                }
                var bgsr = victim.background.GetComponent<SpriteRenderer>();
                if (bgsr != null) {
                    bgsr.color = victim.character.BackgroundColor;
                }
                victim.character.IsChanged = false;
            }
        }
        #endregion

        #region void cellUpdate (ref BufferData)
        /// <summary>
        /// This method updates a cell of the screen buffer.
        /// </summary>
        /// <param name="victim">The cell to update.</param>
        private void cellUpdate (ref BufferData victim) {
            if (victim.character.IsChanged) {
                var fgsr = victim.foreground.GetComponent<SpriteRenderer>();
                if (fgsr != null) {
                    fgsr.sortingOrder = 1;
                    fgsr.sprite = _fontSprites[victim.character.Value & INDEX_MASK];
                    fgsr.color = victim.character.ForegroundColor;
                }
                var bgsr = victim.background.GetComponent<SpriteRenderer>();
                if (bgsr != null) {
                    bgsr.sortingOrder = 0;
                    bgsr.sprite = _backgroundSprite;
                    bgsr.color = victim.character.BackgroundColor;
                }
                victim.character.IsChanged = false;
            }
        }
        #endregion

        //********************************************************************************
        // Methods (Output)
        //********************************************************************************

        #region void Clear ()
        /// <summary>
        /// This method clears the console.
        /// </summary>
        public void Clear () {
            CursorRow = 0;
            CursorColumn = 0;
            for (int i = 0; i < _bufferData.Length; i++) {
                setCharacter(i, ' ');
            }
        }
        #endregion

        #region void Write (char)
        /// <summary>
        /// This method writes a character in the console.
        /// </summary>
        /// <param name="item">The character to write.</param>
        public void Write (char item) {
            switch (item) {
                case '\t': Write(TAB_STRING);   break;
                case '\n': addNewLine();        break;
                case '\b': addBackspace();      break;
                case '\r': addCarriageReturn(); break;
                default:   addCharacter(item);  break;
            }
        }
        #endregion

        #region void Write (string)
        /// <summary>
        /// This method writes a message in the console.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public void Write (string message) {
            foreach (var item in message) {
                Write(item);
            }
        }
        #endregion

        #region void WriteLine (string)
        /// <summary>
        /// This method writes a line in the console.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public void WriteLine (string message) {
            Write(message);
            Write('\n');
        }
        #endregion

        #region void WriteLine ()
        /// <summary>
        /// This method writes a line in the console.
        /// </summary>
        public void WriteLine () {
            Write('\n');
        }
        #endregion

        #region void Write (ConsoleString)
        /// <summary>
        /// This method writes a message in the console.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public void Write (ConsoleString message) {
            BackgroundColor = message.BackgroundColor;
            ForegroundColor = message.ForegroundColor;
            Write(message.TextMessage);
        }
        #endregion

        #region void WriteLine (ConsoleString)
        /// <summary>
        /// This method writes a line in the console.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public void WriteLine (ConsoleString message) {
            Write(message);
            Write('\n');
        }
        #endregion

        #region void GotoXY (int, int)
        /// <summary>
        /// This method sets the cursor location in the console.
        /// </summary>
        /// <param name="column">The column of the cursor.</param>
        /// <param name="row">The row of the cursor.</param>
        public void GotoXY (int column, int row) {
            CursorColumn = column;
            CursorRow = row;
        }
        #endregion

        //********************************************************************************
        // Methods (Input)
        //********************************************************************************

        #region void ResetInput ()
        /// <summary>
        /// This method resets the input data.
        /// </summary>
        public void ResetInput () {
            _currentText = "";
            EnableTextInput = false;
        }
        #endregion

        #region void inputUpdate ()
        /// <summary>
        /// This method updates the input data.
        /// </summary>
        private void inputUpdate () {
            if (EnableTextInput && Input.anyKeyDown) {
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    for (int i = 0; i < _currentText.Length; i++) {
                        deleteLastCharacter();
                    }
                    _currentText = "";
                } else if (Input.GetKeyDown(KeyCode.Tab)) {
                    Write(TAB_STRING);
                    _currentText += TAB_STRING;
                }
                foreach (var item in Input.inputString) {
                    if (item == '\b') {
                        if (_currentText.Length > 0) {
                            deleteLastCharacter();
                            _currentText = _currentText.Substring(0, _currentText.Length - 1);
                        }
                    } else if (item == '\n' || item == '\r') {
                        var currentLine = _currentText;
                        _currentText = "";
                        addNewLine();
                        EnableTextInput = false;
                        if (OnNextLine != null) {
                            OnNextLine(currentLine);
                        }
                    } else {
                        Write(item);
                        _currentText += item;
                    }
                }
            }
        }
        #endregion

        //********************************************************************************
        // Methods (Buffer)
        //********************************************************************************

        #region int coordsToIndex (int, int)
        /// <summary>
        /// This method converts a (row,column) coordinates into an index.
        /// </summary>
        /// <param name="row">The row of the coordinate.</param>
        /// <param name="col">The column of the coordinate.</param>
        /// <returns>The final index.</returns>
        private int coordsToIndex (int row, int col) {
            var idx = row * _columns + col;
            return Mathf.Clamp(idx, 0, _bufferData.Length - 1);
        }
        #endregion

        #region copyCharacter (int, int, int, int)
        /// <summary>
        /// This method copies a character from one cell to another.
        /// </summary>
        /// <param name="dr">The destination row.</param>
        /// <param name="dc">The destination column.</param>
        /// <param name="or">The origin row.</param>
        /// <param name="oc">The origin column.</param>
        private void copyCharacter (int dr, int dc, int or, int oc) {
            var dest = coordsToIndex(dr, dc);
            var orig = coordsToIndex(or, oc);
            _bufferData[dest].character = _bufferData[orig].character;
            _bufferData[dest].character.IsChanged = true;
        }
        #endregion

        #region void setCharacter (int, char)
        /// <summary>
        /// This method sets a character inside the buffer.
        /// </summary>
        /// <param name="index">The position in the buffer.</param>
        /// <param name="item">The character to set.</param>
        private void setCharacter (int index, char item) {
            _bufferData[index].character.Set(item, ForegroundColor, BackgroundColor);
        }
        #endregion

        #region void setCharacter (char)
        /// <summary>
        /// This method sets a character inside the buffer.
        /// </summary>
        /// <param name="item">The character to set.</param>
        private void setCharacter (char item) {
            setCharacter(coordsToIndex(CursorRow, CursorColumn), item);
        }
        #endregion

        #region void deleteLastCharacter ()
        /// <summary>
        /// This method deletes the last character inside the buffer.
        /// </summary>
        private void deleteLastCharacter () {
            addBackspace();
            setCharacter(coordsToIndex(CursorRow, CursorColumn), ' ');
        }
        #endregion

        #region void addNewLine ()
        /// <summary>
        /// This method adds a new line in the console.
        /// </summary>
        private void addNewLine () {
            CursorColumn = 0;
            var nextRow = CursorRow + 1;
            if (nextRow >= _rows) {
                for (int i = 0; i < CursorRow; i++) {
                    for (int j = 0; j < _columns; j++) {
                        copyCharacter(i, j, i + 1, j);
                    }
                }
                for (int j = 0; j < _columns; j++) {
                    setCharacter(coordsToIndex(_rows - 1, j), ' ');
                }
            } else {
                CursorRow = nextRow;
            }
        }
        #endregion

        #region void addBackspace ()
        /// <summary>
        /// This method adds a backspace in the console.
        /// </summary>
        private void addBackspace () {
            if (CursorColumn > 0) {
                CursorColumn--;
            } else if (CursorRow > 0) {
                CursorColumn = _columns - 1;
                CursorRow--;
            }
        }
        #endregion

        #region void addCarriageReturn ()
        /// <summary>
        /// This method adds a carriage return in the console.
        /// </summary>
        private void addCarriageReturn () {
            CursorColumn = 0;
        }
        #endregion

        #region void addCharacter (char)
        /// <summary>
        /// This method adds a character in the console.
        /// </summary>
        /// <param name="item">The character to add.</param>
        private void addCharacter (char item) {
            setCharacter(coordsToIndex(CursorRow, CursorColumn), item);
            var nextColumn = CursorColumn + 1;
            if (nextColumn < _columns) {
                CursorColumn = nextColumn;
            } else {
                addNewLine();
            }
        }
        #endregion
    }
}
