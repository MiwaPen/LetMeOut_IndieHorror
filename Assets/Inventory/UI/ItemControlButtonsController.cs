using System.Collections.Generic;
using Inventory.Enums;
using UnityEngine;

namespace Inventory.UI
{
    public class ItemControlButtonsController : MonoBehaviour
    {
        [SerializeField] private List<ItemControlButton> _itemControlButtons;
        private ItemCell _currentCell;
        private InventoryDialogMode _mode;
        
        public void Initialize(InventoryDialogMode mode)
        {
            _mode = mode;
            _itemControlButtons.ForEach(x=>x.Hide());
        }

        public void SetCellView(ItemCell itemCell)
        {
            _currentCell = itemCell;
            
            if (itemCell.IsEmpty)
            {
                _itemControlButtons.ForEach(x=>x.Hide());
                return;
            }
            
            switch (_mode)
            {
                case InventoryDialogMode.DEFAULT:
                    SetDefaultView();
                    break;
                case InventoryDialogMode.INTERACTION:
                    SetInteractionView();
                    break;
            }
        }

        private void SetInteractionView()
        {
           //check interaction logic (signal with parameters??)
           if (Random.Range(0,2)==1)
           {
               _itemControlButtons[0].Show("APPLY", OnUseItem);
           }
        }

        private void SetDefaultView()
        {
            _itemControlButtons[0].Show("DROP", OnDropItem);
        }

        private void OnDropItem()
        {
            RemoveItemFromInventory();
            Debug.LogWarning("DROP");
        }

        private void OnUseItem()
        {
            RemoveItemFromInventory();
            Debug.LogWarning("USE");
        }

        private void RemoveItemFromInventory()
        {
            Locator.Instance.InventoryController.RemoveItemFromInventory(_currentCell.ItemData.InteractableItemType);
        }

    }
}