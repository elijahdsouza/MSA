// For an introduction to the Page Control template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232511
(function () {
    "use strict";

    var appView = Windows.UI.ViewManagement.ApplicationView;
    var appViewState = Windows.UI.ViewManagement.ApplicationViewState;
    var nav = WinJS.Navigation;
    var ui = WinJS.UI;

    WinJS.UI.Pages.define("/pages/CalculaterPage/CalculaterPage.html", {
        // This function is called whenever a user navigates to this page. It
        // populates the page elements with the app's data.

        initializeListLayout: function (listView, viewState) {
            /// <param name="listView" value="WinJS.UI.ListView.prototype" />
            if (viewState === appViewState.snapped) {
                //listView.layout = new ui.ListLayout();
                listView.itemDataSource = CalculaterPagedata.items.dataSource;
                listView.groupDataSource = null;
                listView.layout = new ui.ListLayout();
            } else {
                listView.layout = new ui.ListLayout({ groupHeaderPosition: "left" });
            }
        },

        ready: function (element, options) {
            // TODO: Initialize the page here.
            var DivlistView = element.querySelector(".itemslist").winControl;
            DivlistView.itemTemplate = element.querySelector(".itemListtemplate");
            DivlistView.itemDataSource = CalculaterPagedata.items.dataSource;
            //DivlistView.oniteminvoked = this._itemInvoked.bind(this);
            DivlistView.layout = new ui.ListLayout();
            //if (options.groupKey == null) {
            //    topText.textContent = options.item[1];
            //} else {
            //    topText.textContent = options.groupKey.title;
            //}
            //this.initializeListLayout(DivlistView, appView.value);
            //this.initializeLayout(listView, appView.value);
            DivlistView.element.focus();
            sendTileLocalImageNotification();
        },

        unload: function () {
            // TODO: Respond to navigations away from this page.
        },

        // This function updates the ListView with new layouts
        _initializeLayout: function (listView, viewState) {
            //Code Here
        },

        updateLayout: function (element, viewState, lastViewState) {
            /// <param name="element" domElement="true" />
            /// <param name="viewState" value="Windows.UI.ViewManagement.ApplicationViewState" />
            /// <param name="lastViewState" value="Windows.UI.ViewManagement.ApplicationViewState" />

            var DivlistView = element.querySelector(".itemslist").winControl;

            if (lastViewState !== viewState) {
                if (lastViewState === appViewState.snapped || viewState === appViewState.snapped) {
                    var handler = function (e) {

                        DivlistView.removeEventListener("contentanimating", handler, false);
                        e.preventDefault();
                    }

                    DivlistView.addEventListener("contentanimating", handler, false);
                    this._initializeLayout(DivlistView, viewState);
                }
            }
        },
    //    _itemInvoked: function (args) {
    //    if (appView.value === appViewState.snapped) {
    //        // If the page is snapped, the user invoked a group.
    //        var group = Data.items.getAt(args.detail.itemIndex);
    //        this.navigateToGroup(group.key);
    //    } else {
    //        // If the page is not snapped, the user invoked an item.
    //        var item = Data.items.getAt(args.detail.itemIndex);
    //        nav.navigate("/pages/CalculaterPage/CalculaterPage.html", { item: Data.getItemReference(item) });
    //    }
    //}
    });
    function sendTileLocalImageNotification() {
        // Note: This sample contains an additional project, NotificationsExtensions.
        // NotificationsExtensions exposes an object model for creating notifications, but you can also modify the xml
        // of the notification directly. See the additional function sendTileLocalImageNotificationWithXml to see how
        // to do it by modifying Xml directly, or sendLocalImageNotificationWithStringManipulation to see how to do it
        // by modifying strings directly

        //Clear Existing Notification
        Windows.UI.Notifications.TileUpdateManager.createTileUpdaterForApplication().clear();

        var imgSource = "ms-appx:///images/BigLiveTile1.jpg";
        var imgSmallSource = "ms-appx:///images/SmallLiveTile1.jpg";

        var tileContent = Windows.UI.Notifications.TileTemplateType.tileWideImageAndText01;
        var tileXml = Windows.UI.Notifications.TileUpdateManager.getTemplateContent(tileContent);
        var tileImageAttributes = tileXml.getElementsByTagName("image");
        var tileTextAttributes = tileXml.getElementsByTagName("text");

        tileImageAttributes[0].setAttribute("src", imgSource);
        tileImageAttributes[0].setAttribute("alt", "A Wide Live Tile.");
        tileTextAttributes[0].innerText = "Unit Calculator";

        var binding = tileXml.getElementsByTagName("binding");

        // create the square template and attach it to the wide template
        var template = Windows.UI.Notifications.TileTemplateType.tileSquareImage;
        var squareTileXml = Windows.UI.Notifications.TileUpdateManager.getTemplateContent(template);
        var squareTileImageElements = squareTileXml.getElementsByTagName("image");
        squareTileImageElements[0].setAttribute("src", imgSmallSource);
        squareTileImageElements[0].setAttribute("alt", "A Square Live Tile.");

        var binding = squareTileXml.getElementsByTagName("binding").item(0);
        var node = tileXml.importNode(binding, true);
        tileXml.getElementsByTagName("visual").item(0).appendChild(node);

        var tileNotification = new Windows.UI.Notifications.TileNotification(tileXml);
        Windows.UI.Notifications.TileUpdateManager.createTileUpdaterForApplication().enableNotificationQueue(true);

        Windows.UI.Notifications.TileUpdateManager.createTileUpdaterForApplication().update(tileNotification);
    }

})();
