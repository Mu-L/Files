﻿// Copyright (c) Files Community
// Licensed under the MIT License.

using Files.Shared.Helpers;
using Microsoft.UI.Xaml;
using Windows.Storage;

namespace Files.App.Data.Factories
{
	public static class PropertiesNavigationViewItemFactory
	{
		public static ObservableCollection<NavigationViewItemButtonStyleItem> Initialize(object item)
		{
			ObservableCollection<NavigationViewItemButtonStyleItem> PropertiesNavigationViewItems = [];

			var generalItem = new NavigationViewItemButtonStyleItem()
			{
				Name = Strings.General.GetLocalizedResource(),
				ItemType = PropertiesNavigationViewItemType.General,
				ThemedIconStyle = (Style)Application.Current.Resources["App.ThemedIcons.Properties.General"],
			};
			var securityItem = new NavigationViewItemButtonStyleItem()
			{
				Name = Strings.Security.GetLocalizedResource(),
				ItemType = PropertiesNavigationViewItemType.Security,
				ThemedIconStyle = (Style)Application.Current.Resources["App.ThemedIcons.Properties.Security"],
			};
			var hashesItem = new NavigationViewItemButtonStyleItem()
			{
				Name = Strings.Hashes.GetLocalizedResource(),
				ItemType = PropertiesNavigationViewItemType.Hashes,
				ThemedIconStyle = (Style)Application.Current.Resources["App.ThemedIcons.Properties.Hashes"],
			};
			var shortcutItem = new NavigationViewItemButtonStyleItem()
			{
				Name = Strings.Shortcut.GetLocalizedResource(),
				ItemType = PropertiesNavigationViewItemType.Shortcut,
				ThemedIconStyle = (Style)Application.Current.Resources["App.ThemedIcons.Properties.Shortcut"],
			};
			var libraryItem = new NavigationViewItemButtonStyleItem()
			{
				Name = Strings.Library.GetLocalizedResource(),
				ItemType = PropertiesNavigationViewItemType.Library,
				ThemedIconStyle = (Style)Application.Current.Resources["App.ThemedIcons.Properties.Library"],
			};
			var detailsItem = new NavigationViewItemButtonStyleItem()
			{
				Name = Strings.Details.GetLocalizedResource(),
				ItemType = PropertiesNavigationViewItemType.Details,
				ThemedIconStyle = (Style)Application.Current.Resources["App.ThemedIcons.Properties.Info"],
			};
			var customizationItem = new NavigationViewItemButtonStyleItem()
			{
				Name = Strings.Customization.GetLocalizedResource(),
				ItemType = PropertiesNavigationViewItemType.Customization,
				ThemedIconStyle = (Style)Application.Current.Resources["App.ThemedIcons.Properties.CustomizeFolder"],
			};
			var compatibilityItem = new NavigationViewItemButtonStyleItem()
			{
				Name = Strings.Compatibility.GetLocalizedResource(),
				ItemType = PropertiesNavigationViewItemType.Compatibility,
				ThemedIconStyle = (Style)Application.Current.Resources["App.ThemedIcons.Properties.Compatability"],
			};
			var signaturesItem = new NavigationViewItemButtonStyleItem()
			{
				Name = Strings.Signatures.GetLocalizedResource(),
				ItemType = PropertiesNavigationViewItemType.Signatures,
				ThemedIconStyle = (Style)Application.Current.Resources["App.ThemedIcons.Properties.Signatures"],
			};

			PropertiesNavigationViewItems.Add(generalItem);
			PropertiesNavigationViewItems.Add(signaturesItem);
			PropertiesNavigationViewItems.Add(securityItem);
			PropertiesNavigationViewItems.Add(hashesItem);
			PropertiesNavigationViewItems.Add(shortcutItem);
			PropertiesNavigationViewItems.Add(libraryItem);
			PropertiesNavigationViewItems.Add(detailsItem);
			PropertiesNavigationViewItems.Add(customizationItem);
			PropertiesNavigationViewItems.Add(compatibilityItem);

			if (item is List<ListedItem> listedItems)
			{
				var firstFileExtension = listedItems.FirstOrDefault()?.FileExtension;
				var commonFileExt = listedItems.All(x => x.FileExtension == firstFileExtension) ? firstFileExtension : null;
				var compatibilityItemEnabled = listedItems.All(listedItem => FileExtensionHelpers.IsExecutableFile(listedItem is IShortcutItem sht ? sht.TargetPath : commonFileExt, true));
				var onlyFiles = listedItems.All(listedItem => listedItem.PrimaryItemAttribute == StorageItemTypes.File || listedItem.IsArchive);

				if (!compatibilityItemEnabled)
					PropertiesNavigationViewItems.Remove(compatibilityItem);

				if (!onlyFiles)
					PropertiesNavigationViewItems.Remove(detailsItem);

				PropertiesNavigationViewItems.Remove(libraryItem);
				PropertiesNavigationViewItems.Remove(shortcutItem);
				PropertiesNavigationViewItems.Remove(securityItem);
				PropertiesNavigationViewItems.Remove(customizationItem);
				PropertiesNavigationViewItems.Remove(hashesItem);
				PropertiesNavigationViewItems.Remove(signaturesItem);
			}
			else if (item is ListedItem listedItem)
			{
				var isShortcut = listedItem.IsShortcut;
				var isLibrary = listedItem.IsLibrary;
				var fileExt = listedItem.FileExtension;
				var isFolder = listedItem.PrimaryItemAttribute == Windows.Storage.StorageItemTypes.Folder;

				var securityItemEnabled = !isLibrary && !listedItem.IsRecycleBinItem;
				var hashItemEnabled = !(isFolder && !listedItem.IsArchive) && !isLibrary && !listedItem.IsRecycleBinItem;
				var detailsItemEnabled = !(isFolder && !listedItem.IsArchive) && !isLibrary && !listedItem.IsRecycleBinItem;
				var customizationItemEnabled = !isLibrary && (isFolder && !listedItem.IsArchive || isShortcut && !listedItem.IsLinkItem);
				var compatibilityItemEnabled = FileExtensionHelpers.IsExecutableFile(listedItem is IShortcutItem sht ? sht.TargetPath : fileExt, true);
				var signaturesItemEnabled = 
					!isFolder &&
					!isLibrary &&
					!listedItem.IsRecycleBinItem &&
					FileExtensionHelpers.IsSignableFile(fileExt, true);

				if (!securityItemEnabled)
					PropertiesNavigationViewItems.Remove(securityItem);

				if (!hashItemEnabled)
					PropertiesNavigationViewItems.Remove(hashesItem);

				if (!signaturesItemEnabled)
					PropertiesNavigationViewItems.Remove(signaturesItem);

				if (!isShortcut)
					PropertiesNavigationViewItems.Remove(shortcutItem);

				if (!isLibrary)
					PropertiesNavigationViewItems.Remove(libraryItem);

				if (!detailsItemEnabled)
					PropertiesNavigationViewItems.Remove(detailsItem);

				if (!customizationItemEnabled)
					PropertiesNavigationViewItems.Remove(customizationItem);

				if (!compatibilityItemEnabled)
					PropertiesNavigationViewItems.Remove(compatibilityItem);
			}
			else if (item is DriveItem)
			{
				PropertiesNavigationViewItems.Remove(hashesItem);
				PropertiesNavigationViewItems.Remove(shortcutItem);
				PropertiesNavigationViewItems.Remove(libraryItem);
				PropertiesNavigationViewItems.Remove(detailsItem);
				PropertiesNavigationViewItems.Remove(customizationItem);
				PropertiesNavigationViewItems.Remove(compatibilityItem);
				PropertiesNavigationViewItems.Remove(signaturesItem);
			}

			return PropertiesNavigationViewItems;
		}
	}
}
