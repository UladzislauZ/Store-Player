using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<Slot> backpackPlayer;
    [SerializeField] private List<Slot> backpackStore;
    [SerializeField] private TextMeshProUGUI moneyPlayer;
    [SerializeField] private TextMeshProUGUI moneyStore;

    // public PlayerBackpack _player = new PlayerBackpack();
    // public StoreBackpack _store = new StoreBackpack();
    public int _playerBank;
    public int _storeBank;

    private void Init()
    {
        foreach (var slotPlayer in backpackPlayer)
        {
            slotPlayer.onBuy += BuySlot;
        }
        
        foreach (var slotStore in backpackStore)
        {
            slotStore.onBuy += BuySlot;
        }

        _playerBank = 1001;
        _storeBank = 1002;
    }

    public int GetCountMoney(int id)
    {
        switch (id)
        {
            case 0:
                return _playerBank;
            case 1:
                return _storeBank;
            default: 
                return 0;
        }
    }

    private void Update()
    {
        moneyPlayer.text = Convert.ToString(_playerBank);
        moneyStore.text = Convert.ToString(_storeBank);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parent">покупатель</param>
    /// <param name="cost">цена</param>
    private void BuySlot(int parent, int cost)
    {
        switch (parent)
        {
            case 0:
            {
                _playerBank -= cost;
                _storeBank += cost;
                break;
            }
            case 1:
            {
                _playerBank += cost;
                _storeBank -= cost;
                break;
            }
        }
    }
}
