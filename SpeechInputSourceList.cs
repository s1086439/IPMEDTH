// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_WSA || UNITY_STANDALONE_WIN
using UnityEngine.Windows.Speech;
#endif

/*	De klasse SpeechInputSource aangepast.
	Deze werkt nu met lists i.p.v. arrrays om de te herkennen woorden dynamisch te kunnen uitbreiden.
*/

namespace HoloToolkit.Unity.InputModule
{
    /// <summary>
    /// SpeechInputSource allows you to specify keywords and methods in the Unity
    /// Inspector, instead of registering them explicitly in code.
    /// This also includes a setting to either automatically start the
    /// keyword recognizer or allow your code to start it.
    ///
    /// IMPORTANT: Please make sure to add the microphone capability in your app, in Unity under
    /// Edit -> Project Settings -> Player -> Settings for Windows Store -> Publishing Settings -> Capabilities
    /// or in your Visual Studio Package.appxmanifest capabilities.
    /// </summary>
    public partial class SpeechInputSourceList : BaseInputSource
    {
        /// <summary>
        /// Keywords are persistent across all scenes.  This Speech Input Source instance will not be destroyed when loading a new scene.
        /// </summary>
        [Tooltip("Keywords are persistent across all scenes.  This Speech Input Source instance will not be destroyed when loading a new scene.")]
        public bool PersistentKeywords;

        // This enumeration gives the manager two different ways to handle the recognizer. Both will
        // set up the recognizer and add all keywords. The first causes the recognizer to start
        // immediately. The second allows the recognizer to be manually started at a later time.
        public enum RecognizerStartBehavior { AutoStart, ManualStart }

        [Tooltip("Whether the recognizer should be activated on start.")]
        public RecognizerStartBehavior RecognizerStart;

        [Tooltip("The keywords to be recognized and optional keyboard shortcuts.")]
        public List<KeywordAndKeyCode> Keywords;

#if UNITY_WSA || UNITY_STANDALONE_WIN

		[Tooltip("The confidence level for the keyword recognizer.")]
        // The serialized data of this field will be lost when switching between platforms and re-serializing this class.
        [SerializeField]
        private ConfidenceLevel recognitionConfidenceLevel = ConfidenceLevel.Medium;

        private KeywordRecognizer keywordRecognizer;

		[Tooltip("A list for GameObject types with one or more childs. The child names could then be used for speech recognition.")]
		[SerializeField]
		private List<GameObject> additionalWordsWrapper;

		#region Unity Methods

		protected virtual void Start()
        {
            if (PersistentKeywords)
            {
                gameObject.DontDestroyOnLoad();
            }

			if (additionalWordsWrapper != null)
			{
				foreach (GameObject g in additionalWordsWrapper)
				{
					foreach (Transform child in g.transform)
					{
						KeywordAndKeyCode k;
						k.Keyword = child.name;
						k.KeyCode = KeyCode.None;
						Keywords.Add(k);
					}
				}
			}

			int keywordCount = Keywords.Count;

			if (keywordCount > 0 || additionalWordsWrapper != null)
            {
                var keywords = new string[keywordCount];

                for (int index = 0; index < keywordCount; index++)
                {
                    keywords[index] = Keywords[index].Keyword;
					Debug.Log(keywords[index]);
				}

				keywordRecognizer = new KeywordRecognizer(keywords, recognitionConfidenceLevel);
                keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;

                if (RecognizerStart == RecognizerStartBehavior.AutoStart)
                {
                    keywordRecognizer.Start();
                }
            }

            else
            {
                Debug.LogError("Must have at least one keyword specified in the Inspector on " + gameObject.name + ".");
            }
        }

        protected virtual void Update()
        {
            if (keywordRecognizer != null && keywordRecognizer.IsRunning)
            {
                ProcessKeyBindings();
            }
        }

        protected virtual void OnDestroy()
        {
            if (keywordRecognizer != null)
            {
                StopKeywordRecognizer();
                keywordRecognizer.OnPhraseRecognized -= KeywordRecognizer_OnPhraseRecognized;
                keywordRecognizer.Dispose();
            }
        }

        protected virtual void OnDisable()
        {
            if (keywordRecognizer != null)
            {
                StopKeywordRecognizer();
            }
        }

        protected virtual void OnEnable()
        {
            if (keywordRecognizer != null && RecognizerStart == RecognizerStartBehavior.AutoStart)
            {
                StartKeywordRecognizer();
            }
        }

        #endregion // Unity Methods

        #region Event Callbacks

        private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
        {
            OnPhraseRecognized(args.confidence, args.phraseDuration, args.phraseStartTime, args.semanticMeanings, args.text);
        }

        #endregion // Event Callbacks

        /// <summary>
        /// Make sure the keyword recognizer is off, then start it.
        /// Otherwise, leave it alone because it's already in the desired state.
        /// </summary>
        public void StartKeywordRecognizer()
        {
            if (keywordRecognizer != null && !keywordRecognizer.IsRunning)
            {
				keywordRecognizer.Start();
            }
        }

        private void ProcessKeyBindings()
        {
            for (int index = Keywords.Count; --index >= 0;)
            {
                if (Input.GetKeyDown(Keywords[index].KeyCode))
                {
                    OnPhraseRecognized(recognitionConfidenceLevel, TimeSpan.Zero, DateTime.Now, null, Keywords[index].Keyword);
                }
            }
        }

        /// <summary>
        /// Make sure the keyword recognizer is on, then stop it.
        /// Otherwise, leave it alone because it's already in the desired state.
        /// </summary>
        public void StopKeywordRecognizer()
        {
            if (keywordRecognizer != null && keywordRecognizer.IsRunning)
            {
                keywordRecognizer.Stop();
            }
        }

        protected void OnPhraseRecognized(ConfidenceLevel confidence, TimeSpan phraseDuration, DateTime phraseStartTime, SemanticMeaning[] semanticMeanings, string text)
        {
            InputManager.Instance.RaiseSpeechKeywordPhraseRecognized(this, 0, confidence, phraseDuration, phraseStartTime, semanticMeanings, text);
        }

#endif

        #region Base Input Source Methods

        public override bool TryGetSourceKind(uint sourceId, out InteractionSourceInfo sourceKind)
        {
            sourceKind = InteractionSourceInfo.Voice;
            return true;
        }

        public override bool TryGetPointerPosition(uint sourceId, out Vector3 position)
        {
            position = Vector3.zero;
            return false;
        }

        public override bool TryGetPointerRotation(uint sourceId, out Quaternion rotation)
        {
            rotation = Quaternion.identity;
            return false;
        }

        public override bool TryGetPointingRay(uint sourceId, out Ray pointingRay)
        {
            pointingRay = default(Ray);
            return false;
        }

        public override bool TryGetGripPosition(uint sourceId, out Vector3 position)
        {
            position = Vector3.zero;
            return false;
        }

        public override bool TryGetGripRotation(uint sourceId, out Quaternion rotation)
        {
            rotation = Quaternion.identity;
            return false;
        }

        public override SupportedInputInfo GetSupportedInputInfo(uint sourceId)
        {
            return SupportedInputInfo.None;
        }

        public override bool TryGetThumbstick(uint sourceId, out bool isPressed, out Vector2 position)
        {
            isPressed = false;
            position = Vector2.zero;
            return false;
        }

        public override bool TryGetTouchpad(uint sourceId, out bool isPressed, out bool isTouched, out Vector2 position)
        {
            isPressed = false;
            isTouched = false;
            position = Vector2.zero;
            return false;
        }

        public override bool TryGetSelect(uint sourceId, out bool isPressed, out double pressedAmount)
        {
            isPressed = false;
            pressedAmount = 0.0;
            return false;
        }

        public override bool TryGetGrasp(uint sourceId, out bool isPressed)
        {
            isPressed = false;
            return false;
        }

        public override bool TryGetMenu(uint sourceId, out bool isPressed)
        {
            isPressed = false;
            return false;
        }

        #endregion // Base Input Source Methods
    }
}
