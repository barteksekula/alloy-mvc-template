define([
    "dojo/_base/declare",
    "epi/_Module"
], function(declare, _Module) {

    return declare([_Module], {
        initialize: function() {
            // summary:
            //		Initialize module

            // Execute the base initialize code first
            this.inherited(arguments);

            alert("foobar addon");
        }
    });
});
