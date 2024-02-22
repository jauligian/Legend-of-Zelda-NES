using CSE3902.Interfaces;
using System.Collections.Generic;

namespace CSE3902.Shared.Managers;

public class GameObjectManager<T> : IGameObjectManager<T> where T : IGameObject
{
    public List<T> ActiveGameObjects { get; private set; }

    public GameObjectManager(List<T> objects)
    {
        ActiveGameObjects = objects;
    }

    public GameObjectManager()
    {
        ActiveGameObjects = new List<T>();
    }

    public void Spawn(T obj)
    {
        ActiveGameObjects.Add(obj);
    }

    public void Spawn(List<T> objList)
    {
        ActiveGameObjects.AddRange(objList);
    }

    public void Update()
    {
        ActiveGameObjects.RemoveAll(obj => !obj.Active);
        ActiveGameObjects.ForEach(obj => obj.Update());
    }

    public void Draw()
    {
        ActiveGameObjects.ForEach(obj => obj.Draw()); //only active enemies should be in this list anyways.
    }

    public void RemoveAll()
    {
        ActiveGameObjects.RemoveAll(obj => obj.Active || !obj.Active);
    }

    public void ScaleGameObjectPositions(int horizontalOffset, int verticalOffset)
    {
        ActiveGameObjects.ForEach(obj => obj.InitializeGlobalPosition(horizontalOffset, verticalOffset));
    }
}