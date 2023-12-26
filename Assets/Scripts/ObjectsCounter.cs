using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsCounter : MonoBehaviour
{
   public Action OnBombDestroyed;
   public Action OnAllCoinsCollected;
   public Action<int> OnCoinsCountChanged;
   
   private readonly List<Coin> _coins = new();
   private readonly List<Bomb> _bombs= new();

   private void Start()
   {
      _coins.AddRange(FindObjectsOfType<Coin>());

      foreach (Coin coin in _coins)
      {
         coin.OnCoinCollected += CollectCoin;
      }
      
      _bombs.AddRange(FindObjectsOfType<Bomb>());

      foreach (Bomb bomb in _bombs)
      {
         bomb.OnBombCollected += CollectBomb;
      }
      
      OnCoinsCountChanged?.Invoke(_coins.Count);
   }

   private void CollectCoin(Coin coin)
   {
      coin.OnCoinCollected -= CollectCoin;

      _coins.Remove(coin);
      
      OnCoinsCountChanged?.Invoke(_coins.Count);

      if (_coins.Count == 0)
      {
         OnAllCoinsCollected?.Invoke();
      }
   }

   private void CollectBomb(Bomb bomb)
   {
      bomb.OnBombCollected -= CollectBomb;
      
      OnBombDestroyed?.Invoke();
   }
}
