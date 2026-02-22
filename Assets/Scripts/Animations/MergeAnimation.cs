using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeAnimation : MonoBehaviour
{
    public static async UniTask MoveCubeToPointAsync(Cube cube, Vector3 needPos, float startSpeed)
    {
        float speed = startSpeed;
        float maxSpeed = 10;
        float accelaration = 20;
        float remainDistance = 1;
        while (remainDistance > 0.5f)
        {
            await UniTask.NextFrame();
            speed = MoveCube(cube, needPos, maxSpeed, accelaration, speed, out remainDistance);
        }
    }

    public static async UniTask MoveCubeToOtherCubeAsync(Cube cube, Cube otherCube, float startSpeed)
    {
        float speed = startSpeed;
        float maxSpeed = 10;
        float accelaration = 20;
        float remainDistance = 1;
        while (remainDistance > 0.5f)
        {
            await UniTask.NextFrame();
            speed = MoveCube(cube, otherCube.transform.position, maxSpeed, accelaration, speed, out remainDistance);
        }
    }

    public static async UniTask BoosterSequence(Cube a, Cube b)
    {
        a.DisablePhysics();
        b.DisablePhysics();

        Vector3 upOffset = Vector3.up * 3f;
        await UniTask.WhenAll(
            MoveCubeToPointAsync(a, a.transform.position + upOffset, 0.4f),
            MoveCubeToPointAsync(b, b.transform.position + upOffset, 0.4f)
        );
        await UniTask.Delay(100);
        Vector3 downOffset = Vector3.down * 1f;
        await UniTask.WhenAll(
            MoveCubeToPointAsync(a, a.transform.position + downOffset, 0f),
            MoveCubeToPointAsync(b, b.transform.position + downOffset, 0f)
        );
        await UniTask.Delay(300);
        await MoveCubeToOtherCubeAsync(a, b, 0f);
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
        direction = direction / remainDistance;
        speed = Mathf.Min(speed + accelaration * Time.deltaTime, maxSpeed);
        float tickDistance = speed * Time.deltaTime;
        tickDistance = Mathf.Min(remainDistance, tickDistance);
        cube.transform.position += tickDistance * direction;
        remainDistance -= tickDistance;
        return speed;
    }
}
