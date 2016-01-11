(function () {
    "use strict";

    var list = new WinJS.Binding.List();
    var groupedItems = list.createGrouped(
        function groupKeySelector(item) { return item.group.key; },
        function groupDataSelector(item) { return item.group; }
    );

    // TODO: Replace the data with your real data.
    // You can add data from asynchronous sources whenever it becomes available.
    generateSampleData().forEach(function (item) {
        list.push(item);
    });

    WinJS.Namespace.define("CalculaterPagedata", {
        items: groupedItems,
        groups: groupedItems.groups,
        getItemReference: getItemReference,
        getItemsFromGroup: getItemsFromGroup,
        resolveGroupReference: resolveGroupReference,
        resolveItemReference: resolveItemReference
    });

    // Get a reference for an item, using the group key and item title as a
    // unique reference to the item that can be easily serialized.
    function getItemReference(item) {
        return [item.group.key, item.title];
    }

    // This function returns a WinJS.Binding.List containing only the items
    // that belong to the provided group.
    function getItemsFromGroup(group) {
        return list.createFiltered(function (item) { return item.group.key === group.key; });
    }

    // Get the unique group corresponding to the provided group key.
    function resolveGroupReference(key) {
        for (var i = 0; i < groupedItems.groups.length; i++) {
            if (groupedItems.groups.getAt(i).key === key) {
                return groupedItems.groups.getAt(i);
            }
        }
    }

    // Get a unique item from the provided string array, which should contain a
    // group key and an item title.
    function resolveItemReference(reference) {
        for (var i = 0; i < groupedItems.length; i++) {
            var item = groupedItems.getAt(i);
            if (item.group.key === reference[0] && item.title === reference[1]) {
                return item;
            }
        }
    }

    // Returns an array of sample data that can be added to the application's
    // data list. 
    function generateSampleData() {
        var itemContent = "<p>Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat</p><p>Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat</p><p>Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat</p><p>Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat</p><p>Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat</p><p>Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat</p><p>Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat";
        var itemDescription = "Item Description: Pellentesque porta mauris quis interdum vehicula urna sapien ultrices velit nec venenatis dui odio in augue cras posuere enim a cursus convallis neque turpis malesuada erat ut adipiscing neque tortor ac erat";
        var groupDescription = "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante";

        // These three strings encode placeholder images. You will want to set the
        // backgroundImage property in your real data to be URLs to images.
        var darkGray = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAANSURBVBhXY3B0cPoPAANMAcOba1BlAAAAAElFTkSuQmCC";
        var lightGray = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAANSURBVBhXY7h4+cp/AAhpA3h+ANDKAAAAAElFTkSuQmCC";
        var mediumGray = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAANSURBVBhXY5g8dcZ/AAY/AsAlWFQ+AAAAAElFTkSuQmCC";

        // Each of these sample groups must have a unique key to be displayed
        // separately.
        var sampleGroups = [
            { key: "group1", title: "", subtitle: "", backgroundImage: darkGray, description: groupDescription },
            { key: "group2", title: "Group Title: 2", subtitle: "Group Subtitle: 2", backgroundImage: lightGray, description: groupDescription },
            { key: "group3", title: "Group Title: 3", subtitle: "Group Subtitle: 3", backgroundImage: mediumGray, description: groupDescription },
            { key: "group4", title: "Group Title: 4", subtitle: "Group Subtitle: 4", backgroundImage: lightGray, description: groupDescription },
            { key: "group5", title: "Group Title: 5", subtitle: "Group Subtitle: 5", backgroundImage: mediumGray, description: groupDescription },
            { key: "group6", title: "Group Title: 6", subtitle: "Group Subtitle: 6", backgroundImage: darkGray, description: groupDescription }
        ];

        // Each of these sample items should have a reference to a particular
        // group.
        var sampleItems = [
            { group: sampleGroups[0], title: "Length", subtitle: "Centimeter, Feet, Inch, Metre, Mile Kilometre, Yard", description: itemDescription, content: itemContent, backgroundImage: "../../images/HubPage/HubPic1.png" },
            { group: sampleGroups[0], title: "Mass", subtitle: "Gram, Killogram, Milligram, Ounce, Pound, Ton", description: itemDescription, content: itemContent, backgroundImage: "../../images/HubPage/HubPic2.png" },
            { group: sampleGroups[0], title: "Data", subtitle: "Bits, Byte, Killobyte, Megabyte, Gigabyte, Terabyte, Petabyte", description: itemDescription, content: itemContent, backgroundImage: "../../images/HubPage/HubPic3.png" },
            { group: sampleGroups[0], title: "Speed", subtitle: "Foot/sec, Kilometer/hour, Kilometer/sec, knot, Mach Meter/min, Meter/sec", description: itemDescription, content: itemContent, backgroundImage: "../../images/HubPage/HubPic4.png" },
            { group: sampleGroups[0], title: "Power", subtitle: "Calories/second, Erg/second, Foot-pond?second, Gigawatt, Horespwer, Kilocalorie?second, Killowatt, Megawatt", description: itemDescription, content: itemContent, backgroundImage: "../../images/HubPage/HubPic5.png" },
            { group: sampleGroups[0], title: "Energy", subtitle: "Calorie, Electronvolt, Erg, Foot pound, Gigajoule, Joule", description: itemDescription, content: itemContent, backgroundImage: "../../images/HubPage/HubPic6.png" },
            { group: sampleGroups[0], title: "Temperature", subtitle: "Calorie, Electronvolt, Erg, Foot pound, Gigajoule, Joule", description: itemDescription, content: itemContent, backgroundImage: "../../images/HubPage/HubPic7.png" },
            { group: sampleGroups[0], title: "Volume", subtitle: "Cub, Liter, Gallon, Tabespoon", description: itemDescription, content: itemContent, backgroundImage: "../../images/HubPage/HubPic8.png" },
            { group: sampleGroups[0], title: "Area", subtitle: "Yard square, Mile square, Milemeter, Centimeter esquare, Keter square, Kilometer square", description: itemDescription, content: itemContent, backgroundImage: "../../images/HubPage/HubPic9.png" },
            { group: sampleGroups[0], title: "Pressure", subtitle: "Bar, Hectopascal, Kilogam, per.cm, Kilogram per.sq. meter", description: itemDescription, content: itemContent, backgroundImage: "../../images/HubPage/HubPic10.png" },
            { group: sampleGroups[0], title: "Force", subtitle: "Foot/sec, Kilometer/hour, Kilometer/sec, knot, Mach Meter/min, Meter/sec", description: itemDescription, content: itemContent, backgroundImage: "../../images/HubPage/HubPic11.png" },

            //{ group: sampleGroups[1], title: "Item Title: 1", subtitle: "Item Subtitle: 1", description: itemDescription, content: itemContent, backgroundImage: darkGray },
            //{ group: sampleGroups[1], title: "Item Title: 2", subtitle: "Item Subtitle: 2", description: itemDescription, content: itemContent, backgroundImage: mediumGray },
            //{ group: sampleGroups[1], title: "Item Title: 3", subtitle: "Item Subtitle: 3", description: itemDescription, content: itemContent, backgroundImage: lightGray },

            //{ group: sampleGroups[2], title: "Item Title: 1", subtitle: "Item Subtitle: 1", description: itemDescription, content: itemContent, backgroundImage: mediumGray },
            //{ group: sampleGroups[2], title: "Item Title: 2", subtitle: "Item Subtitle: 2", description: itemDescription, content: itemContent, backgroundImage: lightGray },
            //{ group: sampleGroups[2], title: "Item Title: 3", subtitle: "Item Subtitle: 3", description: itemDescription, content: itemContent, backgroundImage: darkGray },
            //{ group: sampleGroups[2], title: "Item Title: 4", subtitle: "Item Subtitle: 4", description: itemDescription, content: itemContent, backgroundImage: lightGray },
            //{ group: sampleGroups[2], title: "Item Title: 5", subtitle: "Item Subtitle: 5", description: itemDescription, content: itemContent, backgroundImage: mediumGray },
            //{ group: sampleGroups[2], title: "Item Title: 6", subtitle: "Item Subtitle: 6", description: itemDescription, content: itemContent, backgroundImage: darkGray },
            //{ group: sampleGroups[2], title: "Item Title: 7", subtitle: "Item Subtitle: 7", description: itemDescription, content: itemContent, backgroundImage: mediumGray },

            //{ group: sampleGroups[3], title: "Item Title: 1", subtitle: "Item Subtitle: 1", description: itemDescription, content: itemContent, backgroundImage: darkGray },
            //{ group: sampleGroups[3], title: "Item Title: 2", subtitle: "Item Subtitle: 2", description: itemDescription, content: itemContent, backgroundImage: lightGray },
            //{ group: sampleGroups[3], title: "Item Title: 3", subtitle: "Item Subtitle: 3", description: itemDescription, content: itemContent, backgroundImage: darkGray },
            //{ group: sampleGroups[3], title: "Item Title: 4", subtitle: "Item Subtitle: 4", description: itemDescription, content: itemContent, backgroundImage: lightGray },
            //{ group: sampleGroups[3], title: "Item Title: 5", subtitle: "Item Subtitle: 5", description: itemDescription, content: itemContent, backgroundImage: mediumGray },
            //{ group: sampleGroups[3], title: "Item Title: 6", subtitle: "Item Subtitle: 6", description: itemDescription, content: itemContent, backgroundImage: lightGray },

            //{ group: sampleGroups[4], title: "Item Title: 1", subtitle: "Item Subtitle: 1", description: itemDescription, content: itemContent, backgroundImage: lightGray },
            //{ group: sampleGroups[4], title: "Item Title: 2", subtitle: "Item Subtitle: 2", description: itemDescription, content: itemContent, backgroundImage: darkGray },
            //{ group: sampleGroups[4], title: "Item Title: 3", subtitle: "Item Subtitle: 3", description: itemDescription, content: itemContent, backgroundImage: lightGray },
            //{ group: sampleGroups[4], title: "Item Title: 4", subtitle: "Item Subtitle: 4", description: itemDescription, content: itemContent, backgroundImage: mediumGray },

            //{ group: sampleGroups[5], title: "Item Title: 1", subtitle: "Item Subtitle: 1", description: itemDescription, content: itemContent, backgroundImage: lightGray },
            //{ group: sampleGroups[5], title: "Item Title: 2", subtitle: "Item Subtitle: 2", description: itemDescription, content: itemContent, backgroundImage: darkGray },
            //{ group: sampleGroups[5], title: "Item Title: 3", subtitle: "Item Subtitle: 3", description: itemDescription, content: itemContent, backgroundImage: mediumGray },
            //{ group: sampleGroups[5], title: "Item Title: 4", subtitle: "Item Subtitle: 4", description: itemDescription, content: itemContent, backgroundImage: darkGray },
            //{ group: sampleGroups[5], title: "Item Title: 5", subtitle: "Item Subtitle: 5", description: itemDescription, content: itemContent, backgroundImage: lightGray },
            //{ group: sampleGroups[5], title: "Item Title: 6", subtitle: "Item Subtitle: 6", description: itemDescription, content: itemContent, backgroundImage: mediumGray },
            //{ group: sampleGroups[5], title: "Item Title: 7", subtitle: "Item Subtitle: 7", description: itemDescription, content: itemContent, backgroundImage: darkGray },
            //{ group: sampleGroups[5], title: "Item Title: 8", subtitle: "Item Subtitle: 8", description: itemDescription, content: itemContent, backgroundImage: lightGray }
        ];

        return sampleItems;
    }
})();
