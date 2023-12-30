using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TapSwap.Utils
{
    public static class RandomExtension
    {
        public static T GetRandomItemByWeight<T>(this IReadOnlyList<T> items, IEnumerable<float> weights) where T : class
        {
            float totalWeight = 0;

            // Вычисляем суммарный вес всех объектов
            foreach (var weight in weights)
            {
                totalWeight += weight;
            }

            var randomValue = Random.Range(0, totalWeight); // Случайное значение в диапазоне от 0 до суммарного веса
            var weightSum = 0f;

            for (var i = 0; i < items.Count; i++)
            {
                weightSum += weights.ElementAt(i);

                // Если случайное значение меньше или равно суммарному весу, выбираем этот объект
                if (randomValue <= weightSum)
                {
                    return items[i];
                }
            }

            // Если не удалось выбрать объект, возвращаем null
            return null;
        }
    }
}