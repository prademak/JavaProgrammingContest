$(window).ready(function() {
    /**
     * Settings Model
     */
    var SettingsModel = Backbone.Model.extend({
        // Default attributes
        defaults: function() {
            return {
                IntelliSense: true,
                MatchBrackets: true,
                LineWrapping: false,
                AutoIndent: true,
                Theme: "eclipse",
                TabSize: 4
            };
        },

        // Simplest initializer to maintain a single instance of this model.
        initialize: function() {
            /*if (!this.get("title")) {
                this.set({ "title": this.defaults.title });
            }*/
            if (!window.numSettingObjects) window.numSettingObjects = 0;
            window.numSettingObjects++;
            if (window.numSettingObjects > 1) {
                console.warn("Too many setting model objects.");
                throw new Exception("Too many setting model objects, I already have " + window.numSettingObjects + " instances of myself.");
            }
        }
    });
});