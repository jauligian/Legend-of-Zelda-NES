using CSE3902.Interfaces;
using Microsoft.Xna.Framework;

namespace CSE3902.Items
{
    public class CycleThroughItems
    {
        private IItem _currentItem;
        private IItem[] _itemArray;
        private int _index;

        public CycleThroughItems(Vector2 location)
        {
            InitializeItemArray(location);
            _index = 0;
            _currentItem = _itemArray[_index];
        }
        private void InitializeItemArray(Vector2 location)
        {
            _itemArray = new IItem[13];
            _itemArray[0] = new ArrowItem(location);
            _itemArray[1] = new BombItem(location);
            _itemArray[2] = new BoomerangItem(location);
            _itemArray[3] = new BowItem(location);
            _itemArray[4] = new ClockItem(location);
            _itemArray[5] = new CompassItem(location);
            _itemArray[6] = new FairyItem(location);
            _itemArray[7] = new HeartContainerItem(location);
            _itemArray[8] = new HeartItem(location);
            _itemArray[9] = new KeyItem(location);
            _itemArray[10] = new MapItem(location);
            _itemArray[11] = new RupeeItem(location);
            _itemArray[12] = new TriforcePieceItem(location);
        }
        public void Draw()
        {
            _currentItem.Draw();
        }
        public void Update()
        {
            _currentItem = _itemArray[_index];
        }
        public void NextItem()
        {
            _index = (_index + 1) % 13;
        }
        public void PrevItem()
        {
            _index = (_index + 12) % 13;
        }
    }
}
