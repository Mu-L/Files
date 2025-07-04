﻿// Copyright (c) Files Community
// Licensed under the MIT License.

namespace Files.App.Actions
{
	internal sealed partial class SearchAction : ObservableObject, IAction
	{
		private readonly IContentPageContext context;

		public string Label
			=> Strings.Search.GetLocalizedResource();

		public string Description
			=> Strings.SearchDescription.GetLocalizedResource();

		public HotKey HotKey
			=> new(Keys.F, KeyModifiers.Ctrl);

		public HotKey SecondHotKey
			=> new(Keys.F3);

		public RichGlyph Glyph
			=> new(themedIconStyle: "App.ThemedIcons.Omnibar.Search");

		public bool IsExecutable
			=> !context.IsSearchBoxVisible;

		public SearchAction()
		{
			context = Ioc.Default.GetRequiredService<IContentPageContext>();

			context.PropertyChanged += Context_PropertyChanged;
		}

		public Task ExecuteAsync(object? parameter = null)
		{
			context.ShellPage!.ToolbarViewModel.SwitchToSearchMode();

			return Task.CompletedTask;
		}

		private void Context_PropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case nameof(IContentPageContext.IsSearchBoxVisible):
					OnPropertyChanged(nameof(IsExecutable));
					break;
			}
		}
	}
}
