#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.AssetsAddressablesConstants;
using Data.PuzzleInformation;
using Infrastructure.ProjectStateMachine.Core;
using Services.Factories.AbstractFactory;
using Services.Factories.UIFactory;
using Services.LoadPuzzlesCatalogData;
using UI.InGameMenu;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Infrastructure.ProjectStateMachine.States
{
    public class InGameMenuState : IState<Bootstrap>, IEnter, IExit
    {
        public InGameMenuState(Bootstrap initializer, IUIFactory uiFactory, IAbstractFactory abstractFactory,
            ILoadPuzzlesCatalogData loadPuzzlesCatalogData)
        {
            Initializer = initializer;
            _uiFactory = uiFactory;
            _abstractFactory = abstractFactory;
            _loadPuzzlesCatalogData = loadPuzzlesCatalogData;
        }

        public Bootstrap Initializer { get; }
        private readonly IUIFactory _uiFactory;
        private readonly IAbstractFactory _abstractFactory;
        private readonly ILoadPuzzlesCatalogData _loadPuzzlesCatalogData;

        private InGameMenuUI _inGameMenuUI;

        public async void OnEnter()
        {
            await CreatedUI();
            await CreatedCategoryInfoUI();
            await CreatedPuzzlesInfoUI();
            OpenPanelCategoryInfo();
        }

        public void OnExit()
        {
            Clear();
            DestroyUI();
        }

        private async Task CreatedUI()
        {
            if (_uiFactory.InGameMenuScreen == null)
            {
                var inGameMenuScreen = await _uiFactory.CreatedInGameMenuScreen();
                _inGameMenuUI = inGameMenuScreen.GetComponent<InGameMenuUI>();
            }
            else
            {
                _uiFactory.InGameMenuScreen.SetActive(true);
                _inGameMenuUI = _uiFactory.InGameMenuScreen.GetComponent<InGameMenuUI>();
            }
        }

        private void DestroyUI()
        {
            _uiFactory.InGameMenuScreen.SetActive(false);
        }

        private void Clear()
        {
            _inGameMenuUI.Clear();
        }

        private async Task CreatedCategoryInfoUI()
        {
            var categoryInformation = GetCategoryInfo();

            var rectTransforms = new RectTransform[categoryInformation.Count];

            foreach (var category in categoryInformation)
            {
                var categoryInformationUI = await _abstractFactory.CreateInstance<GameObject>(
                    AssetsAddressablesConstants.CATEGORY_INFORMATION_SCREEN);

                if (!categoryInformationUI.TryGetComponent(out CategoryInformationUI categoryInformationUIComponent))
                    throw new Exception("CategoryInformationUI not found");

                categoryInformationUIComponent.SetUp(
                    category.Image,
                    category.Name,
                    () => OpenPanelPuzzleInfo(category.Id));

                rectTransforms[categoryInformation.IndexOf(category)] =
                    categoryInformationUI.GetComponent<RectTransform>();
            }

            _inGameMenuUI.AddCategoriesPanel(rectTransforms);
        }

        private async Task CreatedPuzzlesInfoUI()
        {
            var categoryInformation = GetCategoryInfo();

            foreach (var category in categoryInformation)
            {
                var puzzlesInformation = GetPuzzlesInfo(category.Id);

                var rectTransforms = new RectTransform[puzzlesInformation.Count];

                foreach (var puzzle in puzzlesInformation)
                {
                    var puzzleInformationUI = await _abstractFactory.CreateInstance<GameObject>(
                        AssetsAddressablesConstants.PUZZLE_INFORMATION_SCREEN);

                    if (!puzzleInformationUI.TryGetComponent(out PuzzleInformationUI puzzleInformationUIComponent))
                        throw new Exception("PuzzleInformationUI not found");

                    puzzleInformationUIComponent.SetUp(
                        puzzle.Image,
                        puzzle.Name,
                        puzzle.ElementsCount,
                        () => StartPuzzle(puzzle));

                    rectTransforms[puzzlesInformation.IndexOf(puzzle)] =
                        puzzleInformationUI.GetComponent<RectTransform>();
                }

                _inGameMenuUI.AddPuzzlesPanel(rectTransforms, category.Id);
            }
        }

        private void OpenPanelCategoryInfo()
        {
            _inGameMenuUI.OpenCategoriesPanel();
            _inGameMenuUI.ClearBackButtonListener();
            _inGameMenuUI.RegisterBackButtonListener(BackInMainMenu);
        }

        private void OpenPanelPuzzleInfo(string panelName)
        {
            _inGameMenuUI.OpenPuzzleInfoPanel(panelName);
            _inGameMenuUI.ClearBackButtonListener();
            _inGameMenuUI.RegisterBackButtonListener(OpenPanelCategoryInfo);
        }

        private List<CategoryInformation> GetCategoryInfo()
        {
            return _loadPuzzlesCatalogData.GetCategoriesInformations();
        }

        private List<PuzzleInformation> GetPuzzlesInfo(string categoryId)
        {
            return _loadPuzzlesCatalogData.GetPuzzlesInformations(categoryId);
        }

        private void StartPuzzle(PuzzleInformation puzzleInformation)
        {
            Initializer.StateMachine.SwitchState<FoldingThePuzzleState, PuzzleInformation>(puzzleInformation);
        }

        private void BackInMainMenu()
        {
            Initializer.StateMachine.SwitchState<MainMenuState>();
        }
    }
}