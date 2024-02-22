using System.Collections.Generic;

namespace CSE3902.Interfaces;

public interface IGameObjectManager<T> where T : IGameObject
{
    public List<T> ActiveGameObjects { get; }

    public void Spawn(T obj);

    public void Spawn(List<T> objList);
    public void Update();

    public void Draw();

    public void RemoveAll();

    public void ScaleGameObjectPositions(int row, int column);
}