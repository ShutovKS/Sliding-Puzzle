#region

#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Path;
using UI.FoldingThePuzzle;
using UI.InGameMenu;
using UI.MainMenu;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace Services.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly Dictionary<Type, Object> _uis = new();

        private static readonly Dictionary<Type, string> _address = new()
        {
            { typeof(MainMenuUI), ResourcesPathsConstants.MAIN_MENU_SCREEN },
            { typeof(InGameMenuUI), ResourcesPathsConstants.IN_GAME_MENU_SCREEN },
            { typeof(FoldingThePuzzlePuzzlesUI), ResourcesPathsConstants.FOLDING_THE_PUZZLE_SCREEN }
        };

        public T? GetUI<T>() where T : Object
        {
            if (_uis.TryGetValue(typeof(T), out var ui))
            {
                return (T)ui;
            }

            return null;
        }

        public async Task<T> Created<T>() where T : Object
        {
            if (_uis.ContainsKey(typeof(T)))
            {
                throw new Exception($"Уже существует пользоательский интерфейс {typeof(T).Name}.");
            }

            var instance = await Instance<T>();

            if (!instance.TryGetComponent<T>(out var ui))
            {
                throw new Exception($"Компонент {typeof(T).Name} не найден.");
            }

            _uis[typeof(T)] = ui;

            return ui;
        }

        public void Destroy<T>() where T : Object
        {
            if (!_uis.ContainsKey(typeof(T)))
            {
                throw new Exception($"Объект типа {typeof(T).Name} не существует.");
            }

            Object.Destroy(_uis[typeof(T)]);

            _uis.Remove(typeof(T));
        }

        private Task<GameObject> Instance<T>() where T : Object
        {
            var type = typeof(T);

            var prefab = Resources.Load<GameObject>(_address[type]);
            
            var instance = Object.Instantiate(prefab);

            Object.DontDestroyOnLoad(instance);

            return Task.FromResult(instance);
        }
    }
}