using System;
using UnityEngine;

public class LevelItem :MonoBehaviour
{

      private Vector3 _startPos;

      private void Awake()
      {
            Init();
      }

      protected virtual void Init()
      {
            SetStartPosition();
      }

      protected void SetStartPosition()
      {
            _startPos = transform.position;
      }

      private void OnEnable()
      {
           SubscribeEvents(); 
      }

      private void OnDisable()
      {
            UnSubscribeEvents();
      }

      protected virtual void Reset()
      {
            transform.position = _startPos;
      } 
      protected virtual void SubscribeEvents()
      {
            SceneManager.OnSceneRestart += Reset;
      }
      protected virtual void UnSubscribeEvents()
      {
            SceneManager.OnSceneRestart -= Reset;

      }
}