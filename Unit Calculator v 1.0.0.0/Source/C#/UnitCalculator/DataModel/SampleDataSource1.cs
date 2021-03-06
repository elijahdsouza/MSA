﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections.Specialized;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace UnitCalculator.Data
{
    /// <summary>
    /// Base class for <see cref="SampleDataItem1"/> and <see cref="SampleDataGroup1"/> that
    /// defines properties common to both.
    /// </summary>
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class SampleDataCommon1 : UnitCalculator.Common.BindableBase
    {
        private static Uri _baseUri = new Uri("ms-appx:///");

        public SampleDataCommon1(String uniqueId, String title, String subtitle, String imagePath, String description)
        {
            this._uniqueId = uniqueId;
            this._title = title;
            this._subtitle = subtitle;
            this._description = description;
            this._imagePath = imagePath;
        }

        private string _uniqueId = string.Empty;
        public string UniqueId
        {
            get { return this._uniqueId; }
            set { this.SetProperty(ref this._uniqueId, value); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }

        private string _subtitle = string.Empty;
        public string Subtitle
        {
            get { return this._subtitle; }
            set { this.SetProperty(ref this._subtitle, value); }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return this._description; }
            set { this.SetProperty(ref this._description, value); }
        }

        private ImageSource _image = null;
        private String _imagePath = null;
        public ImageSource Image
        {
            get
            {
                if (this._image == null && this._imagePath != null)
                {
                    this._image = new BitmapImage(new Uri(SampleDataCommon1._baseUri, this._imagePath));
                }
                return this._image;
            }

            set
            {
                this._imagePath = null;
                this.SetProperty(ref this._image, value);
            }
        }

        public void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            this.OnPropertyChanged("Image");
        }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class SampleDataItem1 : SampleDataCommon1
    {
        public SampleDataItem1(String uniqueId, String title, String subtitle, String imagePath, String description, String content, SampleDataGroup1 group)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this._content = content;
            this._group = group;
        }

        private string _content = string.Empty;
        public string Content
        {
            get { return this._content; }
            set { this.SetProperty(ref this._content, value); }
        }

        private SampleDataGroup1 _group;
        public SampleDataGroup1 Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }
    }

    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class SampleDataGroup1 : SampleDataCommon1
    {
        public SampleDataGroup1(String uniqueId, String title, String subtitle, String imagePath, String description)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            Items.CollectionChanged += ItemsCollectionChanged;
        }

        private void ItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Provides a subset of the full items collection to bind to from a GroupedItemsPage
            // for two reasons: GridView will not virtualize large items collections, and it
            // improves the user experience when browsing through groups with large numbers of
            // items.
            //
            // A maximum of 12 items are displayed because it results in filled grid columns
            // whether there are 1, 2, 3, 4, or 6 rows displayed

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex < 12)
                    {
                        TopItems.Insert(e.NewStartingIndex, Items[e.NewStartingIndex]);
                        if (TopItems.Count > 12)
                        {
                            TopItems.RemoveAt(12);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex < 12 && e.NewStartingIndex < 12)
                    {
                        TopItems.Move(e.OldStartingIndex, e.NewStartingIndex);
                    }
                    else if (e.OldStartingIndex < 12)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        TopItems.Add(Items[11]);
                    }
                    else if (e.NewStartingIndex < 12)
                    {
                        TopItems.Insert(e.NewStartingIndex, Items[e.NewStartingIndex]);
                        TopItems.RemoveAt(12);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex < 12)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        if (Items.Count >= 12)
                        {
                            TopItems.Add(Items[11]);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldStartingIndex < 12)
                    {
                        TopItems[e.OldStartingIndex] = Items[e.OldStartingIndex];
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    TopItems.Clear();
                    while (TopItems.Count < Items.Count && TopItems.Count < 12)
                    {
                        TopItems.Add(Items[TopItems.Count]);
                    }
                    break;
            }
        }

        private ObservableCollection<SampleDataItem1> _items = new ObservableCollection<SampleDataItem1>();
        public ObservableCollection<SampleDataItem1> Items
        {
            get { return this._items; }
        }

        private ObservableCollection<SampleDataItem1> _topItem = new ObservableCollection<SampleDataItem1>();
        public ObservableCollection<SampleDataItem1> TopItems
        {
            get { return this._topItem; }
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// 
    /// SampleDataSource1 initializes with placeholder data rather than live production
    /// data so that sample data is provided at both design-time and run-time.
    /// </summary>
    public sealed class SampleDataSource1
    {
        private static SampleDataSource1 _SampleDataSource1 = new SampleDataSource1();

        private ObservableCollection<SampleDataGroup1> _allGroups = new ObservableCollection<SampleDataGroup1>();
        public ObservableCollection<SampleDataGroup1> AllGroups
        {
            get { return this._allGroups; }
        }

        public static IEnumerable<SampleDataGroup1> GetGroups(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");

            return _SampleDataSource1.AllGroups;
        }

        public static SampleDataGroup1 GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _SampleDataSource1.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static SampleDataItem1 GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _SampleDataSource1.AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public SampleDataSource1()
        {
            String ITEM_CONTENT = String.Format("Item Content: {0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}",
                        "Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");

            var group1 = new SampleDataGroup1("Group-1",
                    "Group Title: 1",
                    "Group Subtitle: 1",
                    "Assets/DarkGray.png",
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            group1.Items.Add(new SampleDataItem1("Group-1-Item-1",
                    "Length",
                    "Centimeter, Feet, Inch, Metre, Mile Kilometre, Yard",
                    "Assets/Images/Length.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group1));
            group1.Items.Add(new SampleDataItem1("Group-1-Item-2",
                    "Mass",
                    "Gram, Killogram, Milligram, Ounce, Pound, Ton",
                    "Assets/Images/Mass.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group1));
            group1.Items.Add(new SampleDataItem1("Group-1-Item-3",
                    "Data",
                    "Bits, Byte, Killobyte, Megabyte, Gigabyte, Terabyte, Petabyte",
                    "Assets/Images/Data.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group1));
            group1.Items.Add(new SampleDataItem1("Group-1-Item-4",
                    "Speed",
                    "Foot/sec, Kilometer/hour, Kilometer/sec, knot, Mach Meter/min, Meter/sec",
                    "Assets/Images/Speed.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group1));
            group1.Items.Add(new SampleDataItem1("Group-1-Item-5",
                    "Power",
                    "Calories/second, Erg/second, Foot-pond?second, Gigawatt, Horespwer, Kilocalorie?second, Killowatt, Megawatt",
                    "Assets/Images/Power.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group1));
            group1.Items.Add(new SampleDataItem1("Group-1-Item-6",
                    "Energy",
                    "Calorie, Electronvolt, Erg, Foot pound, Gigajoule, Joule",
                    "Assets/Images/Energy.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group1));
            group1.Items.Add(new SampleDataItem1("Group-1-Item-7",
                    "Temperature",
                    "Calorie, Electronvolt, Erg, Foot pound, Gigajoule, Joule",
                    "Assets/Images/Temperature.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group1));
            group1.Items.Add(new SampleDataItem1("Group-1-Item-8",
                    "Volume",
                    "Cub, Liter, Gallon, Tabespoon",
                    "Assets/Images/Volume.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group1));
            group1.Items.Add(new SampleDataItem1("Group-1-Item-9",
                    "Area",
                    "Yard square, Mile square, Milemeter, Centimeter esquare, Keter square, Kilometer square",
                    "Assets/Images/Area.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group1));
            group1.Items.Add(new SampleDataItem1("Group-1-Item-10",
                    "Pressure",
                    "Bar, Hectopascal, Kilogam, per.cm, Kilogram per.sq. meter",
                    "Assets/Images/Pressure.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group1));
            group1.Items.Add(new SampleDataItem1("Group-1-Item-11",
                    "Force",
                    "Foot/sec, Kilometer/hour, Kilometer/sec, knot, Mach Meter/min, Meter/sec",
                    "Assets/Images/Force.png",
                    "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    ITEM_CONTENT,
                    group1));
            this.AllGroups.Add(group1);

            //var group2 = new SampleDataGroup1("Group-2",
            //        "Group Title: 2",
            //        "Group Subtitle: 2",
            //        "Assets/LightGray.png",
            //        "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            //group2.Items.Add(new SampleDataItem1("Group-2-Item-1",
            //        "Item Title: 1",
            //        "Item Subtitle: 1",
            //        "Assets/DarkGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group2));
            //group2.Items.Add(new SampleDataItem1("Group-2-Item-2",
            //        "Item Title: 2",
            //        "Item Subtitle: 2",
            //        "Assets/MediumGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group2));
            //group2.Items.Add(new SampleDataItem1("Group-2-Item-3",
            //        "Item Title: 3",
            //        "Item Subtitle: 3",
            //        "Assets/LightGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group2));
            //this.AllGroups.Add(group2);

            //var group3 = new SampleDataGroup1("Group-3",
            //        "Group Title: 3",
            //        "Group Subtitle: 3",
            //        "Assets/MediumGray.png",
            //        "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            //group3.Items.Add(new SampleDataItem1("Group-3-Item-1",
            //        "Item Title: 1",
            //        "Item Subtitle: 1",
            //        "Assets/MediumGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group3));
            //group3.Items.Add(new SampleDataItem1("Group-3-Item-2",
            //        "Item Title: 2",
            //        "Item Subtitle: 2",
            //        "Assets/LightGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group3));
            //group3.Items.Add(new SampleDataItem1("Group-3-Item-3",
            //        "Item Title: 3",
            //        "Item Subtitle: 3",
            //        "Assets/DarkGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group3));
            //group3.Items.Add(new SampleDataItem1("Group-3-Item-4",
            //        "Item Title: 4",
            //        "Item Subtitle: 4",
            //        "Assets/LightGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group3));
            //group3.Items.Add(new SampleDataItem1("Group-3-Item-5",
            //        "Item Title: 5",
            //        "Item Subtitle: 5",
            //        "Assets/MediumGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group3));
            //group3.Items.Add(new SampleDataItem1("Group-3-Item-6",
            //        "Item Title: 6",
            //        "Item Subtitle: 6",
            //        "Assets/DarkGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group3));
            //group3.Items.Add(new SampleDataItem1("Group-3-Item-7",
            //        "Item Title: 7",
            //        "Item Subtitle: 7",
            //        "Assets/MediumGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group3));
            //this.AllGroups.Add(group3);

            //var group4 = new SampleDataGroup1("Group-4",
            //        "Group Title: 4",
            //        "Group Subtitle: 4",
            //        "Assets/LightGray.png",
            //        "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            //group4.Items.Add(new SampleDataItem1("Group-4-Item-1",
            //        "Item Title: 1",
            //        "Item Subtitle: 1",
            //        "Assets/DarkGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group4));
            //group4.Items.Add(new SampleDataItem1("Group-4-Item-2",
            //        "Item Title: 2",
            //        "Item Subtitle: 2",
            //        "Assets/LightGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group4));
            //group4.Items.Add(new SampleDataItem1("Group-4-Item-3",
            //        "Item Title: 3",
            //        "Item Subtitle: 3",
            //        "Assets/DarkGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group4));
            //group4.Items.Add(new SampleDataItem1("Group-4-Item-4",
            //        "Item Title: 4",
            //        "Item Subtitle: 4",
            //        "Assets/LightGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group4));
            //group4.Items.Add(new SampleDataItem1("Group-4-Item-5",
            //        "Item Title: 5",
            //        "Item Subtitle: 5",
            //        "Assets/MediumGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group4));
            //group4.Items.Add(new SampleDataItem1("Group-4-Item-6",
            //        "Item Title: 6",
            //        "Item Subtitle: 6",
            //        "Assets/LightGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group4));
            //this.AllGroups.Add(group4);

            //var group5 = new SampleDataGroup1("Group-5",
            //        "Group Title: 5",
            //        "Group Subtitle: 5",
            //        "Assets/MediumGray.png",
            //        "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            //group5.Items.Add(new SampleDataItem1("Group-5-Item-1",
            //        "Item Title: 1",
            //        "Item Subtitle: 1",
            //        "Assets/LightGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group5));
            //group5.Items.Add(new SampleDataItem1("Group-5-Item-2",
            //        "Item Title: 2",
            //        "Item Subtitle: 2",
            //        "Assets/DarkGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group5));
            //group5.Items.Add(new SampleDataItem1("Group-5-Item-3",
            //        "Item Title: 3",
            //        "Item Subtitle: 3",
            //        "Assets/LightGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group5));
            //group5.Items.Add(new SampleDataItem1("Group-5-Item-4",
            //        "Item Title: 4",
            //        "Item Subtitle: 4",
            //        "Assets/MediumGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group5));
            //this.AllGroups.Add(group5);

            //var group6 = new SampleDataGroup1("Group-6",
            //        "Group Title: 6",
            //        "Group Subtitle: 6",
            //        "Assets/DarkGray.png",
            //        "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");
            //group6.Items.Add(new SampleDataItem1("Group-6-Item-1",
            //        "Item Title: 1",
            //        "Item Subtitle: 1",
            //        "Assets/LightGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group6));
            //group6.Items.Add(new SampleDataItem1("Group-6-Item-2",
            //        "Item Title: 2",
            //        "Item Subtitle: 2",
            //        "Assets/DarkGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group6));
            //group6.Items.Add(new SampleDataItem1("Group-6-Item-3",
            //        "Item Title: 3",
            //        "Item Subtitle: 3",
            //        "Assets/MediumGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group6));
            //group6.Items.Add(new SampleDataItem1("Group-6-Item-4",
            //        "Item Title: 4",
            //        "Item Subtitle: 4",
            //        "Assets/DarkGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group6));
            //group6.Items.Add(new SampleDataItem1("Group-6-Item-5",
            //        "Item Title: 5",
            //        "Item Subtitle: 5",
            //        "Assets/LightGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group6));
            //group6.Items.Add(new SampleDataItem1("Group-6-Item-6",
            //        "Item Title: 6",
            //        "Item Subtitle: 6",
            //        "Assets/MediumGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group6));
            //group6.Items.Add(new SampleDataItem1("Group-6-Item-7",
            //        "Item Title: 7",
            //        "Item Subtitle: 7",
            //        "Assets/DarkGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group6));
            //group6.Items.Add(new SampleDataItem1("Group-6-Item-8",
            //        "Item Title: 8",
            //        "Item Subtitle: 8",
            //        "Assets/LightGray.png",
            //        "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
            //        ITEM_CONTENT,
            //        group6));
            //this.AllGroups.Add(group6);
        }
    }
}
