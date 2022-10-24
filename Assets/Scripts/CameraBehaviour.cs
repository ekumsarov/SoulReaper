using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {
  // Добавляем объект, за которым будет двигаться камера
  [Header("GameObject")]
    // Пишу с нижним подчеркиванием, так gameObject - ключевое слово Unity.
    // а придумывать новое название переменной мне влом (сорян)
      public Transform _gameObject; 
  
  [Header("Camera position restrictions")]
        public float minY;
        public float maxY;
        public float minX;
        public float maxX;

  void Update() {
    UpdateCameraPosition();
  }

  // Изменяем позицию камеры на экране
  void UpdateCameraPosition() {
    try {
        transform.position = new Vector3(
            // Положение игрового объекта, за которым мы двигаемся
            _gameObject.position.x, 
            _gameObject.position.y,
            // Положение камеры z должно оставать неизменным 
            transform.position.z // (если камеры куда-то проваливается, заменить на, например, -10)
          );
      } catch (Exception error) {
        // Ловим ошибку, если по каким то причинам код не может быть выполнен (например, забыли подставить объект в _gameObject)
        Debug.LogError(error);
      }
    }
}