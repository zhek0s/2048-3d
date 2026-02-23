using Assets.Scripts.Gameplay;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Animations
{
    public class MergeAnimation : MonoBehaviour
    {
        public static async UniTask MoveCubeToPointAsync(Cube cube, Vector3 needPos, float startSpeed)
        {
            float speed = startSpeed;
            float maxSpeed = 10;
            float accelaration = 20;
            float remainDistance = 1;
            float safetyTimer = 0f;
            while (remainDistance > 0.5f && safetyTimer < 2f)
            {
                await UniTask.Yield();
                safetyTimer += Time.deltaTime;
                speed = MoveCube(cube, needPos, maxSpeed, accelaration, speed, out remainDistance);
            }
        }

        public static async UniTask MoveCubeToOtherCubeAsync(Cube cube, Cube otherCube, float startSpeed)
        {
            float speed = startSpeed;
            float maxSpeed = 10;
            float accelaration = 20;
            float remainDistance = 1;
            float safetyTimer = 0f;
            while (remainDistance > 0.5f && safetyTimer < 2f)
            {
                await UniTask.Yield();
                safetyTimer += Time.deltaTime;
                speed = MoveCube(cube, otherCube.transform.position, maxSpeed, accelaration, speed, out remainDistance);
            }
        }

        public static async UniTask BoosterSequence(Cube a, Cube b)
        {
            a.DisablePhysics();
            b.DisablePhysics();

            Vector3 merge = Vector3.down * 0.5f;
            Vector3 over = Vector3.up * 3f;
            Vector3 overA = a.transform.position + over;
            Vector3 overB = b.transform.position + over;
            overA.y = Mathf.Min(overA.y, overB.y);
            overB.y = overA.y;
            await UniTask.WhenAll(
                FlyBySplineAsync(a, overA, 1f),
                FlyBySplineAsync(b, overB, 1f)
            );
            await UniTask.Delay(100);
            await UniTask.WhenAll(
                FlyBySplineAsync(a, a.transform.position + merge, 2f),
                FlyBySplineAsync(b, b.transform.position + merge, 2f)
            );
            await UniTask.Delay(300);
            var center = (a.transform.position + b.transform.position) / 2;
            await UniTask.WhenAll(
                FlyBySplineAsync(a, center, 0.6f),
                FlyBySplineAsync(b, center, 0.6f)
            );
        }

        private static float MoveCube(
            Cube cube,
            Vector3 needPos,
            float maxSpeed,
            float accelaration,
            float speed,
            out float remainDistance
        )
        {
            Vector3 direction = needPos - cube.transform.position;
            remainDistance = direction.magnitude;
            if (remainDistance < 0.0001f)
            {
                remainDistance = 0;
                return speed;
            }
            direction = direction / remainDistance;
            speed = Mathf.Min(speed + accelaration * Time.deltaTime, maxSpeed);
            float tickDistance = speed * Time.deltaTime;
            tickDistance = Mathf.Min(remainDistance, tickDistance);
            cube.transform.position += tickDistance * direction;
            remainDistance -= tickDistance;
            return speed;
        }

        private static async UniTask FlyBySplineAsync(
            Cube cube,
            Vector3 targetPosition,
            float duration = 0.6f)
        {
            Vector3 startPos = cube.transform.position;
            Vector3 mid = (startPos + targetPosition) / 2f;
            float time = 0f;
            while (time < duration)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                time += Time.deltaTime;
                float t = time / duration;
                t = EaseOutCubic(t);
                Vector3 pos = CalculateQuadraticBezierPoint(t, startPos, mid, targetPosition);
                cube.transform.position = pos;
            }
            cube.transform.position = targetPosition;
        }

        private static Vector3 CalculateQuadraticBezierPoint(
            float t,
            Vector3 p0,
            Vector3 p1,
            Vector3 p2)
        {
            float u = 1 - t;
            return u * u * p0 +
                   2 * u * t * p1 +
                   t * t * p2;
        }

        private static float EaseOutCubic(float t)
        {
            return 1 - Mathf.Pow(1 - t, 3);
        }
    }
}