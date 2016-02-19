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
using Engine.Data;
using Engine.Script;
using Engine.Util;
using UnityEngine;

namespace Engine {
    /// <summary>
    /// This class represents the adventure engine behaviour.
    /// </summary>
    public class EngineBehaviour : MonoBehaviour {
        //********************************************************************************
        // Constants
        //********************************************************************************

        #region float MAX_FINISH_PAUSE
        /// <summary>
        /// The maximum finish time interval.
        /// </summary>
        public const float MAX_FINISH_PAUSE = 0.5f;
        #endregion

        //********************************************************************************
        // Properties
        //********************************************************************************

        #region EngineBehaviour Instance
        /// <summary>
        /// The main instance of the class.
        /// </summary>
        private static EngineBehaviour _instance;

        /// <summary>
        /// Gets the main instance of the class.
        /// </summary>
        public static EngineBehaviour Instance {
            get { return _instance; }
        }
        #endregion

        #region IOutputDevice Output
        /// <summary>
        /// The output device of the engine.
        /// </summary>
        private IOutputDevice _output = null;

        /// <summary>
        /// Gets the output device of the engine.
        /// </summary>
        public IOutputDevice Output {
            get { return _output; }
        }
        #endregion

        #region IInputDevice Input
        /// <summary>
        /// The input device of the engine.
        /// </summary>
        private IInputDevice _input = null;

        /// <summary>
        /// Gets the input device of the engine.
        /// </summary>
        public IInputDevice Input {
            get { return _input; }
        }
        #endregion

        #region GameObject _device
        /// <summary>
        /// The game object device of the game.
        /// </summary>
        [SerializeField]
        private GameObject _device = null;
        #endregion

        #region string _startScript
        /// <summary>
        /// The start script file path of the game.
        /// </summary>
        [SerializeField]
        private string _startScript = null;
        #endregion

        #region AdventureGame Game
        /// <summary>
        /// The adventure descriptor of the game.
        /// </summary>
        private AdventureGame _game;

        /// <summary>
        /// Gets the adventure descriptor of the game.
        /// </summary>
        public AdventureGame Game {
            get { return _game; }
        }
        #endregion

        #region AdventureContext Context
        /// <summary>
        /// The current context of the game.
        /// </summary>
        private AdventureContext _context = null;

        /// <summary>
        /// Gets the current context of the game.
        /// </summary>
        public AdventureContext Context {
            get { return _context; }
        }
        #endregion

        #region Color _previousColor
        /// <summary>
        /// The previous color used before the line input.
        /// </summary>
        private Color _previousColor;
        #endregion

        #region float _finishPause
        /// <summary>
        /// The finish pause delta time counter.
        /// </summary>
        private float _finishPause;
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region void Awake ()
        /// <summary>
        /// Handles the "awake" event and is used for initialization.
        /// </summary>
        public void Awake () {
            _instance = this;
        }
        #endregion

        #region void Start ()
        /// <summary>
        /// Handles the "start" event and is used for initialization.
        /// </summary>
        public void Start () {
            _output = _device.GetComponent<IOutputDevice>();
            _input = _device.GetComponent<IInputDevice>();
            var text = FileUtil.LoadTextFromResources(_startScript);
            _game = ScriptParser.Parse(text);
            Reset();
        }
        #endregion

        #region void Update ()
        /// <summary>
        /// Handles the "update" event and is called once per frame,
        /// to update the component.
        /// </summary>
        public void Update () {
            if (_context.UpdateChangeRoom()) {
                StartReadLine();
            }
            if (_context.Reset) {
                Reset();
            } else if (_context.Finished) {
                if (_finishPause > MAX_FINISH_PAUSE) {
                    if (UnityEngine.Input.anyKeyDown) {
                        Reset();
                    }
                } else {
                    _finishPause += Time.deltaTime;
                }
            }
        }
        #endregion

        #region void Reset ()
        /// <summary>
        /// Resets the current game.
        /// </summary>
        public void Reset () {
            _finishPause = 0.0f;
            _context = new AdventureContext();
            if (_game != null) {
                _output.BackgroundColor = EgaPalette.Black;
                _output.ForegroundColor = EgaPalette.White;
                _output.Clear();
                _output.WriteLine("Zrukall Text Adventure Engine 0.1\n");
                _context.ChangeRoom(AdventureContext.DEFAULT_ROOM);
                _input.OnNextLine = EndReadLine;
                StartReadLine();
            }
        }
        #endregion

        #region void StartReadLine ()
        /// <summary>
        /// Starts the read line process.
        /// </summary>
        public void StartReadLine () {
            if (!_context.Finished && _context.NextRoom == null) {
                _output.WriteLine();
                _previousColor = _output.ForegroundColor;
                _output.ForegroundColor = EgaPalette.Red;
                _output.Write("> ");
                _output.ForegroundColor = EgaPalette.White;
                _input.EnableTextInput = true;
            }
        }
        #endregion

        #region void EndReadLine (string)
        /// <summary>
        /// Ends the read line process.
        /// </summary>
        /// <param name="line">The readed line.</param>
        public void EndReadLine (string line) {
            _output.ForegroundColor = _previousColor;
            _output.WriteLine();
            _context.ExecuteAction(line);
            StartReadLine();
        }
        #endregion
    }
}
